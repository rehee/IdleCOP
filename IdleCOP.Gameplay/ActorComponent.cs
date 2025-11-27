using Idle.Core.Components;
using Idle.Core.Context;
using IdleCOP.Data.Enums;

namespace IdleCOP.Gameplay.Combat;

/// <summary>
/// Component representing an actor in combat.
/// </summary>
public class ActorComponent : IdleComponent
{
  /// <summary>
  /// Gets or sets the actor name.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the actor level.
  /// </summary>
  public int Level { get; set; } = 1;

  /// <summary>
  /// Gets or sets the faction.
  /// </summary>
  public EnumFaction Faction { get; set; }

  /// <summary>
  /// Gets or sets the current health.
  /// </summary>
  public int CurrentHealth { get; set; }

  /// <summary>
  /// Gets or sets the maximum health.
  /// </summary>
  public int MaxHealth { get; set; }

  /// <summary>
  /// Gets or sets the current energy.
  /// </summary>
  public int CurrentEnergy { get; set; }

  /// <summary>
  /// Gets or sets the maximum energy.
  /// </summary>
  public int MaxEnergy { get; set; }

  /// <summary>
  /// Gets or sets the X position.
  /// </summary>
  public float PositionX { get; set; }

  /// <summary>
  /// Gets or sets the Y position.
  /// </summary>
  public float PositionY { get; set; }

  /// <summary>
  /// Gets or sets the rotation angle in degrees.
  /// </summary>
  public float Rotation { get; set; }

  /// <summary>
  /// Gets whether the actor is alive.
  /// </summary>
  public bool IsAlive => CurrentHealth > 0;

  /// <summary>
  /// Initializes a new instance of the ActorComponent class.
  /// </summary>
  public ActorComponent() : base()
  {
  }

  /// <summary>
  /// Initializes a new instance of the ActorComponent class with the specified id.
  /// </summary>
  /// <param name="id">The unique identifier.</param>
  public ActorComponent(Guid id) : base(id)
  {
  }

  /// <summary>
  /// Called each game tick.
  /// </summary>
  /// <param name="context">The tick context.</param>
  public override void OnTick(TickContext context)
  {
    if (!IsAlive) return;

    base.OnTick(context);
  }

  /// <summary>
  /// Takes damage and updates health.
  /// </summary>
  /// <param name="damage">The damage amount.</param>
  /// <returns>The actual damage taken.</returns>
  public int TakeDamage(int damage)
  {
    if (damage < 0) damage = 0;
    var actualDamage = Math.Min(CurrentHealth, damage);
    CurrentHealth -= actualDamage;
    return actualDamage;
  }

  /// <summary>
  /// Heals the actor.
  /// </summary>
  /// <param name="amount">The heal amount.</param>
  /// <returns>The actual amount healed.</returns>
  public int Heal(int amount)
  {
    if (amount < 0) amount = 0;
    var actualHeal = Math.Min(MaxHealth - CurrentHealth, amount);
    CurrentHealth += actualHeal;
    return actualHeal;
  }

  /// <summary>
  /// Consumes energy.
  /// </summary>
  /// <param name="amount">The energy amount.</param>
  /// <returns>True if energy was consumed successfully.</returns>
  public bool ConsumeEnergy(int amount)
  {
    if (CurrentEnergy < amount) return false;
    CurrentEnergy -= amount;
    return true;
  }
}
