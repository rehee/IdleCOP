using Xunit;
using Idle.Utility;
using IdleCOP.Gameplay.Actors;
using IdleCOP.Gameplay.Maps;

namespace IdleCOP.Gameplay.Tests.Maps;

/// <summary>
/// MapProfile 测试
/// </summary>
public class MapProfileTests
{
  [Fact]
  public void MapProfile_StarterVillage_HasCorrectProperties()
  {
    // Arrange & Act
    var profile = MapProfiles.StarterVillage;

    // Assert
    Assert.Equal((int)EnumMapProfile.StarterVillage, profile.Key);
    Assert.Equal("新手村", profile.Name);
    Assert.Equal(EnumMapType.PvE, profile.MapType);
    Assert.Equal(120, profile.MaxBattleSeconds);
    Assert.Equal(2, profile.MaxWaves);
    Assert.Equal(1, profile.RecommendedLevel);
    Assert.Contains(EnumMonsterProfile.Goblin, profile.PossibleMonsters);
  }

  [Fact]
  public void MapProfile_SkeletonGraveyard_HasCorrectProperties()
  {
    // Arrange & Act
    var profile = MapProfiles.SkeletonGraveyard;

    // Assert
    Assert.Equal((int)EnumMapProfile.SkeletonGraveyard, profile.Key);
    Assert.Equal("骷髅墓地", profile.Name);
    Assert.Equal(EnumMapType.PvE, profile.MapType);
    Assert.Equal(180, profile.MaxBattleSeconds);
    Assert.Equal(3, profile.MaxWaves);
    Assert.Equal(5, profile.RecommendedLevel);
    Assert.Contains(EnumMonsterProfile.SkeletonWarrior, profile.PossibleMonsters);
    Assert.Contains(EnumMonsterProfile.SkeletonArcher, profile.PossibleMonsters);
    Assert.Contains(EnumMonsterProfile.SkeletonMage, profile.PossibleMonsters);
  }

  [Fact]
  public void MapProfile_PvPArena_IsPvPType()
  {
    // Arrange & Act
    var profile = MapProfiles.PvPArena;

    // Assert
    Assert.Equal(EnumMapType.PvP, profile.MapType);
    Assert.Empty(profile.PossibleMonsters);
  }

  [Fact]
  public void MapProfile_GenerateWaves_CreatesCorrectNumberOfWaves()
  {
    // Arrange
    var profile = MapProfiles.SkeletonGraveyard;
    var random = new GameRandom(12345);

    // Act
    var waves = profile.GenerateWaves(1, random);

    // Assert
    Assert.Equal(profile.MaxWaves, waves.Count);
  }

  [Fact]
  public void MapProfile_GenerateWaves_EachWaveHasMonsters()
  {
    // Arrange
    var profile = MapProfiles.StarterVillage;
    var random = new GameRandom(12345);

    // Act
    var waves = profile.GenerateWaves(1, random);

    // Assert
    foreach (var wave in waves)
    {
      Assert.NotEmpty(wave.Monsters);
    }
  }

  [Fact]
  public void MapProfile_GenerateWaves_MonstersAreFromPossibleList()
  {
    // Arrange
    var profile = MapProfiles.SkeletonGraveyard;
    var random = new GameRandom(12345);

    // Act
    var waves = profile.GenerateWaves(1, random);

    // Assert
    foreach (var wave in waves)
    {
      foreach (var monster in wave.Monsters.Keys)
      {
        Assert.Contains(monster, profile.PossibleMonsters);
      }
    }
  }

  [Fact]
  public void MapProfile_PvPArena_GenerateWaves_ReturnsEmpty()
  {
    // Arrange
    var profile = MapProfiles.PvPArena;
    var random = new GameRandom(12345);

    // Act
    var waves = profile.GenerateWaves(1, random);

    // Assert
    Assert.Empty(waves);
  }

  [Fact]
  public void MapProfile_GetProfile_ReturnsCorrectProfile()
  {
    // Arrange & Act
    var profile = MapProfiles.GetProfile(EnumMapProfile.StarterVillage);

    // Assert
    Assert.NotNull(profile);
    Assert.Equal("新手村", profile.Name);
  }

  [Fact]
  public void MapProfile_GetProfileByKey_ReturnsCorrectProfile()
  {
    // Arrange & Act
    var profile = MapProfiles.GetProfileByKey(1);

    // Assert
    Assert.NotNull(profile);
    Assert.Equal("新手村", profile.Name);
  }

  [Fact]
  public void MapProfile_GetProfile_NotSpecified_ReturnsNull()
  {
    // Arrange & Act
    var profile = MapProfiles.GetProfile(EnumMapProfile.NotSpecified);

    // Assert
    Assert.Null(profile);
  }
}
