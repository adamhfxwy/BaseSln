using LanTian.Solution.Core.Domain.INpgSqlService;
using LanTian.Solution.Core.Infrastructure.Utils;
using LanTian.Solution.Core.ParameterModel.ChangeModel.DeviceMaintain;
using LanTian.Solution.Core.ParameterModel.QueryModel.DeviceMaintain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LanTian.Solution.Core.Infrastructure.NpgSqlService
{
    public class DeviceUpkeepService : IDeviceUpkeepService
    {
        private readonly IRepository<LanTianDeviceUpkeep> _repository;
        private readonly LanTianNpgSqlContext _context;
        public DeviceUpkeepService(IRepository<LanTianDeviceUpkeep> repository, LanTianNpgSqlContext context)
        {
            _repository = repository;
            _context = context;
        }
        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<DeviceCommonDTO>> GetDeviceByDicAsync(string className, UpkeepDeviceQueryModel queryModel, CancellationToken cancellationToken = default)
        {
            string specifiedAssemblyName = $"LanTian.Solution.Core.Domain";
            string middleName = "NpgSqlEntities.DeviceMaintain";
            Assembly specifiedAssembly = Assembly.Load(specifiedAssemblyName);
            List<DeviceCommonDTO> dto = new List<DeviceCommonDTO>();
            if (specifiedAssembly == null)
            {
                return null;
            }
            string fullTypeName = $"{specifiedAssembly.GetName().Name}.{middleName}.{className}";
            Type type = specifiedAssembly.GetType(fullTypeName);
            if (type != null)
            {
                // 获取 DbSet 并调用 ToListAsync 方法
                MethodInfo setMethod = typeof(DbContext).GetMethods()
               .Where(m => m.Name == "Set" && m.IsGenericMethodDefinition && m.GetParameters().Length == 0)
               .Single()
               .MakeGenericMethod(type);
                var dbSet = setMethod.Invoke(_context, null);
                // 创建查询表达式
                var parameter = Expression.Parameter(type, "e");
                Expression predicate = null;
                foreach (var property in typeof(UpkeepDeviceQueryModel).GetProperties())
                {
                    var value = property.GetValue(queryModel);
                    if (value != null)
                    {
                        var entityProperty = Expression.Property(parameter, property.Name);
                        var constant = Expression.Constant(value);
                        var equal = Expression.Equal(entityProperty, constant);

                        if (predicate == null)
                        {
                            predicate = equal;
                        }
                        else
                        {
                            predicate = Expression.AndAlso(predicate, equal);
                        }
                    }
                }
                var lambda = Expression.Lambda(predicate, parameter);
                var whereMethod = typeof(Queryable).GetMethods()
                        .Where(m => m.Name == "Where" && m.IsGenericMethodDefinition)
                        .Where(m =>
                        {
                            var parameters = m.GetParameters();
                            return parameters.Length == 2
                                   && parameters[0].ParameterType.GetGenericTypeDefinition() == typeof(IQueryable<>)
                                   && parameters[1].ParameterType.GetGenericTypeDefinition() == typeof(Expression<>)
                                   && parameters[1].ParameterType.GetGenericArguments()[0].GetGenericTypeDefinition() == typeof(Func<,>);
                        })
                        .Single()
                        .MakeGenericMethod(type);
                var filteredDbSet = whereMethod.Invoke(null, new object[] { dbSet, lambda });
                MethodInfo toListAsyncMethod = typeof(EntityFrameworkQueryableExtensions)
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .Where(m => m.Name == "ToListAsync" && m.GetParameters().Length == 2)
                .Single()
                .MakeGenericMethod(type);
                var task = (Task)toListAsyncMethod.Invoke(null, new object[] { filteredDbSet, CancellationToken.None });
                await task.ConfigureAwait(false);

                var resultProperty = task.GetType().GetProperty("Result");
                var result = (IEnumerable<object>)resultProperty.GetValue(task);
                foreach (var item in result)
                {
                    var obj = CommonUtils.Mapper<DeviceCommonDTO>(item);
                    dto.Add(obj);
                }
            }
            return dto;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Tuple<long, string>> AddDeviceUpkeepAsync(DeviceUpkeepChangeModel model, CancellationToken cancellationToken = default)
        {
            bool exists = await _repository.AnyAsync(x => x.DeviceNumber == model.DeviceNumber && x.DeviceType == model.DeviceType && x.IsDeleted == IsDeletedEnum.未删除, cancellationToken);
            if (exists)
            {
                return new Tuple<long, string>(0, "同一个设备类型下的设备编号不能重复");
            }
            LanTianDeviceUpkeep entity = new LanTianDeviceUpkeep(model.DeviceNumber, model.DeviceType, model.UpkeepCycle.Value, Convert.ToDateTime(model.UpkeepLastDate));
            entity = await _repository.InsertAsync(entity, true, cancellationToken);
            return new Tuple<long, string>(entity.Id, "success");
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Tuple<bool, string>> EditDeviceUpkeepAsync(DeviceUpkeepChangeModel model, CancellationToken cancellationToken = default)
        {
            var obj = await _repository.FindAsync(model.Id.Value);
            if (obj == null)
            {
                return new Tuple<bool, string>(false, $"id={model.Id}的维护信息不存在");
            }

            if (!string.IsNullOrEmpty(model.DeviceNumber) && !string.IsNullOrEmpty(model.DeviceType))
            {
                bool exists = await _repository.AnyAsync(x => x.DeviceNumber == model.DeviceNumber && x.DeviceType == model.DeviceType && x.IsDeleted == IsDeletedEnum.未删除 && x.Id != model.Id, cancellationToken);
                if (exists)
                {
                    return new Tuple<bool, string>(false, "同一个设备类型下的设备编号不能重复");
                }
                obj.ChangeDeviceNumber(model.DeviceNumber);
                obj.ChangeDeviceType(model.DeviceType);
            }
            if (model.UpkeepCycle.HasValue)
            {
                obj.ChangeUpkeepCycle(model.UpkeepCycle.Value);
            }
            if (!string.IsNullOrEmpty(model.UpkeepLastDate))
            {
                obj.ChangeUpkeepLastDate(Convert.ToDateTime(model.UpkeepLastDate));
            }
            obj = await _repository.UpdateAsync(obj, true, cancellationToken);
            return new Tuple<bool, string>(true, "success");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Tuple<bool, string>> RemoveDeviceUpkeepAsync(long id, CancellationToken cancellationToken = default)
        {
            var obj = await _repository.FindAsync(id);
            if (obj == null || obj.IsDeleted == IsDeletedEnum.已删除)
            {
                return new Tuple<bool, string>(false, $"id={id}的排班不存在");
            }
            obj.ChangeIsDeleted();
            await _repository.UpdateAsync(obj, true, cancellationToken);
            return new Tuple<bool, string>(true, "success");
        }
        /// <summary>
        /// 根据条件获取分页数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<Pagination<DeviceUpkeepDTO>> GetDeviceUpkeepAsync(DeviceUpkeepQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param)
        {
            var exp = await CommonWhereBuilder.WhereBuilderToExp<LanTianDeviceUpkeep, DeviceUpkeepQueryModel>(queryModel, cancellationToken);//<LantianSimCardInfo, SimCardInfoQueryModel>        
            var select = SelectUtil.GetSelectCol<LanTianDeviceUpkeep>(param);
            var list = await _repository.GetListOfSignalTableAsync(queryModel, exp, select, cancellationToken, true, param);
            var dto = list.List.Select(ToDTOUtils.ToDTO).ToList();
            return new Pagination<DeviceUpkeepDTO> { List = dto, Total = list.Total, Code = 1 };
        }
        /// <summary>
        /// 根据条件获取一条数据
        /// </summary>
        /// <param name="queryModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<DeviceUpkeepDTO> GetDeviceUpkeepByPropAsync(DeviceUpkeepQueryModel queryModel, CancellationToken cancellationToken = default, params string[] param)
        {
            var exp = await CommonWhereBuilder.WhereBuilderToExp<LanTianDeviceUpkeep, DeviceUpkeepQueryModel>(queryModel, cancellationToken);//<LantianSimCardInfo, SimCardInfoQueryModel>
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
        public async Task<DeviceUpkeepDTO> GetDeviceUpkeepByIdAsync(long id)
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
        public async Task<bool> DeviceUpkeepAnyAsync(DeviceUpkeepQueryModel queryModel, CancellationToken cancellationToken = default)
        {
            var exp = await CommonWhereBuilder.WhereBuilderToExp<LanTianDeviceUpkeep, DeviceUpkeepQueryModel>(queryModel, cancellationToken);
            return await _repository.AnyAsync(exp, cancellationToken);
        }
    }
}
