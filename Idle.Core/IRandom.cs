namespace Idle.Utility.Randoms;

/// <summary>
/// Interface for random number generation.
/// </summary>
public interface IRandom
{
  /// <summary>
  /// Returns a non-negative random integer.
  /// </summary>
  /// <returns>A 32-bit signed integer that is greater than or equal to 0.</returns>
  int Next();

  /// <summary>
  /// Returns a non-negative random integer that is less than the specified maximum.
  /// </summary>
  /// <param name="maxValue">The exclusive upper bound.</param>
  /// <returns>A 32-bit signed integer that is greater than or equal to 0 and less than maxValue.</returns>
  int Next(int maxValue);

  /// <summary>
  /// Returns a random integer that is within a specified range.
  /// </summary>
  /// <param name="minValue">The inclusive lower bound.</param>
  /// <param name="maxValue">The exclusive upper bound.</param>
  /// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue.</returns>
  int Next(int minValue, int maxValue);

  /// <summary>
  /// Returns a random floating-point number between 0.0 and 1.0.
  /// </summary>
  /// <returns>A single-precision floating point number.</returns>
  float NextFloat();

  /// <summary>
  /// Returns a random floating-point number between 0.0 and 1.0.
  /// </summary>
  /// <returns>A double-precision floating point number.</returns>
  double NextDouble();
}
