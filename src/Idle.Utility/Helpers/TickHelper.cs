namespace Idle.Utility.Helpers;

/// <summary>
/// Tick 帮助类
/// </summary>
public static class TickHelper
{
  /// <summary>
  /// 默认每秒帧数
  /// </summary>
  public const int DefaultTickRate = 30;

  /// <summary>
  /// 将秒转换为 Tick 数
  /// </summary>
  public static int SecondsToTicks(float seconds, int tickRate = DefaultTickRate)
  {
    return (int)(seconds * tickRate);
  }

  /// <summary>
  /// 将 Tick 数转换为秒
  /// </summary>
  public static float TicksToSeconds(int ticks, int tickRate = DefaultTickRate)
  {
    return (float)ticks / tickRate;
  }
}
