# IdleCOP 设计文档

> 本文档描述 IdleCOP 的核心系统设计、架构规范与接口定义

## 目录

1. [代码规范与上下文](#代码规范与上下文)
2. [项目结构](#项目结构)
3. [核心概念](#核心概念)
4. [战斗流程](#战斗流程)
5. [数据表结构](#数据表结构)
6. [接口定义](#接口定义)
7. [API 格式](#api-格式)

---

## 代码规范与上下文

### 命名规范

| 类型 | 规范 | 示例 |
|------|------|------|
| 枚举 | 以 `Enum` 开头，默认值为 `NotSpecified = 0` | `EnumQuality`, `EnumSkillType` |
| 实体 | 以 `Entity` 结尾 | `ActorEntity`, `SkillEntity` |
| 数据传输对象 | 以 `DTO` 结尾 | `CharacterDTO`, `SkillDTO` |
| 帮助类/扩展方法 | 以 `Helper` 结尾，放在 `Helpers/` 下 | `TickHelper`, `ComponentHelper` |
| 私有成员 | camelCase，不带 `_` 前缀 | `private readonly IService service;` |
| 公共成员 | PascalCase | `public int ProfileKey { get; set; }` |
| Id 字段 | 所有 Entity/DTO/Component 使用 `Guid` | `public Guid Id { get; set; }` |

### 代码缩进

- 使用 **2 个空格** 缩进

### 组件-Profile 对应规范

| Component 方法 | Profile 对应方法 |
|---------------|-----------------|
| `OnTick(TickContext context)` | `OnTick(IdleComponent component, TickContext context)` |

### 数据持久化规范

- 持久化数据保存为 `XXXEntity`
- Entity 根据对应的 Profile 生成 `IdleComponent`
- 存储方式根据平台不同（数据库、IndexedDB 等）

---

## 项目结构

```
src/
├── Idle.Utility/           # 通用工具库（独立，无游戏依赖）
│   └── Helpers/            # 帮助类（XXXHelper）
│
├── Idle.Core/              # 核心基础库（通用玩法框架）
│   ├── Components/         # 组件系统基类
│   ├── Profiles/           # Profile 单例逻辑
│   ├── Context/            # TickContext 战斗上下文
│   └── DI/                 # 依赖注入抽象
│
├── IdleCOP.Gameplay/       # COP 游戏玩法实现
│   ├── Combat/             # 战斗系统
│   ├── Skills/             # 技能系统
│   ├── Equipment/          # 装备系统
│   ├── Maps/               # 地图系统
│   └── Instructions/       # 指令系统
│
├── IdleCOP.AI/             # AI与行为
│   └── Strategies/         # 可配置策略列表
│
├── IdleCOP.Data/           # 数据管理
│   ├── Entities/           # 持久化实体
│   ├── DTOs/               # 数据传输对象
│   └── Configs/            # 配置文件
│
└── IdleCOP.Client/         # 客户端
    ├── Web/                # Blazor Web
    ├── Desktop/            # WPF Blazor Hybrid
    └── Mobile/             # MAUI Blazor Hybrid
```

### 配置文件结构

```
configs/
├── equipment/
│   ├── base_types.json     # 装备底材
│   ├── affixes.json        # 词缀定义
│   └── drop_tables.json    # 掉落表
├── skills/
│   └── skills.json         # 技能定义
├── effects/
│   └── duration_effects.json  # 持续性效果
├── strategies/
│   └── behavior_strategies.json  # 行为策略
└── maps/
    └── maps.json           # 地图配置
```

---

## 核心概念

### 组件系统

```
┌─────────────────────────────────────────────────────────────┐
│                    IdleComponent (数据)                      │
│  - Id: Guid                                                  │
│  - ProfileKey: int                                           │
│  - Parent: IdleComponent?                                    │
│  - SetParent(newParent) / RemoveParent()                    │
└─────────────────────────────────────────────────────────────┘
                              │
                              │ 对应
                              ▼
┌─────────────────────────────────────────────────────────────┐
│                    IdleProfile (逻辑/单例)                    │
│  - Key: int                                                  │
│  - Name / Description                                        │
│  - OnTick(component, context)                               │
└─────────────────────────────────────────────────────────────┘
```

### 组件层级

```
角色组件 (CharacterComponent)
├── 技能组件 (SkillComponent) ─────────┐
│   ├── Cooldown                       │ 兄弟组件
│   └── CastTime                       │
├── 资源组件 (ResourceComponent) ──────┤
│   ├── 主资源                         │
│   └── 副资源                         │
└── 持续性效果组件 (DurationComponent) ┘
    └── RemainingDuration

投射物组件 (ProjectileComponent)
├── 位置
├── 速度
└── 轨迹 (无行为逻辑，仅飞行+碰撞检测)
```

### 阵营系统

```
EnumFaction
├── Creator  ── 创造者阵营（玩家及其召唤物）
└── Enemy    ── 敌对阵营（怪物及其召唤物）

注：阵营存储在 Component 中，不存储在 DTO（可被魅惑等技能改变）
```

---

## 战斗流程

### 战斗初始化

```
输入: BattleSeed
  ├── BattleGuid        (随机数种子)
  ├── MapId             (地图ID)
  ├── Version           (游戏版本)
  ├── CreatorCharacterIds  (创造者阵营角色ID列表)
  ├── EnemyCharacterIds    (敌对阵营角色ID列表)
  └── Characters        (CharacterDTO列表)

处理:
  1. 创建 TickContext
  2. 初始化 BattleRandom (由 BattleGuid 生成)
  3. 初始化 ItemRandom (独立随机数，回放时可跳过)
  4. 根据 MapId 生成地图组件
  5. 根据 Characters 生成角色组件
  6. 设置 MaxTick = 地图最大时间 × TickRate

输出: TickContext
```

### Tick 循环伪代码

```
FUNCTION RunBattle(context: TickContext):
  WHILE context.CurrentTick < context.MaxTick AND NOT context.IsBattleOver:
    
    // 1. 处理投射物（飞行 + 碰撞检测）
    FOR EACH projectile IN context.Projectiles:
      projectile.OnTick(context)
    
    // 2. 处理创造者阵营
    FOR EACH actor IN context.CreatorFaction WHERE IsAlive(actor):
      actor.OnTick(context)
    
    // 3. 处理敌对阵营
    FOR EACH actor IN context.EnemyFaction WHERE IsAlive(actor):
      actor.OnTick(context)
    
    // 4. 检查战斗结束
    CheckBattleEnd(context)
    
    context.CurrentTick++
  
  // 战斗结束处理
  IF NOT context.IsBattleOver:
    context.Result = Timeout
  
  context.Dispose()

FUNCTION CheckBattleEnd(context):
  allCreatorsDead = ALL actors IN CreatorFaction ARE dead
  allEnemiesDead = ALL actors IN EnemyFaction ARE dead
  
  IF allCreatorsDead AND allEnemiesDead:
    context.Result = Draw
  ELSE IF allCreatorsDead:
    context.Result = Defeat
  ELSE IF allEnemiesDead:
    context.Result = Victory
```

### 战斗流程图

```
┌─────────────────────────────────────────────────────────────┐
│                    BattleSeed (战斗种子)                     │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│                  创建 TickContext                           │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│                    Tick 循环                                │
│  ┌──────────────────────────────────────────────────────┐  │
│  │ Tick N:                                               │  │
│  │   1. 投射物 OnTick (飞行/碰撞)                         │  │
│  │   2. 创造者阵营 OnTick (行为逻辑)                      │  │
│  │   3. 敌对阵营 OnTick (行为逻辑)                        │  │
│  │   4. 检查战斗结束                                      │  │
│  └──────────────────────────────────────────────────────┘  │
│                              │                              │
│                              ▼                              │
│                    CurrentTick++ → 继续循环                 │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│                    战斗结果                                 │
│  Victory | Defeat | Timeout | Draw | Error | PlayerExit    │
└─────────────────────────────────────────────────────────────┘
```

---

## 数据表结构

### 装备表 (Equipment)

| 字段 | 类型 | 说明 |
|------|------|------|
| Id | Guid | 唯一标识 |
| BaseType | string | 底材类型 |
| Quality | EnumQuality | 品质 |
| Level | int | 物品等级 |
| RequiredLevel | int | 需求等级 |
| Affixes | List&lt;Affix&gt; | 词缀列表 |

### 品质枚举 (EnumQuality)

| 值 | 名称 | 颜色 |
|------|------|------|
| 0 | NotSpecified | - |
| 1 | Normal | 白 |
| 2 | Magic | 蓝 |
| 3 | Rare | 紫 |
| 4 | Legendary | 橙 |
| 5 | Unique | 红 |

### 词缀表 (Affix)

| 字段 | 类型 | 说明 |
|------|------|------|
| Id | string | 唯一标识 |
| Type | EnumAffixType | 词缀类型 |
| Group | EnumAffixGroup | 词缀分组 |
| Name | string | 名称 |
| Tier | int | T级别 (T1最高) |
| RequiredItemLevel | int | 需要物品等级 |
| Modifiers | List&lt;StatModifier&gt; | 属性修改器 |

### 词缀类型 (EnumAffixType)

| 值 | 名称 | 说明 |
|------|------|------|
| 0 | NotSpecified | - |
| 1 | Prefix | 前缀 |
| 2 | Suffix | 后缀 |
| 3 | Base | 基底词缀 |
| 4 | Implicit | 物品基础词缀 |
| 5 | Legendary | 传奇词缀 |
| 6 | Corrupted | 腐化词缀 |
| 7 | Extra | 额外词缀 |

### 技能表 (Skill)

| 字段 | 类型 | 说明 |
|------|------|------|
| Id | Guid | 唯一标识 |
| ProfileKey | int | 对应的 Profile Key |
| Type | EnumSkillType | 技能类型 |
| Cooldown | float | 冷却时间 (秒) |
| CastTime | float | 读条时间 (秒) |
| ResourceCost | SkillResourceCost | 资源消耗 |

### 技能类型 (EnumSkillType)

| 值 | 名称 | 说明 |
|------|------|------|
| 0 | NotSpecified | - |
| 1 | Active | 主动技能 |
| 2 | Support | 辅助技能 |
| 3 | Trigger | 触发技能 |

### 资源类型 (EnumResourceType)

| 值 | 名称 | 职业 |
|------|------|------|
| 0 | NotSpecified | - |
| 1 | Arcane | 法师 (奥术) |
| 2 | Energy | 冒险者 (能量) |
| 3 | Chi | 侠客 (内力) |
| 4 | Rage | 狂战士 (怒气) |
| 5 | Spirit | 召唤师 (灵力) |
| 6 | Focus | 刺客 (专注) |

### 持续性效果表 (DurationEffect)

| 字段 | 类型 | 说明 |
|------|------|------|
| Id | Guid | 唯一标识 |
| ProfileKey | int | 对应的 Profile Key |
| Type | EnumDurationType | 效果类型 |
| Duration | float | 持续时间 (秒) |
| StackCount | int | 叠加层数 |

### 持续性效果类型 (EnumDurationType)

| 值 | 名称 | 说明 |
|------|------|------|
| 0 | NotSpecified | - |
| 1 | Stun | 眩晕 |
| 2 | Poison | 中毒 |
| 3 | Burn | 燃烧 |
| 4 | Slow | 减速 |
| 5 | Buff | 增益 |

### 行为策略表 (BehaviorStrategy)

| 字段 | 类型 | 说明 |
|------|------|------|
| Id | string | 唯一标识 |
| Priority | int | 优先级 (高优先) |
| IsEnabled | bool | 是否启用 |
| Condition | BehaviorCondition | 触发条件 |
| ActionType | string | 执行动作类型 |

---

## 接口定义

### IWithName

```csharp
public interface IWithName
{
  string? Name { get; }
  string? Description { get; }
}
```

### IRandom

```csharp
public interface IRandom
{
  int Next();
  int Next(int maxValue);
  int Next(int minValue, int maxValue);
  float NextFloat();
  double NextDouble();
}
```

### ILootGenerator

```csharp
public interface ILootGenerator
{
  Equipment GenerateEquipment(LootContext context);
  List<Equipment> GenerateLootDrop(IdleComponent monster, IdleComponent player);
}
```

### IMapGenerator

```csharp
public interface IMapGenerator
{
  GameMap Generate(string seed, MapConfig config);
}
```

### IRepository&lt;T&gt;

```csharp
public interface IRepository<T> where T : class
{
  Task<T> GetByIdAsync(Guid id);
  Task<IEnumerable<T>> GetAllAsync();
  Task SaveAsync(T entity);
  Task DeleteAsync(Guid id);
}
```

### IStrategy

```csharp
public interface IStrategy
{
  int Priority { get; }
  bool CanExecute(TickContext context, IdleComponent actor);
  void Execute(TickContext context, IdleComponent actor);
}
```

---

## API 格式

### 战斗 API

#### 创建战斗

```
POST /api/battle/create

Request:
{
  "mapId": "string",
  "creatorCharacterIds": ["guid", ...],
  "enemyCharacterIds": ["guid", ...]
}

Response:
{
  "battleId": "guid",
  "seed": { ... BattleSeed ... }
}
```

#### 获取战斗结果

```
GET /api/battle/{battleId}/result

Response:
{
  "result": "Victory|Defeat|Timeout|Draw|Error",
  "duration": 120.5,
  "loot": [ ... ],
  "replaySeed": { ... BattleSeed ... }
}
```

### 角色 API

#### 获取角色列表

```
GET /api/characters

Response:
{
  "characters": [
    {
      "id": "guid",
      "name": "string",
      "level": 10,
      "class": "string"
    }
  ]
}
```

#### 获取角色详情

```
GET /api/characters/{characterId}

Response:
{
  "id": "guid",
  "name": "string",
  "health": 1000,
  "equipment": [ ... ],
  "skills": [ ... ],
  "strategies": [ ... ]
}
```

### 装备 API

#### 获取装备列表

```
GET /api/equipment

Response:
{
  "equipment": [
    {
      "id": "guid",
      "baseType": "Sword",
      "quality": "Rare",
      "level": 15,
      "affixes": [ ... ]
    }
  ]
}
```

---

## 附录

### 技术栈

| 组件 | 技术选型 |
|------|----------|
| 核心语言 | C# (.NET 8.0+) |
| Web前端 | Bootstrap Blazor |
| 桌面端 | WPF + Blazor Hybrid |
| 移动端 | MAUI + Blazor Hybrid |
| 存储 | SQLite (本地) / IndexedDB (Web) / PostgreSQL (服务器) |

### 相关文档

- [玩法细节](docs-gameplay.md)
- [数据模型](docs-data-models.md)
- [AI指令系统](docs-ai-instructions.md)
- [开发路线图](docs-roadmap.md)
