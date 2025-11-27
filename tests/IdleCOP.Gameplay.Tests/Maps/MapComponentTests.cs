using Xunit;
using Idle.Core;
using Idle.Core.Combat;
using Idle.Core.Context;
using Idle.Core.DTOs;
using Idle.Core.Helpers;
using IdleCOP.Gameplay.Actors;
using IdleCOP.Gameplay.Maps;
using IdleCOP.Gameplay.Maps.Profiles;

namespace IdleCOP.Gameplay.Tests.Maps;

/// <summary>
/// MapComponent 测试
/// </summary>
public class MapComponentTests
{
  [Fact]
  public void MapComponent_Initialize_CreatesValidComponent()
  {
    // Arrange
    var player = CharacterDTOHelper.CreatePlayer("TestPlayer", 5);
    var request = new CombatRequest
    {
      CreatorCharacterId = Guid.NewGuid(),
      MapId = (int)EnumMap.StarterVillage,
      MapDifficulty = 1,
      CreatorLevel = 5,
      BattleSeed = 12345,
      ItemSeed = 67890,
      CreatorFactionCharacters = new List<CharacterDTO> { player }
    };

    using var context = new TickContext();

    // Act
    var map = MapComponent.Initialize(request, context);

    // Assert
    Assert.NotNull(map);
    Assert.NotNull(map.MapProfile);
    Assert.Equal(request, map.CombatRequest);
    Assert.False(map.IsReplay);
    Assert.Single(context.CreatorFaction);
  }

  [Fact]
  public void MapComponent_Initialize_SetsMaxTick()
  {
    // Arrange
    var player = CharacterDTOHelper.CreatePlayer("TestPlayer", 5);
    var request = new CombatRequest
    {
      CreatorCharacterId = Guid.NewGuid(),
      MapId = (int)EnumMap.StarterVillage,
      MapDifficulty = 1,
      CreatorLevel = 5,
      BattleSeed = 12345,
      ItemSeed = 67890,
      CreatorFactionCharacters = new List<CharacterDTO> { player }
    };

    using var context = new TickContext();

    // Act
    var map = MapComponent.Initialize(request, context);

    // Assert
    // StarterVillage has 120 seconds, at 30 fps = 3600 ticks
    Assert.Equal(120 * 30, context.MaxTick);
  }

  [Fact]
  public void MapComponent_Initialize_SetsBattleRandom()
  {
    // Arrange
    var player = CharacterDTOHelper.CreatePlayer("TestPlayer", 5);
    var request = new CombatRequest
    {
      CreatorCharacterId = Guid.NewGuid(),
      MapId = (int)EnumMap.StarterVillage,
      MapDifficulty = 1,
      CreatorLevel = 5,
      BattleSeed = 12345,
      ItemSeed = 67890,
      CreatorFactionCharacters = new List<CharacterDTO> { player }
    };

    using var context = new TickContext();

    // Act
    var map = MapComponent.Initialize(request, context);

    // Assert
    Assert.NotNull(context.BattleRandom);
    Assert.NotNull(context.ItemRandom);
  }

  [Fact]
  public void MapComponent_Initialize_ReplayMode_NoItemRandom()
  {
    // Arrange
    var player = CharacterDTOHelper.CreatePlayer("TestPlayer", 5);
    var request = new CombatRequest
    {
      CreatorCharacterId = Guid.NewGuid(),
      MapId = (int)EnumMap.StarterVillage,
      MapDifficulty = 1,
      CreatorLevel = 5,
      BattleSeed = 12345,
      ItemSeed = 67890,
      CreatorFactionCharacters = new List<CharacterDTO> { player },
      IsReplay = true
    };

    using var context = new TickContext();

    // Act
    var map = MapComponent.Initialize(request, context);

    // Assert
    Assert.NotNull(context.BattleRandom);
    Assert.Null(context.ItemRandom);
    Assert.True(map.IsReplay);
  }

