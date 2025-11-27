namespace Idle.Utility.Helpers;

/// <summary>
/// Helper class for Guid operations.
/// </summary>
public static class GuidHelper
{
  /// <summary>
  /// Generates a new unique identifier.
  /// </summary>
  /// <returns>A new Guid.</returns>
  public static Guid NewId()
  {
    return Guid.NewGuid();
  }

  /// <summary>
  /// Checks if a Guid is empty.
  /// </summary>
  /// <param name="id">The Guid to check.</param>
  /// <returns>True if the Guid is empty.</returns>
  public static bool IsEmpty(this Guid id)
  {
    return id == Guid.Empty;
  }

  /// <summary>
  /// Checks if a nullable Guid is empty or null.
  /// </summary>
  /// <param name="id">The nullable Guid to check.</param>
  /// <returns>True if the Guid is null or empty.</returns>
  public static bool IsNullOrEmpty(this Guid? id)
  {
    return !id.HasValue || id.Value == Guid.Empty;
  }
}
