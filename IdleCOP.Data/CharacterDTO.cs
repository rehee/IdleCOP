namespace IdleCOP.Data.DTOs;

/// <summary>
/// DTO for character data.
/// </summary>
public class CharacterDTO : BaseDTO
{
  /// <summary>
  /// Gets or sets the name.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the level.
  /// </summary>
  public int Level { get; set; }

  /// <summary>
  /// Gets or sets the character class.
  /// </summary>
  public string Class { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the current health.
  /// </summary>
  public int Health { get; set; }

  /// <summary>
  /// Gets or sets the maximum health.
  /// </summary>
  public int MaxHealth { get; set; }

  /// <summary>
  /// Gets or sets the current energy.
  /// </summary>
  public int Energy { get; set; }

  /// <summary>
  /// Gets or sets the maximum energy.
  /// </summary>
  public int MaxEnergy { get; set; }
}
