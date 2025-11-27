using Xunit;
using Idle.Core;
using Idle.Core.Combat;
using Idle.Core.DTOs;
using Idle.Core.Helpers;

namespace Idle.Core.Tests.Combat;

/// <summary>
/// CombatRequest 测试
/// </summary>
public class CombatRequestTests
{
  [Fact]
  public void CombatRequest_NewInstance_HasDefaultValues()
  {
    // Arrange & Act
    var request = new CombatRequest();

    // Assert
    Assert.Equal(Guid.Empty, request.CreatorCharacterId);
    Assert.Equal(0, request.MapId);
    Assert.Equal(0, request.MapDifficulty);
    Assert.Equal(EnumBattleResult.NotSpecified, request.BattleResult);
    Assert.False(request.IsReplay);
    Assert.NotNull(request.CreatorFactionCharacters);
    Assert.Empty(request.CreatorFactionCharacters);
  }

  [Fact]
  public void CombatRequest_CanSetProperties()
  {
    // Arrange
    var creatorId = Guid.NewGuid();
    var player = CharacterDTOHelper.CreatePlayer("TestPlayer", 5);
    var creatorFaction = new List<CharacterDTO> { player };

    // Act
    var request = new CombatRequest
    {
      CreatorCharacterId = creatorId,
      MapId = 1,
      MapDifficulty = 2,
      CreatorLevel = 5,
      BattleSeed = 12345,
      ItemSeed = 67890,
      CreatorFactionCharacters = creatorFaction,
      IsReplay = false
    };

    // Assert
    Assert.Equal(creatorId, request.CreatorCharacterId);
    Assert.Equal(1, request.MapId);
    Assert.Equal(2, request.MapDifficulty);
    Assert.Equal(5, request.CreatorLevel);
    Assert.Equal(12345, request.BattleSeed);
    Assert.Equal(67890, request.ItemSeed);
    Assert.False(request.IsReplay);
    Assert.Single(request.CreatorFactionCharacters);
    Assert.Empty(request.EnemyFactionCharacters);
  }

  [Fact]
  public void CombatRequest_CanSetBothFactions()
  {
    // Arrange
    var creatorId = Guid.NewGuid();
    var player1 = CharacterDTOHelper.CreatePlayer("Player1", 10);
    var player2 = CharacterDTOHelper.CreatePlayer("Player2", 10);

    // Act
    var request = new CombatRequest
    {
      CreatorCharacterId = creatorId,
      MapId = 100,
      MapDifficulty = 1,
      CreatorLevel = 10,
      BattleSeed = 12345,
      ItemSeed = 67890,
      CreatorFactionCharacters = new List<CharacterDTO> { player1 },
      EnemyFactionCharacters = new List<CharacterDTO> { player2 }
    };

    // Assert
    Assert.Equal(100, request.MapId);
    Assert.Single(request.CreatorFactionCharacters);
    Assert.Single(request.EnemyFactionCharacters);
  }
}
