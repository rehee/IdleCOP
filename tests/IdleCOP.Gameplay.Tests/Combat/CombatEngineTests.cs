using Xunit;
using Idle.Core;
using Idle.Core.Combat;
using Idle.Core.DTOs;
using IdleCOP.Gameplay.Combat;
using IdleCOP.Gameplay.Maps;

namespace IdleCOP.Gameplay.Tests.Combat;

/// <summary>
/// CombatEngine 测试
/// </summary>
public class CombatEngineTests
{
  [Fact]
  public void CombatEngine_RunBattle_ReturnsReplayEntity()
  {
    // Arrange
    var engine = new CombatEngine();
    var player = CharacterDTO.CreatePlayer("TestPlayer", 20);
    player.CombatStats = CombatStatsDTO.CreateDefault(20);
    // Boost player stats for quick win
    player.CombatStats.PhysicalDamageMin = 100;
    player.CombatStats.PhysicalDamageMax = 150;

    var request = CombatRequest.CreateNew(
      Guid.NewGuid(),
      (int)EnumMapProfile.StarterVillage,
      1, 20,
      new List<CharacterDTO> { player });

    // Act
    var replay = engine.RunBattle(request);

    // Assert
    Assert.NotNull(replay);
    Assert.Equal(request.CreatorCharacterId, replay.CreatorCharacterId);
    Assert.Equal(request.MapId, replay.MapId);
    Assert.Equal(request.BattleSeed, replay.BattleSeed);
    Assert.True(replay.DurationTicks > 0);
  }

  [Fact]
  public void CombatEngine_RunBattle_DeterministicResult()
  {
    // Arrange
    var engine = new CombatEngine();

    // Create two separate requests with same seed and same initial stats
    var request1 = new CombatRequest
    {
      CreatorCharacterId = Guid.NewGuid(),
      MapId = (int)EnumMapProfile.StarterVillage,
      MapDifficulty = 1,
      CreatorLevel = 10,
      BattleSeed = 12345,
      ItemSeed = 67890,
      CreatorFactionCharacters = new List<CharacterDTO>
      {
        new CharacterDTO
        {
          Name = "TestPlayer",
          Level = 10,
          ActorType = EnumActorType.Player,
          CombatStats = new CombatStatsDTO
          {
            MaxHealth = 500,
            CurrentHealth = 500,
            PhysicalDamageMin = 50,
            PhysicalDamageMax = 60,
            AttackSpeed = 1.0f,
            CriticalChance = 5.0f,
            CriticalMultiplier = 1.5f
          }
        }
      }
    };

    var request2 = new CombatRequest
    {
      CreatorCharacterId = request1.CreatorCharacterId,
      MapId = request1.MapId,
      MapDifficulty = request1.MapDifficulty,
      CreatorLevel = request1.CreatorLevel,
      BattleSeed = request1.BattleSeed,
      ItemSeed = request1.ItemSeed,
      CreatorFactionCharacters = new List<CharacterDTO>
      {
        new CharacterDTO
        {
          Name = "TestPlayer",
          Level = 10,
          ActorType = EnumActorType.Player,
          CombatStats = new CombatStatsDTO
          {
            MaxHealth = 500,
            CurrentHealth = 500,
            PhysicalDamageMin = 50,
            PhysicalDamageMax = 60,
            AttackSpeed = 1.0f,
            CriticalChance = 5.0f,
            CriticalMultiplier = 1.5f
          }
        }
      }
    };

    // Act
    var replay1 = engine.RunBattle(request1);
    var replay2 = engine.RunBattle(request2);

    // Assert
    Assert.Equal(replay1.BattleResult, replay2.BattleResult);
    Assert.Equal(replay1.DurationTicks, replay2.DurationTicks);
  }

  [Fact]
  public void CombatEngine_RunBattle_WeakPlayer_CanLose()
  {
    // Arrange
    var engine = new CombatEngine();
    var player = CharacterDTO.CreatePlayer("WeakPlayer", 1);
    player.CombatStats = new CombatStatsDTO
    {
      MaxHealth = 10,
      CurrentHealth = 10,
      PhysicalDamageMin = 1,
      PhysicalDamageMax = 2,
      AttackSpeed = 0.5f
    };

    var request = CombatRequest.CreateNew(
      Guid.NewGuid(),
      (int)EnumMapProfile.SkeletonGraveyard, // Harder map
      5, 1,
      new List<CharacterDTO> { player });

    // Act
    var replay = engine.RunBattle(request);

    // Assert
    Assert.NotNull(replay);
    // Player should lose (Defeat or Timeout, but not Victory)
    Assert.NotEqual(EnumBattleResult.Victory, replay.BattleResult);
  }

  [Fact]
  public void CombatEngine_RunBattle_StrongPlayer_CanWin()
  {
    // Arrange
    var engine = new CombatEngine();
    var request = new CombatRequest
    {
      CreatorCharacterId = Guid.NewGuid(),
      MapId = (int)EnumMapProfile.StarterVillage,
      MapDifficulty = 1,
      CreatorLevel = 50,
      BattleSeed = 54321,
      ItemSeed = 12345,
      CreatorFactionCharacters = new List<CharacterDTO>
      {
        new CharacterDTO
        {
          Name = "StrongPlayer",
          Level = 50,
          ActorType = EnumActorType.Player,
          CombatStats = new CombatStatsDTO
          {
            MaxHealth = 5000,
            CurrentHealth = 5000,
            PhysicalDamageMin = 500,
            PhysicalDamageMax = 600,
            AttackSpeed = 5.0f,
            CriticalChance = 50.0f,
            CriticalMultiplier = 2.0f,
            Armor = 1000
          }
        }
      }
    };

    // Act
    var replay = engine.RunBattle(request);

    // Assert
    Assert.Equal(EnumBattleResult.Victory, replay.BattleResult);
  }

  [Fact]
  public void CombatEngine_ReplayBattle_ReturnsTicks()
  {
    // Arrange
    var engine = new CombatEngine();
    var player = CharacterDTO.CreatePlayer("TestPlayer", 20);
    player.CombatStats = CombatStatsDTO.CreateDefault(20);
    player.CombatStats.PhysicalDamageMin = 100;
    player.CombatStats.PhysicalDamageMax = 150;

    var request = CombatRequest.CreateNew(
      Guid.NewGuid(),
      (int)EnumMapProfile.StarterVillage,
      1, 20,
      new List<CharacterDTO> { player });

    var replay = engine.RunBattle(request);

    // Act
    var ticks = engine.ReplayBattle(replay).ToList();

    // Assert
    Assert.NotEmpty(ticks);
    Assert.True(ticks.Count > 0);
  }

  [Fact]
  public void CombatEngine_ReplayBattle_EndsWithSameResult()
  {
    // Arrange
    var engine = new CombatEngine();
    var player = CharacterDTO.CreatePlayer("TestPlayer", 20);
    player.CombatStats = CombatStatsDTO.CreateDefault(20);
    player.CombatStats.PhysicalDamageMin = 100;
    player.CombatStats.PhysicalDamageMax = 150;

    var request = CombatRequest.CreateNew(
      Guid.NewGuid(),
      (int)EnumMapProfile.StarterVillage,
      1, 20,
      new List<CharacterDTO> { player });

    var originalReplay = engine.RunBattle(request);

    // Act
    var ticks = engine.ReplayBattle(originalReplay).ToList();
    var lastTick = ticks.Last();

    // Assert
    Assert.Equal(originalReplay.BattleResult, lastTick.Result);
  }
}
