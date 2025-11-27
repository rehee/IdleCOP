using Idle.Core;
using Idle.Core.Combat;
using Idle.Core.Context;
using Idle.Core.DTOs;
using Idle.Core.Helpers;
using Idle.Utility;
using Idle.Utility.Components;
using Idle.Utility.Helpers;
using IdleCOP.Gameplay.Actors;
using IdleCOP.Gameplay.Actors.Monsters;
using IdleCOP.Gameplay.Actors.Players;
using IdleCOP.Gameplay.Helpers;
using IdleCOP.Gameplay.Maps.Profiles;

namespace IdleCOP.Gameplay.Maps;

/// <summary>
/// 地图组件 - 管理战斗场景和逻辑
/// </summary>
public class MapComponent : IdleComponent
{
  /// <summary>
  /// 地图 Profile
  /// </summary>
  public MapProfile? MapProfile { get; set; }

  /// <summary>
  /// 战斗请求
  /// </summary>
  public CombatRequest? CombatRequest { get; set; }

  /// <summary>
  /// 当前波次
  /// </summary>
  public int CurrentWave { get; set; }

  /// <summary>
  /// 波次配置列表
  /// </summary>
  public List<WaveConfig> Waves { get; set; } = new();

  /// <summary>
  /// 是否为回放模式
  /// </summary>
  public bool IsReplay { get; set; }

  /// <summary>
  /// 临时背包（掉落物品）
  /// </summary>
  public List<Guid> TempInventory { get; set; } = new();

  /// <summary>
  /// 临时背包最大容量
  /// </summary>
  public const int MaxTempInventorySize = 500;

  /// <summary>
  /// 根据战斗请求初始化地图
  /// </summary>
  public static MapComponent Initialize(CombatRequest request, TickContext context)
  {
    var mapProfile = MapProfiles.GetProfileByKey(request.MapId);
    if (mapProfile == null)
    {
      throw new ArgumentException($"Invalid map ID: {request.MapId}");
    }

    var mapComponent = new MapComponent
    {
      MapProfile = mapProfile,
      CombatRequest = request,
      IsReplay = request.IsReplay,
      CurrentWave = 0
    };

    // 设置战斗随机数
    context.BattleRandom = new GameRandom(request.BattleSeed);
    context.ItemRandom = request.IsReplay ? null : new GameRandom(request.ItemSeed);

    // 设置最大 Tick
    context.MaxTick = TickHelper.SecondsToTicks(mapProfile.MaxBattleSeconds);

    // 生成玩家
    mapComponent.SpawnPlayers(request, context);

    // 如果是 PvE 地图，生成怪物波次
    if (mapProfile.MapType == EnumMapType.PvE)
    {
      mapComponent.Waves = mapProfile.GenerateWaves(request.MapDifficulty, context.BattleRandom);
    }
    // 如果是 PvP 地图，生成敌对阵营玩家
    else if (mapProfile.MapType == EnumMapType.PvP)
    {
      mapComponent.SpawnEnemyPlayers(request, context);
    }

    return mapComponent;
  }

  /// <summary>
  /// 生成创造者阵营玩家
  /// </summary>
  private void SpawnPlayers(CombatRequest request, TickContext context)
  {
    foreach (var characterDto in request.CreatorFactionCharacters)
    {
      var actor = ActorComponentHelper.FromDTO(characterDto, EnumFaction.Creator);
      context.CreatorFaction.Add(actor);
    }
  }

  /// <summary>
  /// 生成敌对阵营玩家（PvP）
  /// </summary>
  private void SpawnEnemyPlayers(CombatRequest request, TickContext context)
  {
    foreach (var characterDto in request.EnemyFactionCharacters)
    {
      var actor = ActorComponentHelper.FromDTO(characterDto, EnumFaction.Enemy);
      context.EnemyFaction.Add(actor);
    }
  }

  /// <summary>
  /// 生成当前波次的怪物
  /// </summary>
  private void SpawnWaveMonsters(TickContext context)
  {
    if (CurrentWave < 0 || CurrentWave >= Waves.Count)
      return;

    var wave = Waves[CurrentWave];
    var creatorLevel = CombatRequest?.CreatorLevel ?? 1;

    foreach (var (monsterType, count) in wave.Monsters)
    {
      var monsterProfile = MonsterProfiles.GetProfile(monsterType);
      if (monsterProfile == null)
        continue;

      for (int i = 0; i < count; i++)
      {
        var monsterDto = CharacterDTOHelper.CreateMonster(
          monsterProfile.Name ?? "怪物",
          creatorLevel,
          monsterProfile.Key);

        // 应用等级缩放
        var actor = ActorComponentHelper.FromDTO(monsterDto, EnumFaction.Enemy);
        monsterProfile.ApplyLevelScaling(actor);

        context.EnemyFaction.Add(actor);
      }
    }
  }

