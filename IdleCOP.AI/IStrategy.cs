using Idle.Core.Components;
using Idle.Core.Context;

namespace IdleCOP.AI.Strategies;

/// <summary>
/// Interface for behavior strategies.
/// </summary>
public interface IStrategy
{
  /// <summary>
  /// Gets the priority of this strategy (higher = more important).
  /// </summary>
  int Priority { get; }

  /// <summary>
  /// Checks if this strategy can be executed.
  /// </summary>
  /// <param name="context">The tick context.</param>
  /// <param name="actor">The actor to check.</param>
  /// <returns>True if the strategy can be executed.</returns>
  bool CanExecute(TickContext context, IdleComponent actor);

  /// <summary>
  /// Executes the strategy.
  /// </summary>
  /// <param name="context">The tick context.</param>
  /// <param name="actor">The actor to execute on.</param>
  void Execute(TickContext context, IdleComponent actor);
}

/// <summary>
/// Base class for behavior strategies.
/// </summary>
public abstract class StrategyBase : IStrategy
{
  /// <summary>
  /// Gets the priority of this strategy.
  /// </summary>
  public abstract int Priority { get; }

  /// <summary>
  /// Gets or sets whether this strategy is enabled.
  /// </summary>
  public bool IsEnabled { get; set; } = true;

  /// <summary>
  /// Checks if this strategy can be executed.
  /// </summary>
  /// <param name="context">The tick context.</param>
  /// <param name="actor">The actor to check.</param>
  /// <returns>True if the strategy can be executed.</returns>
  public abstract bool CanExecute(TickContext context, IdleComponent actor);

  /// <summary>
  /// Executes the strategy.
  /// </summary>
  /// <param name="context">The tick context.</param>
  /// <param name="actor">The actor to execute on.</param>
  public abstract void Execute(TickContext context, IdleComponent actor);
}
