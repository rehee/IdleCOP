# 贡献指南

感谢您对 IdleCOP 项目的关注！我们欢迎各种形式的贡献。

## 目录

1. [行为准则](#行为准则)
2. [如何贡献](#如何贡献)
3. [开发环境设置](#开发环境设置)
4. [提交规范](#提交规范)
5. [代码风格](#代码风格)
6. [Pull Request 流程](#pull-request-流程)

---

## 行为准则

请保持友善、尊重他人。我们致力于为所有人提供一个包容和友好的社区环境。

---

## 如何贡献

### 报告问题

1. 检查是否已存在相同的 Issue
2. 使用 Issue 模板创建新问题
3. 提供尽可能详细的复现步骤

### 功能建议

1. 在 Issues 中搜索是否已有类似提案
2. 创建新 Issue 并使用"功能建议"模板
3. 详细描述功能需求和使用场景

### 代码贡献

1. Fork 本仓库
2. 创建功能分支 (`git checkout -b feature/amazing-feature`)
3. 提交更改 (`git commit -m 'feat: add amazing feature'`)
4. 推送到分支 (`git push origin feature/amazing-feature`)
5. 创建 Pull Request

---

## 开发环境设置

### 前置要求

- .NET 8.0 SDK 或更高版本
- Visual Studio 2022 或 VS Code
- Git

### 安装步骤

```bash
# 克隆仓库
git clone https://github.com/rehee/IdleCOP.git
cd IdleCOP

# 恢复依赖（待项目结构建立后）
dotnet restore

# 构建项目
dotnet build

# 运行测试
dotnet test
```

---

## 提交规范

我们遵循 [Conventional Commits](https://www.conventionalcommits.org/) 规范。

### 提交格式

```
<type>(<scope>): <subject>

<body>

<footer>
```

### 类型 (Type)

| 类型 | 说明 |
|------|------|
| `feat` | 新功能 |
| `fix` | Bug 修复 |
| `docs` | 文档更新 |
| `style` | 代码格式化（不影响功能） |
| `refactor` | 代码重构 |
| `test` | 测试相关 |
| `chore` | 构建/工具链更新 |

### 示例

```
feat(equipment): add affix generation system

- Implement prefix and suffix generation
- Add tier-based affix selection
- Include drop rate calculations

Closes #123
```

---

## 代码风格

### C# 代码规范

- 遵循 [Microsoft C# 编码约定](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- 使用 `PascalCase` 命名公共成员
- 使用 `camelCase` 命名私有成员（不带 `_` 前缀）
- 枚举类必须以 `Enum` 开头，默认值必须为 `NotSpecified`
- 代码缩进使用 2 个空格
- 保持方法简短，单一职责
- **所有扩展方法必须命名为 `XXXHelper` 并放在 `Helpers` 目录下**
- **所有实体、DTO、Component 都以 `Guid` 作为 Id**
- **持久化数据保存为 `XXXEntity`，Entity 根据对应的 Profile 生成 `IdleComponent`**

### Component-Profile 方法对应规范

Profile 的方法签名必须与 Component 的方法签名对应，但需要额外接收 `IdleComponent` 参数：

| Component 方法 | Profile 对应方法 |
|---------------|-----------------|
| `OnTick(TickContext context)` | `OnTick(IdleComponent component, TickContext context)` |

### 示例

```csharp
public class EquipmentService
{
  private readonly ILootGenerator lootGenerator;
  private readonly IDropTable dropTable;

  public EquipmentService(ILootGenerator lootGenerator, IDropTable dropTable)
  {
    this.lootGenerator = lootGenerator;
    this.dropTable = dropTable;
  }

  public Equipment GenerateEquipment(LootContext context)
  {
    var possibleDrops = dropTable.GetPossibleDrops(context.ZoneId, context.Level);
    return lootGenerator.GenerateEquipment(context);
  }
}

// 枚举命名示例
public enum EnumQuality
{
  NotSpecified = 0,
  Normal,
  Magic,
  Rare,
  Legendary,
  Unique
}

// Profile 必须实现 IWithName 接口
public abstract class IdleProfile : IWithName
{
  public abstract int Key { get; }
  public virtual int? KeyOverride { get; }  // Key为0时使用
  public abstract string? Name { get; }
  public abstract string? Description { get; }
  
  public abstract void OnTick(IdleComponent component, TickContext context);
}
```

### 文档规范

- 公共 API 必须有 XML 文档注释
- 复杂逻辑需添加行内注释
- README 和设计文档使用中文

---

## Pull Request 流程

### 提交前检查

- [ ] 代码通过所有测试
- [ ] 新功能有对应的单元测试
- [ ] 代码符合项目风格规范
- [ ] 文档已更新（如适用）

### PR 模板

```markdown
## 变更描述

简要描述本 PR 的变更内容

## 变更类型

- [ ] 新功能
- [ ] Bug 修复
- [ ] 文档更新
- [ ] 重构
- [ ] 其他

## 测试

描述如何测试这些变更

## 关联 Issue

Closes #xxx
```

### 代码审查

- 至少需要 1 位维护者审查通过
- 解决所有审查意见后方可合并
- 使用 Squash and Merge 策略

---

## 问题与帮助

如有任何问题，请通过以下方式联系：

- 提交 [Issue](https://github.com/rehee/IdleCOP/issues)
- 在相关 Issue 中留言讨论

感谢您的贡献！
