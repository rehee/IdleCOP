namespace IdleCOP.Gameplay;

/// <summary>
/// 词缀类型枚举
/// </summary>
public enum EnumAffixType
{
  /// <summary>
  /// 未指定
  /// </summary>
  NotSpecified = 0,

  /// <summary>
  /// 前缀
  /// </summary>
  Prefix,

  /// <summary>
  /// 后缀
  /// </summary>
  Suffix,

  /// <summary>
  /// 基底词缀
  /// </summary>
  Base,

  /// <summary>
  /// 物品基础词缀
  /// </summary>
  Implicit,

  /// <summary>
  /// 传奇词缀
  /// </summary>
  Legendary,

  /// <summary>
  /// 腐化词缀
  /// </summary>
  Corrupted,

  /// <summary>
  /// 额外词缀
  /// </summary>
  Extra
}
