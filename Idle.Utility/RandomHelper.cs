namespace Idle.Utility.Helpers;

/// <summary>
/// Helper class for random number generation operations.
/// </summary>
public static class RandomHelper
{
  /// <summary>
  /// Generates a random float between min and max (inclusive).
  /// </summary>
  /// <param name="random">Random instance.</param>
  /// <param name="min">Minimum value.</param>
  /// <param name="max">Maximum value.</param>
  /// <returns>Random float value.</returns>
  public static float NextFloat(this Random random, float min, float max)
  {
    return (float)(random.NextDouble() * (max - min) + min);
  }

  /// <summary>
  /// Returns true with the specified percentage chance.
  /// </summary>
  /// <param name="random">Random instance.</param>
  /// <param name="percentChance">Chance percentage (0-100).</param>
  /// <returns>True if roll succeeded.</returns>
  public static bool RollChance(this Random random, float percentChance)
  {
    return random.NextDouble() * 100 < percentChance;
  }

  /// <summary>
  /// Picks a random element from a list.
  /// </summary>
  /// <typeparam name="T">Element type.</typeparam>
  /// <param name="random">Random instance.</param>
  /// <param name="list">List to pick from.</param>
  /// <returns>Random element from the list.</returns>
  public static T PickRandom<T>(this Random random, IList<T> list)
  {
    if (list == null || list.Count == 0)
    {
      throw new ArgumentException("List cannot be null or empty.", nameof(list));
    }
    return list[random.Next(list.Count)];
  }

  /// <summary>
  /// Shuffles a list in place using Fisher-Yates algorithm.
  /// </summary>
  /// <typeparam name="T">Element type.</typeparam>
  /// <param name="random">Random instance.</param>
  /// <param name="list">List to shuffle.</param>
  public static void Shuffle<T>(this Random random, IList<T> list)
  {
    int n = list.Count;
    for (int i = n - 1; i > 0; i--)
    {
      int j = random.Next(i + 1);
      (list[i], list[j]) = (list[j], list[i]);
    }
  }
}
