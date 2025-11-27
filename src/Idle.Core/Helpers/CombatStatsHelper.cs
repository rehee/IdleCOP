using Idle.Core.DTOs;

namespace Idle.Core.Helpers;

/// <summary>
/// 战斗面板帮助类
/// </summary>
public static class CombatStatsHelper
{
  /// <summary>
  /// 创建默认战斗面板
  /// </summary>
  public static CombatStatsDTO CreateDefault(int level = 1)
  {
    return new CombatStatsDTO
    {
      MaxHealth = 100 + level * 10,
      CurrentHealth = 100 + level * 10,
      MaxEnergy = 50 + level * 5,
      CurrentEnergy = 50 + level * 5,
      Strength = 10 + level,
      Dexterity = 10 + level,
      Intelligence = 10 + level,
      PhysicalDamageMin = 5 + level,
      PhysicalDamageMax = 10 + level * 2,
      AttackSpeed = 1.0f,
      CriticalChance = 5.0f,
      CriticalMultiplier = 1.5f,
      Armor = level * 5,
      Evasion = level * 3
    };
  }

  /// <summary>
  /// 克隆战斗面板
  /// </summary>
  public static CombatStatsDTO Clone(CombatStatsDTO source)
  {
    return new CombatStatsDTO
    {
      MaxHealth = source.MaxHealth,
      CurrentHealth = source.CurrentHealth,
      MaxEnergy = source.MaxEnergy,
      CurrentEnergy = source.CurrentEnergy,
      Strength = source.Strength,
      Dexterity = source.Dexterity,
      Intelligence = source.Intelligence,
      PhysicalDamageMin = source.PhysicalDamageMin,
      PhysicalDamageMax = source.PhysicalDamageMax,
      AttackSpeed = source.AttackSpeed,
      CriticalChance = source.CriticalChance,
      CriticalMultiplier = source.CriticalMultiplier,
      Armor = source.Armor,
      Evasion = source.Evasion,
      FireResistance = source.FireResistance,
      ColdResistance = source.ColdResistance,
      LightningResistance = source.LightningResistance,
      ChaosResistance = source.ChaosResistance
    };
  }
}
