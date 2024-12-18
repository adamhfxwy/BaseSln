

using LanTian.Solution.Core.Domain.INpgSqlService;
using LanTian.Solution.Core.Infrastructure.Utils;
using LanTian.Solution.Core.ParameterModel.ChangeModel.Common;
using LanTian.Solution.Core.ParameterModel.QueryModel.Common;

namespace LanTian.Solution.Core.Infrastructure.NpgSqlService
{
    public class OperationLogService : IOperationLogService
    {
        private readonly IRepository<LanTianOperationLog> _repository;
        public OperationLogService(IRepository<LanTianOperationLog> repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Tuple<long, string>> AddOperationLogAsync(AddEditOperationLogModel model, CancellationToken cancellationToken = default)
        {
            LanTianOperationLog entity = new LanTianOperationLog(model.EmpId.Value, model.EmpName, model.OperationName, model.ApiPath, model.RequestMessage, model.ResponseMessage);
            entity = await _repository.InsertAsync(entity, true, cancellationToken);
            return new Tuple<long, string>(entity.Id, "success");
        }
        /// <summary>admin
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Pagination<OperationLogDTO>> GetOperationLogAsync(OperationLogQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param)
        {
            var exp = await CommonWhereBuilder.WhereBuilderToExp<LanTianOperationLog, OperationLogQueryModel>(queryModel, cancellationToken);//<LantianSimCardInfo, SimCardInfoQueryModel>        
            var select = SelectUtil.GetSelectCol<LanTianOperationLog>(param);
            var list = await _repository.GetListOfSignalTableAsync(queryModel, exp, select, cancellationToken, true, param);
            var dto = list.List.Select(ToDTOUtils.ToDTO).ToList();
            return new Pagination<OperationLogDTO> { List = dto, Total = list.Total, Code = 1 };
        }
    }
}
