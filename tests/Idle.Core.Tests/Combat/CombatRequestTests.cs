using Xunit;
using Idle.Core;
using Idle.Core.Combat;
using Idle.Core.DTOs;

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
  public void CombatRequest_CreateNew_HasCorrectValues()
  {
    // Arrange
    var creatorId = Guid.NewGuid();
    var player = CharacterDTO.CreatePlayer("TestPlayer", 5);
    var creatorFaction = new List<CharacterDTO> { player };

    // Act
    var request = CombatRequest.CreateNew(creatorId, 1, 2, 5, creatorFaction);

    // Assert
    Assert.Equal(creatorId, request.CreatorCharacterId);
    Assert.Equal(1, request.MapId);
    Assert.Equal(2, request.MapDifficulty);
    Assert.Equal(5, request.CreatorLevel);
    Assert.NotEqual(0, request.BattleSeed);
    Assert.NotEqual(0, request.ItemSeed);
    Assert.False(request.IsReplay);
    Assert.Single(request.CreatorFactionCharacters);
    Assert.Empty(request.EnemyFactionCharacters);
  }

  [Fact]
  public void CombatRequest_CreatePvP_HasBothFactions()
  {
    // Arrange
    var creatorId = Guid.NewGuid();
    var player1 = CharacterDTO.CreatePlayer("Player1", 10);
    var player2 = CharacterDTO.CreatePlayer("Player2", 10);

    // Act
    var request = CombatRequest.CreatePvP(
      creatorId, 100, 1, 10,
      new List<CharacterDTO> { player1 },
      new List<CharacterDTO> { player2 });

    // Assert
    Assert.Equal(100, request.MapId);
    Assert.Single(request.CreatorFactionCharacters);
    Assert.Single(request.EnemyFactionCharacters);
  }
}
