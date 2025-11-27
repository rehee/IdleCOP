namespace IdleCOP.Data.Enums;

/// <summary>
/// Quality/rarity levels for items.
/// </summary>
public enum EnumQuality
{
  /// <summary>
  /// Not specified quality.
  /// </summary>
  NotSpecified = 0,

  /// <summary>
  /// Normal (white) quality.
  /// </summary>
  Normal,

  /// <summary>
  /// Magic (blue) quality.
  /// </summary>
  Magic,

  /// <summary>
  /// Rare (purple) quality.
  /// </summary>
  Rare,

  /// <summary>
  /// Legendary (orange) quality.
  /// </summary>
  Legendary,

  /// <summary>
  /// Unique (red) quality.
  /// </summary>
  Unique
}

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

/// <summary>
/// Affix types for equipment.
/// </summary>
public enum EnumAffixType
{
  /// <summary>
  /// Not specified type.
  /// </summary>
  NotSpecified = 0,

  /// <summary>
  /// Prefix affix.
  /// </summary>
  Prefix,

  /// <summary>
  /// Suffix affix.
  /// </summary>
  Suffix,

  /// <summary>
  /// Base affix.
  /// </summary>
  Base,

  /// <summary>
  /// Implicit affix.
  /// </summary>
  Implicit,

  /// <summary>
  /// Legendary affix.
  /// </summary>
  Legendary,

  /// <summary>
  /// Corrupted affix.
  /// </summary>
  Corrupted,

  /// <summary>
  /// Extra affix.
  /// </summary>
  Extra
}

/// <summary>
/// Resource types for skills.
/// </summary>
public enum EnumResourceType
{
  /// <summary>
  /// Not specified type.
  /// </summary>
  NotSpecified = 0,

  /// <summary>
  /// Arcane - for mages.
  /// </summary>
  Arcane,

  /// <summary>
  /// Energy - for adventurers.
  /// </summary>
  Energy,

  /// <summary>
  /// Chi - for martial artists.
  /// </summary>
  Chi,

  /// <summary>
  /// Rage - for berserkers.
  /// </summary>
  Rage,

  /// <summary>
  /// Spirit - for summoners.
  /// </summary>
  Spirit,

  /// <summary>
  /// Focus - for assassins.
  /// </summary>
  Focus
}

/// <summary>
/// Duration effect types.
/// </summary>
public enum EnumDurationType
{
  /// <summary>
  /// Not specified type.
  /// </summary>
  NotSpecified = 0,

  /// <summary>
  /// Stun effect.
  /// </summary>
  Stun,

  /// <summary>
  /// Poison effect.
  /// </summary>
  Poison,

  /// <summary>
  /// Burn effect.
  /// </summary>
  Burn,

  /// <summary>
  /// Slow effect.
  /// </summary>
  Slow,

  /// <summary>
  /// Buff effect.
  /// </summary>
  Buff
}

/// <summary>
/// Faction types for battle.
/// </summary>
public enum EnumFaction
{
  /// <summary>
  /// Not specified faction.
  /// </summary>
  NotSpecified = 0,

  /// <summary>
  /// Creator faction (player and summons).
  /// </summary>
  Creator,

  /// <summary>
  /// Enemy faction (monsters and their summons).
  /// </summary>
  Enemy
}

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
