using IdleCOP.Data.Enums;

namespace IdleCOP.Data.DTOs;

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
