namespace IdleCOP.Data.Enums;

/// <summary>
/// Skill types.
/// </summary>
public enum EnumSkillType
{
  /// <summary>
  /// Not specified type.
  /// </summary>
  NotSpecified = 0,

  /// <summary>
  /// Active skill - player/AI controlled.
  /// </summary>
  Active,

  /// <summary>
  /// Support skill - modifies other skills.
  /// </summary>
  Support,

  /// <summary>
  /// Trigger skill - activates on conditions.
  /// </summary>
  Trigger
}
