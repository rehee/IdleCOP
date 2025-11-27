using IdleCOP.Data.Enums;

namespace IdleCOP.Data.Entities;

/// <summary>
/// Base entity class for all persistent data.
/// </summary>
public abstract class BaseEntity
{
  /// <summary>
  /// Gets or sets the unique identifier.
  /// </summary>
  public Guid Id { get; set; }

  /// <summary>
  /// Gets or sets the creation timestamp.
  /// </summary>
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  /// <summary>
  /// Gets or sets the last update timestamp.
  /// </summary>
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}

/// <summary>
/// Entity representing an actor (character, monster, etc.).
/// </summary>
public class ActorEntity : BaseEntity
{
  /// <summary>
  /// Gets or sets the profile key.
  /// </summary>
  public int ProfileKey { get; set; }

  /// <summary>
  /// Gets or sets the actor type.
  /// </summary>
  public EnumActorType ActorType { get; set; }

  /// <summary>
  /// Gets or sets the name.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the level.
  /// </summary>
  public int Level { get; set; } = 1;

  /// <summary>
  /// Gets or sets the experience points.
  /// </summary>
  public long Experience { get; set; }

  /// <summary>
  /// Gets or sets the faction.
  /// </summary>
  public EnumFaction Faction { get; set; }
}

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

/// <summary>
/// Entity representing equipment.
/// </summary>
public class EquipmentEntity : BaseEntity
{
  /// <summary>
  /// Gets or sets the profile key.
  /// </summary>
  public int ProfileKey { get; set; }

  /// <summary>
  /// Gets or sets the base type.
  /// </summary>
  public string BaseType { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the quality.
  /// </summary>
  public EnumQuality Quality { get; set; }

  /// <summary>
  /// Gets or sets the item level.
  /// </summary>
  public int Level { get; set; }

  /// <summary>
  /// Gets or sets the required character level.
  /// </summary>
  public int RequiredLevel { get; set; }

  /// <summary>
  /// Gets or sets whether the item is identified.
  /// </summary>
  public bool IsIdentified { get; set; }
}

/// <summary>
/// Entity representing an affix on equipment.
/// </summary>
public class AffixEntity : BaseEntity
{
  /// <summary>
  /// Gets or sets the equipment id this affix belongs to.
  /// </summary>
  public Guid EquipmentId { get; set; }

  /// <summary>
  /// Gets or sets the profile key.
  /// </summary>
  public int ProfileKey { get; set; }

  /// <summary>
  /// Gets or sets the affix type.
  /// </summary>
  public EnumAffixType AffixType { get; set; }

  /// <summary>
  /// Gets or sets the tier (T1 is highest).
  /// </summary>
  public int Tier { get; set; }

  /// <summary>
  /// Gets or sets the rolled value.
  /// </summary>
  public float Value { get; set; }
}

/// <summary>
/// Entity representing a strategy configuration.
/// </summary>
public class StrategyEntity : BaseEntity
{
  /// <summary>
  /// Gets or sets the profile key.
  /// </summary>
  public int ProfileKey { get; set; }

  /// <summary>
  /// Gets or sets the priority (higher is more important).
  /// </summary>
  public int Priority { get; set; }

  /// <summary>
  /// Gets or sets whether the strategy is enabled.
  /// </summary>
  public bool IsEnabled { get; set; } = true;

  /// <summary>
  /// Gets or sets the condition JSON.
  /// </summary>
  public string ConditionJson { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the action type.
  /// </summary>
  public string ActionType { get; set; } = string.Empty;
}
