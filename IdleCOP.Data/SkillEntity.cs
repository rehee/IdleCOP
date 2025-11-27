using IdleCOP.Data.Enums;

namespace IdleCOP.Data.Entities;

/// <summary>
/// Entity representing a skill.
/// </summary>
public class SkillEntity : BaseEntity
{
  /// <summary>
  /// Gets or sets the profile key.
  /// </summary>
  public int ProfileKey { get; set; }

  /// <summary>
  /// Gets or sets the skill type.
  /// </summary>
  public EnumSkillType SkillType { get; set; }

  /// <summary>
  /// Gets or sets the cooldown in seconds.
  /// </summary>
  public float Cooldown { get; set; }

  /// <summary>
  /// Gets or sets the cast time in seconds.
  /// </summary>
  public float CastTime { get; set; }

  /// <summary>
  /// Gets or sets the resource type.
  /// </summary>
  public EnumResourceType ResourceType { get; set; }

  /// <summary>
  /// Gets or sets the resource cost.
  /// </summary>
  public int ResourceCost { get; set; }
}
