using Idle.Core.Components;
using Idle.Core.Context;
using IdleCOP.Data.Enums;

namespace IdleCOP.Gameplay.Equipment;

/// <summary>
/// Component representing an affix on equipment.
/// </summary>
public class AffixComponent : IdleComponent
{
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

  /// <summary>
  /// Initializes a new instance of the AffixComponent class.
  /// </summary>
  public AffixComponent() : base()
  {
  }

  /// <summary>
  /// Initializes a new instance of the AffixComponent class with the specified id.
  /// </summary>
  /// <param name="id">The unique identifier.</param>
  public AffixComponent(Guid id) : base(id)
  {
  }
}
