using Idle.Core.DTOs;

namespace IdleCOP.Gameplay.Actors;

/// <summary>
/// 怪物 Profile 基类 - 定义怪物的行为逻辑
/// </summary>
public abstract class MonsterProfile : ActorProfile<EnumMonster>
{
  /// <inheritdoc/>
  public override EnumActorType ActorType => EnumActorType.Monster;
}
