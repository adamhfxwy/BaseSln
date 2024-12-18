using LanTian.Solution.Core.Domain.INpgSqlService;
using LanTian.Solution.Core.Infrastructure.Utils;
using LanTian.Solution.Core.ParameterModel.ChangeModel.DeviceMaintain;
using LanTian.Solution.Core.ParameterModel.QueryModel.DeviceMaintain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.Infrastructure.NpgSqlService
{
    public class DeviceUpkeepStatementService : IDeviceUpkeepStatementService
    {
        private readonly IRepository<LanTianDeviceUpkeepStatement> _repository;
        public DeviceUpkeepStatementService(IRepository<LanTianDeviceUpkeepStatement> repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Tuple<long, string>> AddDeviceUpkeepStatementAsync(DeviceUpkeepStatementChangeModel model, CancellationToken cancellationToken = default)
        {
            bool exists = await _repository.AnyAsync(x => x.DeviceNumber == model.DeviceNumber && x.DeviceType == model.DeviceType && x.IsDeleted == IsDeletedEnum.未删除, cancellationToken);
            if (exists)
            {
                return new Tuple<long, string>(0, "同一个设备类型下的设备编号不能重复");
            }
            LanTianDeviceUpkeepStatement entity = new LanTianDeviceUpkeepStatement(model.DeviceNumber, model.DeviceType, model.UpkeepCycle.Value, model.Description
                , model.EmployeeId.Value, model.EmployeeName, model.ThisUpkeepTime, model.IsTimeout.Value, model.GenerateCosts.Value, Convert.ToDateTime(model.RealityUpkeepTime));
            entity = await _repository.InsertAsync(entity, true, cancellationToken);
            return new Tuple<long, string>(entity.Id, "success");
        }
        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Pagination<DeviceUpkeepStatementDTO>> GetDeviceUpkeepStatementAsync(DeviceUpkeepStatementQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param)
        {
            var exp = await CommonWhereBuilder.WhereBuilderToExp<LanTianDeviceUpkeepStatement, DeviceUpkeepStatementQueryModel>(queryModel, cancellationToken);//<LantianSimCardInfo, SimCardInfoQueryModel>        
            var select = SelectUtil.GetSelectCol<LanTianDeviceUpkeepStatement>(param);
            var list = await _repository.GetListOfSignalTableAsync(queryModel, exp, select, cancellationToken, true, param);
            var dto = list.List.Select(ToDTOUtils.ToDTO).ToList();
            return new Pagination<DeviceUpkeepStatementDTO> { List = dto, Total = list.Total, Code = 1 };
        }
        /// <summary>
        /// 根据条件获取一条数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DeviceUpkeepStatementDTO> GetDeviceUpkeepStatementByPropAsync(DeviceUpkeepStatementQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param)
        {
            var exp = await CommonWhereBuilder.WhereBuilderToExp<LanTianDeviceUpkeepStatement, DeviceUpkeepStatementQueryModel>(queryModel, cancellationToken);//<LantianSimCardInfo, SimCardInfoQueryModel>
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
        public async Task<DeviceUpkeepStatementDTO> GetDeviceUpkeepStatementByIdAsync(long id)
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
        public async Task<bool> DeviceUpkeepStatementAnyAsync(DeviceUpkeepStatementQueryModel queryModel, CancellationToken cancellationToken = default)
        {
            var exp = await CommonWhereBuilder.WhereBuilderToExp<LanTianDeviceUpkeepStatement, DeviceUpkeepStatementQueryModel>(queryModel, cancellationToken);
            return await _repository.AnyAsync(exp, cancellationToken);
        }
    }
}
