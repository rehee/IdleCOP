using Idle.Core.DTOs;

namespace IdleCOP.Gameplay.Actors;

/// <summary>
/// 玩家 Profile Key 枚举
/// </summary>
public enum EnumPlayerProfile
{
  /// <summary>
  /// 未指定
  /// </summary>
  NotSpecified = 0,

  /// <summary>
  /// 基础玩家
  /// </summary>
  BasicPlayer = 1
}

/// <summary>
/// 玩家 Profile - 定义玩家角色的行为逻辑
/// </summary>
public class PlayerProfile : ActorProfile
{
  private readonly EnumPlayerProfile profileType;

  /// <summary>
  /// 构造函数
  /// </summary>
  public PlayerProfile(EnumPlayerProfile profileType = EnumPlayerProfile.BasicPlayer)
  {
    this.profileType = profileType;
  }

  /// <inheritdoc/>
  public override int Key => (int)profileType;

  /// <inheritdoc/>
  public override string? Name => profileType switch
  {
    EnumPlayerProfile.BasicPlayer => "基础玩家",
    _ => "未知玩家"
  };

  /// <inheritdoc/>
  public override string? Description => profileType switch
  {
    EnumPlayerProfile.BasicPlayer => "基础玩家角色，具有均衡的属性",
    _ => null
  };

  /// <inheritdoc/>
  public override EnumActorType ActorType => EnumActorType.Player;

  /// <inheritdoc/>
  public override int BaseMaxHealth => 150;

  /// <inheritdoc/>
  public override int BaseMaxEnergy => 100;

  /// <inheritdoc/>
  public override int BasePhysicalDamageMin => 8;

  /// <inheritdoc/>
  public override int BasePhysicalDamageMax => 15;

  /// <inheritdoc/>
  public override float BaseAttackSpeed => 1.2f;
}

/// <summary>
/// 基础玩家 Profile 单例
/// </summary>
public static class PlayerProfiles
{
  /// <summary>
  /// 基础玩家 Profile
  /// </summary>
  public static readonly PlayerProfile BasicPlayer = new(EnumPlayerProfile.BasicPlayer);
}
