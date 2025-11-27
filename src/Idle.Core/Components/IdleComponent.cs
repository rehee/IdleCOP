namespace Idle.Core.Components;

/// <summary>
/// 组件基类 - 游戏中实例化的数据对象，保存数据和状态
/// </summary>
public class IdleComponent
{
  /// <summary>
  /// 唯一标识符
  /// </summary>
  public Guid Id { get; set; } = Guid.NewGuid();

  /// <summary>
  /// 对应的 Profile Key
  /// </summary>
  public int ProfileKey { get; set; }

  /// <summary>
  /// 父组件
  /// </summary>
  public IdleComponent? Parent { get; private set; }

  /// <summary>
  /// 子组件列表
  /// </summary>
  public List<IdleComponent> Children { get; } = new();

  /// <summary>
  /// 设置父组件
  /// </summary>
  public void SetParent(IdleComponent? newParent)
  {
    if (Parent != null)
    {
      Parent.Children.Remove(this);
    }

    Parent = newParent;

    if (newParent != null && !newParent.Children.Contains(this))
    {
      newParent.Children.Add(this);
    }
  }

  /// <summary>
  /// 移除父组件
  /// </summary>
  public void RemoveParent()
  {
    SetParent(null);
  }
}
