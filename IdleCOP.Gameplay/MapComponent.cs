using Idle.Core.Components;
using Idle.Core.Context;

namespace IdleCOP.Gameplay.Maps;

/// <summary>
/// Component representing a game map.
/// </summary>
public class MapComponent : IdleComponent
{
  /// <summary>
  /// Gets or sets the map name.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the map seed for generation.
  /// </summary>
  public string Seed { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the map level.
  /// </summary>
  public int Level { get; set; }

  /// <summary>
  /// Gets or sets the map width.
  /// </summary>
  public int Width { get; set; }

  /// <summary>
  /// Gets or sets the map height.
  /// </summary>
  public int Height { get; set; }

  /// <summary>
  /// Gets or sets the maximum battle time in ticks.
  /// </summary>
  public int MaxBattleTicks { get; set; }

  /// <summary>
  /// Initializes a new instance of the MapComponent class.
  /// </summary>
  public MapComponent() : base()
  {
  }

  /// <summary>
  /// Initializes a new instance of the MapComponent class with the specified id.
  /// </summary>
  /// <param name="id">The unique identifier.</param>
  public MapComponent(Guid id) : base(id)
  {
  }
}

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
