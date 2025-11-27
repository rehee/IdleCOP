using IdleCOP.Gameplay.Actors;

namespace IdleCOP.Gameplay.Maps.Profiles;

/// <summary>
/// 骷髅墓地地图 Profile
/// </summary>
public class SkeletonGraveyardMapProfile : MapProfile<EnumMap>
{
  /// <inheritdoc/>
  public override EnumMap ProfileType => EnumMap.SkeletonGraveyard;

  /// <inheritdoc/>
  public override string? Name => "骷髅墓地";

  /// <inheritdoc/>
  public override string? Description => "充满骷髅怪物的古老墓地";

  /// <inheritdoc/>
  public override EnumMapType MapType => EnumMapType.PvE;

  /// <inheritdoc/>
  public override int MaxBattleSeconds => 180;

  /// <inheritdoc/>
  public override int MaxWaves => 3;

  /// <inheritdoc/>
  public override int RecommendedLevel => 5;

  /// <inheritdoc/>
  public override int MinMonstersPerWave => 2;

  /// <inheritdoc/>
  public override int MaxMonstersPerWave => 5;

  /// <inheritdoc/>
  public override List<EnumMonster> PossibleMonsters => new()
  {
    EnumMonster.SkeletonWarrior,
    EnumMonster.SkeletonArcher,
    EnumMonster.SkeletonMage
  };
}
