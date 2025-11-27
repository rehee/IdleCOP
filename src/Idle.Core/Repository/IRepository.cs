using System.Linq.Expressions;

namespace Idle.Core.Repository;

/// <summary>
/// Entity 的增删改查抽象，根据平台实现（数据库、IndexedDB 等）
/// </summary>
public interface IRepository<T> where T : class
{
  /// <summary>
  /// 根据ID获取实体
  /// </summary>
  Task<T?> GetByIdAsync(Guid id);

  /// <summary>
  /// 获取所有实体
  /// </summary>
  Task<IEnumerable<T>> GetAllAsync();

  /// <summary>
  /// 根据条件查询实体
  /// </summary>
  Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate);

  /// <summary>
  /// 添加实体
  /// </summary>
  Task AddAsync(T entity);

  /// <summary>
  /// 更新实体
  /// </summary>
  Task UpdateAsync(T entity);

  /// <summary>
  /// 根据ID删除实体
  /// </summary>
  Task DeleteAsync(Guid id);

  /// <summary>
  /// 保存所有更改
  /// </summary>
  Task SaveChangesAsync();
}
