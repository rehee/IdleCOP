namespace Idle.Core.Context;

/// <summary>
/// Battle result enumeration.
/// </summary>
public enum EnumBattleResult
{
  /// <summary>
  /// Not specified result.
  /// </summary>
  NotSpecified = 0,

  /// <summary>
  /// Creator faction won.
  /// </summary>
  Victory,

  /// <summary>
  /// Enemy faction won.
  /// </summary>
  Defeat,

  /// <summary>
  /// Battle timed out.
  /// </summary>
  Timeout,

  /// <summary>
  /// Both factions eliminated.
  /// </summary>
  Draw,

  /// <summary>
  /// Battle ended due to error.
  /// </summary>
  Error,

  /// <summary>
  /// Player exited the battle.
  /// </summary>
  PlayerExit
}
