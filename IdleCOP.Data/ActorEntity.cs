using IdleCOP.Data.Enums;

namespace IdleCOP.Data.Entities;

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
