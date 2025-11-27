using Idle.Core.Context;
using Idle.Core.DTOs;
using Idle.Utility.Profiles;

namespace IdleCOP.Gameplay.Actors;

/// <summary>
/// 演员 Profile 基类 - 定义演员的行为逻辑
/// </summary>
public abstract class ActorProfile : IdleProfile
{
  /// <summary>
  /// 演员类型
  /// </summary>
  public abstract EnumActorType ActorType { get; }

  /// <summary>
  /// 基础最大生命值
  /// </summary>
  public virtual int BaseMaxHealth => 100;

  /// <summary>
  /// 基础最大能量
  /// </summary>
  public virtual int BaseMaxEnergy => 50;

  /// <summary>
  /// 基础攻击力（最小）
  /// </summary>
  public virtual int BasePhysicalDamageMin => 5;

  /// <summary>
  /// 基础攻击力（最大）
  /// </summary>
  public virtual int BasePhysicalDamageMax => 10;

  /// <summary>
  /// 基础攻击速度
  /// </summary>
  public virtual float BaseAttackSpeed => 1.0f;

  /// <summary>
  /// Tick 处理
  /// </summary>
  public virtual void OnTick(ActorComponent actor, TickContext context)
  {
    if (!actor.IsAlive)
      return;

    // 减少攻击冷却
    if (actor.AttackCooldown > 0)
    {
      actor.AttackCooldown--;
    }

    // 如果没有目标或目标已死亡，寻找新目标
    if (actor.CurrentTarget == null || !actor.CurrentTarget.IsAlive)
    {
      actor.CurrentTarget = FindTarget(actor, context);
    }

    // 如果有目标且攻击冷却结束，进行攻击
    if (actor.CurrentTarget != null && actor.AttackCooldown <= 0)
    {
      Attack(actor, actor.CurrentTarget, context);
    }
  }

  /// <summary>
  /// 寻找目标
  /// </summary>
  protected virtual ActorComponent? FindTarget(ActorComponent actor, TickContext context)
  {
    var enemyList = actor.Faction == Idle.Core.EnumFaction.Creator
      ? context.EnemyFaction
      : context.CreatorFaction;

    foreach (var component in enemyList)
    {
      if (component is ActorComponent target && target.IsAlive)
      {
        return target;
      }
    }

    return null;
  }

  /// <summary>
  /// 执行攻击
  /// </summary>
  protected virtual void Attack(ActorComponent attacker, ActorComponent target, TickContext context)
  {
    if (context.BattleRandom == null)
      return;

    // 计算伤害
    var damage = context.BattleRandom.Next(
      attacker.CombatStats.PhysicalDamageMin,
      attacker.CombatStats.PhysicalDamageMax + 1);

    // 检查暴击
    if (context.BattleRandom.NextFloat() * 100 < attacker.CombatStats.CriticalChance)
    {
      damage = (int)(damage * attacker.CombatStats.CriticalMultiplier);
    }

    // 计算伤害减免（护甲）
    var damageReduction = target.CombatStats.Armor / (float)(target.CombatStats.Armor + 100);
    damage = (int)(damage * (1 - damageReduction));

    // 造成伤害
    target.TakeDamage(Math.Max(1, damage));

    // 设置攻击冷却（基于攻击速度）
    attacker.AttackCooldown = (int)(30 / attacker.CombatStats.AttackSpeed);
  }

  /// <summary>
  /// 根据等级计算属性
  /// </summary>
  public virtual void ApplyLevelScaling(ActorComponent actor)
  {
    var level = actor.Level;
    actor.CombatStats.MaxHealth = BaseMaxHealth + level * 10;
    actor.CombatStats.CurrentHealth = actor.CombatStats.MaxHealth;
    actor.CombatStats.MaxEnergy = BaseMaxEnergy + level * 5;
    actor.CombatStats.CurrentEnergy = actor.CombatStats.MaxEnergy;
    actor.CombatStats.PhysicalDamageMin = BasePhysicalDamageMin + level;
    actor.CombatStats.PhysicalDamageMax = BasePhysicalDamageMax + level * 2;
    actor.CombatStats.AttackSpeed = BaseAttackSpeed;
    actor.CombatStats.Armor = level * 5;
    actor.CombatStats.Evasion = level * 3;
  }
}

/// <summary>
/// 泛型演员 Profile 基类 - 使用枚举类型约束
/// </summary>
/// <typeparam name="TEnum">枚举类型</typeparam>
public abstract class ActorProfile<TEnum> : ActorProfile where TEnum : struct, Enum
{
  /// <summary>
  /// Profile 对应的枚举值
  /// </summary>
  public abstract TEnum ProfileType { get; }

  /// <inheritdoc/>
  public override int Key => Convert.ToInt32(ProfileType);
}
