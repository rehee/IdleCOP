using Idle.Core.Components;
using Idle.Core.Context;
using Idle.Utility.Commons;

namespace Idle.Core.Profiles;

/// <summary>
/// Base class for all game profiles (singleton logic handlers).
/// </summary>
public abstract class IdleProfile : IWithName
{
  /// <summary>
  /// Gets the unique key for this profile.
  /// </summary>
  public abstract int Key { get; }

  /// <summary>
  /// Gets the override key for this profile. Used when Key is 0.
  /// </summary>
  public virtual int? KeyOverride { get; }

  /// <summary>
  /// Gets the name of this profile.
  /// </summary>
  public abstract string? Name { get; }

  /// <summary>
  /// Gets the description of this profile.
  /// </summary>
  public abstract string? Description { get; }

  /// <summary>
  /// Gets the effective key, using KeyOverride when Key is 0.
  /// </summary>
  public int EffectiveKey => Key != 0 ? Key : (KeyOverride ?? 0);

  /// <summary>
  /// Called each game tick to process a component.
  /// </summary>
  /// <param name="component">The component to process.</param>
  /// <param name="context">The tick context.</param>
  public abstract void OnTick(IdleComponent component, TickContext context);
}
