using Idle.Core;
using Idle.Core.DTOs;
using Idle.Utility.Components;

namespace IdleCOP.Gameplay.Actors;

/// <summary>
/// 演员组件 - 所有战斗单位（玩家、怪物、召唤物）的基类
/// </summary>
public class ActorComponent : IdleComponent
{
  /// <summary>
  /// 演员名称
  /// </summary>
  public string? Name { get; set; }

  /// <summary>
  /// 演员等级
  /// </summary>
  public int Level { get; set; } = 1;

  /// <summary>
  /// 演员类型
  /// </summary>
  public EnumActorType ActorType { get; set; } = EnumActorType.NotSpecified;

  /// <summary>
  /// 阵营
  /// </summary>
  public EnumFaction Faction { get; set; }

  /// <summary>
  /// 战斗面板
  /// </summary>
  public CombatStatsDTO CombatStats { get; set; } = new();

  /// <summary>
  /// 是否存活
  /// </summary>
  public bool IsAlive => CombatStats.CurrentHealth > 0;

  /// <summary>
  /// 当前目标
  /// </summary>
  public ActorComponent? CurrentTarget { get; set; }

  /// <summary>
  /// 攻击冷却（tick数）
  /// </summary>
  public int AttackCooldown { get; set; }

  /// <summary>
  /// 从DTO创建组件
  /// </summary>
  public static ActorComponent FromDTO(CharacterDTO dto, EnumFaction faction)
  {
    return new ActorComponent
    {
      Id = dto.Id,
      Name = dto.Name,
      Level = dto.Level,
      ActorType = dto.ActorType,
      ProfileKey = dto.ProfileKey,
      Faction = faction,
      CombatStats = dto.CombatStats.Clone()
    };
  }

  /// <summary>
  /// 转换为DTO
  /// </summary>
  public CharacterDTO ToDTO()
  {
    return new CharacterDTO
    {
      Id = Id,
      Name = Name,
      Level = Level,
      ActorType = ActorType,
      ProfileKey = ProfileKey,
      CombatStats = CombatStats
    };
  }

  /// <summary>
  /// 受到伤害
  /// </summary>
  public void TakeDamage(int damage)
  {
    CombatStats.CurrentHealth = Math.Max(0, CombatStats.CurrentHealth - damage);
  }

  /// <summary>
  /// 恢复生命
  /// </summary>
  public void Heal(int amount)
  {
    CombatStats.CurrentHealth = Math.Min(CombatStats.MaxHealth, CombatStats.CurrentHealth + amount);
  }
}
