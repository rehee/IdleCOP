# IdleCOP - ARPG 放置类游戏

> 一款融合暗黑破坏神、流放之路与荣誉圣殿特色的ARPG放置类游戏

## 项目简介

IdleCOP 是一款高自由度装备、技能与指令体系的ARPG放置类游戏。游戏支持放置模式（角色自动按指令执行）和手操模式（玩家实时输入），提供丰富的装备词缀系统、技能联动和程序化生成的地图体验。

## 核心特性

- **装备系统**：随机掉落装备，区分底材、品质（白/蓝/紫/橙）、词缀（前缀/后缀）
- **程序化地图**：每场游戏随机生成，包含多种区域和怪物配置
- **技能系统**：主动技能、辅助技能、触发技能，支持多样化配置与动态联动
- **通货系统**：多种可堆叠小通货，用于交易、合成、物品刷新
- **指令脚本**：支持玩家自定义指令序列，实现自动化策略行为
- **双模式切换**：放置模式与手操模式无缝切换

## 技术架构

- **语言**：C#
- **前端框架**：Bootstrap Blazor
- **跨平台支持**：
  - 网页端
  - 桌面端（WPF Blazor 混合）
  - 移动端（MAUI Blazor 混合）
- **游戏引擎兼容**：核心代码可在 Godot、Unreal 引擎中使用（后续扩展）

## 项目结构

```
IdleCOP/
├── README.md                 # 项目说明
├── DESIGN.md                 # 主设计文档
├── CONTRIBUTING.md           # 贡献规范
├── docs-gameplay.md          # 玩法细节分解
├── docs-data-models.md       # 数据模型示例
├── docs-ai-instructions.md   # 指令/行为脚本详细说明
├── docs-roadmap.md           # 功能迭代与时间线
├── IdleCOP.sln               # 解决方案文件
├── configs/                  # 配置文件
│   ├── equipment/            # 装备配置
│   ├── skills/               # 技能配置
│   ├── effects/              # 效果配置
│   ├── strategies/           # 策略配置
│   └── maps/                 # 地图配置
├── src/
│   ├── Idle.Utility/         # 通用工具库
│   ├── Idle.Core/            # 核心基础库
│   ├── IdleCOP.Gameplay/     # 游戏玩法实现
│   ├── IdleCOP.AI/           # AI与行为
│   ├── IdleCOP.Data/         # 数据管理
│   └── IdleCOP.Client/       # 客户端
│       └── Web/              # Blazor Web
└── .github/
    └── ISSUE_TEMPLATE.md     # 提案/任务模板
```

## 快速开始

### 前置要求

- .NET 8.0 SDK 或更高版本
- Visual Studio 2022 或 VS Code

### 安装与运行

```bash
# 克隆仓库
git clone https://github.com/rehee/IdleCOP.git
cd IdleCOP

# 恢复依赖
dotnet restore

# 构建项目
dotnet build

# 运行 Web 客户端
cd src/IdleCOP.Client/Web
dotnet run
```

## 文档导航

- [设计文档](DESIGN.md) - 系统接口与架构设计
- [玩法细节](docs-gameplay.md) - 游戏玩法详细说明
- [数据模型](docs-data-models.md) - 数据结构示例
- [AI指令系统](docs-ai-instructions.md) - 行为脚本说明
- [开发路线图](docs-roadmap.md) - 功能迭代计划
- [贡献指南](CONTRIBUTING.md) - 如何参与项目

## 开发里程碑

| 阶段 | 目标 | 状态 |
|------|------|------|
| 阶段0 | 固化设计文档与仓库结构 | 进行中 |
| 阶段1 | 实现地图、演员、基础装备/技能/指令系统 | 计划中 |
| 阶段2 | UX编辑器、账号系统、存储同步 | 计划中 |
| 阶段3 | 多人交易、扩展内容、平衡 | 计划中 |

## 许可证

待定

## 联系我们

如有问题或建议，请通过 [Issues](https://github.com/rehee/IdleCOP/issues) 提交。
