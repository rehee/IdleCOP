using IdleCOP.Gameplay.Actors;

namespace IdleCOP.Gameplay.Maps;

/// <summary>
/// 地图 Profile Key 枚举
/// </summary>
public enum EnumMapProfile
{
  /// <summary>
  /// 未指定
  /// </summary>
  NotSpecified = 0,

  /// <summary>
  /// 新手村
  /// </summary>
  StarterVillage = 1,

  /// <summary>
  /// 骷髅墓地
  /// </summary>
  SkeletonGraveyard = 2,

  /// <summary>
  /// 哥布林洞穴
  /// </summary>
  GoblinCave = 3,

  /// <summary>
  /// PvP 竞技场
  /// </summary>
  PvPArena = 100
}

/// <summary>
/// 波次配置 - 定义每个波次的怪物
/// </summary>
public class WaveConfig
{
  /// <summary>
  /// 波次编号
  /// </summary>
  public int WaveNumber { get; set; }

  /// <summary>
  /// 怪物配置（怪物类型 -> 数量）
  /// </summary>
  public Dictionary<EnumMonsterProfile, int> Monsters { get; set; } = new();
}

/// <summary>
/// 地图 Profile - 定义地图的配置和逻辑
/// </summary>
public abstract class MapProfile : Idle.Utility.Profiles.IdleProfile
{
  /// <summary>
  /// 地图类型
  /// </summary>
  public abstract EnumMapType MapType { get; }

  /// <summary>
  /// 最大战斗秒数
  /// </summary>
  public abstract int MaxBattleSeconds { get; }

  /// <summary>
  /// 最大波次数量（最小为1）
  /// </summary>
  public abstract int MaxWaves { get; }

  /// <summary>
  /// 推荐等级
  /// </summary>
  public virtual int RecommendedLevel => 1;

  /// <summary>
  /// 每波最小怪物数量
  /// </summary>
  public virtual int MinMonstersPerWave => 1;

  /// <summary>
  /// 每波最大怪物数量
  /// </summary>
  public virtual int MaxMonstersPerWave => 5;

  /// <summary>
  /// 可能出现的怪物类型
  /// </summary>
  public abstract List<EnumMonsterProfile> PossibleMonsters { get; }

  /// <summary>
  /// 获取波次配置
  /// </summary>
  public virtual List<WaveConfig> GenerateWaves(int difficulty, Idle.Utility.IRandom random)
  {
    var waves = new List<WaveConfig>();
    for (int i = 1; i <= MaxWaves; i++)
    {
      var wave = new WaveConfig { WaveNumber = i };

      // 根据难度和波次调整怪物数量
      var monsterCount = random.Next(MinMonstersPerWave, MaxMonstersPerWave + 1);
      monsterCount = Math.Max(1, monsterCount + difficulty / 2 + (i - 1));

      // 随机选择怪物类型
      for (int j = 0; j < monsterCount; j++)
      {
        var monsterIndex = random.Next(PossibleMonsters.Count);
        var monsterType = PossibleMonsters[monsterIndex];

        if (wave.Monsters.ContainsKey(monsterType))
          wave.Monsters[monsterType]++;
        else
          wave.Monsters[monsterType] = 1;
      }

      waves.Add(wave);
    }

    return waves;
  }
}

/// <summary>
/// 新手村地图 Profile
/// </summary>
public class StarterVillageMapProfile : MapProfile
{
  /// <inheritdoc/>
  public override int Key => (int)EnumMapProfile.StarterVillage;

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
  public override List<EnumMonsterProfile> PossibleMonsters => new()
  {
    EnumMonsterProfile.Goblin
  };
}

/// <summary>
/// 骷髅墓地地图 Profile
/// </summary>
public class SkeletonGraveyardMapProfile : MapProfile
{
  /// <inheritdoc/>
  public override int Key => (int)EnumMapProfile.SkeletonGraveyard;

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
  public override List<EnumMonsterProfile> PossibleMonsters => new()
  {
    EnumMonsterProfile.SkeletonWarrior,
    EnumMonsterProfile.SkeletonArcher,
    EnumMonsterProfile.SkeletonMage
  };
}

/// <summary>
/// PvP 竞技场地图 Profile
/// </summary>
public class PvPArenaMapProfile : MapProfile
{
  /// <inheritdoc/>
  public override int Key => (int)EnumMapProfile.PvPArena;

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
  public override List<EnumMonsterProfile> PossibleMonsters => new();

  /// <inheritdoc/>
  public override List<WaveConfig> GenerateWaves(int difficulty, Idle.Utility.IRandom random)
  {
    // PvP 地图不生成怪物波次
    return new List<WaveConfig>();
  }
}

/// <summary>
/// 地图 Profile 单例集合
/// </summary>
public static class MapProfiles
{
  /// <summary>
  /// 新手村
  /// </summary>
  public static readonly StarterVillageMapProfile StarterVillage = new();

  /// <summary>
  /// 骷髅墓地
  /// </summary>
  public static readonly SkeletonGraveyardMapProfile SkeletonGraveyard = new();

  /// <summary>
  /// PvP 竞技场
  /// </summary>
  public static readonly PvPArenaMapProfile PvPArena = new();

  /// <summary>
  /// 根据枚举获取 Profile
  /// </summary>
  public static MapProfile? GetProfile(EnumMapProfile type)
  {
    return type switch
    {
      EnumMapProfile.StarterVillage => StarterVillage,
      EnumMapProfile.SkeletonGraveyard => SkeletonGraveyard,
      EnumMapProfile.PvPArena => PvPArena,
      _ => null
    };
  }

  /// <summary>
  /// 根据 Key 获取 Profile
  /// </summary>
  public static MapProfile? GetProfileByKey(int key)
  {
    return GetProfile((EnumMapProfile)key);
  }
}
