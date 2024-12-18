



using Microsoft.EntityFrameworkCore.Query;

namespace LanTian.Solution.Core.Domain.ICommonService;

public interface IRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="autoSave"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
    /// <summary>
    /// 批量新增
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="autoSave"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default);
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="autoSave"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
    /// <summary>
    /// 批量更新
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="autoSave"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default);
    /// <summary>
    /// 根据实体真删除
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="autoSave"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
    /// <summary>
    /// 根据条件真删除
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="autoSave"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false, CancellationToken cancellationToken = default);
    /// <summary>
    /// 批量真删除
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="autoSave"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default);
    /// <summary>
    /// 根据条件获取一条数据
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
    /// <summary>
    /// 根据id获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity> FindAsync(long id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
    /// <summary>
    /// 根据条件获取列表
    /// </summary>
    /// <param name="searchBase"></param>
    /// <param name="predicate"></param>
    /// <param name="include"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="disableTracking"></param>
    /// <returns></returns>
    Task<Pagination<TEntity>> GetListOfSignalTableAsync(QueryBase searchBase,
     Expression<Func<TEntity, bool>>? predicate = null,
     Expression<Func<TEntity, object[]>>? selectExpression = null,
     CancellationToken cancellationToken = default,
     bool disableTracking = true,
     params string[] param);

    /// <summary>
    /// 根据条件获取列表
    /// </summary>
    /// <param name="searchBase"></param>
    /// <param name="predicate"></param>
    /// <param name="include"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="disableTracking"></param>
    /// <returns></returns>
    Task<Pagination<TEntity>> GetListOfWithIncludDataAsync(QueryBase searchBase,
     Expression<Func<TEntity, bool>>? predicate = null,
     Expression<Func<TEntity, object[]>>? selectExpression = null,
     Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
     CancellationToken cancellationToken = default,
     bool disableTracking = true,
     params string[] param);

    /// <summary>
    /// 根据条件获取数量
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    /// <summary>
    /// 根据条件查看表中是否存在
    /// </summary>
    /// <param name="predicate"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
    /// <summary>
    /// 获取全部数据的IQueryable
    /// </summary>
    /// <returns></returns>
    IQueryable<TEntity> GetAllAsync();
}
