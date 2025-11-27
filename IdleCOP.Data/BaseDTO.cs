namespace IdleCOP.Data.DTOs;

/// <summary>
/// Base DTO class for data transfer.
/// </summary>
public abstract class BaseDTO
{
  /// <summary>
  /// Gets or sets the unique identifier.
  /// </summary>
  public Guid Id { get; set; }
}
