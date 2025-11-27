namespace Idle.Utility.Profiles;

/// <summary>
/// 泛型 Profile 基类 - 使用枚举类型约束的 Profile
/// </summary>
/// <typeparam name="TEnum">枚举类型</typeparam>
public abstract class IdleProfile<TEnum> : IdleProfile where TEnum : struct, Enum
{
  /// <summary>
  /// Profile 对应的枚举值
  /// </summary>
  public abstract TEnum ProfileType { get; }

  /// <inheritdoc/>
  public override int Key => Convert.ToInt32(ProfileType);
}
