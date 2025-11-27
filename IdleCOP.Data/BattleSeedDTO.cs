namespace IdleCOP.Data.DTOs;

/// <summary>
/// DTO for battle seed data.
/// </summary>
public class BattleSeedDTO : BaseDTO
{
  /// <summary>
  /// Gets or sets the battle GUID (used for random seed).
  /// </summary>
  public Guid BattleGuid { get; set; }

  /// <summary>
  /// Gets or sets the map ID.
  /// </summary>
  public string MapId { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the game version.
  /// </summary>
  public string Version { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the creator faction character IDs.
  /// </summary>
  public List<Guid> CreatorCharacterIds { get; set; } = new();

  /// <summary>
  /// Gets or sets the enemy faction character IDs.
  /// </summary>
  public List<Guid> EnemyCharacterIds { get; set; } = new();

  /// <summary>
  /// Gets or sets the character data.
  /// </summary>
  public List<CharacterDTO> Characters { get; set; } = new();
}
