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
│     Actor System | Event System | State Machine             │
├─────────────────────────────────────────────────────────────┤
│                      数据层 (Data)                            │
│   Repository | Persistence | Serialization | Config         │
└─────────────────────────────────────────────────────────────┘
```

### 项目模块结构

```
src/
├── IdleCOP.Core/           # 核心基础库
│   ├── Actors/             # 演员系统基类
│   ├── Events/             # 事件系统
│   ├── States/             # 状态机
│   └── Utils/              # 工具类
│
├── IdleCOP.Gameplay/       # 游戏逻辑
│   ├── Combat/             # 战斗系统
│   ├── Skills/             # 技能系统
│   ├── Equipment/          # 装备系统
│   ├── Maps/               # 地图系统
│   ├── Currency/           # 通货系统
│   └── Instructions/       # 指令系统
│
├── IdleCOP.AI/             # AI与行为
│   ├── BehaviorTree/       # 行为树
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

### 1. 核心层 (IdleCOP.Core)

#### 1.1 演员系统 (Actor System)

所有游戏实体（玩家、NPC、怪物、投射物等）均继承自 `Actor` 基类。

```csharp
public abstract class Actor
{
    public string Id { get; set; }
    public ActorType Type { get; set; }
    public Stats Stats { get; set; }
    public Inventory Inventory { get; set; }
    public List<Instruction> Instructions { get; set; }
    public AIState AIState { get; set; }
    
    public virtual void OnTick(GameTime gameTime) { }
    public virtual void OnEvent(GameEvent gameEvent) { }
}

public enum ActorType
{
    Player,
    NPC,
    Monster,
    Projectile,
    Environment
}
```

#### 1.2 事件系统 (Event System)

基于发布-订阅模式的事件系统，用于模块间解耦通信。

```csharp
public interface IEventBus
{
    void Publish<T>(T gameEvent) where T : GameEvent;
    void Subscribe<T>(Action<T> handler) where T : GameEvent;
    void Unsubscribe<T>(Action<T> handler) where T : GameEvent;
}

public abstract class GameEvent
{
    public string EventId { get; set; }
    public DateTime Timestamp { get; set; }
    public Actor Source { get; set; }
}
```

#### 1.3 状态机 (State Machine)

用于管理演员行为状态的有限状态机。

```csharp
public interface IStateMachine<TState> where TState : Enum
{
    TState CurrentState { get; }
    void TransitionTo(TState newState);
    void Update(GameTime gameTime);
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
    public Quality Quality { get; set; }
    public List<Affix> Affixes { get; set; }
    public int Level { get; set; }
    public int RequiredLevel { get; set; }
    
    public Stats CalculateTotalStats() { }
}

public enum Quality
{
    Normal,     // 白
    Magic,      // 蓝
    Rare,       // 紫
    Legendary   // 橙
}

public class Affix
{
    public string Id { get; set; }
    public AffixType Type { get; set; }  // Prefix / Suffix
    public string Name { get; set; }
    public List<StatModifier> Modifiers { get; set; }
    public int Tier { get; set; }
}
```

**掉落系统接口：**

```csharp
public interface ILootGenerator
{
    Equipment GenerateEquipment(LootContext context);
    List<Equipment> GenerateLootDrop(Monster monster, Player player);
}

public interface IDropTable
{
    List<DropEntry> GetPossibleDrops(string zoneId, int monsterLevel);
}
```

#### 2.2 技能系统

```csharp
public abstract class Skill
{
    public string Id { get; set; }
    public SkillType Type { get; set; }
    public SkillParams Params { get; set; }
    public List<SupportSkill> Supports { get; set; }
    
    public abstract void Execute(Actor caster, SkillContext context);
}

public enum SkillType
{
    Active,     // 主动技能
    Support,    // 辅助技能
    Trigger     // 触发技能
}

public class ActiveSkill : Skill
{
    public float Cooldown { get; set; }
    public float ManaCost { get; set; }
    public float CastTime { get; set; }
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
    public ZoneType Type { get; set; }
    public List<Tile> Tiles { get; set; }
    public List<Actor> Actors { get; set; }
}
```

#### 2.4 通货系统

```csharp
public class Currency
{
    public string Id { get; set; }
    public CurrencyType Type { get; set; }
    public int Amount { get; set; }
    public bool IsShared { get; set; }  // 账号全局共享
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
    void Execute(Actor actor, List<Instruction> instructions);
    bool EvaluateCondition(Actor actor, InstructionCondition condition);
}
```

---

### 3. AI层 (IdleCOP.AI)

#### 3.1 行为树

```csharp
public interface IBehaviorNode
{
    BehaviorStatus Execute(BehaviorContext context);
}

public enum BehaviorStatus
{
    Success,
    Failure,
    Running
}

public class BehaviorTree
{
    public IBehaviorNode Root { get; set; }
    public void Tick(BehaviorContext context) { }
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

### 游戏循环

```
┌──────────────┐    ┌──────────────┐    ┌──────────────┐
│    Input     │───>│  Game Logic  │───>│   Render     │
│  (手操/放置)  │    │  (Tick循环)   │    │  (UI更新)    │
└──────────────┘    └──────────────┘    └──────────────┘
       │                   │                    │
       └───────────────────┴────────────────────┘
                          │
                    ┌─────┴─────┐
                    │  Events   │
                    └───────────┘
```

### 放置模式数据流

```
服务器
  │
  ├─ 接收玩家指令配置
  │
  ├─ 后台执行游戏Tick
  │     │
  │     ├─ 执行指令队列
  │     ├─ 更新演员状态
  │     ├─ 处理战斗/掉落
  │     └─ 记录游戏日志
  │
  └─ 同步结果到客户端
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
