using IdleCOP.Gameplay.Actors;

namespace IdleCOP.Gameplay.Maps.Profiles;

/// <summary>
/// 新手村地图 Profile
/// </summary>
public class StarterVillageMapProfile : MapProfile<EnumMap>
{
  /// <inheritdoc/>
  public override EnumMap ProfileType => EnumMap.StarterVillage;

  /// <inheritdoc/>
  public override string? Name => "新手村";

  /// <inheritdoc/>
  public override string? Description => "适合新手的起始区域，怪物较弱";

  /// <inheritdoc/>
  public override EnumMapType MapType => EnumMapType.PvE;

  /// <inheritdoc/>
  public override int MaxBattleSeconds => 120;

  /// <inheritdoc/>
  public override int MaxWaves => 2;

  /// <inheritdoc/>
  public override int RecommendedLevel => 1;

  /// <inheritdoc/>
  public override int MinMonstersPerWave => 1;

  /// <inheritdoc/>
  public override int MaxMonstersPerWave => 3;

  /// <inheritdoc/>
  public override List<EnumMonster> PossibleMonsters => new()
  {
    EnumMonster.Goblin
  };
}
