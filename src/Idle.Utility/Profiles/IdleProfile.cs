using Idle.Utility.Components;

namespace Idle.Utility.Profiles;

/// <summary>
/// Profile 基类 - 组件对应的逻辑处理单例，包含业务逻辑
/// </summary>
public abstract class IdleProfile : IWithName
{
  /// <summary>
  /// Profile Key，作为唯一标识
  /// </summary>
  public abstract int Key { get; }

  /// <summary>
  /// Key 覆盖值，当 Key 为 0 时使用
  /// </summary>
  public virtual int? KeyOverride { get; }

  /// <summary>
  /// Profile 名称
  /// </summary>
  public abstract string? Name { get; }

  /// <summary>
  /// Profile 描述
  /// </summary>
  public abstract string? Description { get; }

  /// <summary>
  /// 获取有效的 Key 值
  /// </summary>
  public int EffectiveKey => Key != 0 ? Key : KeyOverride ?? 0;
}
