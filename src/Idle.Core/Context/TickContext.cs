using Idle.Utility;
using Idle.Utility.Components;

namespace Idle.Core.Context;

/// <summary>
/// Tick 上下文 - 战斗运行时环境和状态容器
/// </summary>
public class TickContext : IDisposable
{
  /// <summary>
  /// 当前 Tick 数
  /// </summary>
  public int CurrentTick { get; set; }

  /// <summary>
  /// 最大 Tick 数
  /// </summary>
  public int MaxTick { get; set; }

  /// <summary>
  /// 战斗是否结束
  /// </summary>
  public bool IsBattleOver { get; set; }

  /// <summary>
  /// 战斗结果
  /// </summary>
  public EnumBattleResult Result { get; set; }

  /// <summary>
  /// 战斗随机数生成器
  /// </summary>
  public IRandom? BattleRandom { get; set; }

  /// <summary>
  /// 物品随机数生成器（回放时可跳过）
  /// </summary>
  public IRandom? ItemRandom { get; set; }

  /// <summary>
  /// 创造者阵营组件列表
  /// </summary>
  public List<IdleComponent> CreatorFaction { get; } = new();

  /// <summary>
  /// 敌对阵营组件列表
  /// </summary>
  public List<IdleComponent> EnemyFaction { get; } = new();

  /// <summary>
  /// 投射物列表
  /// </summary>
  public List<IdleComponent> Projectiles { get; } = new();

  /// <summary>
  /// 释放资源
  /// </summary>
  public void Dispose()
  {
    CreatorFaction.Clear();
    EnemyFaction.Clear();
    Projectiles.Clear();
    GC.SuppressFinalize(this);
  }
}
