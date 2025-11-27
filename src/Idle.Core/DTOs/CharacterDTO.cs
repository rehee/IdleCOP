namespace Idle.Core.DTOs;

/// <summary>
/// 角色数据传输对象 - 用于战斗中传递角色信息
/// </summary>
public class CharacterDTO
{
  /// <summary>
  /// 角色唯一标识
  /// </summary>
  public Guid Id { get; set; } = Guid.NewGuid();

  /// <summary>
  /// 角色名称
  /// </summary>
  public string? Name { get; set; }

  /// <summary>
  /// 角色等级
  /// </summary>
  public int Level { get; set; } = 1;

  /// <summary>
  /// 演员类型
  /// </summary>
  public EnumActorType ActorType { get; set; }

  /// <summary>
  /// Profile Key - 对应角色的 Profile
  /// </summary>
  public int ProfileKey { get; set; }

  /// <summary>
  /// 战斗面板数据
  /// </summary>
  public CombatStatsDTO CombatStats { get; set; } = new();

  /// <summary>
  /// 技能ID列表
  /// </summary>
  public List<Guid> SkillIds { get; set; } = new();

  /// <summary>
  /// 装备ID列表
  /// </summary>
  public List<Guid> EquipmentIds { get; set; } = new();

  /// <summary>
  /// 策略ID列表
  /// </summary>
  public List<Guid> StrategyIds { get; set; } = new();
}
