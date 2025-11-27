using IdleCOP.Data.Enums;

namespace IdleCOP.Data.Entities;

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
