using IdleCOP.Gameplay;

namespace IdleCOP.Data.Entities;

/// <summary>
/// 装备实体
/// </summary>
public class EquipmentEntity
{
  /// <summary>
  /// 唯一标识符
  /// </summary>
  public Guid Id { get; set; } = Guid.NewGuid();

  /// <summary>
  /// 底材类型
  /// </summary>
  public string? BaseType { get; set; }

  /// <summary>
  /// 品质
  /// </summary>
  public EnumQuality Quality { get; set; }

  /// <summary>
  /// 物品等级
  /// </summary>
  public int Level { get; set; }

  /// <summary>
  /// 需求等级
  /// </summary>
  public int RequiredLevel { get; set; }
}
