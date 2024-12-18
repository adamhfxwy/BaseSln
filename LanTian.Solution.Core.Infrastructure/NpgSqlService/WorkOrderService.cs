

using LanTian.Solution.Core.Domain.INpgSqlService;
using LanTian.Solution.Core.Infrastructure.Utils;
using LanTian.Solution.Core.ParameterModel.ChangeModel.Common;
using LanTian.Solution.Core.ParameterModel.QueryModel.Common;

namespace LanTian.Solution.Core.Infrastructure.NpgSqlService
{
    public class WorkOrderService :IWorkOrderService
    {
        private readonly IRepository<LanTianWorkOrder> _repository;
        public WorkOrderService(IRepository<LanTianWorkOrder> repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Tuple<long, string>> AddWorkOrderAsync(WorkOrderChangeModel model, CancellationToken cancellationToken = default)
        {
            if (!model.OrderType.HasValue)
            {
                return new Tuple<long, string>(0, "工单类型不能为空");
            }
            if (!model.ReportingChannels.HasValue)
            {
                return new Tuple<long, string>(0, "上报途径不能为空");
            }
            if (!model.ReportPersonId.HasValue)
            {
                return new Tuple<long, string>(0, "上报人id不能为空");
            }
            if (string.IsNullOrEmpty(model.OrderNumber))
            {
                return new Tuple<long, string>(0, "工单号不能为空");
            }
            if (string.IsNullOrEmpty(model.ReportPersonName))
            {
                return new Tuple<long, string>(0, "上报人姓名不能为空");
            }
            LanTianWorkOrder entity = new LanTianWorkOrder(model.OrderNumber, model.OrderType.Value, model.ReportingChannels.Value, model.ProblemDescription, model.ProblemPicPath,
                model.ReportPersonId.Value, model.ReportPersonName, model.DepartmentId, model.DepartmentName);
            entity = await _repository.InsertAsync(entity, true, cancellationToken);
            return new Tuple<long, string>(entity.Id, "success");
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Tuple<bool, string>> EditWorkOrderAsync(WorkOrderChangeModel model, CancellationToken cancellationToken = default)
        {
            var obj = await _repository.FindAsync(model.Id.Value);
            if (obj == null)
            {
                return new Tuple<bool, string>(false, $"id={model.Id}的工单不存在");
            }
            if (model.HandlePersonId.HasValue)
            {
                obj.ChangeHandlePersonId(model.HandlePersonId.Value);
            }
            if (!string.IsNullOrEmpty(model.HandlePersonName))
            {
                obj.ChangeHandlePersonName(model.HandlePersonName);
            }
            if (!string.IsNullOrEmpty(model.HandledDescription))
            {
                obj.ChangeHandledDescription(model.HandledDescription);
            }
            if (!string.IsNullOrEmpty(model.HandledPicPath))
            {
                obj.ChangeHandledPicPath(model.HandledPicPath);
            }
            if (!string.IsNullOrEmpty(model.HandledTime))
            {
                obj.ChangeHandledTime(Convert.ToDateTime(model.HandledTime));
            }
            obj = await _repository.UpdateAsync(obj, true, cancellationToken);
            return new Tuple<bool, string>(true, "success");
        }
        /// <summary>
        /// 完成工单
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Tuple<bool, string>> FinishWorkOrderAsync(long id, CancellationToken cancellationToken = default)
        {
            var obj = await _repository.FindAsync(id);
            if (obj == null)
            {
                return new Tuple<bool, string>(false, $"id={id}的工单不存在");
            }
            obj.ChangeStatus();
            obj = await _repository.UpdateAsync(obj, true, cancellationToken);
            return new Tuple<bool, string>(true, "success");
        }
        /// <summary>
        /// 按年月统计数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Pagination<WorkOrderDTO>> GetWorkOrderGroupDataAsync(WorkOrderQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param)
        {
            var data = _repository.GetAllAsync().AsNoTracking();
            var tuple = await CommonWhereBuilder.WhereBuilder(data, queryModel, cancellationToken, false, false);
            if (!string.IsNullOrEmpty(queryModel.SearchYear) && !string.IsNullOrEmpty(queryModel.SearchYearMonth))
            {
                return new Pagination<WorkOrderDTO> { List = null, Total = 0, Code = 2, Message = "按年和按月不可以同时查询" };
            }
            if (!string.IsNullOrEmpty(queryModel.SearchYear))
            {
                var result = tuple.Item1.Where(x => x.CreateTime.Year == Convert.ToInt32(queryModel.SearchYear))
                    .GroupBy(x => new { x.CreateTime.Month })
                    .Select(x => new WorkOrderDTO
                    {
                        TimeAxis = x.Key.Month,
                        ProcessedCount = x.Count(x => x.Status == WorkOrderStatusEnum.已处理),
                        UnprocessedCount = x.Count(x => x.Status == WorkOrderStatusEnum.未处理)
                    });
                if (queryModel.PageIndex.HasValue && queryModel.PageSize.HasValue)
                {
                    var count = await result.CountAsync(cancellationToken);
                    var list = await result.OrderBy(x => x.TimeAxis).Skip((queryModel.PageIndex.Value) * queryModel.PageSize.Value).Take(queryModel.PageSize.Value).ToListAsync(cancellationToken);
                    return new Pagination<WorkOrderDTO> { List = list, Total = count, Code = 1 };
                }
                else
                {
                    return new Pagination<WorkOrderDTO> { List = await result.OrderBy(x => x.TimeAxis).ToListAsync(cancellationToken), Code = 1 };
                }
            }
            else if (!string.IsNullOrEmpty(queryModel.SearchYearMonth))
            {
                var yearMonthSp = queryModel.SearchYearMonth.Split('-');
                var result = tuple.Item1.Where(x => x.CreateTime.Year == Convert.ToInt32(yearMonthSp[0]) && x.CreateTime.Month == Convert.ToInt32(yearMonthSp[1]))
                     .GroupBy(x => new { x.CreateTime.Day })
                    .Select(x => new WorkOrderDTO
                    {
                        TimeAxis = x.Key.Day,
                        ProcessedCount = x.Count(x => x.Status == WorkOrderStatusEnum.已处理),
                        UnprocessedCount = x.Count(x => x.Status == WorkOrderStatusEnum.未处理)
                    });
                if (queryModel.PageIndex.HasValue && queryModel.PageSize.HasValue)
                {
                    var count = await result.CountAsync(cancellationToken);
                    var list = await result.OrderBy(x => x.TimeAxis).Skip((queryModel.PageIndex.Value) * queryModel.PageSize.Value).Take(queryModel.PageSize.Value).ToListAsync(cancellationToken);
                    return new Pagination<WorkOrderDTO> { List = list, Total = count, Code = 1 };
                }
                else
                {
                    return new Pagination<WorkOrderDTO> { List = await result.OrderBy(x => x.TimeAxis).ToListAsync(cancellationToken), Code = 1 };
                }
            }
            else
            {
                return new Pagination<WorkOrderDTO> { List = null, Total = 0, Code = 2, Message = "按年和按月不可以同时为空" };
            }
        }
        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Pagination<WorkOrderDTO>> GetWorkOrderAsync(WorkOrderQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param)
        {
            var exp = await CommonWhereBuilder.WhereBuilderToExp<LanTianWorkOrder, WorkOrderQueryModel>(queryModel, cancellationToken);//<LantianSimCardInfo, SimCardInfoQueryModel>        
            var select = SelectUtil.GetSelectCol<LanTianWorkOrder>(param);
            var list = await _repository.GetListOfSignalTableAsync(queryModel, exp, select, cancellationToken, true, param);
            var dto = list.List.Select(ToDTOUtils.ToDTO).ToList();
            return new Pagination<WorkOrderDTO> { List = dto, Total = list.Total, Code = 1 };
        }
        /// <summary>
        /// 根据条件获取一条数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<WorkOrderDTO> GetWorkOrderByPropAsync(WorkOrderQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param)
        {
            var exp = await CommonWhereBuilder.WhereBuilderToExp<LanTianWorkOrder, WorkOrderQueryModel>(queryModel, cancellationToken);//<LantianSimCardInfo, SimCardInfoQueryModel>
            var obj = await _repository.FindAsync(exp, cancellationToken);
            if (obj != null)
            {
                return ToDTOUtils.ToDTO(obj);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据id获取一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<WorkOrderDTO> GetWorkOrderByIdAsync(long id)
        {
            var obj = await _repository.FindAsync(id);
            if (obj != null)
            {
                return ToDTOUtils.ToDTO(obj);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 根据条件查看是否有符合条件的数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> WorkOrderAnyAsyncAsync(WorkOrderQueryModel queryModel, CancellationToken cancellationToken = default)
        {
            var exp = await CommonWhereBuilder.WhereBuilderToExp<LanTianWorkOrder, WorkOrderQueryModel>(queryModel, cancellationToken);
            return await _repository.AnyAsync(exp, cancellationToken);
        }
    }
}
