using Idle.Core.DTOs;

namespace Idle.Core.Combat;

/// <summary>
/// 战斗请求 - 用于创建和重现战斗
/// </summary>
public class CombatRequest
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
  /// 战斗结果（回放时使用）
  /// </summary>
  public EnumBattleResult BattleResult { get; set; }

  /// <summary>
  /// 战斗持续的tick数量（回放时使用）
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
  /// 与创造者同阵营的玩家DTO
  /// </summary>
  public List<CharacterDTO> CreatorFactionCharacters { get; set; } = new();

  /// <summary>
  /// 创造者敌对阵营的玩家DTO
  /// </summary>
  public List<CharacterDTO> EnemyFactionCharacters { get; set; } = new();

  /// <summary>
  /// 是否是回放模式
  /// </summary>
  public bool IsReplay { get; set; }

  /// <summary>
  /// 创建新战斗请求
  /// </summary>
  public static CombatRequest CreateNew(
    Guid creatorCharacterId,
    int mapId,
    int mapDifficulty,
    int creatorLevel,
    List<CharacterDTO> creatorFaction)
  {
    var random = new Random();
    return new CombatRequest
    {
      CreatorCharacterId = creatorCharacterId,
      MapId = mapId,
      MapDifficulty = mapDifficulty,
      CreatorLevel = creatorLevel,
      BattleSeed = random.Next(),
      ItemSeed = random.Next(),
      CreatorFactionCharacters = creatorFaction,
      IsReplay = false
    };
  }

  /// <summary>
  /// 创建PvP战斗请求
  /// </summary>
  public static CombatRequest CreatePvP(
    Guid creatorCharacterId,
    int mapId,
    int mapDifficulty,
    int creatorLevel,
    List<CharacterDTO> creatorFaction,
    List<CharacterDTO> enemyFaction)
  {
    var random = new Random();
    return new CombatRequest
    {
      CreatorCharacterId = creatorCharacterId,
      MapId = mapId,
      MapDifficulty = mapDifficulty,
      CreatorLevel = creatorLevel,
      BattleSeed = random.Next(),
      ItemSeed = random.Next(),
      CreatorFactionCharacters = creatorFaction,
      EnemyFactionCharacters = enemyFaction,
      IsReplay = false
    };
  }
}
