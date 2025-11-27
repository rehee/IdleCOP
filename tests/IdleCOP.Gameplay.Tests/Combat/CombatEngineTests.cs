using Xunit;
using Idle.Core;
using Idle.Core.Combat;
using Idle.Core.DTOs;
using Idle.Core.Helpers;
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
    var player = CharacterDTOHelper.CreatePlayer("TestPlayer", 20);
    player.CombatStats = CombatStatsHelper.CreateDefault(20);
    // Boost player stats for quick win
    player.CombatStats.PhysicalDamageMin = 100;
    player.CombatStats.PhysicalDamageMax = 150;

    var request = new CombatRequest
    {
      CreatorCharacterId = Guid.NewGuid(),
      MapId = (int)EnumMap.StarterVillage,
      MapDifficulty = 1,
      CreatorLevel = 20,
      BattleSeed = 12345,
      ItemSeed = 67890,
      CreatorFactionCharacters = new List<CharacterDTO> { player }
    };

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
      MapId = (int)EnumMap.StarterVillage,
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
    var player = CharacterDTOHelper.CreatePlayer("WeakPlayer", 1);
    player.CombatStats = new CombatStatsDTO
    {
      MaxHealth = 10,
      CurrentHealth = 10,
      PhysicalDamageMin = 1,
      PhysicalDamageMax = 2,
      AttackSpeed = 0.5f
    };

    var request = new CombatRequest
    {
      CreatorCharacterId = Guid.NewGuid(),
      MapId = (int)EnumMap.SkeletonGraveyard, // Harder map
      MapDifficulty = 5,
      CreatorLevel = 1,
      BattleSeed = 12345,
      ItemSeed = 67890,
      CreatorFactionCharacters = new List<CharacterDTO> { player }
    };

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
      MapId = (int)EnumMap.StarterVillage,
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
  public void CombatEngine_ReplayBattle_CallsCallback()
  {
    // Arrange
    var engine = new CombatEngine();
    var player = CharacterDTOHelper.CreatePlayer("TestPlayer", 20);
    player.CombatStats = CombatStatsHelper.CreateDefault(20);
    player.CombatStats.PhysicalDamageMin = 100;
    player.CombatStats.PhysicalDamageMax = 150;

    var request = new CombatRequest
    {
      CreatorCharacterId = Guid.NewGuid(),
      MapId = (int)EnumMap.StarterVillage,
      MapDifficulty = 1,
      CreatorLevel = 20,
      BattleSeed = 12345,
      ItemSeed = 67890,
      CreatorFactionCharacters = new List<CharacterDTO> { player }
    };

    var replay = engine.RunBattle(request);

    // Act
    var callCount = 0;
    engine.ReplayBattle(replay, ctx => callCount++);

    // Assert
    Assert.True(callCount > 0);
  }

  [Fact]
  public void CombatEngine_ReplayBattle_EndsWithSameResult()
  {
    // Arrange
    var engine = new CombatEngine();
    var player = CharacterDTOHelper.CreatePlayer("TestPlayer", 20);
    player.CombatStats = CombatStatsHelper.CreateDefault(20);
    player.CombatStats.PhysicalDamageMin = 100;
    player.CombatStats.PhysicalDamageMax = 150;

    var request = new CombatRequest
    {
      CreatorCharacterId = Guid.NewGuid(),
      MapId = (int)EnumMap.StarterVillage,
      MapDifficulty = 1,
      CreatorLevel = 20,
      BattleSeed = 12345,
      ItemSeed = 67890,
      CreatorFactionCharacters = new List<CharacterDTO> { player }
    };

    var originalReplay = engine.RunBattle(request);

    // Act
    EnumBattleResult? lastResult = null;
    engine.ReplayBattle(originalReplay, ctx => lastResult = ctx.Result);

    // Assert
    Assert.NotNull(lastResult);
    Assert.Equal(originalReplay.BattleResult, lastResult.Value);
  }
}
