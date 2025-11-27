namespace IdleCOP.Data.Enums;

/// <summary>
/// Actor types.
/// </summary>
public enum EnumActorType
{
  /// <summary>
  /// Not specified type.
  /// </summary>
  NotSpecified = 0,

  /// <summary>
  /// Player controlled character.
  /// </summary>
  Player,

  /// <summary>
  /// Non-player character.
  /// </summary>
  NPC,

  /// <summary>
  /// Monster enemy.
  /// </summary>
  Monster,

  /// <summary>
  /// Projectile entity.
  /// </summary>
  Projectile,

  /// <summary>
  /// Environment object.
  /// </summary>
  Environment
}
