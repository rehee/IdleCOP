using Xunit;
using Idle.Core;
using Idle.Core.Context;
using Idle.Utility;
using Idle.Utility.Components;

namespace Idle.Core.Tests.Context;

/// <summary>
/// TickContext 测试
/// </summary>
public class TickContextTests
{
  [Fact]
  public void TickContext_NewInstance_HasDefaultValues()
  {
    // Arrange & Act
    using var context = new TickContext();

    // Assert
    Assert.Equal(0, context.CurrentTick);
    Assert.Equal(0, context.MaxTick);
    Assert.False(context.IsBattleOver);
    Assert.Equal(EnumBattleResult.NotSpecified, context.Result);
    Assert.Null(context.BattleRandom);
    Assert.Null(context.ItemRandom);
  }

  [Fact]
  public void TickContext_CreatorFaction_StartsEmpty()
  {
    // Arrange & Act
    using var context = new TickContext();

    // Assert
    Assert.NotNull(context.CreatorFaction);
    Assert.Empty(context.CreatorFaction);
  }

  [Fact]
  public void TickContext_EnemyFaction_StartsEmpty()
  {
    // Arrange & Act
    using var context = new TickContext();

    // Assert
    Assert.NotNull(context.EnemyFaction);
    Assert.Empty(context.EnemyFaction);
  }

  [Fact]
  public void TickContext_Projectiles_StartsEmpty()
  {
    // Arrange & Act
    using var context = new TickContext();

    // Assert
    Assert.NotNull(context.Projectiles);
    Assert.Empty(context.Projectiles);
  }

  [Fact]
  public void TickContext_CurrentTick_CanBeSetAndGet()
  {
    // Arrange
    using var context = new TickContext();
    const int expectedTick = 100;

    // Act
    context.CurrentTick = expectedTick;

    // Assert
    Assert.Equal(expectedTick, context.CurrentTick);
  }

  [Fact]
  public void TickContext_MaxTick_CanBeSetAndGet()
  {
    // Arrange
    using var context = new TickContext();
    const int expectedMaxTick = 1000;

    // Act
    context.MaxTick = expectedMaxTick;

    // Assert
    Assert.Equal(expectedMaxTick, context.MaxTick);
  }

  [Fact]
  public void TickContext_IsBattleOver_CanBeSetAndGet()
  {
    // Arrange
    using var context = new TickContext();

    // Act
    context.IsBattleOver = true;

    // Assert
    Assert.True(context.IsBattleOver);
  }

  [Fact]
  public void TickContext_Result_CanBeSetAndGet()
  {
    // Arrange
    using var context = new TickContext();

    // Act
    context.Result = EnumBattleResult.Victory;

    // Assert
    Assert.Equal(EnumBattleResult.Victory, context.Result);
  }

  [Fact]
  public void TickContext_BattleRandom_CanBeSetAndGet()
  {
    // Arrange
    using var context = new TickContext();
    var random = new GameRandom(42);

    // Act
    context.BattleRandom = random;

    // Assert
    Assert.Equal(random, context.BattleRandom);
  }

  [Fact]
  public void TickContext_ItemRandom_CanBeSetAndGet()
  {
    // Arrange
    using var context = new TickContext();
    var random = new GameRandom(123);

    // Act
    context.ItemRandom = random;

    // Assert
    Assert.Equal(random, context.ItemRandom);
  }

  [Fact]
  public void TickContext_AddToCreatorFaction_AddsComponent()
  {
    // Arrange
    using var context = new TickContext();
    var component = new IdleComponent();

    // Act
    context.CreatorFaction.Add(component);

    // Assert
    Assert.Single(context.CreatorFaction);
    Assert.Contains(component, context.CreatorFaction);
  }

  [Fact]
  public void TickContext_AddToEnemyFaction_AddsComponent()
  {
    // Arrange
    using var context = new TickContext();
    var component = new IdleComponent();

    // Act
    context.EnemyFaction.Add(component);

    // Assert
    Assert.Single(context.EnemyFaction);
    Assert.Contains(component, context.EnemyFaction);
  }

  [Fact]
  public void TickContext_AddToProjectiles_AddsComponent()
  {
    // Arrange
    using var context = new TickContext();
    var component = new IdleComponent();

    // Act
    context.Projectiles.Add(component);

    // Assert
    Assert.Single(context.Projectiles);
    Assert.Contains(component, context.Projectiles);
  }

  [Fact]
  public void TickContext_Dispose_ClearsAllLists()
  {
    // Arrange
    var context = new TickContext();
    context.CreatorFaction.Add(new IdleComponent());
    context.EnemyFaction.Add(new IdleComponent());
    context.Projectiles.Add(new IdleComponent());

    // Act
    context.Dispose();

    // Assert
    Assert.Empty(context.CreatorFaction);
    Assert.Empty(context.EnemyFaction);
    Assert.Empty(context.Projectiles);
  }
}
