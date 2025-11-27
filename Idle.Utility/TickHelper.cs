namespace Idle.Utility.Helpers;

/// <summary>
/// Helper class for tick-related calculations.
/// </summary>
public static class TickHelper
{
  /// <summary>
  /// Default tick rate (30 ticks per second).
  /// </summary>
  public const int DefaultTickRate = 30;

  /// <summary>
  /// Converts seconds to ticks.
  /// </summary>
  /// <param name="seconds">Time in seconds.</param>
  /// <param name="tickRate">Tick rate per second.</param>
  /// <returns>Number of ticks.</returns>
  public static int SecondsToTicks(float seconds, int tickRate = DefaultTickRate)
  {
    return (int)(seconds * tickRate);
  }

  /// <summary>
  /// Converts ticks to seconds.
  /// </summary>
  /// <param name="ticks">Number of ticks.</param>
  /// <param name="tickRate">Tick rate per second.</param>
  /// <returns>Time in seconds.</returns>
  public static float TicksToSeconds(int ticks, int tickRate = DefaultTickRate)
  {
    return (float)ticks / tickRate;
  }

  /// <summary>
  /// Converts milliseconds to ticks.
  /// </summary>
  /// <param name="milliseconds">Time in milliseconds.</param>
  /// <param name="tickRate">Tick rate per second.</param>
  /// <returns>Number of ticks.</returns>
  public static int MillisecondsToTicks(int milliseconds, int tickRate = DefaultTickRate)
  {
    return (int)(milliseconds / 1000f * tickRate);
  }

  /// <summary>
  /// Converts ticks to milliseconds.
  /// </summary>
  /// <param name="ticks">Number of ticks.</param>
  /// <param name="tickRate">Tick rate per second.</param>
  /// <returns>Time in milliseconds.</returns>
  public static int TicksToMilliseconds(int ticks, int tickRate = DefaultTickRate)
  {
    return (int)(ticks * 1000f / tickRate);
  }
}
