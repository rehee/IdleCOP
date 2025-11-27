using Xunit;
using Idle.Core.DTOs;
using Idle.Core.Helpers;

namespace Idle.Core.Tests.DTOs;

/// <summary>
/// CombatStatsDTO 测试
/// </summary>
public class CombatStatsDTOTests
{
  [Fact]
  public void CombatStatsDTO_NewInstance_HasDefaultValues()
  {
    // Arrange & Act
    var stats = new CombatStatsDTO();

    // Assert
    Assert.Equal(0, stats.MaxHealth);
    Assert.Equal(0, stats.CurrentHealth);
    Assert.Equal(1.0f, stats.AttackSpeed);
    Assert.Equal(1.5f, stats.CriticalMultiplier);
  }

  [Fact]
  public void CombatStatsHelper_CreateDefault_Level1_HasCorrectValues()
  {
    // Arrange & Act
    var stats = CombatStatsHelper.CreateDefault(1);

    // Assert
    Assert.Equal(110, stats.MaxHealth); // 100 + 1 * 10
    Assert.Equal(110, stats.CurrentHealth);
    Assert.Equal(55, stats.MaxEnergy); // 50 + 1 * 5
    Assert.Equal(55, stats.CurrentEnergy);
    Assert.Equal(6, stats.PhysicalDamageMin); // 5 + 1
    Assert.Equal(12, stats.PhysicalDamageMax); // 10 + 1 * 2
    Assert.Equal(1.0f, stats.AttackSpeed);
    Assert.Equal(5.0f, stats.CriticalChance);
    Assert.Equal(1.5f, stats.CriticalMultiplier);
    Assert.Equal(5, stats.Armor); // 1 * 5
    Assert.Equal(3, stats.Evasion); // 1 * 3
  }

  [Fact]
  public void CombatStatsHelper_CreateDefault_Level10_ScalesCorrectly()
  {
    // Arrange & Act
    var stats = CombatStatsHelper.CreateDefault(10);

    // Assert
    Assert.Equal(200, stats.MaxHealth); // 100 + 10 * 10
    Assert.Equal(100, stats.MaxEnergy); // 50 + 10 * 5
    Assert.Equal(15, stats.PhysicalDamageMin); // 5 + 10
    Assert.Equal(30, stats.PhysicalDamageMax); // 10 + 10 * 2
    Assert.Equal(50, stats.Armor); // 10 * 5
    Assert.Equal(30, stats.Evasion); // 10 * 3
  }
}
