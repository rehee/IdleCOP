using Xunit;
using Idle.Core.DTOs;
using IdleCOP.Gameplay.Actors;
using IdleCOP.Gameplay.Actors.Players;

namespace IdleCOP.Gameplay.Tests.Actors;

/// <summary>
/// PlayerProfile 测试
/// </summary>
public class PlayerProfileTests
{
  [Fact]
  public void PlayerProfile_BasicPlayer_HasCorrectKey()
  {
    // Arrange & Act
    var profile = PlayerProfiles.BasicPlayer;

    // Assert
    Assert.Equal((int)EnumPlayer.BasicPlayer, profile.Key);
    Assert.Equal(1, profile.Key);
  }

  [Fact]
  public void PlayerProfile_BasicPlayer_HasCorrectName()
  {
    // Arrange & Act
    var profile = PlayerProfiles.BasicPlayer;

    // Assert
    Assert.Equal("基础玩家", profile.Name);
  }

  [Fact]
  public void PlayerProfile_BasicPlayer_HasCorrectActorType()
  {
    // Arrange & Act
    var profile = PlayerProfiles.BasicPlayer;

    // Assert
    Assert.Equal(EnumActorType.Player, profile.ActorType);
  }

  [Fact]
  public void PlayerProfile_BasicPlayer_HasBalancedBaseStats()
  {
    // Arrange & Act
    var profile = PlayerProfiles.BasicPlayer;

    // Assert
    Assert.Equal(150, profile.BaseMaxHealth);
    Assert.Equal(100, profile.BaseMaxEnergy);
    Assert.Equal(8, profile.BasePhysicalDamageMin);
    Assert.Equal(15, profile.BasePhysicalDamageMax);
    Assert.Equal(1.2f, profile.BaseAttackSpeed);
  }

  [Fact]
  public void PlayerProfile_ApplyLevelScaling_ScalesCorrectly()
  {
    // Arrange
    var profile = PlayerProfiles.BasicPlayer;
    var actor = new ActorComponent { Level = 10 };

    // Act
    profile.ApplyLevelScaling(actor);

    // Assert
    Assert.Equal(150 + 10 * 10, actor.CombatStats.MaxHealth);
    Assert.Equal(100 + 10 * 5, actor.CombatStats.MaxEnergy);
    Assert.Equal(8 + 10, actor.CombatStats.PhysicalDamageMin);
    Assert.Equal(15 + 10 * 2, actor.CombatStats.PhysicalDamageMax);
  }
}
