namespace Idle.Core.DTOs;

/// <summary>
/// 演员类型枚举
/// </summary>
public enum EnumActorType
{
  /// <summary>
  /// 未指定
  /// </summary>
  NotSpecified = 0,

  /// <summary>
  /// 玩家角色
  /// </summary>
  Player,

  /// <summary>
  /// 怪物
  /// </summary>
  Monster,

  /// <summary>
  /// 召唤物
  /// </summary>
  Summon,

  /// <summary>
  /// 投射物
  /// </summary>
  Projectile
}

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

  /// <summary>
  /// 创建玩家角色DTO
  /// </summary>
  public static CharacterDTO CreatePlayer(string name, int level = 1)
  {
    return new CharacterDTO
    {
      Name = name,
      Level = level,
      ActorType = EnumActorType.Player,
      CombatStats = CombatStatsDTO.CreateDefault(level)
    };
  }

  /// <summary>
  /// 创建怪物角色DTO
  /// </summary>
  public static CharacterDTO CreateMonster(string name, int level, int profileKey)
  {
    return new CharacterDTO
    {
      Name = name,
      Level = level,
      ActorType = EnumActorType.Monster,
      ProfileKey = profileKey,
      CombatStats = CombatStatsDTO.CreateDefault(level)
    };
  }
}
