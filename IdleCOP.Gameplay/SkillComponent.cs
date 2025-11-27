using Idle.Core.Components;
using Idle.Core.Context;
using IdleCOP.Data.Enums;

namespace IdleCOP.Gameplay.Skills;

/// <summary>
/// Component representing a skill attached to an actor.
/// </summary>
public class SkillComponent : IdleComponent
{
  /// <summary>
  /// Gets or sets the skill type.
  /// </summary>
  public EnumSkillType SkillType { get; set; }

  /// <summary>
  /// Gets or sets the cooldown in ticks.
  /// </summary>
  public int CooldownTicks { get; set; }

  /// <summary>
  /// Gets or sets the remaining cooldown in ticks.
  /// </summary>
  public int RemainingCooldownTicks { get; set; }

  /// <summary>
  /// Gets or sets the cast time in ticks.
  /// </summary>
  public int CastTimeTicks { get; set; }

  /// <summary>
  /// Gets or sets the resource type.
  /// </summary>
  public EnumResourceType ResourceType { get; set; }

  /// <summary>
  /// Gets or sets the resource cost.
  /// </summary>
  public int ResourceCost { get; set; }

  /// <summary>
  /// Gets whether the skill is ready to use.
  /// </summary>
  public bool IsReady => RemainingCooldownTicks <= 0;

  /// <summary>
  /// Initializes a new instance of the SkillComponent class.
  /// </summary>
  public SkillComponent() : base()
  {
  }

  /// <summary>
  /// Initializes a new instance of the SkillComponent class with the specified id.
  /// </summary>
  /// <param name="id">The unique identifier.</param>
  public SkillComponent(Guid id) : base(id)
  {
  }

  /// <summary>
  /// Called each game tick.
  /// </summary>
  /// <param name="context">The tick context.</param>
  public override void OnTick(TickContext context)
  {
    if (RemainingCooldownTicks > 0)
    {
      RemainingCooldownTicks--;
    }

    base.OnTick(context);
  }

  /// <summary>
  /// Triggers the skill cooldown.
  /// </summary>
  public void TriggerCooldown()
  {
    RemainingCooldownTicks = CooldownTicks;
  }
}
