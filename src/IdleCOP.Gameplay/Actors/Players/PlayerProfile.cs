using Idle.Core.DTOs;

namespace IdleCOP.Gameplay.Actors;

/// <summary>
/// 玩家 Profile 基类 - 定义玩家角色的行为逻辑
/// </summary>
public abstract class PlayerProfile : ActorProfile<EnumPlayer>
{
  /// <inheritdoc/>
  public override EnumActorType ActorType => EnumActorType.Player;
}
