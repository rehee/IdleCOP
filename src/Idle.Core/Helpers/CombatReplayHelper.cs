using Idle.Core.Combat;
using Idle.Core.DTOs;

namespace Idle.Core.Helpers;

/// <summary>
/// 战斗回放帮助类
/// </summary>
public static class CombatReplayHelper
{
  /// <summary>
  /// 从回放实体生成战斗请求
  /// </summary>
  public static CombatRequest ToCombatRequest(CombatReplayEntity entity)
  {
    return new CombatRequest
    {
      CreatorCharacterId = entity.CreatorCharacterId,
      MapId = entity.MapId,
      MapDifficulty = entity.MapDifficulty,
      CreatorLevel = entity.CreatorLevel,
      BattleResult = entity.BattleResult,
      DurationTicks = entity.DurationTicks,
      BattleSeed = entity.BattleSeed,
      ItemSeed = entity.ItemSeed,
      CreatorFactionCharacters = entity.CreatorFactionCharacters,
      EnemyFactionCharacters = entity.EnemyFactionCharacters,
      IsReplay = true
    };
  }

  /// <summary>
  /// 从战斗请求创建回放实体
  /// </summary>
  public static CombatReplayEntity FromCombatRequest(CombatRequest request)
  {
    return new CombatReplayEntity
    {
      CreatorCharacterId = request.CreatorCharacterId,
      MapId = request.MapId,
      MapDifficulty = request.MapDifficulty,
      CreatorLevel = request.CreatorLevel,
      BattleResult = request.BattleResult,
      DurationTicks = request.DurationTicks,
      BattleSeed = request.BattleSeed,
      ItemSeed = request.ItemSeed,
      CreatorFactionCharacters = request.CreatorFactionCharacters,
      EnemyFactionCharacters = request.EnemyFactionCharacters
    };
  }
}
