

using LanTian.Solution.Core.DTO.Common;
using LanTian.Solution.Core.ParameterModel.ChangeModel.Common;
using LanTian.Solution.Core.ParameterModel.QueryModel.Common;

namespace LanTian.Solution.Core.Domain.INpgSqlService
{
    public interface IUserInfoService : IServiceSupport
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Tuple<long, string>> AddUserInfoAsync(AddEditUserInfoModel model, CancellationToken cancellationToken = default);
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Tuple<bool, string>> EditUserInfoAsync(AddEditUserInfoModel model, CancellationToken cancellationToken = default);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Tuple<bool, string>> RemoveUserInfoAsync(long id, CancellationToken cancellationToken = default);
        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<Pagination<UserInfoDTO>> GetUserInfoAsync(UserInfoQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param);
        /// <summary>
        /// 根据条件获取一条数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UserInfoDTO> GetUserInfoByPropAsync(UserInfoQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param);
        /// <summary>
        /// 根据id获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<UserInfoDTO> GetUserInfoByIdAsync(long id);
    }
}
