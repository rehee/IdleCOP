using Idle.Core.Components;
using Idle.Core.Context;

namespace IdleCOP.Gameplay.Maps;

/// <summary>
/// Component representing a zone within a map.
/// </summary>
public class ZoneComponent : IdleComponent
{
  /// <summary>
  /// Gets or sets the zone name.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the zone type.
  /// </summary>
  public string ZoneType { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the X position of the zone.
  /// </summary>
  public int X { get; set; }

  /// <summary>
  /// Gets or sets the Y position of the zone.
  /// </summary>
  public int Y { get; set; }

  /// <summary>
  /// Gets or sets the zone width.
  /// </summary>
  public int Width { get; set; }

  /// <summary>
  /// Gets or sets the zone height.
  /// </summary>
  public int Height { get; set; }

  /// <summary>
  /// Gets or sets the connected zone IDs.
  /// </summary>
  public List<Guid> ConnectedZoneIds { get; set; } = new();

  /// <summary>
  /// Initializes a new instance of the ZoneComponent class.
  /// </summary>
  public ZoneComponent() : base()
  {
  }

  /// <summary>
  /// Initializes a new instance of the ZoneComponent class with the specified id.
  /// </summary>
  /// <param name="id">The unique identifier.</param>
  public ZoneComponent(Guid id) : base(id)
  {
  }
}
