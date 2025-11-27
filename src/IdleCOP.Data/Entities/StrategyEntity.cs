using Idle.Utility.Entities;

namespace IdleCOP.Data.Entities;

/// <summary>
/// 策略实体
/// </summary>
public class StrategyEntity : BaseEntity
{
  /// <summary>
  /// 优先级（高优先）
  /// </summary>
  public int Priority { get; set; }

  /// <summary>
  /// 是否启用
  /// </summary>
  public bool IsEnabled { get; set; } = true;

  /// <summary>
  /// 执行动作类型
  /// </summary>
  public string? ActionType { get; set; }
}
