# 数据模型

> 本文档定义 IdleCOP 的核心数据模型及 JSON 示例

## 目录

1. [Actor 演员模型](#actor-演员模型)
2. [Item 物品模型](#item-物品模型)
3. [Skill 技能模型](#skill-技能模型)
4. [Currency 通货模型](#currency-通货模型)
5. [Map 地图模型](#map-地图模型)
6. [Instruction 指令模型](#instruction-指令模型)
7. [Account 账号模型](#account-账号模型)

---

## Actor 演员模型

所有游戏实体的基础模型。

### 模型定义

```typescript
interface Actor {
  id: string;                    // 唯一标识符
  type: ActorType;               // 演员类型
  name: string;                  // 显示名称
  level: number;                 // 等级
  stats: Stats;                  // 属性数据
  inventory: Inventory;          // 背包/装备
  instructions: Instruction[];   // 行为指令
  aiState: AIState;              // AI状态
  position: Vector2;             // 位置坐标
  rotation: number;              // 朝向角度
}

enum ActorType {
  Player = "player",
  NPC = "npc",
  Monster = "monster",
  Projectile = "projectile",
  Environment = "environment"
}
```

### JSON 示例

```json
{
  "id": "player_001",
  "type": "player",
  "name": "勇者小明",
  "level": 45,
  "stats": {
    "strength": 150,
    "dexterity": 80,
    "intelligence": 60,
    "maxHealth": 3500,
    "currentHealth": 3200,
    "maxEnergy": 200,
    "currentEnergy": 180,
    "armor": 450,
    "evasion": 120,
    "resistances": {
      "fire": 45,
      "cold": 30,
      "lightning": 25,
      "chaos": 10
    }
  },
  "inventory": {
    "equipment": {
      "helmet": "item_helm_001",
      "chest": "item_chest_001",
      "mainHand": "item_sword_001",
      "offHand": "item_shield_001"
    },
    "bag": ["item_potion_001", "item_scroll_001"],
    "bagSize": 60
  },
  "instructions": ["instr_001", "instr_002"],
  "aiState": {
    "currentState": "idle",
    "targetId": null,
    "lastAction": "patrol"
  },
  "position": { "x": 100.5, "y": 200.3 },
  "rotation": 45.0
}
```

### 怪物示例

```json
{
  "id": "monster_skeleton_001",
  "type": "monster",
  "name": "骷髅战士",
  "level": 20,
  "stats": {
    "strength": 40,
    "dexterity": 30,
    "intelligence": 10,
    "maxHealth": 800,
    "currentHealth": 800,
    "maxEnergy": 50,
    "currentEnergy": 50,
    "armor": 100,
    "evasion": 50,
    "resistances": {
      "fire": 0,
      "cold": 50,
      "lightning": 0,
      "chaos": 75
    }
  },
  "inventory": {
    "equipment": {
      "mainHand": "item_rusty_sword"
    },
    "dropTable": "droptable_skeleton_basic"
  },
  "instructions": [],
  "aiState": {
    "currentState": "patrol",
    "aggroRange": 150,
    "leashRange": 500
  },
  "position": { "x": 450.0, "y": 320.0 },
  "rotation": 180.0
}
```

---

## Item 物品模型

装备和消耗品的数据模型。

### 模型定义

```typescript
interface Item {
  id: string;                    // 唯一标识符
  baseType: string;              // 底材类型ID
  quality: Quality;              // 品质等级
  affixes: Affix[];              // 词缀列表
  level: number;                 // 物品等级
  requiredLevel: number;         // 需求等级
  sockets: Socket[];             // 镶嵌孔
  identified: boolean;           // 是否已鉴定
}

enum Quality {
  Normal = "normal",
  Magic = "magic",
  Rare = "rare",
  Legendary = "legendary"
}

interface Affix {
  id: string;
  type: "prefix" | "suffix";
  tier: number;
  values: number[];
}

interface Socket {
  id: string;
  color: "red" | "green" | "blue" | "white";
  gemId: string | null;
  linked: boolean;
}
```

### JSON 示例 - 稀有武器

```json
{
  "id": "item_sword_001",
  "baseType": "base_longsword",
  "quality": "rare",
  "name": "凶残的巨人长剑",
  "level": 45,
  "requiredLevel": 42,
  "identified": true,
  "affixes": [
    {
      "id": "affix_phys_dmg_percent",
      "type": "prefix",
      "name": "凶残的",
      "tier": 1,
      "values": [28],
      "description": "增加 28% 物理伤害"
    },
    {
      "id": "affix_flat_phys_dmg",
      "type": "prefix",
      "name": "锋利的",
      "tier": 2,
      "values": [15, 25],
      "description": "附加 15-25 物理伤害"
    },
    {
      "id": "affix_max_life",
      "type": "suffix",
      "name": "巨人",
      "tier": 1,
      "values": [95],
      "description": "增加 95 最大生命"
    },
    {
      "id": "affix_attack_speed",
      "type": "suffix",
      "name": "速度",
      "tier": 2,
      "values": [12],
      "description": "增加 12% 攻击速度"
    }
  ],
  "sockets": [
    { "id": "socket_1", "color": "red", "gemId": "gem_melee_splash", "linked": true },
    { "id": "socket_2", "color": "red", "gemId": null, "linked": true },
    { "id": "socket_3", "color": "green", "gemId": null, "linked": false }
  ],
  "baseStats": {
    "physicalDamage": { "min": 45, "max": 85 },
    "attackSpeed": 1.2,
    "criticalChance": 5.0
  }
}
```

### JSON 示例 - 传奇装备

```json
{
  "id": "item_unique_chest_001",
  "baseType": "base_plate_armor",
  "quality": "legendary",
  "name": "不灭铠甲",
  "level": 68,
  "requiredLevel": 65,
  "identified": true,
  "uniqueProperties": [
    {
      "description": "受到致命伤害时，有 20% 几率免疫该次伤害",
      "trigger": "on_fatal_damage",
      "chance": 20
    },
    {
      "description": "每秒回复 2% 最大生命",
      "type": "life_regen_percent",
      "value": 2
    }
  ],
  "affixes": [
    {
      "id": "affix_max_life_percent",
      "type": "prefix",
      "tier": 1,
      "values": [15],
      "description": "增加 15% 最大生命"
    },
    {
      "id": "affix_all_resistances",
      "type": "suffix",
      "tier": 1,
      "values": [20],
      "description": "所有元素抗性 +20%"
    }
  ],
  "sockets": [],
  "baseStats": {
    "armor": 850,
    "energyShield": 0
  },
  "flavorText": "传说中永不破碎的神圣铠甲，守护着无数勇士的生命。"
}
```

---

## Skill 技能模型

技能系统的数据模型。

### 模型定义

```typescript
interface Skill {
  id: string;
  type: SkillType;
  name: string;
  description: string;
  params: SkillParams;
  supports: string[];            // 辅助技能ID列表
  tags: string[];                // 技能标签
}

enum SkillType {
  Active = "active",
  Support = "support",
  Trigger = "trigger"
}

interface SkillParams {
  manaCost?: number;
  cooldown?: number;
  castTime?: number;
  damageMultiplier?: number;
  areaOfEffect?: number;
  projectileCount?: number;
  // ... 其他参数
}
```

### JSON 示例 - 主动技能

```json
{
  "id": "skill_fireball",
  "type": "active",
  "name": "火球术",
  "description": "发射一颗火球，对敌人造成火焰伤害并有几率点燃目标",
  "icon": "icons/skills/fireball.png",
  "tags": ["spell", "projectile", "fire", "area"],
  "params": {
    "manaCost": 25,
    "cooldown": 0.5,
    "castTime": 0.3,
    "damageBase": 50,
    "damageMultiplier": 1.5,
    "fireConversion": 100,
    "projectileSpeed": 800,
    "areaOfEffect": 80,
    "igniteChance": 20
  },
  "supports": ["skill_support_multi_proj", "skill_support_burning"],
  "levelScaling": {
    "damageBase": 10,
    "areaOfEffect": 2
  },
  "requirements": {
    "level": 5,
    "intelligence": 30
  }
}
```

### JSON 示例 - 辅助技能

```json
{
  "id": "skill_support_multi_proj",
  "type": "support",
  "name": "多重投射",
  "description": "使关联技能发射额外的投射物",
  "icon": "icons/skills/multi_projectile.png",
  "tags": ["support", "projectile"],
  "supportedTags": ["projectile"],
  "params": {
    "manaMultiplier": 1.4,
    "damageMultiplier": 0.7,
    "additionalProjectiles": 2
  },
  "levelScaling": {
    "damageMultiplier": 0.02,
    "additionalProjectiles": 0.1
  }
}
```

### JSON 示例 - 触发技能

```json
{
  "id": "skill_trigger_on_kill_explode",
  "type": "trigger",
  "name": "击杀爆炸",
  "description": "击杀敌人时，使其尸体爆炸对周围敌人造成伤害",
  "icon": "icons/skills/corpse_explosion.png",
  "tags": ["trigger", "area", "fire"],
  "triggerCondition": {
    "type": "on_kill",
    "chance": 100,
    "cooldown": 0
  },
  "params": {
    "damagePercent": 8,
    "areaOfEffect": 150,
    "fireConversion": 50
  }
}
```

---

## Currency 通货模型

通货系统的数据模型。

### 模型定义

```typescript
interface Currency {
  id: string;
  type: CurrencyType;
  name: string;
  description: string;
  amount: number;
  isShared: boolean;             // 是否账号共享
  maxStack: number;
}

enum CurrencyType {
  Crafting = "crafting",
  Trading = "trading",
  Special = "special"
}
```

### JSON 示例

```json
{
  "currencies": [
    {
      "id": "currency_transmutation",
      "type": "crafting",
      "name": "蜕变石",
      "description": "将普通装备升级为魔法装备",
      "icon": "icons/currency/transmutation.png",
      "amount": 150,
      "isShared": true,
      "maxStack": 9999
    },
    {
      "id": "currency_alteration",
      "type": "crafting",
      "name": "改造石",
      "description": "重新随机魔法装备的词缀",
      "icon": "icons/currency/alteration.png",
      "amount": 85,
      "isShared": true,
      "maxStack": 9999
    },
    {
      "id": "currency_augmentation",
      "type": "crafting",
      "name": "增幅石",
      "description": "为魔法装备添加一条词缀",
      "icon": "icons/currency/augmentation.png",
      "amount": 42,
      "isShared": true,
      "maxStack": 9999
    },
    {
      "id": "currency_regal",
      "type": "crafting",
      "name": "富豪石",
      "description": "将魔法装备升级为稀有装备",
      "icon": "icons/currency/regal.png",
      "amount": 12,
      "isShared": true,
      "maxStack": 9999
    },
    {
      "id": "currency_chaos",
      "type": "crafting",
      "name": "混沌石",
      "description": "重新随机稀有装备的词缀",
      "icon": "icons/currency/chaos.png",
      "amount": 28,
      "isShared": true,
      "maxStack": 9999
    },
    {
      "id": "currency_exalted",
      "type": "crafting",
      "name": "崇高石",
      "description": "为稀有装备添加一条词缀",
      "icon": "icons/currency/exalted.png",
      "amount": 3,
      "isShared": true,
      "maxStack": 9999
    },
    {
      "id": "currency_divine",
      "type": "crafting",
      "name": "神圣石",
      "description": "重新随机装备词缀的数值范围",
      "icon": "icons/currency/divine.png",
      "amount": 1,
      "isShared": true,
      "maxStack": 9999
    }
  ]
}
```

---

## Map 地图模型

程序化生成地图的数据模型。

### 模型定义

```typescript
interface GameMap {
  id: string;
  seed: string;
  name: string;
  level: number;
  zones: Zone[];
  spawns: SpawnPoint[];
  layout: MapLayout;
}

interface Zone {
  id: string;
  type: ZoneType;
  bounds: Rectangle;
  tiles: Tile[][];
  connections: string[];
}

interface SpawnPoint {
  id: string;
  position: Vector2;
  spawnType: "player" | "monster" | "boss";
  monsterPool?: string;
}
```

### JSON 示例

```json
{
  "id": "map_dungeon_001",
  "seed": "a1b2c3d4e5f6",
  "name": "遗忘地下城 - 层级3",
  "level": 35,
  "difficulty": "normal",
  "modifiers": [
    { "type": "monster_damage", "value": 20 },
    { "type": "monster_life", "value": 30 }
  ],
  "zones": [
    {
      "id": "zone_entrance",
      "type": "entrance",
      "name": "地下城入口",
      "bounds": { "x": 0, "y": 0, "width": 50, "height": 50 },
      "connections": ["zone_hallway_01"],
      "spawns": [
        {
          "id": "spawn_player",
          "position": { "x": 25, "y": 25 },
          "spawnType": "player"
        }
      ]
    },
    {
      "id": "zone_hallway_01",
      "type": "hallway",
      "name": "幽暗走廊",
      "bounds": { "x": 50, "y": 0, "width": 100, "height": 30 },
      "connections": ["zone_entrance", "zone_room_01"],
      "spawns": [
        {
          "id": "spawn_monsters_01",
          "position": { "x": 80, "y": 15 },
          "spawnType": "monster",
          "monsterPool": "pool_skeleton_basic",
          "count": { "min": 3, "max": 5 }
        }
      ]
    },
    {
      "id": "zone_boss_room",
      "type": "boss",
      "name": "骷髅王座",
      "bounds": { "x": 200, "y": 0, "width": 80, "height": 80 },
      "connections": ["zone_room_02"],
      "spawns": [
        {
          "id": "spawn_boss",
          "position": { "x": 240, "y": 40 },
          "spawnType": "boss",
          "bossId": "boss_skeleton_king"
        }
      ]
    }
  ],
  "layout": {
    "theme": "dungeon",
    "tileset": "tileset_stone_dungeon",
    "lighting": "dark",
    "ambientColor": "#1a1a2e"
  },
  "rewards": {
    "experience": 5000,
    "dropMultiplier": 1.2
  }
}
```

---

## Instruction 指令模型

行为脚本系统的数据模型。

### 模型定义

```typescript
interface Instruction {
  id: string;
  name: string;
  condition: Condition;
  action: Action;
  children: Instruction[];
  priority: number;
  enabled: boolean;
}

interface Condition {
  type: ConditionType;
  params: Record<string, any>;
  operator?: "and" | "or";
  children?: Condition[];
}

interface Action {
  type: ActionType;
  params: Record<string, any>;
}
```

### JSON 示例 - 完整指令集

```json
{
  "instructionSet": {
    "id": "instrset_player_001",
    "name": "自动战斗脚本",
    "version": 1,
    "instructions": [
      {
        "id": "instr_001",
        "name": "生命危急时使用药水",
        "priority": 100,
        "enabled": true,
        "condition": {
          "type": "health_percent_below",
          "params": { "threshold": 30 }
        },
        "action": {
          "type": "use_item",
          "params": { 
            "itemType": "health_potion",
            "preferHighest": true
          }
        },
        "children": []
      },
      {
        "id": "instr_002",
        "name": "攻击最近的敌人",
        "priority": 50,
        "enabled": true,
        "condition": {
          "type": "compound",
          "operator": "and",
          "children": [
            {
              "type": "enemy_in_range",
              "params": { "range": 200 }
            },
            {
              "type": "skill_ready",
              "params": { "skillId": "skill_basic_attack" }
            }
          ]
        },
        "action": {
          "type": "use_skill",
          "params": {
            "skillId": "skill_basic_attack",
            "target": "nearest_enemy"
          }
        },
        "children": []
      },
      {
        "id": "instr_003",
        "name": "使用火球术攻击群怪",
        "priority": 60,
        "enabled": true,
        "condition": {
          "type": "compound",
          "operator": "and",
          "children": [
            {
              "type": "enemies_in_range_count",
              "params": { "range": 300, "minCount": 3 }
            },
            {
              "type": "skill_ready",
              "params": { "skillId": "skill_fireball" }
            },
            {
              "type": "energy_above",
              "params": { "threshold": 25 }
            }
          ]
        },
        "action": {
          "type": "use_skill",
          "params": {
            "skillId": "skill_fireball",
            "target": "enemy_cluster_center"
          }
        },
        "children": []
      },
      {
        "id": "instr_004",
        "name": "拾取附近掉落物",
        "priority": 40,
        "enabled": true,
        "condition": {
          "type": "compound",
          "operator": "and",
          "children": [
            {
              "type": "loot_in_range",
              "params": { "range": 100 }
            },
            {
              "type": "no_enemy_in_range",
              "params": { "range": 150 }
            }
          ]
        },
        "action": {
          "type": "pickup_loot",
          "params": {
            "filter": {
              "minQuality": "magic",
              "includeCurrency": true
            }
          }
        },
        "children": []
      },
      {
        "id": "instr_005",
        "name": "背包满时回城",
        "priority": 30,
        "enabled": true,
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
              { "type": "sell_items", "params": { "filter": "normal_magic" } },
              { "type": "return_to_map" }
            ]
          }
        },
        "children": []
      },
      {
        "id": "instr_006",
        "name": "巡逻探索",
        "priority": 10,
        "enabled": true,
        "condition": {
          "type": "always"
        },
        "action": {
          "type": "patrol",
          "params": {
            "mode": "explore_unexplored",
            "stayInZone": true
          }
        },
        "children": []
      }
    ]
  }
}
```

---

## Account 账号模型

账号和存档数据模型。

### 模型定义

```typescript
interface Account {
  id: string;
  username: string;
  characters: Character[];
  sharedStash: Stash;
  currencies: Currency[];
  settings: AccountSettings;
  statistics: Statistics;
}

interface Character {
  id: string;
  name: string;
  class: string;
  level: number;
  experience: number;
  actorData: Actor;
  skillLoadout: SkillLoadout;
  instructionSet: string;
}
```

### JSON 示例

```json
{
  "account": {
    "id": "account_001",
    "username": "player@example.com",
    "createdAt": "2024-01-15T10:30:00Z",
    "lastLogin": "2024-12-01T18:45:00Z",
    "characters": [
      {
        "id": "char_001",
        "name": "勇者小明",
        "class": "warrior",
        "level": 45,
        "experience": 1250000,
        "playTime": 86400,
        "actorId": "player_001",
        "skillLoadout": {
          "slot1": "skill_whirlwind",
          "slot2": "skill_warcry",
          "slot3": "skill_leap",
          "slot4": "skill_fireball"
        },
        "instructionSetId": "instrset_player_001",
        "lastMapId": "map_dungeon_001",
        "lastPosition": { "x": 100.5, "y": 200.3 }
      },
      {
        "id": "char_002",
        "name": "法师小红",
        "class": "mage",
        "level": 32,
        "experience": 450000,
        "playTime": 43200,
        "actorId": "player_002"
      }
    ],
    "sharedStash": {
      "tabs": [
        {
          "id": "stash_tab_001",
          "name": "装备",
          "type": "normal",
          "items": ["item_sword_001", "item_chest_002"]
        },
        {
          "id": "stash_tab_002",
          "name": "通货",
          "type": "currency",
          "currencies": "see_currencies_array"
        }
      ]
    },
    "currencies": [
      { "id": "currency_chaos", "amount": 28 },
      { "id": "currency_exalted", "amount": 3 }
    ],
    "settings": {
      "language": "zh-CN",
      "autoLoot": true,
      "autoSell": "normal",
      "showDamageNumbers": true
    },
    "statistics": {
      "totalPlayTime": 129600,
      "monstersKilled": 15680,
      "bossesKilled": 42,
      "itemsLooted": 8500,
      "highestLevel": 45
    }
  }
}
```

---

## 附录

### 数据类型参考

| 类型 | 说明 |
|------|------|
| Vector2 | `{ x: number, y: number }` |
| Rectangle | `{ x, y, width, height }` |
| Color | `"#RRGGBB"` 格式 |
| Timestamp | ISO 8601 格式字符串 |

### 相关文档

- [玩法细节](docs-gameplay.md)
- [AI指令系统](docs-ai-instructions.md)
- [开发路线图](docs-roadmap.md)
