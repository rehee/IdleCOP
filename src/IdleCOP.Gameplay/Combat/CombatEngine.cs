using Idle.Core;
using Idle.Core.Combat;
using Idle.Core.Context;
using IdleCOP.Gameplay.Maps;

namespace IdleCOP.Gameplay.Combat;

/// <summary>
/// 战斗引擎 - 执行战斗逻辑
/// </summary>
public class CombatEngine
{
  /// <summary>
  /// 运行战斗
  /// </summary>
  /// <param name="request">战斗请求</param>
  /// <returns>战斗回放实体</returns>
  public CombatReplayEntity RunBattle(CombatRequest request)
  {
    using var context = new TickContext();

    // 初始化地图
    var mapComponent = MapComponent.Initialize(request, context);

    // 运行战斗循环
    RunBattleLoop(mapComponent, context);

    // 生成回放实体
    return mapComponent.GenerateReplayEntity(context);
  }

  /// <summary>
  /// 回放战斗
  /// </summary>
  /// <param name="replayEntity">回放实体</param>
  /// <returns>每个 Tick 的上下文状态（用于渲染）</returns>
  public IEnumerable<TickContext> ReplayBattle(CombatReplayEntity replayEntity)
  {
    var request = replayEntity.ToCombatRequest();

    using var context = new TickContext();
    var mapComponent = MapComponent.Initialize(request, context);

    while (context.CurrentTick < context.MaxTick && !context.IsBattleOver)
    {
      // 处理地图逻辑
      mapComponent.OnTick(context);

      // 返回当前帧状态（克隆以避免后续修改）
      yield return CloneContext(context);

      context.CurrentTick++;
    }

    // 处理超时
    if (!context.IsBattleOver)
    {
      context.Result = EnumBattleResult.Timeout;
      context.IsBattleOver = true;
      yield return CloneContext(context);
    }
  }

  /// <summary>
  /// 运行战斗循环
  /// </summary>
  private void RunBattleLoop(MapComponent mapComponent, TickContext context)
  {
    while (context.CurrentTick < context.MaxTick && !context.IsBattleOver)
    {
      // 处理地图逻辑
      mapComponent.OnTick(context);

      context.CurrentTick++;
    }

    // 处理超时
    if (!context.IsBattleOver)
    {
      context.Result = EnumBattleResult.Timeout;
      context.IsBattleOver = true;
    }
  }

  /// <summary>
  /// 克隆上下文（简化版，仅用于回放）
  /// </summary>
  private TickContext CloneContext(TickContext source)
  {
    // 注意：这是简化实现，实际应该深度克隆所有组件
    return new TickContext
    {
      CurrentTick = source.CurrentTick,
      MaxTick = source.MaxTick,
      IsBattleOver = source.IsBattleOver,
      Result = source.Result
    };
  }
}
