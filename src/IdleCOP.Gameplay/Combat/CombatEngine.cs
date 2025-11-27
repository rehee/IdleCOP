using Idle.Core;
using Idle.Core.Combat;
using Idle.Core.Context;
using Idle.Core.Helpers;
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
  /// <param name="onTick">每个 Tick 的回调（用于渲染）</param>
  public void ReplayBattle(CombatReplayEntity replayEntity, Action<TickContext>? onTick = null)
  {
    var request = CombatReplayHelper.ToCombatRequest(replayEntity);

    using var context = new TickContext();
    var mapComponent = MapComponent.Initialize(request, context);

    while (context.CurrentTick < context.MaxTick && !context.IsBattleOver)
    {
      // 处理地图逻辑
      mapComponent.OnTick(context);

      // 回调渲染
      onTick?.Invoke(context);

      context.CurrentTick++;
    }

    // 处理超时
    if (!context.IsBattleOver)
    {
      context.Result = EnumBattleResult.Timeout;
      context.IsBattleOver = true;
      onTick?.Invoke(context);
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
}
