namespace Idle.Core;

/// <summary>
/// 战斗结果枚举
/// </summary>
public enum EnumBattleResult
{
  /// <summary>
  /// 未指定
  /// </summary>
  NotSpecified = 0,

  /// <summary>
  /// 胜利
  /// </summary>
  Victory,

  /// <summary>
  /// 失败
  /// </summary>
  Defeat,

  /// <summary>
  /// 超时
  /// </summary>
  Timeout,

  /// <summary>
  /// 平局
  /// </summary>
  Draw,

  /// <summary>
  /// 错误
  /// </summary>
  Error,

  /// <summary>
  /// 玩家退出
  /// </summary>
  PlayerExit
}
