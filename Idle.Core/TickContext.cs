using Idle.Core.Components;
using Idle.Core.Enums;
using Idle.Utility.Commons;
using Idle.Utility.Randoms;

namespace Idle.Core.Context;

/// <summary>
/// Represents the context for a battle/game tick.
/// </summary>
public class TickContext : IDisposable
{
  /// <summary>
  /// Gets or sets the current tick number.
  /// </summary>
  public int CurrentTick { get; set; }

  /// <summary>
  /// Gets or sets the maximum tick number for this battle.
  /// </summary>
  public int MaxTick { get; set; }

  /// <summary>
  /// Gets or sets whether the battle is over.
  /// </summary>
  public bool IsBattleOver { get; set; }

  /// <summary>
  /// Gets or sets the battle result.
  /// </summary>
  public EnumBattleResult Result { get; set; } = EnumBattleResult.NotSpecified;

  /// <summary>
  /// Gets or sets the battle random number generator.
  /// </summary>
  public IRandom? BattleRandom { get; set; }

  /// <summary>
  /// Gets or sets the item random number generator (independent for replay).
  /// </summary>
  public IRandom? ItemRandom { get; set; }

  /// <summary>
  /// Gets or sets the map component.
  /// </summary>
  public IdleComponent? Map { get; set; }

  /// <summary>
  /// Gets the creator faction actors.
  /// </summary>
  public List<IdleComponent> CreatorFaction { get; } = new();

  /// <summary>
  /// Gets the enemy faction actors.
  /// </summary>
  public List<IdleComponent> EnemyFaction { get; } = new();

  /// <summary>
  /// Gets the projectiles in the battle.
  /// </summary>
  public List<IdleComponent> Projectiles { get; } = new();

  /// <summary>
  /// Gets or sets the tick rate (ticks per second).
  /// </summary>
  public int TickRate { get; set; } = 30;

  /// <summary>
  /// Disposes of the context resources.
  /// </summary>
  public void Dispose()
  {
    CreatorFaction.Clear();
    EnemyFaction.Clear();
    Projectiles.Clear();
    Map = null;
    BattleRandom = null;
    ItemRandom = null;
    GC.SuppressFinalize(this);
  }
}
