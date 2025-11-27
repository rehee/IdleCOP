using Xunit;
using Idle.Core;
using Idle.Core.DTOs;
using Idle.Core.Helpers;
using IdleCOP.Gameplay.Actors;

namespace IdleCOP.Gameplay.Tests.Actors;

/// <summary>
/// ActorComponent 测试
/// </summary>
public class ActorComponentTests
{
  [Fact]
  public void ActorComponent_NewInstance_HasDefaultValues()
  {
    // Arrange & Act
    var actor = new ActorComponent();

    // Assert
    Assert.NotEqual(Guid.Empty, actor.Id);
    Assert.Null(actor.Name);
    Assert.Equal(1, actor.Level);
    Assert.Equal(EnumActorType.NotSpecified, actor.ActorType);
    Assert.Equal(EnumFaction.NotSpecified, actor.Faction);
    Assert.NotNull(actor.CombatStats);
    Assert.False(actor.IsAlive); // CurrentHealth is 0 by default
  }

  [Fact]
  public void ActorComponent_FromDTO_CreatesCorrectComponent()
  {
    // Arrange
    var dto = CharacterDTOHelper.CreatePlayer("TestPlayer", 5);

    // Act
    var actor = ActorComponent.FromDTO(dto, EnumFaction.Creator);

    // Assert
    Assert.Equal(dto.Id, actor.Id);
    Assert.Equal("TestPlayer", actor.Name);
    Assert.Equal(5, actor.Level);
    Assert.Equal(EnumActorType.Player, actor.ActorType);
    Assert.Equal(EnumFaction.Creator, actor.Faction);
    Assert.True(actor.IsAlive);
  }

  [Fact]
  public void ActorComponent_Properties_AreCorrect()
  {
    // Arrange
    var actor = new ActorComponent
    {
      Name = "TestActor",
      Level = 10,
      ActorType = EnumActorType.Monster,
      ProfileKey = 100,
      CombatStats = CombatStatsHelper.CreateDefault(10)
    };

    // Assert
    Assert.Equal("TestActor", actor.Name);
    Assert.Equal(10, actor.Level);
    Assert.Equal(EnumActorType.Monster, actor.ActorType);
    Assert.Equal(100, actor.ProfileKey);
  }

  [Fact]
  public void ActorComponent_TakeDamage_ReducesHealth()
  {
    // Arrange
    var dto = CharacterDTOHelper.CreatePlayer("TestPlayer", 1);
    var actor = ActorComponent.FromDTO(dto, EnumFaction.Creator);
    var initialHealth = actor.CombatStats.CurrentHealth;

    // Act
    actor.TakeDamage(50);

    // Assert
    Assert.Equal(initialHealth - 50, actor.CombatStats.CurrentHealth);
    Assert.True(actor.IsAlive);
  }

  [Fact]
  public void ActorComponent_TakeDamage_CannotGoNegative()
  {
    // Arrange
    var dto = CharacterDTOHelper.CreatePlayer("TestPlayer", 1);
    var actor = ActorComponent.FromDTO(dto, EnumFaction.Creator);

    // Act
    actor.TakeDamage(1000);

    // Assert
    Assert.Equal(0, actor.CombatStats.CurrentHealth);
    Assert.False(actor.IsAlive);
  }

  [Fact]
  public void ActorComponent_Heal_IncreasesHealth()
  {
    // Arrange
    var dto = CharacterDTOHelper.CreatePlayer("TestPlayer", 1);
    var actor = ActorComponent.FromDTO(dto, EnumFaction.Creator);
    actor.TakeDamage(50);
    var healthAfterDamage = actor.CombatStats.CurrentHealth;

    // Act
    actor.Heal(30);

    // Assert
    Assert.Equal(healthAfterDamage + 30, actor.CombatStats.CurrentHealth);
  }

  [Fact]
  public void ActorComponent_Heal_CannotExceedMaxHealth()
  {
    // Arrange
    var dto = CharacterDTOHelper.CreatePlayer("TestPlayer", 1);
    var actor = ActorComponent.FromDTO(dto, EnumFaction.Creator);
    var maxHealth = actor.CombatStats.MaxHealth;

    // Act
    actor.Heal(1000);

    // Assert
    Assert.Equal(maxHealth, actor.CombatStats.CurrentHealth);
  }
}
