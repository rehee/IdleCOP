using Idle.Utility.Profiles;
using IdleCOP.Gameplay.Actors;

namespace IdleCOP.Gameplay.Maps;

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
  public Dictionary<EnumMonster, int> Monsters { get; set; } = new();
}

/// <summary>
/// 地图 Profile 基类 - 定义地图的配置和逻辑
/// </summary>
public abstract class MapProfile : IdleProfile
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
  public abstract List<EnumMonster> PossibleMonsters { get; }

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
/// 泛型地图 Profile 基类 - 使用枚举类型约束
/// </summary>
/// <typeparam name="TEnum">枚举类型</typeparam>
public abstract class MapProfile<TEnum> : MapProfile where TEnum : struct, Enum
{
  /// <summary>
  /// Profile 对应的枚举值
  /// </summary>
  public abstract TEnum ProfileType { get; }

  /// <inheritdoc/>
  public override int Key => Convert.ToInt32(ProfileType);
}
