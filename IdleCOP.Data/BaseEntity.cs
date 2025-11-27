using IdleCOP.Data.Enums;

namespace IdleCOP.Data.Entities;

/// <summary>
/// Base entity class for all persistent data.
/// </summary>
public abstract class BaseEntity
{
  /// <summary>
  /// Gets or sets the unique identifier.
  /// </summary>
  public Guid Id { get; set; }

  /// <summary>
  /// Gets or sets the creation timestamp.
  /// </summary>
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  /// <summary>
  /// Gets or sets the last update timestamp.
  /// </summary>
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
