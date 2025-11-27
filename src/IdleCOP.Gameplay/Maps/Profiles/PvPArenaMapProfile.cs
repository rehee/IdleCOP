using IdleCOP.Gameplay.Actors;

namespace IdleCOP.Gameplay.Maps.Profiles;

/// <summary>
/// PvP 竞技场地图 Profile
/// </summary>
public class PvPArenaMapProfile : MapProfile<EnumMap>
{
  /// <inheritdoc/>
  public override EnumMap ProfileType => EnumMap.PvPArena;

  /// <inheritdoc/>
  public override string? Name => "PvP 竞技场";

  /// <inheritdoc/>
  public override string? Description => "玩家对战的竞技场";

  /// <inheritdoc/>
  public override EnumMapType MapType => EnumMapType.PvP;

  /// <inheritdoc/>
  public override int MaxBattleSeconds => 300;

  /// <inheritdoc/>
  public override int MaxWaves => 1;

  /// <inheritdoc/>
  public override List<EnumMonster> PossibleMonsters => new();

  /// <inheritdoc/>
  public override List<WaveConfig> GenerateWaves(int difficulty, Idle.Utility.IRandom random)
  {
    // PvP 地图不生成怪物波次
    return new List<WaveConfig>();
  }
}
