using IdleCOP.Gameplay;

namespace IdleCOP.Data.Entities;

/// <summary>
/// 技能实体
/// </summary>
public class SkillEntity
{
  /// <summary>
  /// 唯一标识符
  /// </summary>
  public Guid Id { get; set; } = Guid.NewGuid();

  /// <summary>
  /// 对应的 Profile Key
  /// </summary>
  public int ProfileKey { get; set; }

  /// <summary>
  /// 技能类型
  /// </summary>
  public EnumSkillType Type { get; set; }

  /// <summary>
  /// 冷却时间（秒）
  /// </summary>
  public float Cooldown { get; set; }

  /// <summary>
  /// 读条时间（秒）
  /// </summary>
  public float CastTime { get; set; }
}
