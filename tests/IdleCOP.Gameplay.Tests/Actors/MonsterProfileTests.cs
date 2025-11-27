using Xunit;
using Idle.Core.DTOs;
using IdleCOP.Gameplay.Actors;

namespace IdleCOP.Gameplay.Tests.Actors;

/// <summary>
/// MonsterProfile 测试
/// </summary>
public class MonsterProfileTests
{
  [Fact]
  public void MonsterProfile_SkeletonWarrior_HasCorrectKey()
  {
    // Arrange & Act
    var profile = MonsterProfiles.SkeletonWarrior;

    // Assert
    Assert.Equal((int)EnumMonsterProfile.SkeletonWarrior, profile.Key);
    Assert.Equal(100, profile.Key);
  }

  [Fact]
  public void MonsterProfile_SkeletonWarrior_HasCorrectName()
  {
    // Arrange & Act
    var profile = MonsterProfiles.SkeletonWarrior;

    // Assert
    Assert.Equal("骷髅战士", profile.Name);
  }

  [Fact]
  public void MonsterProfile_AllProfiles_HaveUniqueKeys()
  {
    // Arrange
    var profiles = new[]
    {
      MonsterProfiles.SkeletonWarrior,
      MonsterProfiles.SkeletonArcher,
      MonsterProfiles.SkeletonMage,
      MonsterProfiles.Goblin,
      MonsterProfiles.GoblinChief
    };

    // Act
    var keys = profiles.Select(p => p.Key).ToList();

    // Assert
    Assert.Equal(keys.Count, keys.Distinct().Count());
  }

  [Fact]
  public void MonsterProfile_GetProfile_ReturnsCorrectProfile()
  {
    // Arrange & Act
    var profile = MonsterProfiles.GetProfile(EnumMonsterProfile.Goblin);

    // Assert
    Assert.NotNull(profile);
    Assert.Equal("哥布林", profile.Name);
    Assert.Equal(EnumActorType.Monster, profile.ActorType);
  }

  [Fact]
  public void MonsterProfile_GetProfileByKey_ReturnsCorrectProfile()
  {
    // Arrange & Act
    var profile = MonsterProfiles.GetProfileByKey(100);

    // Assert
    Assert.NotNull(profile);
    Assert.Equal("骷髅战士", profile.Name);
  }

  [Fact]
  public void MonsterProfile_GetProfile_NotSpecified_ReturnsNull()
  {
    // Arrange & Act
    var profile = MonsterProfiles.GetProfile(EnumMonsterProfile.NotSpecified);

    // Assert
    Assert.Null(profile);
  }

  [Fact]
  public void MonsterProfile_DifferentTypes_HaveDifferentBaseStats()
  {
    // Arrange
    var warrior = MonsterProfiles.SkeletonWarrior;
    var goblin = MonsterProfiles.Goblin;

    // Assert - Warrior should have more health than Goblin
    Assert.True(warrior.BaseMaxHealth > goblin.BaseMaxHealth);
  }

  [Fact]
  public void MonsterProfile_GoblinChief_IsBossType()
  {
    // Arrange & Act
    var chief = MonsterProfiles.GoblinChief;

    // Assert - Boss should have higher stats
    Assert.True(chief.BaseMaxHealth > 100);
    Assert.True(chief.BasePhysicalDamageMax > 15);
  }
}