  /// <summary>
  /// Tick 处理
  /// </summary>
  public void OnTick(TickContext context)
  {
    if (context.IsBattleOver)
      return;

    // 检查是否需要开始下一波
    CheckAndStartNextWave(context);

    // 处理所有存活的创造者阵营单位
    ProcessFaction(context.CreatorFaction, context);

    // 处理所有存活的敌对阵营单位
    ProcessFaction(context.EnemyFaction, context);

    // 检查战斗结束
    CheckBattleEnd(context);
  }

  /// <summary>
  /// 检查并开始下一波次
  /// </summary>
  private void CheckAndStartNextWave(TickContext context)
  {
    // PvP 地图不使用波次
    if (MapProfile?.MapType == EnumMapType.PvP)
      return;

    // 检查敌对阵营是否已清空
    var hasAliveEnemies = context.EnemyFaction
      .OfType<ActorComponent>()
      .Any(a => a.IsAlive);

    if (!hasAliveEnemies)
    {
      // 如果还有更多波次，生成下一波
      if (CurrentWave < Waves.Count)
      {
        SpawnWaveMonsters(context);
        CurrentWave++;
      }
    }
  }

  /// <summary>
  /// 处理阵营内所有单位
  /// </summary>
  private void ProcessFaction(List<IdleComponent> faction, TickContext context)
  {
    foreach (var component in faction)
    {
      if (component is ActorComponent actor && actor.IsAlive)
      {
        // 根据 ProfileKey 获取对应的 Profile 并处理
        var profile = GetActorProfile(actor);
        profile?.OnTick(actor, context);
      }
    }
  }

  /// <summary>
  /// 获取演员的 Profile
  /// </summary>
  private ActorProfile? GetActorProfile(ActorComponent actor)
  {
    return actor.ActorType switch
    {
      EnumActorType.Player => PlayerProfiles.BasicPlayer,
      EnumActorType.Monster => MonsterProfiles.GetProfileByKey(actor.ProfileKey),
      _ => null
    };
  }

  /// <summary>
  /// 检查战斗结束
  /// </summary>
  private void CheckBattleEnd(TickContext context)
  {
    var allCreatorsDead = !context.CreatorFaction
      .OfType<ActorComponent>()
      .Any(a => a.IsAlive);

    var allEnemiesDead = !context.EnemyFaction
      .OfType<ActorComponent>()
      .Any(a => a.IsAlive);

    // PvE 模式：需要清除所有波次的怪物
    if (MapProfile?.MapType == EnumMapType.PvE)
    {
      // 检查是否还有更多波次
      var allWavesComplete = CurrentWave >= Waves.Count && allEnemiesDead;

      if (allCreatorsDead && allEnemiesDead)
      {
        context.Result = EnumBattleResult.Draw;
        context.IsBattleOver = true;
      }
      else if (allCreatorsDead)
      {
        context.Result = EnumBattleResult.Defeat;
        context.IsBattleOver = true;
      }
      else if (allWavesComplete)
      {
        context.Result = EnumBattleResult.Victory;
        context.IsBattleOver = true;
      }
    }
    // PvP 模式：任一方全灭即结束
    else
    {
      if (allCreatorsDead && allEnemiesDead)
      {
        context.Result = EnumBattleResult.Draw;
        context.IsBattleOver = true;
      }
      else if (allCreatorsDead)
      {
        context.Result = EnumBattleResult.Defeat;
        context.IsBattleOver = true;
      }
      else if (allEnemiesDead)
      {
        context.Result = EnumBattleResult.Victory;
        context.IsBattleOver = true;
      }
    }
  }

  /// <summary>
  /// 添加物品到临时背包
  /// </summary>
  public void AddToTempInventory(Guid itemId)
  {
    if (TempInventory.Count >= MaxTempInventorySize)
    {
      // TODO: 变卖多余物品
      return;
    }

    TempInventory.Add(itemId);
  }

  /// <summary>
  /// 生成战斗回放实体
  /// </summary>
  public CombatReplayEntity GenerateReplayEntity(TickContext context)
  {
    if (CombatRequest == null)
      throw new InvalidOperationException("CombatRequest is null");

    return new CombatReplayEntity
    {
      CreatorCharacterId = CombatRequest.CreatorCharacterId,
      MapId = CombatRequest.MapId,
      MapDifficulty = CombatRequest.MapDifficulty,
      CreatorLevel = CombatRequest.CreatorLevel,
      BattleResult = context.Result,
      DurationTicks = context.CurrentTick,
      BattleSeed = CombatRequest.BattleSeed,
      ItemSeed = CombatRequest.ItemSeed,
      CreatorFactionCharacters = CombatRequest.CreatorFactionCharacters,
      EnemyFactionCharacters = CombatRequest.EnemyFactionCharacters
    };
  }
}
