using LanTian.Solution.Core.Domain.NpgSqlEntities.DeviceMaintain;
using LanTian.Solution.Core.DTO.DeviceMaintain;
using LanTian.Solution.Core.ParameterModel.ChangeModel.DeviceMaintain;
using LanTian.Solution.Core.ParameterModel.QueryModel.DeviceMaintain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.Domain.INpgSqlService
{
    public interface IDeviceUpkeepStatementService :IServiceSupport
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Tuple<long, string>> AddDeviceUpkeepStatementAsync(DeviceUpkeepStatementChangeModel model, CancellationToken cancellationToken = default);
        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<Pagination<DeviceUpkeepStatementDTO>> GetDeviceUpkeepStatementAsync(DeviceUpkeepStatementQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param);
        /// <summary>
        /// 根据条件获取一条数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DeviceUpkeepStatementDTO> GetDeviceUpkeepStatementByPropAsync(DeviceUpkeepStatementQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param);
        /// <summary>
        /// 根据id获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DeviceUpkeepStatementDTO> GetDeviceUpkeepStatementByIdAsync(long id);
        /// <summary>
        /// 根据条件查看是否有符合条件的数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeviceUpkeepStatementAnyAsync(DeviceUpkeepStatementQueryModel queryModel, CancellationToken cancellationToken = default);
    }
}
