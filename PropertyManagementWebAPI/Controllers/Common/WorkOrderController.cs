
namespace LanTian.Solution.Core.PropertyManagementWebAPI.Controllers.Common
{
    /// <summary>
    /// 工单管理相关
    /// </summary>
    [Authorize]
    [Route("propertyMgtWeb/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "Common")]
    [ApiController]
    public class WorkOrderController : ControllerBase
    {
        private readonly IWorkOrderService  _workOrderService;
        private readonly IConfiguration _config;
        private readonly IDictionaryService _dictionaryService;
        private readonly IMessageService _messageService;
        private readonly IDepartmentService _departmentService;
        public WorkOrderController(IWorkOrderService workOrderService, IConfiguration config, IDictionaryService dictionaryService, IMessageService messageService, IDepartmentService departmentService)
        {
            _workOrderService = workOrderService;
            _config = config;
            _dictionaryService = dictionaryService;
            _messageService = messageService;
            _departmentService = departmentService;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebWorkOrder.WorkOrderListadd))]
        public async Task<ActionResult> AddWorkOrderAsync(WorkOrderChangeModel model, CancellationToken cancellationToken = default)
        {
            if (!model.OrderType.HasValue)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "工单类型不能为空"
                });
            }
            if (!model.ReportingChannels.HasValue)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "上报途径不能为空"
                });
            }
            if (!model.ReportPersonId.HasValue)
            { 
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "上报人id不能为空"
                });
            }
            model.OrderNumber = CommonUtils.GenerateNumber().ToString();
            if (string.IsNullOrEmpty(model.ReportPersonName))
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "上报人姓名不能为空"
                });
            }
            foreach (var pic in model.ProblemPicPathArr)
            {
                if (ValidateHelper.IsBase64(pic))
                {
                    string basePath = _config["FilePathBase"];
                    var picSuffix = _config["PicSuffix"];
                    string DirUrl = "/Storage/WorkOrder/Unprocessed/" + model.OrderNumber;
                    if (Directory.Exists(basePath + DirUrl))
                    {
                        Directory.Delete(basePath + DirUrl, true);
                    }
                    Directory.CreateDirectory(basePath + DirUrl);
                    model.ProblemPicPath += CommonUtils.SavaImg(pic, basePath, DirUrl, null, picSuffix) + ",";
                }
            }
            if (!string.IsNullOrEmpty(model.ProblemPicPath))
            {
                model.ProblemPicPath = model.ProblemPicPath.Substring(0, model.ProblemPicPath.Length - 1);
            }
            var orderType = await _dictionaryService.GetDictionaryByPropAsync(new DictionaryQueryModel { Key = model.OrderType.ToString() }, cancellationToken);
            if (orderType==null)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "字典未维护工单类型"
                });
            }
            var depart = await _departmentService.GetDepartmentByPropAsync(new DepartmentQueryModel { DepartmentName = orderType.Value });
            if (depart == null)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = $"部门名称={orderType.Value}的部门不存在"
                });
            }
            model.DepartmentId = depart.Id;
            model.DepartmentName = depart.DepartmentName;
            var tuple = await _workOrderService.AddWorkOrderAsync(model, cancellationToken);

            if (tuple.Item1 > 0)
            {       
                
                if (orderType != null)
                {
                    if (depart != null)
                    {
                        if (depart.DepartmentLeader!=null)
                        {
                            var workOrder= await _workOrderService.GetWorkOrderByIdAsync(tuple.Item1);
                            var templatecodeObj = await _dictionaryService.GetDictionaryByPropAsync(new DictionaryQueryModel { Key = "工单类短信模板" }, cancellationToken);
                            var cellphone = depart.DepartmentLeader.Cellphone;
                            var createTime = workOrder.CreateTime.ToString("yyyy年MM月dd日 HH:mm:ss", CultureInfo.InvariantCulture);
                            //string orderTypeStr = model.OrderType.ToString().Contains("类") ? model.OrderType.ToString().Substring(0, model.OrderType.ToString().Length - 1) : model.OrderType.ToString();
                            string param = string.Format(MessageResources.WorkOrderMessage, model.OrderType.ToString(), workOrder.OrderNumber, workOrder.ProblemDescription, createTime);
                            await _messageService.AliSendSms(templatecodeObj.Value, cellphone, param);
                        }              
                    }
                }                           
            }
            return Ok(new
            {
                Status = tuple.Item1 != null ? "Ok" : "Failed",
                Id = tuple.Item1,
                Msg = tuple.Item2
            });
        }
        /// <summary>
        /// 结案
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebWorkOrder.WorkOrderListedit))]
        public async Task<ActionResult> EditWorkOrderAsync(WorkOrderChangeModel model, CancellationToken cancellationToken = default)
        {
            if (!model.Id.HasValue || model.Id.Value <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "id不可为空"
                });
            }
            foreach (var pic in model.HandledPicPathArr)
            {
                if (ValidateHelper.IsBase64(pic))
                {
                    string basePath = _config["FilePathBase"];
                    var picSuffix = _config["PicSuffix"];
                    string DirUrl = "/Storage/WorkOrder/Processed/" + model.OrderNumber;
                    if (Directory.Exists(basePath + DirUrl))
                    {
                        Directory.Delete(basePath + DirUrl, true);
                    }
                    Directory.CreateDirectory(basePath + DirUrl);
                    model.HandledPicPath += CommonUtils.SavaImg(pic, basePath, DirUrl, null, picSuffix) + ",";
                }
            }
            if (!string.IsNullOrEmpty(model.HandledPicPath))
            {
                model.HandledPicPath = model.HandledPicPath.Substring(0, model.HandledPicPath.Length - 1);
            }
          
            var tuple = await _workOrderService.EditWorkOrderAsync(model, cancellationToken);
            if (tuple.Item1)
            {
                await _workOrderService.FinishWorkOrderAsync(model.Id.Value, cancellationToken);
            }
            return Ok(new
            {
                Status = tuple.Item1 ? "Ok" : "Failed",
                Id = tuple.Item1,
                Msg = tuple.Item2
            });
        }
        /// <summary>
        /// 根据条件查询列表
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebWorkOrder.WorkOrderList))]
        [SwaggerResponse(0, "返回数据属性注释", typeof(WorkOrderDTO))]
        public async Task<ActionResult> GetWorkOrderAsync(WorkOrderQueryModel query, CancellationToken cancellationToken = default)
        {
            query.OrderBy = "createTime";
            query.IsDescending = true;
            var tuple = await _workOrderService.GetWorkOrderAsync(query, cancellationToken);
            return Ok(new
            {
                Status = "Ok",
                Data = tuple.List,
                TotalCount = tuple.Total
            });
        }
        /// <summary>
        /// 根据条件按年或按月查询运行统计数据
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebWorkOrder.WorkOrderList))]
        [SwaggerResponse(0, "返回数据属性注释", typeof(WorkOrderDTO))]
        public async Task<ActionResult> GetWorkOrderByYearOrMonthAsync(WorkOrderQueryModel query, CancellationToken cancellationToken = default)
        {
            var tuple = await _workOrderService.GetWorkOrderGroupDataAsync(query, cancellationToken);
            return Ok(new
            {
                Status = "Ok",
                Data = tuple.List,
                TotalCount = tuple.Total
            });
        }
        /// <summary>
        /// 根据id获取数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebWorkOrder.WorkOrderList))]
        [SwaggerResponse(0, "返回数据属性注释", typeof(WorkOrderDTO))]
        public async Task<ActionResult> GetWorkOrderByIdAsync(GetByIdModel model)
        {
            if (model.Id <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "请选择Id"
                });
            }
            var obj = await _workOrderService.GetWorkOrderByIdAsync(model.Id);
            return Ok(new
            {
                Status = "Ok",
                Data = obj
            });
        }
        /// <summary>
        /// 根据字段获取一条数据
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebWorkOrder.WorkOrderList))]
        [SwaggerResponse(0, "返回数据属性注释", typeof(WorkOrderDTO))]
        public async Task<ActionResult> GetWorkOrderByPropsAsync(WorkOrderQueryModel query, CancellationToken cancellationToken = default)
        {
            var obj = await _workOrderService.GetWorkOrderByPropAsync(query, cancellationToken);
            return Ok(new
            {
                Status = "Ok",
                Data = obj
            });
        }
    }
}
