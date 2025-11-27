namespace Idle.Core.DTOs;

/// <summary>
/// 战斗面板数据 - 暗黑风格攻防面板
/// </summary>
public class CombatStatsDTO
{
  /// <summary>
  /// 最大生命值
  /// </summary>
  public int MaxHealth { get; set; }

  /// <summary>
  /// 当前生命值
  /// </summary>
  public int CurrentHealth { get; set; }

  /// <summary>
  /// 最大能量
  /// </summary>
  public int MaxEnergy { get; set; }

  /// <summary>
  /// 当前能量
  /// </summary>
  public int CurrentEnergy { get; set; }

  /// <summary>
  /// 力量属性
  /// </summary>
  public int Strength { get; set; }

  /// <summary>
  /// 敏捷属性
  /// </summary>
  public int Dexterity { get; set; }

  /// <summary>
  /// 智力属性
  /// </summary>
  public int Intelligence { get; set; }

  /// <summary>
  /// 物理伤害（最小）
  /// </summary>
  public int PhysicalDamageMin { get; set; }

  /// <summary>
  /// 物理伤害（最大）
  /// </summary>
  public int PhysicalDamageMax { get; set; }

  /// <summary>
  /// 攻击速度（每秒攻击次数）
  /// </summary>
  public float AttackSpeed { get; set; } = 1.0f;

  /// <summary>
  /// 暴击率（百分比）
  /// </summary>
  public float CriticalChance { get; set; }

  /// <summary>
  /// 暴击伤害倍数
  /// </summary>
  public float CriticalMultiplier { get; set; } = 1.5f;

  /// <summary>
  /// 护甲值
  /// </summary>
  public int Armor { get; set; }

  /// <summary>
  /// 闪避值
  /// </summary>
  public int Evasion { get; set; }

  /// <summary>
  /// 火焰抗性（百分比）
  /// </summary>
  public int FireResistance { get; set; }

  /// <summary>
  /// 冰冷抗性（百分比）
  /// </summary>
  public int ColdResistance { get; set; }

  /// <summary>
  /// 闪电抗性（百分比）
  /// </summary>
  public int LightningResistance { get; set; }

  /// <summary>
  /// 混沌抗性（百分比）
  /// </summary>
  public int ChaosResistance { get; set; }

  /// <summary>
  /// 克隆战斗面板
  /// </summary>
  public CombatStatsDTO Clone()
  {
    return new CombatStatsDTO
    {
      MaxHealth = MaxHealth,
      CurrentHealth = CurrentHealth,
      MaxEnergy = MaxEnergy,
      CurrentEnergy = CurrentEnergy,
      Strength = Strength,
      Dexterity = Dexterity,
      Intelligence = Intelligence,
      PhysicalDamageMin = PhysicalDamageMin,
      PhysicalDamageMax = PhysicalDamageMax,
      AttackSpeed = AttackSpeed,
      CriticalChance = CriticalChance,
      CriticalMultiplier = CriticalMultiplier,
      Armor = Armor,
      Evasion = Evasion,
      FireResistance = FireResistance,
      ColdResistance = ColdResistance,
      LightningResistance = LightningResistance,
      ChaosResistance = ChaosResistance
    };
  }

}
