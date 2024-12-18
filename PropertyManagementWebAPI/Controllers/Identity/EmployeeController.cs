

using LanTian.Solution.Core.Domain.INpgSqlService;
using LanTian.Solution.Core.Domain.NpgSqlEntities.Identity;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http;

namespace LanTian.Solution.Core.PropertyManagementWebAPI.Controllers.Identity
{
    /// <summary>
    /// 员工相关
    /// </summary>
    [Authorize]
    [Route("propertyMgtWeb/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "Identity")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMemoryCacheHelper _memoryCacheHelper;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;
        public EmployeeController(IEmployeeService employeeService, IMemoryCacheHelper memoryCacheHelper, IConfiguration config, HttpClient httpClient)
        {
            _employeeService = employeeService;
            _memoryCacheHelper = memoryCacheHelper;
            _config = config;
            _httpClient = httpClient;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebEmployeeMatching.EmployeeMentadd))]
        public async Task<ActionResult> AddEmployeeAsync(AddEditEmployeeModel model, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(model.RealName))
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "姓名不可为空"
                });
            }
            //string s = null;
            //s.ToString();
            if (string.IsNullOrEmpty(model.Cellphone))
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "手机号不可为空"
                });
            }
            if (model.Cellphone.Length < 11)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "手机号位数不正确"
                });
            }
            if (model.LoginPermissions == null)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "权限配置不可为空"
                });
            }
            if (model.Sex == null)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "性别不可为空"
                });
            }
            if (!string.IsNullOrEmpty(model.PhotoPath))
            {
                if (ValidateHelper.IsBase64(model.PhotoPath))
                {
                    string basePath = _config["FilePathBase"];
                    var picSuffix = _config["PicSuffix"];
                    DateTime date = DateTime.Now;
                    string DirUrl = "/Storage/SmartPark/Employee/" + date.Year + "/" + date.Month + "/" + date.Day + "/" + model.RealName + "/";

                    if (!Directory.Exists(basePath + DirUrl)) //检测文件夹是否存在，不存在则创建
                    {
                        Directory.CreateDirectory(basePath + DirUrl);
                    }
                    model.PhotoPath = CommonUtils.SavaImg(model.PhotoPath, basePath, DirUrl, null, picSuffix);
                }
            }
            var tuple = await _employeeService.AddEmployeeAsync(model, cancellationToken);
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
        [CheckPermission(nameof(WebEmployeeMatching.EmployeeMentedit))]
        public async Task<ActionResult> EditEmployeeAsync(AddEditEmployeeModel model, CancellationToken cancellationToken = default)
        {
            if (!model.Id.HasValue || model.Id.Value <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "id不可为空"
                });
            }

            var employee = await _employeeService.GetEmployeeByIdAsync(model.Id.Value);
            if (employee == null)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = $"id={model.Id}的员工不存在"
                });
            }
            bool isChange = false;
            if (model.RoleId != null && model.RoleId != employee.RoleId)
            {
                isChange = true;
            }
            var tuple = await _employeeService.EditEmployeeAsync(model, cancellationToken);
            if (tuple.Item1 && isChange)
            {
                string webKey = $"WebPermission-{employee.Id}-{employee.UserName}";
                _memoryCacheHelper.Remove(webKey);
                var webPermissions = await _memoryCacheHelper.GetOrCreateAsync(webKey, async (e) =>
                {
                    return new Tuple<bool, List<string>>(false, null);
                });
                //http请求移动端删除key
                var baseUrl = _config["Curdomain"];
                var url = $"{baseUrl}/smartParkApp/Main/RemoveMobilePermissionByEmployee";
                string json = JsonConvert.SerializeObject(employee);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);
            }
            return Ok(new
            {
                Status = tuple.Item1 ? "Ok" : "Failed",
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
        [SwaggerResponse(0, "返回数据属性注释", typeof(EmployeeDTO))]
        [CheckPermission(nameof(WebEmployeeMatching.EmployeeMent))]
        public async Task<ActionResult> GetEmployeeAsync(EmployeeQueryModel query, CancellationToken cancellationToken = default)
        {
            query.OrderBy = "createTime";
            query.IsDescending = true;
            var tuple = await _employeeService.GetEmployeeAsync(query, cancellationToken);
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
        [SwaggerResponse(0, "返回数据属性注释", typeof(EmployeeDTO))]
        [CheckPermission(nameof(WebEmployeeMatching.EmployeeMent))]
        public async Task<ActionResult> GetEmployeeByIdAsync(GetByIdModel model, CancellationToken cancellationToken = default)
        {
            if (model.Id <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "请选择Id"
                });
            }
            var obj = await _employeeService.GetEmployeeByIdAsync(model.Id, cancellationToken);
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
        [SwaggerResponse(0, "返回数据属性注释", typeof(EmployeeDTO))]
        [CheckPermission(nameof(WebEmployeeMatching.EmployeeMent))]
        public async Task<ActionResult> GetEmployeeByPropsAsync(EmployeeQueryModel query, CancellationToken cancellationToken = default)
        {
            var obj = await _employeeService.GetEmployeeByPropAsync(query, cancellationToken);
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
        [CheckPermission(nameof(WebEmployeeMatching.EmployeeMentdel))]
        public async Task<ActionResult> RemoveEmployeeAsync(GetByIdModel model, CancellationToken cancellationToken = default)
        {
            if (model.Id <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "请选择Id"
                });
            }

            var tuple = await _employeeService.RemoveEmployeeAsync(model.Id, cancellationToken);
            return Ok(new
            {
                Status = tuple.Item1 ? "Ok" : "Failed",
                Id = tuple.Item1,
                Msg = tuple.Item2
            });
        }
    }
}
