using LanTian.Solution.Core.Domain.NpgSqlEntities.DeviceMaintain;
using LanTian.Solution.Core.DTO.DeviceMaintain;
using LanTian.Solution.Core.ParameterModel.ChangeModel.DeviceMaintain;
using LanTian.Solution.Core.ParameterModel.QueryModel.DeviceMaintain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.Domain.INpgSqlService
{
    public interface IDeviceUpkeepService : IServiceSupport
    {
        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        Task<List<DeviceCommonDTO>> GetDeviceByDicAsync(string className, UpkeepDeviceQueryModel queryModel, CancellationToken cancellationToken = default);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Tuple<long, string>> AddDeviceUpkeepAsync(DeviceUpkeepChangeModel model, CancellationToken cancellationToken = default);
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Tuple<bool, string>> EditDeviceUpkeepAsync(DeviceUpkeepChangeModel model, CancellationToken cancellationToken = default);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Tuple<bool, string>> RemoveDeviceUpkeepAsync(long id, CancellationToken cancellationToken = default);
        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<Pagination<DeviceUpkeepDTO>> GetDeviceUpkeepAsync(DeviceUpkeepQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param);
        /// <summary>
        /// 根据条件获取一条数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DeviceUpkeepDTO> GetDeviceUpkeepByPropAsync(DeviceUpkeepQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param);
        /// <summary>
        /// 根据id获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DeviceUpkeepDTO> GetDeviceUpkeepByIdAsync(long id);
        /// <summary>
        /// 根据条件查看是否有符合条件的数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeviceUpkeepAnyAsync(DeviceUpkeepQueryModel queryModel, CancellationToken cancellationToken = default);
    }
}
