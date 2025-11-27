# IdleCOP 设计文档

> 本文档描述 IdleCOP 的核心系统设计、架构规范与接口定义

## 目录

1. [架构概览](#架构概览)
2. [模块设计](#模块设计)
3. [核心系统接口](#核心系统接口)
4. [数据流](#数据流)
5. [扩展性设计](#扩展性设计)

---

## 架构概览

### 分层架构

```
┌─────────────────────────────────────────────────────────────┐
│                      表现层 (Presentation)                    │
│  Bootstrap Blazor | WPF Blazor Hybrid | MAUI Blazor Hybrid  │
├─────────────────────────────────────────────────────────────┤
│                      服务层 (Services)                        │
│      Game Server | Session Manager | Sync Service           │
├─────────────────────────────────────────────────────────────┤
│                      游戏逻辑层 (Gameplay)                    │
│  Combat | Skills | Equipment | Maps | Instructions | AI     │
├─────────────────────────────────────────────────────────────┤
│                      核心层 (Core)                            │
│   Component System | TickContext | Behavior Strategy        │
├─────────────────────────────────────────────────────────────┤
│                      工具层 (Utility)                         │
│       Common Utils | Extensions | Helpers                   │
├─────────────────────────────────────────────────────────────┤
│                      数据层 (Data)                            │
│   Repository | Persistence | Serialization | Config         │
└─────────────────────────────────────────────────────────────┘
```

> **设计原则：**
> - 玩法逻辑与 COP 实现解耦，核心逻辑可复用于其他项目
> - 服务通过依赖注入方式根据表现平台注入至游戏逻辑
> - 使用可配置的策略列表（参考荣耀殿堂）替代状态机

### 项目模块结构

```
src/
├── Idle.Utility/           # 通用工具库（独立，无游戏依赖）
│   ├── Extensions/         # 扩展方法
│   ├── Helpers/            # 帮助类
│   └── Common/             # 通用工具
│
├── Idle.Core/              # 核心基础库（通用玩法框架）
│   ├── Components/         # 组件系统基类
│   ├── Profiles/           # Profile 单例逻辑
│   ├── Context/            # TickContext 战斗上下文
│   ├── Behaviors/          # 可配置行为策略
│   └── DI/                 # 依赖注入抽象
│
├── IdleCOP.Gameplay/       # COP 游戏玩法实现
│   ├── Combat/             # 战斗系统
│   ├── Skills/             # 技能系统
│   ├── Equipment/          # 装备系统
│   ├── Maps/               # 地图系统
│   ├── Currency/           # 通货系统
│   └── Instructions/       # 指令系统
│
├── IdleCOP.AI/             # AI与行为
│   ├── Strategies/         # 可配置策略列表
│   ├── Scripting/          # 脚本执行
│   └── Pathfinding/        # 寻路
│
├── IdleCOP.Data/           # 数据管理
│   ├── Models/             # 数据模型
│   ├── Repositories/       # 数据仓储
│   ├── Configs/            # 配置加载
│   └── Persistence/        # 持久化
│
├── IdleCOP.Server/         # 服务器端
│   ├── Sessions/           # 会话管理
│   ├── Sync/               # 数据同步
│   └── Tasks/              # 挂机任务
│
└── IdleCOP.Client/         # 客户端
    ├── Web/                # Blazor Web
    ├── Desktop/            # WPF Blazor
    └── Mobile/             # MAUI Blazor
```

---

## 模块设计

### 1. 核心层 (Idle.Core)

#### 1.1 组件系统 (Component System)

使用组件化设计。所有游戏实例化对象都是 `IdleComponent`，负责保存数据。
组件的逻辑在 `IdleProfile` 中，`IdleProfile` 是单例模式。

每个 `IdleComponent` 对应一个 `IdleProfile`，每个 `IdleProfile` 都有唯一的 Key (int)。

**组件层级结构：**
- 演员（Actor）是父组件
- 技能（Skill）是子组件
- 持续性效果（Effect）是子组件
- 技能与持续性效果为兄弟组件
- `TickContext` 依次传递给所有组件

```csharp
/// <summary>
/// 游戏组件基类 - 保存数据，支持父子层级
/// </summary>
public abstract class IdleComponent
{
  public string Id { get; set; }
  public int ProfileKey { get; set; }
  public Dictionary<string, object> Data { get; set; }
  public List<BehaviorStrategy> Behaviors { get; set; }
  
  // 父子组件关系
  public IdleComponent Parent { get; set; }
  public List<IdleComponent> Children { get; set; }
  
  public virtual void OnTick(TickContext context)
  {
    // 先执行自身逻辑
    var profile = ProfileManager.GetProfile(ProfileKey);
    profile?.Execute(this, context);
    
    // 再依次执行子组件
    foreach (var child in Children)
    {
      child.OnTick(context);
    }
  }
  
  public void AddChild(IdleComponent child)
  {
    child.Parent = this;
    Children.Add(child);
  }
  
  public void RemoveChild(IdleComponent child)
  {
    child.Parent = null;
    Children.Remove(child);
  }
}

/// <summary>
/// Profile 基类 - 单例，包含组件逻辑
/// </summary>
public abstract class IdleProfile
{
  public abstract int Key { get; }
  public abstract void Execute(IdleComponent component, TickContext context);
}

// Profile Key 枚举示例
public enum EnumProfileKey
{
  NotSpecified = 0,
  Player = 1,
  Monster = 2,
  NPC = 3,
  Projectile = 4,
  Environment = 5
}

// 技能 Profile Key
public enum EnumSkill
{
  NotSpecified = 0,
  BasicAttack = 1,
  Fireball = 2,
  // ... 更多技能
}

// 物品 Profile Key
public enum EnumItem
{
  NotSpecified = 0,
  Sword = 1,
  Shield = 2,
  // ... 更多物品
}
```

> **注意：** 如果一个 Profile 使用 `NotSpecified` 作为 Key，则需要额外设置一个不与现有 Enum 冲突的 Key。

#### 1.2 战斗上下文系统 (TickContext)

使用确定性的战斗系统，通过种子文件确保同一种子在同一版本下产生相同的战斗演出。

**种子文件结构：**

```csharp
/// <summary>
/// 战斗种子文件 - 确保确定性战斗回放
/// </summary>
public class BattleSeed
{
  public long RandomSeed { get; set; }        // 随机数种子
  public string MapId { get; set; }           // 地图ID
  public string Version { get; set; }         // 游戏版本（确保兼容性）
  public List<ActorSnapshot> PlayerSnapshots { get; set; }  // 玩家加入战斗时的属性快照
}

/// <summary>
/// 演员属性快照 - 玩家加入战斗时的状态
/// </summary>
public class ActorSnapshot
{
  public string ActorId { get; set; }
  public Dictionary<string, object> Stats { get; set; }
  public List<EquipmentSnapshot> Equipment { get; set; }
  public List<SkillSnapshot> Skills { get; set; }
}
```

**战斗上下文：**

```csharp
/// <summary>
/// Tick 上下文 - 战斗循环的核心参数
/// </summary>
public class TickContext
{
  public BattleSeed Seed { get; set; }
  public Random Random { get; set; }          // 由种子初始化的随机数生成器
  public int CurrentTick { get; set; }        // 当前帧数（从0开始）
  public int MaxTick { get; set; }            // 最大帧数（根据地图最大时间计算）
  public float TickRate { get; set; }         // 每秒帧数（默认30）
  public float DeltaTime => 1f / TickRate;    // 每帧时间间隔
  
  public IdleComponent Map { get; set; }      // 地图组件
  public List<IdleComponent> Projectiles { get; set; }  // 投射物列表
  public List<IdleComponent> Players { get; set; }      // 玩家列表
  public List<IdleComponent> Monsters { get; set; }     // 怪物列表（含NPC波次）
  
  public bool IsBattleOver { get; set; }      // 战斗是否结束
  public EnumBattleResult Result { get; set; } // 战斗结果
}

public enum EnumBattleResult
{
  NotSpecified = 0,
  Victory,        // 玩家胜利
  Defeat,         // 玩家失败
  Timeout,        // 超时
  Draw            // 平局
}
```

**战斗循环：**

```csharp
/// <summary>
/// 战斗管理器 - 执行确定性战斗循环
/// </summary>
public class BattleManager
{
  public TickContext CreateBattle(BattleSeed seed)
  {
    var context = new TickContext
    {
      Seed = seed,
      Random = new Random((int)seed.RandomSeed),
      CurrentTick = 0,
      TickRate = 30f,  // 30帧/秒
    };
    
    // 根据地图ID和种子生成地图组件
    var mapProfile = ProfileManager.GetMapProfile(seed.MapId);
    context.Map = mapProfile.GenerateMap(context);
    context.MaxTick = (int)(mapProfile.MaxPlayTime * context.TickRate);
    
    // 根据玩家快照生成玩家组件
    foreach (var snapshot in seed.PlayerSnapshots)
    {
      var player = CreatePlayerFromSnapshot(snapshot, context);
      context.Players.Add(player);
    }
    
    return context;
  }
  
  public void RunBattle(TickContext context)
  {
    while (context.CurrentTick < context.MaxTick && !context.IsBattleOver)
    {
      ExecuteTick(context);
      context.CurrentTick++;
    }
    
    // 检查战斗结果
    if (!context.IsBattleOver)
    {
      context.Result = EnumBattleResult.Timeout;
    }
  }
  
  private void ExecuteTick(TickContext context)
  {
    // 按顺序执行：投射物 -> 玩家 -> 怪物
    foreach (var projectile in context.Projectiles.Where(p => IsAlive(p)))
    {
      projectile.OnTick(context);
    }
    
    foreach (var player in context.Players.Where(p => IsAlive(p)))
    {
      player.OnTick(context);
    }
    
    foreach (var monster in context.Monsters.Where(m => IsAlive(m)))
    {
      monster.OnTick(context);
    }
    
    // 检查战斗结束条件
    CheckBattleEnd(context);
  }
  
  private void CheckBattleEnd(TickContext context)
  {
    var allPlayersDead = context.Players.All(p => !IsAlive(p));
    var allMonstersDead = context.Monsters.All(m => !IsAlive(m));
    
    if (allPlayersDead)
    {
      context.IsBattleOver = true;
      context.Result = EnumBattleResult.Defeat;
    }
    else if (allMonstersDead)
    {
      context.IsBattleOver = true;
      context.Result = EnumBattleResult.Victory;
    }
  }
  
  private bool IsAlive(IdleComponent component)
  {
    return component.Data.TryGetValue("Health", out var hp) && (float)hp > 0;
  }
}
```

#### 1.3 可配置行为策略 (Behavior Strategy)

使用可配置的行为列表替代状态机，参考荣耀殿堂的策略设计。

```csharp
/// <summary>
/// 行为策略基类
/// </summary>
public abstract class BehaviorStrategy
{
  public int Priority { get; set; }
  public bool IsEnabled { get; set; }
  public BehaviorCondition Condition { get; set; }
  
  public abstract bool CanExecute(IdleComponent component, TickContext context);
  public abstract void Execute(IdleComponent component, TickContext context);
}

/// <summary>
/// 策略列表执行器
/// </summary>
public class StrategyExecutor
{
  private List<BehaviorStrategy> strategies;
  
  public void Execute(IdleComponent component, TickContext context)
  {
    var sortedStrategies = strategies
      .Where(s => s.IsEnabled && s.CanExecute(component, context))
      .OrderByDescending(s => s.Priority);
    
    foreach (var strategy in sortedStrategies)
    {
      strategy.Execute(component, context);
    }
  }
}
```

---

### 2. 游戏逻辑层 (IdleCOP.Gameplay)

#### 2.1 装备系统

```csharp
public class Equipment
{
  public string Id { get; set; }
  public EquipmentBaseType BaseType { get; set; }
  public EnumQuality Quality { get; set; }
  public List<Affix> Affixes { get; set; }
  public int Level { get; set; }
  public int RequiredLevel { get; set; }
  
  public Stats CalculateTotalStats() { }
}

/// <summary>
/// 装备品质枚举
/// </summary>
public enum EnumQuality
{
  NotSpecified = 0,
  Normal,     // 白
  Magic,      // 蓝
  Rare,       // 紫
  Legendary,  // 橙
  Unique      // 红（唯一）
}

/// <summary>
/// 词缀类型枚举
/// </summary>
public enum EnumAffixType
{
  NotSpecified = 0,
  Prefix,         // 前缀
  Suffix,         // 后缀
  Base,           // 基底词缀
  Implicit,       // 物品本身的基础词缀
  Legendary,      // 传奇词缀
  Corrupted,      // 腐化词缀
  Extra           // 额外词缀
}

/// <summary>
/// 词缀分组枚举
/// </summary>
public enum EnumAffixGroup
{
  NotSpecified = 0,
  Offensive,      // 攻击组
  Defensive,      // 防御组
  Utility,        // 功能组
  Elemental,      // 元素组
  Resource        // 资源组
}

/// <summary>
/// 词缀类 - 支持分级(T1最高)和物品等级要求
/// </summary>
public class Affix
{
  public string Id { get; set; }
  public EnumAffixType Type { get; set; }
  public EnumAffixGroup Group { get; set; }
  public string Name { get; set; }
  public List<StatModifier> Modifiers { get; set; }
  public int Tier { get; set; }              // T级别，T1最高
  public int RequiredItemLevel { get; set; } // 需要的物品等级才能出现
}
```

**掉落系统接口：**

```csharp
public interface ILootGenerator
{
  Equipment GenerateEquipment(LootContext context);
  List<Equipment> GenerateLootDrop(IdleComponent monster, IdleComponent player);
}

public interface IDropTable
{
  List<DropEntry> GetPossibleDrops(string zoneId, int monsterLevel);
}
```

#### 2.2 技能系统

技能作为演员的子组件存在，在 `OnTick` 中通过 `TickContext` 执行。

```csharp
public abstract class Skill
{
  public string Id { get; set; }
  public EnumSkillType Type { get; set; }
  public SkillParams Params { get; set; }
  public List<SupportSkill> Supports { get; set; }
  
  public abstract void Execute(IdleComponent caster, TickContext context);
}

public enum EnumSkillType
{
  NotSpecified = 0,
  Active,     // 主动技能
  Support,    // 辅助技能
  Trigger     // 触发技能
}

/// <summary>
/// 资源类型枚举 - 不同职业使用不同资源
/// </summary>
public enum EnumResourceType
{
  NotSpecified = 0,
  Arcane,     // 奥术（法师）
  Energy,     // 能量（冒险者）
  Chi,        // 内力（侠客）
  Rage,       // 怒气（狂战士）
  Spirit,     // 灵力（召唤师）
  Focus       // 专注（刺客）
}

/// <summary>
/// 资源配置 - 支持主资源和副资源
/// </summary>
public class ResourceConfig
{
  public EnumResourceType PrimaryResource { get; set; }   // 主资源
  public EnumResourceType SecondaryResource { get; set; } // 副资源
  public float PrimaryMax { get; set; }
  public float SecondaryMax { get; set; }
  public float PrimaryRegenRate { get; set; }
  public float SecondaryRegenRate { get; set; }
  public bool ConvertOnFull { get; set; }  // 主资源满时是否转换为副资源
  public float ConversionRate { get; set; } // 转换比例
}

/// <summary>
/// 技能资源消耗配置
/// </summary>
public class SkillResourceCost
{
  public EnumResourceType ResourceType { get; set; }
  public float Cost { get; set; }
  public bool UsePrimary { get; set; }  // true=主资源, false=副资源
}

public class ActiveSkill : Skill
{
  public float Cooldown { get; set; }
  public SkillResourceCost ResourceCost { get; set; }  // 资源消耗（替代 ManaCost）
  public float CastTime { get; set; }
  public bool GeneratesResource { get; set; }  // 是否产生资源（如格斗家击中生成资源）
  public float ResourceGenerated { get; set; } // 产生的资源量
}

public class SupportSkill : Skill
{
  public List<SkillTag> SupportedTags { get; set; }
  public List<SkillModifier> Modifiers { get; set; }
}

public class TriggerSkill : Skill
{
  public TriggerCondition Condition { get; set; }
  public float TriggerChance { get; set; }
  public float InternalCooldown { get; set; }
}
```

> **资源系统示例 - 格斗家：**
> - 施展技能击中对手或造成伤害后增加主资源
> - 主资源满后转换为一个副资源
> - 副资源用于释放绝技（类似主流格斗游戏）

#### 2.3 地图系统

```csharp
public class GameMap
{
  public string Seed { get; set; }
  public List<Zone> Zones { get; set; }
  public List<SpawnPoint> Spawns { get; set; }
  public MapLayout Layout { get; set; }
}

public interface IMapGenerator
{
  GameMap Generate(string seed, MapConfig config);
}

public class Zone
{
  public string Id { get; set; }
  public EnumZoneType Type { get; set; }
  public List<Tile> Tiles { get; set; }
  public List<IdleComponent> Components { get; set; }
}

public enum EnumZoneType
{
  NotSpecified = 0,
  Entrance,
  Combat,
  Boss,
  Safe,
  Treasure
}
```

#### 2.4 通货系统

```csharp
public class Currency
{
  public string Id { get; set; }
  public EnumCurrencyType Type { get; set; }
  public int Amount { get; set; }
  public bool IsShared { get; set; }  // 账号全局共享
}

public enum EnumCurrencyType
{
  NotSpecified = 0,
  Crafting,
  Trading,
  Special
}

public interface ICurrencyManager
{
  void Add(string currencyId, int amount);
  bool TrySpend(string currencyId, int amount);
  int GetBalance(string currencyId);
  List<Currency> GetAllCurrencies();
}
```

#### 2.5 指令系统

```csharp
public class Instruction
{
  public string Id { get; set; }
  public InstructionCondition Condition { get; set; }
  public InstructionAction Action { get; set; }
  public List<Instruction> Children { get; set; }
  public int Priority { get; set; }
}

public interface IInstructionExecutor
{
  void Execute(IdleComponent component, List<Instruction> instructions);
  bool EvaluateCondition(IdleComponent component, InstructionCondition condition);
}
```

---

### 3. AI层 (IdleCOP.AI)

#### 3.1 可配置策略列表

使用可配置的策略列表替代传统行为树，支持更灵活的行为定制。

```csharp
public interface IStrategy
{
  int Priority { get; }
  bool CanExecute(TickContext context, IdleComponent actor);
  void Execute(TickContext context, IdleComponent actor);
}

public enum EnumStrategyStatus
{
  NotSpecified = 0,
  Success,
  Failure,
  Running
}

public class StrategyList
{
  public List<IStrategy> Strategies { get; set; }
  
  public void Tick(TickContext context, IdleComponent actor)
  {
    foreach (var strategy in Strategies.OrderByDescending(s => s.Priority))
    {
      if (strategy.CanExecute(context, actor))
      {
        strategy.Execute(context, actor);
        break;  // 执行最高优先级匹配策略后停止
      }
    }
  }
}
```

---

### 4. 数据层 (IdleCOP.Data)

#### 4.1 数据仓储

```csharp
public interface IRepository<T> where T : class
{
  Task<T> GetByIdAsync(string id);
  Task<IEnumerable<T>> GetAllAsync();
  Task SaveAsync(T entity);
  Task DeleteAsync(string id);
}

public interface IAccountRepository : IRepository<Account>
{
  Task<Account> GetByUsernameAsync(string username);
}
```

#### 4.2 持久化策略

- **本地存储**：装备快照、游戏进度
- **服务器同步**：账号数据、通货余额、关键状态

```csharp
public interface ISyncService
{
  Task SyncToServerAsync(SyncData data);
  Task<SyncData> FetchFromServerAsync(string accountId);
  Task<bool> ResolveConflictsAsync(SyncConflict conflict);
}
```

---

## 数据流

### 战斗循环（确定性）

```
┌─────────────────────────────────────────────────────────────┐
│                    战斗种子 (BattleSeed)                      │
│         随机种子 + 地图ID + 玩家快照 + 游戏版本               │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│                  创建战斗上下文 (TickContext)                 │
│    初始化随机数生成器 | 生成地图组件 | 生成玩家组件           │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│                    Tick 循环 (0 → MaxTick)                   │
│  ┌─────────────┐  ┌─────────────┐  ┌─────────────┐         │
│  │  投射物     │─>│   玩家      │─>│   怪物      │         │
│  │  OnTick()   │  │  OnTick()   │  │  OnTick()   │         │
│  └─────────────┘  └─────────────┘  └─────────────┘         │
│         │               │               │                   │
│         └───────────────┼───────────────┘                   │
│                         ▼                                   │
│              ┌─────────────────────┐                        │
│              │  检查战斗结束条件   │                        │
│              └─────────────────────┘                        │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│                      战斗结果                                │
│          Victory | Defeat | Timeout | Draw                  │
└─────────────────────────────────────────────────────────────┘
```

> **确定性保证：** 相同的种子文件 + 相同的游戏版本 = 相同的战斗演出

### 组件层级执行流程

```
演员 (Actor Component)
  │
  ├─ OnTick(TickContext)
  │     │
  │     ├─ 执行自身 Profile 逻辑
  │     │
  │     └─ 依次执行子组件
  │           │
  │           ├─ 技能组件 (Skill Component)
  │           │     └─ OnTick(TickContext)
  │           │
  │           └─ 效果组件 (Effect Component)
  │                 └─ OnTick(TickContext)
  │
  └─ TickContext 传递完成
```

### 放置模式数据流

```
服务器
  │
  ├─ 接收玩家指令配置
  │
  ├─ 创建战斗种子 (BattleSeed)
  │     │
  │     ├─ 生成随机数种子
  │     ├─ 确定地图ID
  │     └─ 保存玩家属性快照
  │
  ├─ 执行战斗循环 (TickContext)
  │     │
  │     ├─ 按 Tick 顺序执行
  │     ├─ 更新演员状态
  │     ├─ 处理战斗/掉落
  │     └─ 记录战斗日志
  │
  └─ 同步结果到客户端
       │
       └─ 可选：发送种子文件用于战斗回放
```

---

## 扩展性设计

### 插件系统

```csharp
public interface IGamePlugin
{
  string PluginId { get; }
  void OnLoad(IGameContext context);
  void OnUnload();
}
```

### 配置驱动

所有游戏数据（装备底材、词缀、技能、怪物等）均通过配置文件定义，支持热加载：

```
configs/
├── equipment/
│   ├── base_types.json
│   ├── affixes.json
│   └── drop_tables.json
├── skills/
│   ├── active_skills.json
│   ├── support_skills.json
│   └── trigger_skills.json
├── monsters/
│   └── monster_definitions.json
└── maps/
    └── zone_configs.json
```

---

## 附录

### A. 技术栈详情

| 组件 | 技术选型 |
|------|----------|
| 核心语言 | C# (.NET 8.0+) |
| Web前端 | Bootstrap Blazor |
| 桌面端 | WPF + Blazor Hybrid |
| 移动端 | MAUI + Blazor Hybrid |
| 数据库 | SQLite (本地) / PostgreSQL (服务器) |
| 缓存 | Redis |
| 消息队列 | RabbitMQ (可选) |

### B. 相关文档

- [玩法细节](docs-gameplay.md)
- [数据模型](docs-data-models.md)
- [AI指令系统](docs-ai-instructions.md)
- [开发路线图](docs-roadmap.md)
