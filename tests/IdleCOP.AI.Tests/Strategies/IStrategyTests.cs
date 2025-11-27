using Xunit;
using IdleCOP.AI.Strategies;
using Idle.Core.Context;
using Idle.Utility.Components;

namespace IdleCOP.AI.Tests.Strategies;

/// <summary>
/// IStrategy 测试用具体实现
/// </summary>
public class TestStrategy : IStrategy
{
  private readonly int priority;
  private readonly Func<TickContext, IdleComponent, bool> canExecuteFunc;
  private readonly Action<TickContext, IdleComponent>? executeAction;

  public bool WasExecuted { get; private set; }

  public TestStrategy(
    int priority,
    Func<TickContext, IdleComponent, bool>? canExecuteFunc = null,
    Action<TickContext, IdleComponent>? executeAction = null)
  {
    this.priority = priority;
    this.canExecuteFunc = canExecuteFunc ?? ((_, _) => true);
    this.executeAction = executeAction;
  }

  public int Priority => priority;

  public bool CanExecute(TickContext context, IdleComponent actor)
  {
    return canExecuteFunc(context, actor);
  }

  public void Execute(TickContext context, IdleComponent actor)
  {
    WasExecuted = true;
    executeAction?.Invoke(context, actor);
  }
}

/// <summary>
/// IStrategy 接口测试
/// </summary>
public class IStrategyTests
{
  [Fact]
  public void IStrategy_Priority_ReturnsExpectedValue()
  {
    // Arrange
    const int expectedPriority = 100;
    var strategy = new TestStrategy(expectedPriority);

    // Assert
    Assert.Equal(expectedPriority, strategy.Priority);
  }

  [Fact]
  public void IStrategy_CanExecute_WhenAlwaysTrue_ReturnsTrue()
  {
    // Arrange
    using var context = new TickContext();
    var actor = new IdleComponent();
    var strategy = new TestStrategy(1, (_, _) => true);

    // Act
    var result = strategy.CanExecute(context, actor);

    // Assert
    Assert.True(result);
  }

  [Fact]
  public void IStrategy_CanExecute_WhenAlwaysFalse_ReturnsFalse()
  {
    // Arrange
    using var context = new TickContext();
    var actor = new IdleComponent();
    var strategy = new TestStrategy(1, (_, _) => false);

    // Act
    var result = strategy.CanExecute(context, actor);

    // Assert
    Assert.False(result);
  }

  [Fact]
  public void IStrategy_CanExecute_WithContextCondition_EvaluatesCorrectly()
  {
    // Arrange
    using var context = new TickContext { CurrentTick = 50 };
    var actor = new IdleComponent();
    var strategy = new TestStrategy(1, (ctx, _) => ctx.CurrentTick >= 50);

    // Act
    var result = strategy.CanExecute(context, actor);

    // Assert
    Assert.True(result);
  }

  [Fact]
  public void IStrategy_Execute_PerformsAction()
  {
    // Arrange
    using var context = new TickContext();
    var actor = new IdleComponent();
    var executed = false;
    var strategy = new TestStrategy(1, null, (_, _) => executed = true);

    // Act
    strategy.Execute(context, actor);

    // Assert
    Assert.True(executed);
    Assert.True(strategy.WasExecuted);
  }

  [Fact]
  public void IStrategy_PriorityComparison_HigherPriorityFirst()
  {
    // Arrange
    var lowPriorityStrategy = new TestStrategy(10);
    var highPriorityStrategy = new TestStrategy(100);

    // Act
    var strategies = new List<IStrategy> { lowPriorityStrategy, highPriorityStrategy };
    var sortedStrategies = strategies.OrderByDescending(s => s.Priority).ToList();

    // Assert
    Assert.Equal(highPriorityStrategy, sortedStrategies[0]);
    Assert.Equal(lowPriorityStrategy, sortedStrategies[1]);
  }

  [Fact]
  public void IStrategy_MultipleStrategies_SortByPriority()
  {
    // Arrange
    var strategies = new List<IStrategy>
    {
      new TestStrategy(30),
      new TestStrategy(90),
      new TestStrategy(50),
      new TestStrategy(10),
      new TestStrategy(70)
    };

    // Act
    var sortedPriorities = strategies
      .OrderByDescending(s => s.Priority)
      .Select(s => s.Priority)
      .ToList();

    // Assert
    Assert.Equal(new[] { 90, 70, 50, 30, 10 }, sortedPriorities);
  }
}
