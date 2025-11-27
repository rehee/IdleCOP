using IdleCOP.Data.Enums;

namespace IdleCOP.Data.Entities;

/// <summary>
/// Entity representing a strategy configuration.
/// </summary>
public class StrategyEntity : BaseEntity
{
  /// <summary>
  /// Gets or sets the profile key.
  /// </summary>
  public int ProfileKey { get; set; }

  /// <summary>
  /// Gets or sets the priority (higher is more important).
  /// </summary>
  public int Priority { get; set; }

  /// <summary>
  /// Gets or sets whether the strategy is enabled.
  /// </summary>
  public bool IsEnabled { get; set; } = true;

  /// <summary>
  /// Gets or sets the condition JSON.
  /// </summary>
  public string ConditionJson { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the action type.
  /// </summary>
  public string ActionType { get; set; } = string.Empty;
}
