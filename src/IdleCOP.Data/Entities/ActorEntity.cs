namespace IdleCOP.Data.Entities;

/// <summary>
/// 角色实体
/// </summary>
public class ActorEntity
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
  /// 等级
  /// </summary>
  public int Level { get; set; }

  /// <summary>
  /// 对应的 Profile Key
  /// </summary>
  public int ProfileKey { get; set; }

  /// <summary>
  /// 创建时间
  /// </summary>
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

  /// <summary>
  /// 更新时间
  /// </summary>
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
