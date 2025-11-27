using IdleCOP.Data.Enums;

namespace IdleCOP.Data.DTOs;

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
