namespace Idle.Utility.Entities;

/// <summary>
/// 实体基类 - 所有持久化实体的抽象基类
/// </summary>
public abstract class BaseEntity : IWithName
{
  /// <summary>
  /// 唯一标识符
  /// </summary>
  public Guid Id { get; set; } = Guid.NewGuid();

  /// <summary>
  /// 名称
  /// </summary>
  public string? Name { get; set; }

  /// <summary>
  /// 描述（虚方法，只读，默认返回空）
  /// </summary>
  public virtual string? Description => null;
}
