using Xunit;
using Idle.Core;
using Idle.Core.Combat;
using Idle.Core.DTOs;
using Idle.Core.Helpers;

namespace Idle.Core.Tests.Combat;

/// <summary>
/// CombatReplayEntity 测试
/// </summary>
public class CombatReplayEntityTests
{
  [Fact]
  public void CombatReplayEntity_NewInstance_HasDefaultValues()
  {
    // Arrange & Act
    var entity = new CombatReplayEntity();

    // Assert
    Assert.NotEqual(Guid.Empty, entity.Id);
    Assert.Equal(Guid.Empty, entity.CreatorCharacterId);
    Assert.Equal(0, entity.MapId);
    Assert.Equal(EnumBattleResult.NotSpecified, entity.BattleResult);
    Assert.NotNull(entity.CreatorFactionCharacters);
    Assert.Empty(entity.CreatorFactionCharacters);
  }

  [Fact]
  public void CombatReplayHelper_ToCombatRequest_CreatesCorrectRequest()
  {
    // Arrange
    var entity = new CombatReplayEntity
    {
      CreatorCharacterId = Guid.NewGuid(),
      MapId = 1,
      MapDifficulty = 2,
      CreatorLevel = 10,
      BattleResult = EnumBattleResult.Victory,
      DurationTicks = 1000,
      BattleSeed = 12345,
      ItemSeed = 67890,
      CreatorFactionCharacters = new List<CharacterDTO>
      {
        CharacterDTOHelper.CreatePlayer("Player1", 10)
      }
    };

    // Act
    var request = CombatReplayHelper.ToCombatRequest(entity);

    // Assert
    Assert.Equal(entity.CreatorCharacterId, request.CreatorCharacterId);
    Assert.Equal(entity.MapId, request.MapId);
    Assert.Equal(entity.MapDifficulty, request.MapDifficulty);
    Assert.Equal(entity.CreatorLevel, request.CreatorLevel);
    Assert.Equal(entity.BattleResult, request.BattleResult);
    Assert.Equal(entity.DurationTicks, request.DurationTicks);
    Assert.Equal(entity.BattleSeed, request.BattleSeed);
    Assert.Equal(entity.ItemSeed, request.ItemSeed);
    Assert.True(request.IsReplay);
  }

  [Fact]
  public void CombatReplayHelper_FromCombatRequest_CreatesCorrectEntity()
  {
    // Arrange
    var creatorId = Guid.NewGuid();
    var request = new CombatRequest
    {
      CreatorCharacterId = creatorId,
      MapId = 2,
      MapDifficulty = 3,
      CreatorLevel = 15,
      BattleResult = EnumBattleResult.Defeat,
      DurationTicks = 2000,
      BattleSeed = 11111,
      ItemSeed = 22222,
      CreatorFactionCharacters = new List<CharacterDTO>
      {
        CharacterDTOHelper.CreatePlayer("Player1", 15)
      }
    };

    // Act
    var entity = CombatReplayHelper.FromCombatRequest(request);

    // Assert
    Assert.Equal(request.CreatorCharacterId, entity.CreatorCharacterId);
    Assert.Equal(request.MapId, entity.MapId);
    Assert.Equal(request.MapDifficulty, entity.MapDifficulty);
    Assert.Equal(request.CreatorLevel, entity.CreatorLevel);
    Assert.Equal(request.BattleResult, entity.BattleResult);
    Assert.Equal(request.DurationTicks, entity.DurationTicks);
    Assert.Equal(request.BattleSeed, entity.BattleSeed);
    Assert.Equal(request.ItemSeed, entity.ItemSeed);
    Assert.Single(entity.CreatorFactionCharacters);
  }
}