  [Fact]
  public void MapComponent_Initialize_PvE_GeneratesWaves()
  {
    // Arrange
    var player = CharacterDTOHelper.CreatePlayer("TestPlayer", 5);
    var request = new CombatRequest
    {
      CreatorCharacterId = Guid.NewGuid(),
      MapId = (int)EnumMap.StarterVillage,
      MapDifficulty = 1,
      CreatorLevel = 5,
      BattleSeed = 12345,
      ItemSeed = 67890,
      CreatorFactionCharacters = new List<CharacterDTO> { player }
    };

    using var context = new TickContext();

    // Act
    var map = MapComponent.Initialize(request, context);

    // Assert
    Assert.NotEmpty(map.Waves);
    Assert.Equal(MapProfiles.StarterVillage.MaxWaves, map.Waves.Count);
  }

  [Fact]
  public void MapComponent_Initialize_PvP_SpawnsEnemyPlayers()
  {
    // Arrange
    var player1 = CharacterDTOHelper.CreatePlayer("Player1", 10);
    var player2 = CharacterDTOHelper.CreatePlayer("Player2", 10);
    var request = new CombatRequest
    {
      CreatorCharacterId = Guid.NewGuid(),
      MapId = (int)EnumMap.PvPArena,
      MapDifficulty = 1,
      CreatorLevel = 10,
      BattleSeed = 12345,
      ItemSeed = 67890,
      CreatorFactionCharacters = new List<CharacterDTO> { player1 },
      EnemyFactionCharacters = new List<CharacterDTO> { player2 }
    };

    using var context = new TickContext();

    // Act
    var map = MapComponent.Initialize(request, context);

    // Assert
    Assert.Single(context.CreatorFaction);
    Assert.Single(context.EnemyFaction);
    Assert.Empty(map.Waves);
  }

  [Fact]
  public void MapComponent_Initialize_InvalidMapId_Throws()
  {
    // Arrange
    var player = CharacterDTOHelper.CreatePlayer("TestPlayer", 5);
    var request = new CombatRequest
    {
      CreatorCharacterId = Guid.NewGuid(),
      MapId = 9999, // Invalid map ID
      MapDifficulty = 1,
      CreatorLevel = 5,
      BattleSeed = 12345,
      ItemSeed = 67890,
      CreatorFactionCharacters = new List<CharacterDTO> { player }
    };

    using var context = new TickContext();

    // Act & Assert
    Assert.Throws<ArgumentException>(() => MapComponent.Initialize(request, context));
  }

  [Fact]
  public void MapComponent_GenerateReplayEntity_CreatesCorrectEntity()
  {
    // Arrange
    var player = CharacterDTOHelper.CreatePlayer("TestPlayer", 5);
    var request = new CombatRequest
    {
      CreatorCharacterId = Guid.NewGuid(),
      MapId = (int)EnumMap.StarterVillage,
      MapDifficulty = 1,
      CreatorLevel = 5,
      BattleSeed = 12345,
      ItemSeed = 67890,
      CreatorFactionCharacters = new List<CharacterDTO> { player }
    };

    using var context = new TickContext();
    var map = MapComponent.Initialize(request, context);
    context.Result = EnumBattleResult.Victory;
    context.CurrentTick = 500;

    // Act
    var replay = map.GenerateReplayEntity(context);

    // Assert
    Assert.Equal(request.CreatorCharacterId, replay.CreatorCharacterId);
    Assert.Equal(request.MapId, replay.MapId);
    Assert.Equal(request.BattleSeed, replay.BattleSeed);
    Assert.Equal(EnumBattleResult.Victory, replay.BattleResult);
    Assert.Equal(500, replay.DurationTicks);
  }

  [Fact]
  public void MapComponent_TempInventory_HasMaxSize()
  {
    // Assert
    Assert.Equal(500, MapComponent.MaxTempInventorySize);
  }

  [Fact]
  public void MapComponent_AddToTempInventory_AddsItem()
  {
    // Arrange
    var map = new MapComponent();
    var itemId = Guid.NewGuid();

    // Act
    map.AddToTempInventory(itemId);

    // Assert
    Assert.Single(map.TempInventory);
    Assert.Contains(itemId, map.TempInventory);
  }
}
