namespace IdleCOP.Gameplay.Maps.Profiles;

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
  public static MapProfile? GetProfile(EnumMap type)
  {
    return type switch
    {
      EnumMap.StarterVillage => StarterVillage,
      EnumMap.SkeletonGraveyard => SkeletonGraveyard,
      EnumMap.PvPArena => PvPArena,
      _ => null
    };
  }

  /// <summary>
  /// 根据 Key 获取 Profile
  /// </summary>
  public static MapProfile? GetProfileByKey(int key)
  {
    return GetProfile((EnumMap)key);
  }
}
