using IdleCOP.Data.Enums;

namespace IdleCOP.Data.Entities;

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
