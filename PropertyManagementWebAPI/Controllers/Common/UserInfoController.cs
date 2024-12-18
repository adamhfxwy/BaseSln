﻿
namespace LanTian.Solution.Core.PropertyManagementWebAPI.Controllers.Common
{
    /// <summary>
    /// 用户相关
    /// </summary>
    [Authorize]
    [Route("propertyMgtWeb/[controller]/[action]")]
    [ApiExplorerSettings(GroupName = "Common")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {
        private readonly IUserInfoService _userInfoService;

        public UserInfoController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

     
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        [CheckPermission(nameof(WebUserMentMatching.UserMentadd))]
        public async Task<ActionResult> AddUserInfoAsync(AddEditUserInfoModel model, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "用户名称不可为空"
                });
            }
            if (string.IsNullOrEmpty(model.Cellphone))
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "用户联系方式不可为空"
                });
            }
            if (string.IsNullOrEmpty(model.Address))
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "用户地址不可为空"
                });
            }
            var tuple = await _userInfoService.AddUserInfoAsync(model, cancellationToken);
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
        [CheckPermission(nameof(WebUserMentMatching.UserMentedit))]
        public async Task<ActionResult> EditUserInfoAsync(AddEditUserInfoModel model, CancellationToken cancellationToken = default)
        {
            if (!model.Id.HasValue || model.Id.Value <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "id不可为空"
                });
            }
            var tuple = await _userInfoService.EditUserInfoAsync(model, cancellationToken);
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
        [CheckPermission(nameof(WebUserMentMatching.UserMent))]
        //[CheckPermission("Department")]
        [SwaggerResponse(0, "返回数据属性注释", typeof(UserInfoDTO))]
        public async Task<ActionResult> GetUserInfoAsync(UserInfoQueryModel query, CancellationToken cancellationToken = default)
        {
            query.OrderBy = "createTime";
            query.IsDescending = true;
            var tuple = await _userInfoService.GetUserInfoAsync(query, cancellationToken);
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
        [CheckPermission(nameof(WebUserMentMatching.UserMent))]
        [SwaggerResponse(0, "返回数据属性注释", typeof(UserInfoDTO))]
        public async Task<ActionResult> GetUserInfoByIdAsync(GetByIdModel model)
        {
            if (model.Id <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "请选择Id"
                });
            }
            var obj = await _userInfoService.GetUserInfoByIdAsync(model.Id);
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
        [CheckPermission(nameof(WebUserMentMatching.UserMent))]
        [SwaggerResponse(0, "返回数据属性注释", typeof(UserInfoDTO))]
        public async Task<ActionResult> GetUserInfoByPropsAsync(UserInfoQueryModel query, CancellationToken cancellationToken = default)
        {
            var obj = await _userInfoService.GetUserInfoByPropAsync(query, cancellationToken);
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
        [CheckPermission(nameof(WebUserMentMatching.UserMentdel))]
        public async Task<ActionResult> RemoveUserInfoAsync(GetByIdModel model, CancellationToken cancellationToken = default)
        {
            if (model.Id <= 0)
            {
                return Ok(new
                {
                    Status = "Failed",
                    Msg = "请选择Id"
                });
            }

            var tuple = await _userInfoService.RemoveUserInfoAsync(model.Id, cancellationToken);
            return Ok(new
            {
                Status = tuple.Item1 ? "Ok" : "Failed",
                Id = tuple.Item1,
                Msg = tuple.Item2
            });
        }
    }
}
