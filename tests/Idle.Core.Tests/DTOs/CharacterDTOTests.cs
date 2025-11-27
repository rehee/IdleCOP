using Xunit;
using Idle.Core.DTOs;

namespace Idle.Core.Tests.DTOs;

/// <summary>
/// CharacterDTO 测试
/// </summary>
public class CharacterDTOTests
{
  [Fact]
  public void CharacterDTO_NewInstance_HasDefaultValues()
  {
    // Arrange & Act
    var dto = new CharacterDTO();

    // Assert
    Assert.NotEqual(Guid.Empty, dto.Id);
    Assert.Null(dto.Name);
    Assert.Equal(1, dto.Level);
    Assert.Equal(EnumActorType.NotSpecified, dto.ActorType);
    Assert.Equal(0, dto.ProfileKey);
    Assert.NotNull(dto.CombatStats);
    Assert.NotNull(dto.SkillIds);
    Assert.Empty(dto.SkillIds);
  }

  [Fact]
  public void CharacterDTO_CreatePlayer_HasCorrectValues()
  {
    // Arrange & Act
    var dto = CharacterDTO.CreatePlayer("TestPlayer", 5);

    // Assert
    Assert.NotEqual(Guid.Empty, dto.Id);
    Assert.Equal("TestPlayer", dto.Name);
    Assert.Equal(5, dto.Level);
    Assert.Equal(EnumActorType.Player, dto.ActorType);
    Assert.NotNull(dto.CombatStats);
    Assert.Equal(150, dto.CombatStats.MaxHealth); // 100 + 5 * 10
  }

  [Fact]
  public void CharacterDTO_CreateMonster_HasCorrectValues()
  {
    // Arrange & Act
    var dto = CharacterDTO.CreateMonster("TestMonster", 10, 100);

    // Assert
    Assert.NotEqual(Guid.Empty, dto.Id);
    Assert.Equal("TestMonster", dto.Name);
    Assert.Equal(10, dto.Level);
    Assert.Equal(EnumActorType.Monster, dto.ActorType);
    Assert.Equal(100, dto.ProfileKey);
    Assert.NotNull(dto.CombatStats);
    Assert.Equal(200, dto.CombatStats.MaxHealth); // 100 + 10 * 10
  }
}
