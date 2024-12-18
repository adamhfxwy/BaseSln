

namespace LanTian.Solution.Core.PropertyManagementWebAPI.Controllers.Common
{
    /// <summary>
    /// 排班相关
    /// </summary>
    [Authorize]
    [Route("propertyMgtWeb/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "Common")]
    [ApiController]
    public class ShiftInfoController : ControllerBase
    {
        private readonly IShiftInfoService  _shiftInfoService;
        private readonly IEmployeeService _employeeService;

        public ShiftInfoController(IShiftInfoService shiftInfoService, IEmployeeService employeeService)
        {
            _shiftInfoService = shiftInfoService;
            _employeeService = employeeService;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebShiftMent.ShiftMentadd))]
        public async Task<ActionResult> AddShiftInfoAsync(ShiftInfoChangeModel model, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(model.ShiftName))
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "排版名称不可为空"
                });
            }
            if (string.IsNullOrEmpty(model.BeginTime))
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "开始时间不可为空"
                });
            }
            if (string.IsNullOrEmpty(model.EndTime))
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "结束时间不可为空"
                });
            }
            var tuple = await _shiftInfoService.AddShiftInfoAsync(model, cancellationToken);           
            return Ok(new
            {
                Status = tuple.Item1 != null ? "Ok" : "Failed",
                Id = tuple.Item1,
                Msg = tuple.Item2
            });
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebShiftMent.ShiftMentedit))]
        public async Task<ActionResult> EditShiftInfoAsync(ShiftInfoChangeModel model, CancellationToken cancellationToken = default)
        {
            if (!model.Id.HasValue || model.Id.Value <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "id不可为空"
                });
            }
            var tuple = await _shiftInfoService.EditShiftInfoAsync(model, cancellationToken);
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
        [CheckPermission(nameof(WebShiftMent.ShiftMent))]
        [SwaggerResponse(0, "返回数据属性注释", typeof(ShiftInfoDTO))]
        public async Task<ActionResult> GetShiftInfoAsync(ShiftInfoQueryModel query, CancellationToken cancellationToken = default)
        {
            query.OrderBy = "createTime";
            query.IsDescending = true;
            var tuple = await _shiftInfoService.GetShiftInfoAsync(query, cancellationToken);
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
        [CheckPermission(nameof(WebShiftMent.ShiftMent))]
        [SwaggerResponse(0, "返回数据属性注释", typeof(ShiftInfoDTO))]
        public async Task<ActionResult> GetShiftInfoByIdAsync(GetByIdModel model)
        {
            if (model.Id <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "请选择Id"
                });
            }
            var obj = await _shiftInfoService.GetShiftInfoByIdAsync(model.Id);
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
        [CheckPermission(nameof(WebShiftMent.ShiftMent))]
        [SwaggerResponse(0, "返回数据属性注释", typeof(ShiftInfoDTO))]
        public async Task<ActionResult> GetShiftInfoByPropsAsync(ShiftInfoQueryModel query, CancellationToken cancellationToken = default)
        {
            var obj = await _shiftInfoService.GetShiftInfoByPropAsync(query, cancellationToken);
            return Ok(new
            {
                Status = "Ok",
                Data = obj
            });
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebShiftMent.ShiftMentdeldel1))]
        public async Task<ActionResult> RemoveShiftInfoAsync(GetByIdModel model, CancellationToken cancellationToken = default)
        {
            if (model.Id <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "请选择Id"
                });
            }
            bool exists = await _employeeService.EmployeeAnyAsync(new EmployeeQueryModel { ShiftId = model.Id });
            if (exists)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = $"Id={model.Id}的排班已被员工绑定，无法删除"
                });
            }
            var tuple = await _shiftInfoService.RemoveShiftInfoAsync(model.Id, cancellationToken);
            return Ok(new
            {
                Status = tuple.Item1 ? "Ok" : "Failed",
                Id = tuple.Item1,
                Msg = tuple.Item2
            });
        }
    }
}
