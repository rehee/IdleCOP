using Idle.Utility.Components;
using Idle.Core.Context;

namespace IdleCOP.AI.Strategies;

/// <summary>
/// 行为策略接口
/// </summary>
public interface IStrategy
{
  /// <summary>
  /// 优先级（高优先）
  /// </summary>
  int Priority { get; }

  /// <summary>
  /// 检查是否可以执行
  /// </summary>
  bool CanExecute(TickContext context, IdleComponent actor);

  /// <summary>
  /// 执行策略
  /// </summary>
  void Execute(TickContext context, IdleComponent actor);
}
