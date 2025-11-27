namespace IdleCOP.Gameplay.Actors.Monsters;

/// <summary>
/// 怪物 Profile 单例集合
/// </summary>
public static class MonsterProfiles
{
  /// <summary>
  /// 骷髅战士
  /// </summary>
  public static readonly SkeletonWarriorProfile SkeletonWarrior = new();

  /// <summary>
  /// 骷髅弓手
  /// </summary>
  public static readonly SkeletonArcherProfile SkeletonArcher = new();

  /// <summary>
  /// 骷髅法师
  /// </summary>
  public static readonly SkeletonMageProfile SkeletonMage = new();

  /// <summary>
  /// 哥布林
  /// </summary>
  public static readonly GoblinProfile Goblin = new();

  /// <summary>
  /// 哥布林首领
  /// </summary>
  public static readonly GoblinChiefProfile GoblinChief = new();

  /// <summary>
  /// 根据枚举获取 Profile
  /// </summary>
  public static MonsterProfile? GetProfile(EnumMonster type)
  {
    return type switch
    {
      EnumMonster.SkeletonWarrior => SkeletonWarrior,
      EnumMonster.SkeletonArcher => SkeletonArcher,
      EnumMonster.SkeletonMage => SkeletonMage,
      EnumMonster.Goblin => Goblin,
      EnumMonster.GoblinChief => GoblinChief,
      _ => null
    };
  }

  /// <summary>
  /// 根据 Key 获取 Profile
  /// </summary>
  public static MonsterProfile? GetProfileByKey(int key)
  {
    return GetProfile((EnumMonster)key);
  }
}
