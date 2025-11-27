namespace Idle.Utility.Helpers;

/// <summary>
/// Helper class for mathematical operations.
/// </summary>
public static class MathHelper
{
  /// <summary>
  /// Clamps a value between a minimum and maximum.
  /// </summary>
  /// <param name="value">The value to clamp.</param>
  /// <param name="min">The minimum value.</param>
  /// <param name="max">The maximum value.</param>
  /// <returns>The clamped value.</returns>
  public static int Clamp(int value, int min, int max)
  {
    if (value < min) return min;
    if (value > max) return max;
    return value;
  }

  /// <summary>
  /// Clamps a value between a minimum and maximum.
  /// </summary>
  /// <param name="value">The value to clamp.</param>
  /// <param name="min">The minimum value.</param>
  /// <param name="max">The maximum value.</param>
  /// <returns>The clamped value.</returns>
  public static float Clamp(float value, float min, float max)
  {
    if (value < min) return min;
    if (value > max) return max;
    return value;
  }

  /// <summary>
  /// Linearly interpolates between two values.
  /// </summary>
  /// <param name="a">Start value.</param>
  /// <param name="b">End value.</param>
  /// <param name="t">Interpolation factor (0-1).</param>
  /// <returns>Interpolated value.</returns>
  public static float Lerp(float a, float b, float t)
  {
    t = Clamp(t, 0f, 1f);
    return a + (b - a) * t;
  }

  /// <summary>
  /// Calculates the percentage of a value relative to a maximum.
  /// </summary>
  /// <param name="current">Current value.</param>
  /// <param name="max">Maximum value.</param>
  /// <returns>Percentage (0-100).</returns>
  public static float Percentage(float current, float max)
  {
    if (max <= 0) return 0;
    return Clamp(current / max * 100f, 0f, 100f);
  }
}
