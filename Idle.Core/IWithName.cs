namespace Idle.Utility.Commons;

/// <summary>
/// Interface for objects that have a name and description.
/// </summary>
public interface IWithName
{
  /// <summary>
  /// Gets the name of the object.
  /// </summary>
  string? Name { get; }

  /// <summary>
  /// Gets the description of the object.
  /// </summary>
  string? Description { get; }
}
