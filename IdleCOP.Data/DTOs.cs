using IdleCOP.Data.Enums;

namespace IdleCOP.Data.DTOs;

/// <summary>
/// Base DTO class for data transfer.
/// </summary>
public abstract class BaseDTO
{
  /// <summary>
  /// Gets or sets the unique identifier.
  /// </summary>
  public Guid Id { get; set; }
}

/// <summary>
/// DTO for character data.
/// </summary>
public class CharacterDTO : BaseDTO
{
  /// <summary>
  /// Gets or sets the name.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the level.
  /// </summary>
  public int Level { get; set; }

  /// <summary>
  /// Gets or sets the character class.
  /// </summary>
  public string Class { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the current health.
  /// </summary>
  public int Health { get; set; }

  /// <summary>
  /// Gets or sets the maximum health.
  /// </summary>
  public int MaxHealth { get; set; }

  /// <summary>
  /// Gets or sets the current energy.
  /// </summary>
  public int Energy { get; set; }

  /// <summary>
  /// Gets or sets the maximum energy.
  /// </summary>
  public int MaxEnergy { get; set; }
}

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

/// <summary>
/// DTO for equipment data.
/// </summary>
public class EquipmentDTO : BaseDTO
{
  /// <summary>
  /// Gets or sets the base type.
  /// </summary>
  public string BaseType { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the name.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the quality.
  /// </summary>
  public EnumQuality Quality { get; set; }

  /// <summary>
  /// Gets or sets the item level.
  /// </summary>
  public int Level { get; set; }

  /// <summary>
  /// Gets or sets the required level.
  /// </summary>
  public int RequiredLevel { get; set; }

  /// <summary>
  /// Gets or sets the affixes.
  /// </summary>
  public List<AffixDTO> Affixes { get; set; } = new();
}

/// <summary>
/// DTO for affix data.
/// </summary>
public class AffixDTO : BaseDTO
{
  /// <summary>
  /// Gets or sets the affix type.
  /// </summary>
  public EnumAffixType AffixType { get; set; }

  /// <summary>
  /// Gets or sets the tier.
  /// </summary>
  public int Tier { get; set; }

  /// <summary>
  /// Gets or sets the name.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the description.
  /// </summary>
  public string Description { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the value.
  /// </summary>
  public float Value { get; set; }
}

/// <summary>
/// DTO for battle seed data.
/// </summary>
public class BattleSeedDTO : BaseDTO
{
  /// <summary>
  /// Gets or sets the battle GUID (used for random seed).
  /// </summary>
  public Guid BattleGuid { get; set; }

  /// <summary>
  /// Gets or sets the map ID.
  /// </summary>
  public string MapId { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the game version.
  /// </summary>
  public string Version { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the creator faction character IDs.
  /// </summary>
  public List<Guid> CreatorCharacterIds { get; set; } = new();

  /// <summary>
  /// Gets or sets the enemy faction character IDs.
  /// </summary>
  public List<Guid> EnemyCharacterIds { get; set; } = new();

  /// <summary>
  /// Gets or sets the character data.
  /// </summary>
  public List<CharacterDTO> Characters { get; set; } = new();
}
