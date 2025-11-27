using Idle.Core.Components;
using Idle.Core.Context;

namespace IdleCOP.AI.Strategies;

/// <summary>
/// Manages and executes strategies for an actor.
/// </summary>
public class StrategyManager
{
  private readonly List<IStrategy> strategies = new();

  /// <summary>
  /// Adds a strategy to the manager.
  /// </summary>
  /// <param name="strategy">The strategy to add.</param>
  public void AddStrategy(IStrategy strategy)
  {
    strategies.Add(strategy);
  }

  /// <summary>
  /// Removes a strategy from the manager.
  /// </summary>
  /// <param name="strategy">The strategy to remove.</param>
  public void RemoveStrategy(IStrategy strategy)
  {
    strategies.Remove(strategy);
  }

  /// <summary>
  /// Clears all strategies.
  /// </summary>
  public void ClearStrategies()
  {
    strategies.Clear();
  }

  /// <summary>
  /// Gets all strategies sorted by priority.
  /// </summary>
  /// <returns>Strategies sorted by priority (highest first).</returns>
  public IEnumerable<IStrategy> GetStrategies()
  {
    return strategies.OrderByDescending(s => s.Priority);
  }

  /// <summary>
  /// Evaluates and executes the highest priority strategy that can execute.
  /// </summary>
  /// <param name="context">The tick context.</param>
  /// <param name="actor">The actor.</param>
  /// <returns>True if a strategy was executed.</returns>
  public bool ExecuteFirst(TickContext context, IdleComponent actor)
  {
    foreach (var strategy in GetStrategies())
    {
      if (strategy.CanExecute(context, actor))
      {
        strategy.Execute(context, actor);
        return true;
      }
    }
    return false;
  }

  /// <summary>
  /// Evaluates and executes all strategies that can execute.
  /// </summary>
  /// <param name="context">The tick context.</param>
  /// <param name="actor">The actor.</param>
  /// <returns>The number of strategies executed.</returns>
  public int ExecuteAll(TickContext context, IdleComponent actor)
  {
    int count = 0;
    foreach (var strategy in GetStrategies())
    {
      if (strategy.CanExecute(context, actor))
      {
        strategy.Execute(context, actor);
        count++;
      }
    }
    return count;
  }
}
