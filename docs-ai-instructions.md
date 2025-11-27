# AI 指令系统详细说明

> 本文档详细描述 IdleCOP 的指令/行为脚本系统

## 目录

1. [系统概述](#系统概述)
2. [指令结构](#指令结构)
3. [条件类型](#条件类型)
4. [动作类型](#动作类型)
5. [优先级与执行](#优先级与执行)
6. [高级特性](#高级特性)
7. [示例脚本](#示例脚本)

---

## 系统概述

指令系统是 IdleCOP 放置玩法的核心，允许玩家定义角色的自动行为逻辑。

### 设计目标

- **灵活性**：支持复杂的条件判断和行为组合
- **易用性**：提供可视化编辑器，无需编程知识
- **可扩展**：支持自定义条件和动作
- **高效性**：优化执行性能，支持大量指令同时运行

### 核心概念

```
指令 (Instruction)
├── 条件 (Condition) - 何时执行
├── 动作 (Action) - 做什么
├── 优先级 (Priority) - 执行顺序
└── 子指令 (Children) - 嵌套逻辑
```

---

## 指令结构

### 基础结构

```json
{
  "id": "string",           // 唯一标识符
  "name": "string",         // 显示名称
  "enabled": true,          // 是否启用
  "priority": 50,           // 优先级 (0-100)
  "condition": { },         // 条件定义
  "action": { },            // 动作定义
  "children": [ ],          // 子指令列表
  "cooldown": 0,            // 冷却时间(ms)
  "interruptible": true     // 是否可被打断
}
```

### 指令集

多条指令组成指令集：

```json
{
  "instructionSet": {
    "id": "set_001",
    "name": "我的战斗脚本",
    "description": "自动战斗和拾取",
    "version": 1,
    "author": "player001",
    "instructions": [ ... ]
  }
}
```

---

## 条件类型

### 基础条件

#### 属性条件

| 条件类型 | 参数 | 说明 |
|----------|------|------|
| `health_percent_below` | threshold | 生命百分比低于 |
| `health_percent_above` | threshold | 生命百分比高于 |
| `energy_below` | threshold | 能量低于 |
| `energy_above` | threshold | 能量高于 |
| `has_buff` | buffId | 拥有增益效果 |
| `has_debuff` | debuffId | 拥有减益效果 |

```json
{
  "type": "health_percent_below",
  "params": { "threshold": 30 }
}
```

#### 战斗条件

| 条件类型 | 参数 | 说明 |
|----------|------|------|
| `enemy_in_range` | range | 范围内有敌人 |
| `no_enemy_in_range` | range | 范围内无敌人 |
| `enemies_in_range_count` | range, minCount | 范围内敌人数量 |
| `target_health_below` | threshold | 目标生命低于 |
| `target_is_boss` | - | 目标是BOSS |
| `in_combat` | - | 正在战斗中 |

```json
{
  "type": "enemies_in_range_count",
  "params": { "range": 200, "minCount": 3 }
}
```

#### 技能条件

| 条件类型 | 参数 | 说明 |
|----------|------|------|
| `skill_ready` | skillId | 技能可用 |
| `skill_on_cooldown` | skillId | 技能冷却中 |
| `skill_charges_above` | skillId, count | 技能充能数量 |

```json
{
  "type": "skill_ready",
  "params": { "skillId": "skill_fireball" }
}
```

#### 物品条件

| 条件类型 | 参数 | 说明 |
|----------|------|------|
| `has_item` | itemType, count | 拥有物品 |
| `inventory_full_percent` | threshold | 背包占用百分比 |
| `loot_in_range` | range | 范围内有掉落物 |
| `equipment_durability_below` | slot, threshold | 装备耐久度低于 |

```json
{
  "type": "inventory_full_percent",
  "params": { "threshold": 90 }
}
```

#### 位置条件

| 条件类型 | 参数 | 说明 |
|----------|------|------|
| `in_zone` | zoneId | 在指定区域 |
| `near_waypoint` | range | 靠近传送点 |
| `near_npc` | npcType, range | 靠近NPC |
| `path_blocked` | - | 路径受阻 |

#### 时间条件

| 条件类型 | 参数 | 说明 |
|----------|------|------|
| `elapsed_time` | duration | 经过时间 |
| `time_since_action` | actionType, duration | 距上次动作时间 |

### 复合条件

#### AND 条件

所有子条件都满足时触发：

```json
{
  "type": "compound",
  "operator": "and",
  "children": [
    { "type": "health_percent_below", "params": { "threshold": 50 } },
    { "type": "has_item", "params": { "itemType": "health_potion", "count": 1 } }
  ]
}
```

#### OR 条件

任一子条件满足时触发：

```json
{
  "type": "compound",
  "operator": "or",
  "children": [
    { "type": "health_percent_below", "params": { "threshold": 20 } },
    { "type": "has_debuff", "params": { "debuffId": "poison" } }
  ]
}
```

#### NOT 条件

条件不满足时触发：

```json
{
  "type": "not",
  "child": {
    "type": "in_combat"
  }
}
```

### 特殊条件

| 条件类型 | 说明 |
|----------|------|
| `always` | 始终为真 |
| `never` | 始终为假 |
| `random_chance` | 随机概率 |
| `first_run` | 仅首次执行 |

```json
{
  "type": "random_chance",
  "params": { "chance": 25 }
}
```

---

## 动作类型

### 战斗动作

| 动作类型 | 参数 | 说明 |
|----------|------|------|
| `use_skill` | skillId, target | 使用技能 |
| `basic_attack` | target | 普通攻击 |
| `stop_attack` | - | 停止攻击 |
| `select_target` | criteria | 选择目标 |

```json
{
  "type": "use_skill",
  "params": {
    "skillId": "skill_fireball",
    "target": "nearest_enemy"
  }
}
```

**目标选择器：**
- `nearest_enemy` - 最近的敌人
- `lowest_health_enemy` - 血量最低的敌人
- `highest_level_enemy` - 等级最高的敌人
- `enemy_cluster_center` - 敌人密集中心
- `current_target` - 当前目标
- `self` - 自己
- `ally_lowest_health` - 血量最低的友方

### 移动动作

| 动作类型 | 参数 | 说明 |
|----------|------|------|
| `move_to` | position/target | 移动到位置 |
| `patrol` | mode | 巡逻 |
| `flee` | distance | 逃跑 |
| `follow` | targetId, distance | 跟随 |
| `stop_movement` | - | 停止移动 |

```json
{
  "type": "patrol",
  "params": {
    "mode": "explore_unexplored",
    "stayInZone": true
  }
}
```

**巡逻模式：**
- `explore_unexplored` - 探索未知区域
- `random_walk` - 随机移动
- `predefined_path` - 预设路线
- `clear_zone` - 清理区域所有怪物

### 物品动作

| 动作类型 | 参数 | 说明 |
|----------|------|------|
| `use_item` | itemType, preferHighest | 使用物品 |
| `pickup_loot` | filter | 拾取掉落物 |
| `drop_item` | itemId | 丢弃物品 |
| `equip_item` | itemId | 装备物品 |
| `sell_items` | filter | 出售物品 |

```json
{
  "type": "pickup_loot",
  "params": {
    "filter": {
      "minQuality": "magic",
      "includeCurrency": true,
      "maxDistance": 100
    }
  }
}
```

### 交互动作

| 动作类型 | 参数 | 说明 |
|----------|------|------|
| `use_portal_scroll` | - | 使用传送卷轴 |
| `return_to_map` | - | 返回地图 |
| `interact_npc` | npcId, action | 与NPC交互 |
| `open_chest` | - | 开启宝箱 |
| `use_waypoint` | destination | 使用传送点 |

```json
{
  "type": "interact_npc",
  "params": {
    "npcId": "npc_blacksmith",
    "action": "repair_all"
  }
}
```

### 控制动作

| 动作类型 | 参数 | 说明 |
|----------|------|------|
| `wait` | duration | 等待 |
| `sequence` | actions | 顺序执行多个动作 |
| `parallel` | actions | 并行执行多个动作 |
| `random` | actions | 随机执行一个动作 |
| `enable_instruction` | instructionId | 启用指令 |
| `disable_instruction` | instructionId | 禁用指令 |
| `switch_mode` | mode | 切换游戏模式 |

```json
{
  "type": "sequence",
  "params": {
    "actions": [
      { "type": "use_portal_scroll" },
      { "type": "wait", "params": { "duration": 3000 } },
      { "type": "sell_items", "params": { "filter": "normal" } },
      { "type": "return_to_map" }
    ]
  }
}
```

---

## 优先级与执行

### 执行流程

```
每个游戏Tick:
1. 获取所有启用的指令
2. 按优先级排序（高到低）
3. 遍历指令：
   a. 检查冷却
   b. 评估条件
   c. 如果条件满足，执行动作
   d. 根据配置决定是否继续检查
4. 处理子指令
```

### 优先级规则

- 优先级范围：0-100
- 数值越高越先执行
- 相同优先级按添加顺序执行
- 建议优先级分配：

| 优先级范围 | 用途 |
|------------|------|
| 90-100 | 紧急生存（使用药水、逃跑） |
| 70-89 | 增益维护（Buff刷新） |
| 50-69 | 主动战斗（技能释放） |
| 30-49 | 辅助行为（拾取、整理） |
| 10-29 | 常规行为（巡逻、探索） |
| 0-9 | 默认/兜底行为 |

### 执行模式

#### 阻断模式（默认）

高优先级指令执行后，跳过低优先级指令：

```json
{
  "executionMode": "interrupt",
  "instructions": [ ... ]
}
```

#### 并行模式

允许多条指令同时执行：

```json
{
  "executionMode": "parallel",
  "maxConcurrent": 3,
  "instructions": [ ... ]
}
```

---

## 高级特性

### 变量系统

支持在指令间共享变量：

```json
{
  "variables": {
    "farming_zone": "zone_03",
    "min_health_threshold": 30,
    "target_monster_type": "skeleton"
  },
  "instructions": [
    {
      "condition": {
        "type": "health_percent_below",
        "params": { "threshold": "$min_health_threshold" }
      }
    }
  ]
}
```

### 标签过滤

使用标签批量控制指令：

```json
{
  "id": "instr_001",
  "tags": ["combat", "aoe", "mana_heavy"],
  "condition": { ... }
}
```

批量操作：

```json
{
  "type": "disable_instructions_by_tag",
  "params": { "tag": "mana_heavy" }
}
```

### 条件缓存

对于计算密集的条件，可启用缓存：

```json
{
  "condition": {
    "type": "enemies_in_range_count",
    "params": { "range": 500, "minCount": 5 },
    "cache": {
      "enabled": true,
      "duration": 500
    }
  }
}
```

### 自定义脚本

高级用户可编写自定义逻辑：

```json
{
  "condition": {
    "type": "custom_script",
    "script": "return actor.Stats.Health / actor.Stats.MaxHealth < 0.3 && actor.HasItem('health_potion');"
  }
}
```

> 注：自定义脚本会在沙箱环境中执行，有访问限制。

---

## 示例脚本

### 示例1：自动刷怪脚本

```json
{
  "instructionSet": {
    "id": "auto_farm_basic",
    "name": "基础刷怪脚本",
    "description": "自动战斗、拾取、回城",
    "instructions": [
      {
        "id": "emergency_heal",
        "name": "紧急治疗",
        "priority": 100,
        "condition": {
          "type": "health_percent_below",
          "params": { "threshold": 25 }
        },
        "action": {
          "type": "use_item",
          "params": { "itemType": "health_potion" }
        }
      },
      {
        "id": "attack_enemy",
        "name": "攻击敌人",
        "priority": 50,
        "condition": {
          "type": "enemy_in_range",
          "params": { "range": 200 }
        },
        "action": {
          "type": "use_skill",
          "params": {
            "skillId": "skill_basic_attack",
            "target": "nearest_enemy"
          }
        }
      },
      {
        "id": "loot_items",
        "name": "拾取物品",
        "priority": 40,
        "condition": {
          "type": "compound",
          "operator": "and",
          "children": [
            { "type": "loot_in_range", "params": { "range": 100 } },
            { "type": "no_enemy_in_range", "params": { "range": 150 } }
          ]
        },
        "action": {
          "type": "pickup_loot",
          "params": { "filter": { "includeCurrency": true } }
        }
      },
      {
        "id": "return_town",
        "name": "回城整理",
        "priority": 30,
        "condition": {
          "type": "inventory_full_percent",
          "params": { "threshold": 90 }
        },
        "action": {
          "type": "sequence",
          "params": {
            "actions": [
              { "type": "use_portal_scroll" },
              { "type": "wait", "params": { "duration": 3000 } },
              { "type": "sell_items", "params": { "filter": "normal" } },
              { "type": "interact_npc", "params": { "npcId": "npc_stash", "action": "deposit_rare" } },
              { "type": "return_to_map" }
            ]
          }
        }
      },
      {
        "id": "explore",
        "name": "探索地图",
        "priority": 10,
        "condition": { "type": "always" },
        "action": {
          "type": "patrol",
          "params": { "mode": "explore_unexplored" }
        }
      }
    ]
  }
}
```

### 示例2：BOSS战专用脚本

```json
{
  "instructionSet": {
    "id": "boss_fight",
    "name": "BOSS战脚本",
    "instructions": [
      {
        "id": "dodge_skill",
        "name": "躲避BOSS技能",
        "priority": 95,
        "condition": {
          "type": "boss_casting_skill",
          "params": { "dangerLevel": "high" }
        },
        "action": {
          "type": "use_skill",
          "params": { "skillId": "skill_dodge_roll", "target": "safe_position" }
        }
      },
      {
        "id": "heal_during_boss",
        "name": "战斗中回复",
        "priority": 90,
        "condition": {
          "type": "health_percent_below",
          "params": { "threshold": 50 }
        },
        "action": {
          "type": "use_item",
          "params": { "itemType": "health_potion" }
        }
      },
      {
        "id": "buff_self",
        "name": "自我增益",
        "priority": 80,
        "cooldown": 30000,
        "condition": {
          "type": "compound",
          "operator": "and",
          "children": [
            { "type": "in_combat" },
            { "type": "not", "child": { "type": "has_buff", "params": { "buffId": "warcry" } } }
          ]
        },
        "action": {
          "type": "use_skill",
          "params": { "skillId": "skill_warcry" }
        }
      },
      {
        "id": "burst_damage",
        "name": "爆发输出",
        "priority": 70,
        "condition": {
          "type": "target_is_boss"
        },
        "action": {
          "type": "sequence",
          "params": {
            "actions": [
              { "type": "use_skill", "params": { "skillId": "skill_charge" } },
              { "type": "use_skill", "params": { "skillId": "skill_whirlwind" } }
            ]
          }
        }
      }
    ]
  }
}
```

---

## 可视化编辑器

指令系统配套可视化编辑器，支持：

- 拖拽式指令创建
- 条件/动作参数配置
- 实时预览和测试
- 指令导入/导出
- 模板库

### 编辑器界面结构

```
┌────────────────────────────────────────────────────┐
│  工具栏: [新建] [保存] [导入] [导出] [测试]           │
├──────────────┬─────────────────────────────────────┤
│  指令列表    │         指令编辑区                    │
│  ┌────────┐  │  ┌─────────────────────────────────┐│
│  │指令1   │  │  │ 名称: _______________            ││
│  │指令2   │  │  │ 优先级: [50]                     ││
│  │指令3   │  │  │                                  ││
│  │  ...   │  │  │ 条件:                            ││
│  └────────┘  │  │ [+] 添加条件                     ││
│              │  │                                  ││
│  [+] 添加    │  │ 动作:                            ││
│              │  │ [选择动作类型 ▼]                 ││
│              │  └─────────────────────────────────┘│
├──────────────┴─────────────────────────────────────┤
│  测试面板: [运行] [暂停] [单步]     日志输出区       │
└────────────────────────────────────────────────────┘
```

---

## 相关文档

- [玩法细节](docs-gameplay.md)
- [数据模型](docs-data-models.md)
- [开发路线图](docs-roadmap.md)
