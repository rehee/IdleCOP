using Idle.Core.Components;
using Idle.Core.Context;

namespace IdleCOP.Gameplay.Instructions;

/// <summary>
/// Component representing an instruction for AI behavior.
/// </summary>
public class InstructionComponent : IdleComponent
{
  /// <summary>
  /// Gets or sets the instruction name.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets whether the instruction is enabled.
  /// </summary>
  public bool IsEnabled { get; set; } = true;

  /// <summary>
  /// Gets or sets the priority (higher = more important).
  /// </summary>
  public int Priority { get; set; }

  /// <summary>
  /// Gets or sets the cooldown in ticks.
  /// </summary>
  public int CooldownTicks { get; set; }

  /// <summary>
  /// Gets or sets the remaining cooldown in ticks.
  /// </summary>
  public int RemainingCooldownTicks { get; set; }

  /// <summary>
  /// Gets or sets whether the instruction can be interrupted.
  /// </summary>
  public bool IsInterruptible { get; set; } = true;

  /// <summary>
  /// Gets whether the instruction is ready to execute.
  /// </summary>
  public bool IsReady => IsEnabled && RemainingCooldownTicks <= 0;

  /// <summary>
  /// Initializes a new instance of the InstructionComponent class.
  /// </summary>
  public InstructionComponent() : base()
  {
  }

  /// <summary>
  /// Initializes a new instance of the InstructionComponent class with the specified id.
  /// </summary>
  /// <param name="id">The unique identifier.</param>
  public InstructionComponent(Guid id) : base(id)
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
  /// Triggers the instruction cooldown.
  /// </summary>
  public void TriggerCooldown()
  {
    RemainingCooldownTicks = CooldownTicks;
  }
}
