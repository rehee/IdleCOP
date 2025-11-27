using System.Linq.Expressions;

namespace Idle.Utility.Repository;

/// <summary>
/// Generic repository interface for data access.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public interface IRepository<T> where T : class
{
  /// <summary>
  /// Gets an entity by its identifier.
  /// </summary>
  /// <param name="id">The entity identifier.</param>
  /// <returns>The entity or null if not found.</returns>
  Task<T?> GetByIdAsync(Guid id);

  /// <summary>
  /// Gets all entities.
  /// </summary>
  /// <returns>All entities in the repository.</returns>
  Task<IEnumerable<T>> GetAllAsync();

  /// <summary>
  /// Queries entities based on a predicate.
  /// </summary>
  /// <param name="predicate">The query predicate.</param>
  /// <returns>Entities matching the predicate.</returns>
  Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate);

  /// <summary>
  /// Adds a new entity.
  /// </summary>
  /// <param name="entity">The entity to add.</param>
  Task AddAsync(T entity);

  /// <summary>
  /// Updates an existing entity.
  /// </summary>
  /// <param name="entity">The entity to update.</param>
  Task UpdateAsync(T entity);

  /// <summary>
  /// Deletes an entity by its identifier.
  /// </summary>
  /// <param name="id">The entity identifier.</param>
  Task DeleteAsync(Guid id);

  /// <summary>
  /// Saves all pending changes.
  /// </summary>
  Task SaveChangesAsync();
}
