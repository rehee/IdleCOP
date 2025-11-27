namespace Idle.Core;

/// <summary>
/// 提供名称和描述的接口
/// </summary>
public interface IWithName
{
  /// <summary>
  /// 名称
  /// </summary>
  string? Name { get; }

  /// <summary>
  /// 描述
  /// </summary>
  string? Description { get; }
}
