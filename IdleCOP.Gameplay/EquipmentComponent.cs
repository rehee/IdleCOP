using Idle.Core.Components;
using Idle.Core.Context;
using IdleCOP.Data.Enums;

namespace IdleCOP.Gameplay.Equipment;

/// <summary>
/// Component representing equipment attached to an actor.
/// </summary>
public class EquipmentComponent : IdleComponent
{
  /// <summary>
  /// Gets or sets the equipment name.
  /// </summary>
  public string Name { get; set; } = string.Empty;

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
  /// Gets or sets whether the equipment is identified.
  /// </summary>
  public bool IsIdentified { get; set; }

  /// <summary>
  /// Initializes a new instance of the EquipmentComponent class.
  /// </summary>
  public EquipmentComponent() : base()
  {
  }

  /// <summary>
  /// Initializes a new instance of the EquipmentComponent class with the specified id.
  /// </summary>
  /// <param name="id">The unique identifier.</param>
  public EquipmentComponent(Guid id) : base(id)
  {
  }
}

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
