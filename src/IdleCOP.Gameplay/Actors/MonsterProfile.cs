using Idle.Core.DTOs;

namespace IdleCOP.Gameplay.Actors;

/// <summary>
/// 怪物 Profile Key 枚举
/// </summary>
public enum EnumMonsterProfile
{
  /// <summary>
  /// 未指定
  /// </summary>
  NotSpecified = 0,

  /// <summary>
  /// 骷髅战士
  /// </summary>
  SkeletonWarrior = 100,

  /// <summary>
  /// 骷髅弓手
  /// </summary>
  SkeletonArcher = 101,

  /// <summary>
  /// 骷髅法师
  /// </summary>
  SkeletonMage = 102,

  /// <summary>
  /// 哥布林
  /// </summary>
  Goblin = 200,

  /// <summary>
  /// 哥布林首领
  /// </summary>
  GoblinChief = 201
}

/// <summary>
/// 怪物 Profile - 定义怪物的行为逻辑
/// </summary>
public class MonsterProfile : ActorProfile
{
  private readonly EnumMonsterProfile profileType;

  /// <summary>
  /// 构造函数
  /// </summary>
  public MonsterProfile(EnumMonsterProfile profileType)
  {
    this.profileType = profileType;
  }

  /// <inheritdoc/>
  public override int Key => (int)profileType;

  /// <inheritdoc/>
  public override string? Name => profileType switch
  {
    EnumMonsterProfile.SkeletonWarrior => "骷髅战士",
    EnumMonsterProfile.SkeletonArcher => "骷髅弓手",
    EnumMonsterProfile.SkeletonMage => "骷髅法师",
    EnumMonsterProfile.Goblin => "哥布林",
    EnumMonsterProfile.GoblinChief => "哥布林首领",
    _ => "未知怪物"
  };

  /// <inheritdoc/>
  public override string? Description => profileType switch
  {
    EnumMonsterProfile.SkeletonWarrior => "普通的骷髅战士，使用近战攻击",
    EnumMonsterProfile.SkeletonArcher => "远程攻击的骷髅弓手",
    EnumMonsterProfile.SkeletonMage => "使用魔法的骷髅法师",
    EnumMonsterProfile.Goblin => "弱小但数量众多的哥布林",
    EnumMonsterProfile.GoblinChief => "哥布林部落的首领",
    _ => null
  };

  /// <inheritdoc/>
  public override EnumActorType ActorType => EnumActorType.Monster;

  /// <inheritdoc/>
  public override int BaseMaxHealth => profileType switch
  {
    EnumMonsterProfile.SkeletonWarrior => 80,
    EnumMonsterProfile.SkeletonArcher => 50,
    EnumMonsterProfile.SkeletonMage => 40,
    EnumMonsterProfile.Goblin => 30,
    EnumMonsterProfile.GoblinChief => 150,
    _ => 50
  };

  /// <inheritdoc/>
  public override int BaseMaxEnergy => 30;

  /// <inheritdoc/>
  public override int BasePhysicalDamageMin => profileType switch
  {
    EnumMonsterProfile.SkeletonWarrior => 6,
    EnumMonsterProfile.SkeletonArcher => 8,
    EnumMonsterProfile.SkeletonMage => 10,
    EnumMonsterProfile.Goblin => 3,
    EnumMonsterProfile.GoblinChief => 12,
    _ => 5
  };

  /// <inheritdoc/>
  public override int BasePhysicalDamageMax => profileType switch
  {
    EnumMonsterProfile.SkeletonWarrior => 12,
    EnumMonsterProfile.SkeletonArcher => 14,
    EnumMonsterProfile.SkeletonMage => 18,
    EnumMonsterProfile.Goblin => 6,
    EnumMonsterProfile.GoblinChief => 20,
    _ => 10
  };

  /// <inheritdoc/>
  public override float BaseAttackSpeed => profileType switch
  {
    EnumMonsterProfile.SkeletonWarrior => 0.8f,
    EnumMonsterProfile.SkeletonArcher => 1.0f,
    EnumMonsterProfile.SkeletonMage => 0.6f,
    EnumMonsterProfile.Goblin => 1.5f,
    EnumMonsterProfile.GoblinChief => 0.7f,
    _ => 1.0f
  };
}

/// <summary>
/// 怪物 Profile 单例集合
/// </summary>
public static class MonsterProfiles
{
  /// <summary>
  /// 骷髅战士
  /// </summary>
  public static readonly MonsterProfile SkeletonWarrior = new(EnumMonsterProfile.SkeletonWarrior);

  /// <summary>
  /// 骷髅弓手
  /// </summary>
  public static readonly MonsterProfile SkeletonArcher = new(EnumMonsterProfile.SkeletonArcher);

  /// <summary>
  /// 骷髅法师
  /// </summary>
  public static readonly MonsterProfile SkeletonMage = new(EnumMonsterProfile.SkeletonMage);

  /// <summary>
  /// 哥布林
  /// </summary>
  public static readonly MonsterProfile Goblin = new(EnumMonsterProfile.Goblin);

  /// <summary>
  /// 哥布林首领
  /// </summary>
  public static readonly MonsterProfile GoblinChief = new(EnumMonsterProfile.GoblinChief);

  /// <summary>
  /// 根据枚举获取 Profile
  /// </summary>
  public static MonsterProfile? GetProfile(EnumMonsterProfile type)
  {
    return type switch
    {
      EnumMonsterProfile.SkeletonWarrior => SkeletonWarrior,
      EnumMonsterProfile.SkeletonArcher => SkeletonArcher,
      EnumMonsterProfile.SkeletonMage => SkeletonMage,
      EnumMonsterProfile.Goblin => Goblin,
      EnumMonsterProfile.GoblinChief => GoblinChief,
      _ => null
    };
  }

  /// <summary>
  /// 根据 Key 获取 Profile
  /// </summary>
  public static MonsterProfile? GetProfileByKey(int key)
  {
    return GetProfile((EnumMonsterProfile)key);
  }
}
