using LanTian.Solution.Core.DTO.Common;
using LanTian.Solution.Core.ParameterModel.ChangeModel.Common;
using LanTian.Solution.Core.ParameterModel.QueryModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.Domain.INpgSqlService
{
    public interface IWorkOrderService : IServiceSupport
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Tuple<long, string>> AddWorkOrderAsync(WorkOrderChangeModel model, CancellationToken cancellationToken = default);
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Tuple<bool, string>> EditWorkOrderAsync(WorkOrderChangeModel model, CancellationToken cancellationToken = default);
        /// <summary>
        /// 完成工单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<Tuple<bool, string>> FinishWorkOrderAsync(long id, CancellationToken cancellationToken = default);
        /// <summary>
        /// 按年月统计数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<Pagination<WorkOrderDTO>> GetWorkOrderGroupDataAsync(WorkOrderQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param);

        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<Pagination<WorkOrderDTO>> GetWorkOrderAsync(WorkOrderQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param);
        /// <summary>
        /// 根据条件获取一条数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<WorkOrderDTO> GetWorkOrderByPropAsync(WorkOrderQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param);
        /// <summary>
        /// 根据id获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<WorkOrderDTO> GetWorkOrderByIdAsync(long id);
        /// <summary>
        /// 根据条件查看是否有符合条件的数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> WorkOrderAnyAsyncAsync(WorkOrderQueryModel queryModel, CancellationToken cancellationToken = default);
    }
}
