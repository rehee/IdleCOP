using IdleCOP.Data.Enums;

namespace IdleCOP.Data.DTOs;

/// <summary>
/// DTO for skill data.
/// </summary>
public class SkillDTO : BaseDTO
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
  /// Gets or sets the name.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the description.
  /// </summary>
  public string Description { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the cooldown in seconds.
  /// </summary>
  public float Cooldown { get; set; }

  /// <summary>
  /// Gets or sets the cast time in seconds.
  /// </summary>
  public float CastTime { get; set; }

  /// <summary>
  /// Gets or sets the resource cost.
  /// </summary>
  public int ResourceCost { get; set; }
}
