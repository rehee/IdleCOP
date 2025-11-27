using Idle.Core.DTOs;
using Idle.Utility.Entities;

namespace Idle.Core.Combat;

/// <summary>
/// 战斗回放实体 - 存储战斗种子以便重现战斗
/// </summary>
public class CombatReplayEntity : BaseEntity
{
  /// <summary>
  /// 创建地图的玩家角色ID
  /// </summary>
  public Guid CreatorCharacterId { get; set; }

  /// <summary>
  /// 地图ID
  /// </summary>
  public int MapId { get; set; }

  /// <summary>
  /// 地图难度
  /// </summary>
  public int MapDifficulty { get; set; }

  /// <summary>
  /// 创建地图时玩家的等级
  /// </summary>
  public int CreatorLevel { get; set; }

  /// <summary>
  /// 战斗结果
  /// </summary>
  public EnumBattleResult BattleResult { get; set; }

  /// <summary>
  /// 战斗持续的tick数量
  /// </summary>
  public int DurationTicks { get; set; }

  /// <summary>
  /// 战斗用随机数种子
  /// </summary>
  public int BattleSeed { get; set; }

  /// <summary>
  /// 物品生成用随机数种子
  /// </summary>
  public int ItemSeed { get; set; }

  /// <summary>
  /// 与创造者同阵营的玩家DTO（JSON序列化存储）
  /// </summary>
  public List<CharacterDTO> CreatorFactionCharacters { get; set; } = new();

  /// <summary>
  /// 创造者敌对阵营的玩家DTO（JSON序列化存储）
  /// </summary>
  public List<CharacterDTO> EnemyFactionCharacters { get; set; } = new();

  /// <summary>
  /// 创建时间
  /// </summary>
  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
