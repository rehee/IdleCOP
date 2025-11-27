#!/bin/bash

# IdleCOP 完整项目初始化脚本
# 此脚本将创建所有必要的目录和文件

set -e

echo "=== IdleCOP Project Initialization ==="
echo ""

# Create all directories
echo "Creating directory structure..."

mkdir -p src/Idle.Utility/Helpers
mkdir -p src/Idle.Core/Components
mkdir -p src/Idle.Core/Profiles
mkdir -p src/Idle.Core/Context
mkdir -p src/Idle.Core/Repository
mkdir -p src/Idle.Core/DI
mkdir -p src/IdleCOP.Gameplay/Combat
mkdir -p src/IdleCOP.Gameplay/Skills
mkdir -p src/IdleCOP.Gameplay/Equipment
mkdir -p src/IdleCOP.Gameplay/Maps
mkdir -p src/IdleCOP.Gameplay/Instructions
mkdir -p src/IdleCOP.AI/Strategies
mkdir -p src/IdleCOP.Data/Entities
mkdir -p src/IdleCOP.Data/DTOs
mkdir -p src/IdleCOP.Data/Configs
mkdir -p src/IdleCOP.Console
mkdir -p tests/Idle.Utility.Tests
mkdir -p tests/Idle.Core.Tests
mkdir -p tests/IdleCOP.Gameplay.Tests
mkdir -p tests/IdleCOP.AI.Tests
mkdir -p tests/IdleCOP.Data.Tests

echo "Directories created."
echo ""

# ========================================
# src/Idle.Utility/Idle.Utility.csproj
# ========================================
echo "Creating Idle.Utility project..."

cat > src/Idle.Utility/Idle.Utility.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>Idle.Utility</RootNamespace>
    <Description>通用工具库（独立，无游戏依赖）</Description>
  </PropertyGroup>

</Project>
EOF

# src/Idle.Utility/Helpers/TickHelper.cs
cat > src/Idle.Utility/Helpers/TickHelper.cs << 'EOF'
namespace Idle.Utility.Helpers;

/// <summary>
/// Tick 相关的帮助方法
/// </summary>
public static class TickHelper
{
  /// <summary>
  /// 默认每秒的 Tick 数量
  /// </summary>
  public const int DefaultTickRate = 30;

  /// <summary>
  /// 将秒数转换为 Tick 数
  /// </summary>
  /// <param name="seconds">秒数</param>
  /// <param name="tickRate">每秒 Tick 数，默认为 30</param>
  /// <returns>对应的 Tick 数</returns>
  public static int SecondsToTicks(float seconds, int tickRate = DefaultTickRate)
  {
    return (int)(seconds * tickRate);
  }

  /// <summary>
  /// 将 Tick 数转换为秒数
  /// </summary>
  /// <param name="ticks">Tick 数</param>
  /// <param name="tickRate">每秒 Tick 数，默认为 30</param>
  /// <returns>对应的秒数</returns>
  public static float TicksToSeconds(int ticks, int tickRate = DefaultTickRate)
  {
    return (float)ticks / tickRate;
  }
}
EOF

# ========================================
# src/Idle.Core/Idle.Core.csproj
# ========================================
echo "Creating Idle.Core project..."

cat > src/Idle.Core/Idle.Core.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>Idle.Core</RootNamespace>
    <Description>核心基础库（通用玩法框架）</Description>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Idle.Utility\Idle.Utility.csproj" />
  </ItemGroup>

</Project>
EOF

# src/Idle.Core/Components/IdleComponent.cs
cat > src/Idle.Core/Components/IdleComponent.cs << 'EOF'
namespace Idle.Core.Components;

/// <summary>
/// 游戏中实例化的数据对象，保存数据和状态
/// </summary>
public class IdleComponent
{
  /// <summary>
  /// 唯一标识符
  /// </summary>
  public Guid Id { get; set; } = Guid.NewGuid();

  /// <summary>
  /// Profile 的 Key 值
  /// </summary>
  public int ProfileKey { get; set; }

  /// <summary>
  /// 父组件
  /// </summary>
  public IdleComponent? Parent { get; private set; }

  /// <summary>
  /// 子组件列表
  /// </summary>
  public List<IdleComponent> Children { get; } = new();

  /// <summary>
  /// 设置父组件
  /// </summary>
  /// <param name="newParent">新的父组件</param>
  public void SetParent(IdleComponent? newParent)
  {
    if (Parent != null)
    {
      Parent.Children.Remove(this);
    }

    Parent = newParent;

    if (newParent != null)
    {
      newParent.Children.Add(this);
    }
  }

  /// <summary>
  /// 移除父组件
  /// </summary>
  public void RemoveParent()
  {
    SetParent(null);
  }
}
EOF

# src/Idle.Core/Profiles/IdleProfile.cs
cat > src/Idle.Core/Profiles/IdleProfile.cs << 'EOF'
using Idle.Core.Context;
using Idle.Core.Components;

namespace Idle.Core.Profiles;

/// <summary>
/// 组件对应的逻辑处理单例，包含业务逻辑
/// </summary>
public abstract class IdleProfile : IWithName
{
  /// <summary>
  /// Profile 的 Key 值（通常使用枚举值）
  /// </summary>
  public abstract int Key { get; }

  /// <summary>
  /// 名称
  /// </summary>
  public virtual string? Name { get; }

  /// <summary>
  /// 描述
  /// </summary>
  public virtual string? Description { get; }

  /// <summary>
  /// 在每个 Tick 执行的逻辑
  /// </summary>
  /// <param name="component">对应的组件实例</param>
  /// <param name="context">Tick 上下文</param>
  public virtual void OnTick(IdleComponent component, TickContext context)
  {
  }
}

/// <summary>
/// 具有名称和描述的接口
/// </summary>
public interface IWithName
{
  /// <summary>
  /// 名称
  /// </summary>
  string? Name { get; }

  /// <summary>
  /// 描述
  /// </summary>
  string? Description { get; }
}
EOF

# src/Idle.Core/Context/TickContext.cs
cat > src/Idle.Core/Context/TickContext.cs << 'EOF'
using Idle.Core.Components;

namespace Idle.Core.Context;

/// <summary>
/// Tick 执行上下文，包含战斗状态信息
/// </summary>
public class TickContext : IDisposable
{
  /// <summary>
  /// 当前 Tick 数
  /// </summary>
  public int CurrentTick { get; set; }

  /// <summary>
  /// 最大 Tick 数
  /// </summary>
  public int MaxTick { get; set; }

  /// <summary>
  /// 战斗是否已结束
  /// </summary>
  public bool IsBattleOver { get; set; }

  /// <summary>
  /// 战斗结果
  /// </summary>
  public EnumBattleResult Result { get; set; } = EnumBattleResult.NotSpecified;

  /// <summary>
  /// 创造者阵营的角色列表
  /// </summary>
  public List<IdleComponent> CreatorFaction { get; } = new();

  /// <summary>
  /// 敌对阵营的角色列表
  /// </summary>
  public List<IdleComponent> EnemyFaction { get; } = new();

  /// <summary>
  /// 投射物列表
  /// </summary>
  public List<IdleComponent> Projectiles { get; } = new();

  /// <summary>
  /// 战斗随机数生成器
  /// </summary>
  public IRandom? BattleRandom { get; set; }

  /// <summary>
  /// 物品随机数生成器（独立，回放时可跳过）
  /// </summary>
  public IRandom? ItemRandom { get; set; }

  /// <summary>
  /// 释放资源
  /// </summary>
  public void Dispose()
  {
    CreatorFaction.Clear();
    EnemyFaction.Clear();
    Projectiles.Clear();
    GC.SuppressFinalize(this);
  }
}

/// <summary>
/// 战斗结果枚举
/// </summary>
public enum EnumBattleResult
{
  NotSpecified = 0,
  Victory = 1,
  Defeat = 2,
  Timeout = 3,
  Draw = 4,
  Error = 5,
  PlayerExit = 6
}

/// <summary>
/// 随机数生成器接口
/// </summary>
public interface IRandom
{
  int Next();
  int Next(int maxValue);
  int Next(int minValue, int maxValue);
  float NextFloat();
  double NextDouble();
}
EOF

# src/Idle.Core/Repository/IRepository.cs
cat > src/Idle.Core/Repository/IRepository.cs << 'EOF'
using System.Linq.Expressions;

namespace Idle.Core.Repository;

/// <summary>
/// Entity 的增删改查抽象层
/// </summary>
/// <typeparam name="T">实体类型</typeparam>
public interface IRepository<T> where T : class
{
  Task<T?> GetByIdAsync(Guid id);
  Task<IEnumerable<T>> GetAllAsync();
  Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> predicate);
  Task AddAsync(T entity);
  Task UpdateAsync(T entity);
  Task DeleteAsync(Guid id);
  Task SaveChangesAsync();
}
EOF

# src/Idle.Core/Repository/IDataContext.cs
cat > src/Idle.Core/Repository/IDataContext.cs << 'EOF'
namespace Idle.Core.Repository;

/// <summary>
/// 数据访问上下文抽象，管理多个 Repository
/// </summary>
public interface IDataContext : IDisposable
{
  Task SaveChangesAsync();
}
EOF

# ========================================
# src/IdleCOP.Gameplay/IdleCOP.Gameplay.csproj
# ========================================
echo "Creating IdleCOP.Gameplay project..."

cat > src/IdleCOP.Gameplay/IdleCOP.Gameplay.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>IdleCOP.Gameplay</RootNamespace>
    <Description>COP 游戏玩法实现</Description>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Idle.Core\Idle.Core.csproj" />
  </ItemGroup>

</Project>
EOF

# src/IdleCOP.Gameplay/Combat/BattleSeed.cs
cat > src/IdleCOP.Gameplay/Combat/BattleSeed.cs << 'EOF'
namespace IdleCOP.Gameplay.Combat;

/// <summary>
/// 战斗种子，确定性战斗的输入参数
/// </summary>
public class BattleSeed
{
  /// <summary>
  /// 战斗 GUID（随机数种子）
  /// </summary>
  public Guid BattleGuid { get; set; }

  /// <summary>
  /// 地图 ID
  /// </summary>
  public string MapId { get; set; } = string.Empty;

  /// <summary>
  /// 游戏版本
  /// </summary>
  public string Version { get; set; } = string.Empty;

  /// <summary>
  /// 创造者阵营角色 ID 列表
  /// </summary>
  public List<Guid> CreatorCharacterIds { get; set; } = new();

  /// <summary>
  /// 敌对阵营角色 ID 列表
  /// </summary>
  public List<Guid> EnemyCharacterIds { get; set; } = new();
}
EOF

# src/IdleCOP.Gameplay/Enums/EnumQuality.cs
cat > src/IdleCOP.Gameplay/Equipment/EnumQuality.cs << 'EOF'
namespace IdleCOP.Gameplay.Equipment;

/// <summary>
/// 品质枚举（装备稀有度）
/// </summary>
public enum EnumQuality
{
  NotSpecified = 0,
  Normal = 1,      // 白色
  Magic = 2,       // 蓝色
  Rare = 3,        // 紫色
  Legendary = 4,   // 橙色
  Unique = 5       // 红色
}
EOF

# src/IdleCOP.Gameplay/Skills/EnumSkillType.cs
cat > src/IdleCOP.Gameplay/Skills/EnumSkillType.cs << 'EOF'
namespace IdleCOP.Gameplay.Skills;

/// <summary>
/// 技能类型枚举
/// </summary>
public enum EnumSkillType
{
  NotSpecified = 0,
  Active = 1,      // 主动技能
  Support = 2,     // 辅助技能
  Trigger = 3      // 触发技能
}
EOF

# ========================================
# src/IdleCOP.AI/IdleCOP.AI.csproj
# ========================================
echo "Creating IdleCOP.AI project..."

cat > src/IdleCOP.AI/IdleCOP.AI.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>IdleCOP.AI</RootNamespace>
    <Description>AI与行为系统</Description>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Idle.Core\Idle.Core.csproj" />
  </ItemGroup>

</Project>
EOF

# src/IdleCOP.AI/Strategies/IStrategy.cs
cat > src/IdleCOP.AI/Strategies/IStrategy.cs << 'EOF'
using Idle.Core.Components;
using Idle.Core.Context;

namespace IdleCOP.AI.Strategies;

/// <summary>
/// 策略接口，定义可配置的行为逻辑
/// </summary>
public interface IStrategy
{
  /// <summary>
  /// 优先级（数值越高越先执行）
  /// </summary>
  int Priority { get; }

  /// <summary>
  /// 判断是否可以执行
  /// </summary>
  /// <param name="context">Tick 上下文</param>
  /// <param name="actor">执行者组件</param>
  /// <returns>是否可以执行</returns>
  bool CanExecute(TickContext context, IdleComponent actor);

  /// <summary>
  /// 执行策略
  /// </summary>
  /// <param name="context">Tick 上下文</param>
  /// <param name="actor">执行者组件</param>
  void Execute(TickContext context, IdleComponent actor);
}
EOF

# ========================================
# src/IdleCOP.Data/IdleCOP.Data.csproj
# ========================================
echo "Creating IdleCOP.Data project..."

cat > src/IdleCOP.Data/IdleCOP.Data.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>IdleCOP.Data</RootNamespace>
    <Description>数据管理</Description>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Idle.Core\Idle.Core.csproj" />
    <ProjectReference Include="..\IdleCOP.Gameplay\IdleCOP.Gameplay.csproj" />
  </ItemGroup>

</Project>
EOF

# src/IdleCOP.Data/Entities/ActorEntity.cs
cat > src/IdleCOP.Data/Entities/ActorEntity.cs << 'EOF'
namespace IdleCOP.Data.Entities;

/// <summary>
/// 演员实体（持久化存储）
/// </summary>
public class ActorEntity
{
  /// <summary>
  /// 唯一标识符
  /// </summary>
  public Guid Id { get; set; }

  /// <summary>
  /// 名称
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// 等级
  /// </summary>
  public int Level { get; set; }

  /// <summary>
  /// Profile Key
  /// </summary>
  public int ProfileKey { get; set; }
}
EOF

# src/IdleCOP.Data/DTOs/CharacterDTO.cs
cat > src/IdleCOP.Data/DTOs/CharacterDTO.cs << 'EOF'
namespace IdleCOP.Data.DTOs;

/// <summary>
/// 角色数据传输对象
/// </summary>
public class CharacterDTO
{
  /// <summary>
  /// 唯一标识符
  /// </summary>
  public Guid Id { get; set; }

  /// <summary>
  /// 名称
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// 等级
  /// </summary>
  public int Level { get; set; }

  /// <summary>
  /// 最大生命值
  /// </summary>
  public int MaxHealth { get; set; }

  /// <summary>
  /// 当前生命值
  /// </summary>
  public int CurrentHealth { get; set; }
}
EOF

# ========================================
# src/IdleCOP.Console/IdleCOP.Console.csproj
# ========================================
echo "Creating IdleCOP.Console project..."

cat > src/IdleCOP.Console/IdleCOP.Console.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <RootNamespace>IdleCOP.Console</RootNamespace>
    <Description>控制台实验程序</Description>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Idle.Utility\Idle.Utility.csproj" />
    <ProjectReference Include="..\Idle.Core\Idle.Core.csproj" />
    <ProjectReference Include="..\IdleCOP.Gameplay\IdleCOP.Gameplay.csproj" />
    <ProjectReference Include="..\IdleCOP.AI\IdleCOP.AI.csproj" />
    <ProjectReference Include="..\IdleCOP.Data\IdleCOP.Data.csproj" />
  </ItemGroup>

</Project>
EOF

# src/IdleCOP.Console/Program.cs
cat > src/IdleCOP.Console/Program.cs << 'EOF'
using Idle.Utility.Helpers;
using Idle.Core.Components;
using Idle.Core.Context;
using IdleCOP.Gameplay.Combat;
using IdleCOP.Gameplay.Equipment;

namespace IdleCOP.Console;

/// <summary>
/// IdleCOP 控制台实验程序
/// 用于手动调用脚本进行实验性功能测试
/// </summary>
public class Program
{
  public static void Main(string[] args)
  {
    System.Console.WriteLine("=== IdleCOP Console ===");
    System.Console.WriteLine();

    // 示例：测试 TickHelper
    TestTickHelper();

    // 示例：测试组件系统
    TestComponentSystem();

    // 示例：测试战斗种子
    TestBattleSeed();

    System.Console.WriteLine();
    System.Console.WriteLine("Press any key to exit...");
    System.Console.ReadKey();
  }

  private static void TestTickHelper()
  {
    System.Console.WriteLine("--- Testing TickHelper ---");

    var ticks = TickHelper.SecondsToTicks(2.5f);
    System.Console.WriteLine($"2.5 seconds = {ticks} ticks (at {TickHelper.DefaultTickRate} ticks/sec)");

    var seconds = TickHelper.TicksToSeconds(90);
    System.Console.WriteLine($"90 ticks = {seconds} seconds");

    System.Console.WriteLine();
  }

  private static void TestComponentSystem()
  {
    System.Console.WriteLine("--- Testing Component System ---");

    var parent = new IdleComponent { ProfileKey = 1 };
    var child1 = new IdleComponent { ProfileKey = 2 };
    var child2 = new IdleComponent { ProfileKey = 3 };

    child1.SetParent(parent);
    child2.SetParent(parent);

    System.Console.WriteLine($"Parent has {parent.Children.Count} children");
    System.Console.WriteLine($"Child1 parent is set: {child1.Parent != null}");

    child1.RemoveParent();
    System.Console.WriteLine($"After removal, parent has {parent.Children.Count} children");

    System.Console.WriteLine();
  }

  private static void TestBattleSeed()
  {
    System.Console.WriteLine("--- Testing Battle Seed ---");

    var seed = new BattleSeed
    {
      BattleGuid = Guid.NewGuid(),
      MapId = "map_test_001",
      Version = "0.1.0"
    };

    seed.CreatorCharacterIds.Add(Guid.NewGuid());
    seed.EnemyCharacterIds.Add(Guid.NewGuid());
    seed.EnemyCharacterIds.Add(Guid.NewGuid());

    System.Console.WriteLine($"Battle GUID: {seed.BattleGuid}");
    System.Console.WriteLine($"Map ID: {seed.MapId}");
    System.Console.WriteLine($"Creator characters: {seed.CreatorCharacterIds.Count}");
    System.Console.WriteLine($"Enemy characters: {seed.EnemyCharacterIds.Count}");

    System.Console.WriteLine();
  }
}
EOF

# ========================================
# Test Projects
# ========================================
echo "Creating test projects..."

# tests/Idle.Utility.Tests/Idle.Utility.Tests.csproj
cat > tests/Idle.Utility.Tests/Idle.Utility.Tests.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="coverlet.collector" Version="6.0.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="xunit" Version="2.6.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.3" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\Idle.Utility\Idle.Utility.csproj" />
  </ItemGroup>

  <ItemGroup>
    <Using Include="Xunit" />
  </ItemGroup>

</Project>
EOF

# tests/Idle.Utility.Tests/TickHelperTests.cs
cat > tests/Idle.Utility.Tests/TickHelperTests.cs << 'EOF'
using Idle.Utility.Helpers;

namespace Idle.Utility.Tests;

public class TickHelperTests
{
  [Fact]
  public void SecondsToTicks_WithDefaultRate_ReturnsCorrectTicks()
  {
    // Arrange
    float seconds = 2.0f;

    // Act
    int ticks = TickHelper.SecondsToTicks(seconds);

    // Assert
    Assert.Equal(60, ticks); // 2 * 30 = 60
  }

  [Fact]
  public void SecondsToTicks_WithCustomRate_ReturnsCorrectTicks()
  {
    // Arrange
    float seconds = 2.0f;
    int tickRate = 60;

    // Act
    int ticks = TickHelper.SecondsToTicks(seconds, tickRate);

    // Assert
    Assert.Equal(120, ticks); // 2 * 60 = 120
  }

  [Fact]
  public void TicksToSeconds_WithDefaultRate_ReturnsCorrectSeconds()
  {
    // Arrange
    int ticks = 90;

    // Act
    float seconds = TickHelper.TicksToSeconds(ticks);

    // Assert
    Assert.Equal(3.0f, seconds); // 90 / 30 = 3
  }

  [Fact]
  public void TicksToSeconds_WithCustomRate_ReturnsCorrectSeconds()
  {
    // Arrange
    int ticks = 120;
    int tickRate = 60;

    // Act
    float seconds = TickHelper.TicksToSeconds(ticks, tickRate);

    // Assert
    Assert.Equal(2.0f, seconds); // 120 / 60 = 2
  }

  [Fact]
  public void DefaultTickRate_IsThirty()
  {
    Assert.Equal(30, TickHelper.DefaultTickRate);
  }
}
EOF

# tests/Idle.Core.Tests/Idle.Core.Tests.csproj
cat > tests/Idle.Core.Tests/Idle.Core.Tests.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="coverlet.collector" Version="6.0.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="xunit" Version="2.6.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.3" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\Idle.Core\Idle.Core.csproj" />
  </ItemGroup>

  <ItemGroup>
    <Using Include="Xunit" />
  </ItemGroup>

</Project>
EOF

# tests/Idle.Core.Tests/IdleComponentTests.cs
cat > tests/Idle.Core.Tests/IdleComponentTests.cs << 'EOF'
using Idle.Core.Components;

namespace Idle.Core.Tests;

public class IdleComponentTests
{
  [Fact]
  public void NewComponent_HasUniqueId()
  {
    // Arrange & Act
    var component1 = new IdleComponent();
    var component2 = new IdleComponent();

    // Assert
    Assert.NotEqual(component1.Id, component2.Id);
    Assert.NotEqual(Guid.Empty, component1.Id);
  }

  [Fact]
  public void SetParent_AddsChildToParent()
  {
    // Arrange
    var parent = new IdleComponent();
    var child = new IdleComponent();

    // Act
    child.SetParent(parent);

    // Assert
    Assert.Equal(parent, child.Parent);
    Assert.Contains(child, parent.Children);
  }

  [Fact]
  public void SetParent_RemovesFromPreviousParent()
  {
    // Arrange
    var parent1 = new IdleComponent();
    var parent2 = new IdleComponent();
    var child = new IdleComponent();
    child.SetParent(parent1);

    // Act
    child.SetParent(parent2);

    // Assert
    Assert.Equal(parent2, child.Parent);
    Assert.DoesNotContain(child, parent1.Children);
    Assert.Contains(child, parent2.Children);
  }

  [Fact]
  public void RemoveParent_SetsParentToNull()
  {
    // Arrange
    var parent = new IdleComponent();
    var child = new IdleComponent();
    child.SetParent(parent);

    // Act
    child.RemoveParent();

    // Assert
    Assert.Null(child.Parent);
    Assert.DoesNotContain(child, parent.Children);
  }

  [Fact]
  public void Children_IsEmptyByDefault()
  {
    // Arrange & Act
    var component = new IdleComponent();

    // Assert
    Assert.Empty(component.Children);
  }
}
EOF

# tests/Idle.Core.Tests/TickContextTests.cs
cat > tests/Idle.Core.Tests/TickContextTests.cs << 'EOF'
using Idle.Core.Context;

namespace Idle.Core.Tests;

public class TickContextTests
{
  [Fact]
  public void NewContext_HasDefaultValues()
  {
    // Arrange & Act
    var context = new TickContext();

    // Assert
    Assert.Equal(0, context.CurrentTick);
    Assert.Equal(0, context.MaxTick);
    Assert.False(context.IsBattleOver);
    Assert.Equal(EnumBattleResult.NotSpecified, context.Result);
  }

  [Fact]
  public void Dispose_ClearsAllLists()
  {
    // Arrange
    var context = new TickContext();
    context.CreatorFaction.Add(new Idle.Core.Components.IdleComponent());
    context.EnemyFaction.Add(new Idle.Core.Components.IdleComponent());
    context.Projectiles.Add(new Idle.Core.Components.IdleComponent());

    // Act
    context.Dispose();

    // Assert
    Assert.Empty(context.CreatorFaction);
    Assert.Empty(context.EnemyFaction);
    Assert.Empty(context.Projectiles);
  }

  [Fact]
  public void EnumBattleResult_HasCorrectValues()
  {
    Assert.Equal(0, (int)EnumBattleResult.NotSpecified);
    Assert.Equal(1, (int)EnumBattleResult.Victory);
    Assert.Equal(2, (int)EnumBattleResult.Defeat);
    Assert.Equal(3, (int)EnumBattleResult.Timeout);
    Assert.Equal(4, (int)EnumBattleResult.Draw);
  }
}
EOF

# tests/IdleCOP.Gameplay.Tests/IdleCOP.Gameplay.Tests.csproj
cat > tests/IdleCOP.Gameplay.Tests/IdleCOP.Gameplay.Tests.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="coverlet.collector" Version="6.0.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="xunit" Version="2.6.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.3" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\IdleCOP.Gameplay\IdleCOP.Gameplay.csproj" />
  </ItemGroup>

  <ItemGroup>
    <Using Include="Xunit" />
  </ItemGroup>

</Project>
EOF

# tests/IdleCOP.Gameplay.Tests/BattleSeedTests.cs
cat > tests/IdleCOP.Gameplay.Tests/BattleSeedTests.cs << 'EOF'
using IdleCOP.Gameplay.Combat;

namespace IdleCOP.Gameplay.Tests;

public class BattleSeedTests
{
  [Fact]
  public void NewBattleSeed_HasDefaultValues()
  {
    // Arrange & Act
    var seed = new BattleSeed();

    // Assert
    Assert.Equal(Guid.Empty, seed.BattleGuid);
    Assert.Equal(string.Empty, seed.MapId);
    Assert.Equal(string.Empty, seed.Version);
    Assert.Empty(seed.CreatorCharacterIds);
    Assert.Empty(seed.EnemyCharacterIds);
  }

  [Fact]
  public void BattleSeed_CanSetProperties()
  {
    // Arrange
    var battleGuid = Guid.NewGuid();
    var characterId = Guid.NewGuid();

    // Act
    var seed = new BattleSeed
    {
      BattleGuid = battleGuid,
      MapId = "test_map",
      Version = "1.0.0"
    };
    seed.CreatorCharacterIds.Add(characterId);

    // Assert
    Assert.Equal(battleGuid, seed.BattleGuid);
    Assert.Equal("test_map", seed.MapId);
    Assert.Equal("1.0.0", seed.Version);
    Assert.Single(seed.CreatorCharacterIds);
    Assert.Equal(characterId, seed.CreatorCharacterIds[0]);
  }
}
EOF

# tests/IdleCOP.Gameplay.Tests/EnumTests.cs
cat > tests/IdleCOP.Gameplay.Tests/EnumTests.cs << 'EOF'
using IdleCOP.Gameplay.Equipment;
using IdleCOP.Gameplay.Skills;

namespace IdleCOP.Gameplay.Tests;

public class EnumTests
{
  [Fact]
  public void EnumQuality_HasCorrectValues()
  {
    Assert.Equal(0, (int)EnumQuality.NotSpecified);
    Assert.Equal(1, (int)EnumQuality.Normal);
    Assert.Equal(2, (int)EnumQuality.Magic);
    Assert.Equal(3, (int)EnumQuality.Rare);
    Assert.Equal(4, (int)EnumQuality.Legendary);
    Assert.Equal(5, (int)EnumQuality.Unique);
  }

  [Fact]
  public void EnumSkillType_HasCorrectValues()
  {
    Assert.Equal(0, (int)EnumSkillType.NotSpecified);
    Assert.Equal(1, (int)EnumSkillType.Active);
    Assert.Equal(2, (int)EnumSkillType.Support);
    Assert.Equal(3, (int)EnumSkillType.Trigger);
  }
}
EOF

# tests/IdleCOP.AI.Tests/IdleCOP.AI.Tests.csproj
cat > tests/IdleCOP.AI.Tests/IdleCOP.AI.Tests.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="coverlet.collector" Version="6.0.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="xunit" Version="2.6.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.3" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\IdleCOP.AI\IdleCOP.AI.csproj" />
  </ItemGroup>

  <ItemGroup>
    <Using Include="Xunit" />
  </ItemGroup>

</Project>
EOF

# tests/IdleCOP.AI.Tests/StrategyTests.cs
cat > tests/IdleCOP.AI.Tests/StrategyTests.cs << 'EOF'
using IdleCOP.AI.Strategies;
using Idle.Core.Components;
using Idle.Core.Context;

namespace IdleCOP.AI.Tests;

public class StrategyTests
{
  private class TestStrategy : IStrategy
  {
    public int Priority => 50;
    public bool ShouldExecute { get; set; } = true;
    public bool WasExecuted { get; private set; }

    public bool CanExecute(TickContext context, IdleComponent actor)
    {
      return ShouldExecute;
    }

    public void Execute(TickContext context, IdleComponent actor)
    {
      WasExecuted = true;
    }
  }

  [Fact]
  public void Strategy_HasPriority()
  {
    // Arrange & Act
    var strategy = new TestStrategy();

    // Assert
    Assert.Equal(50, strategy.Priority);
  }

  [Fact]
  public void Strategy_CanExecute_WhenConditionMet()
  {
    // Arrange
    var strategy = new TestStrategy { ShouldExecute = true };
    var context = new TickContext();
    var actor = new IdleComponent();

    // Act
    var canExecute = strategy.CanExecute(context, actor);

    // Assert
    Assert.True(canExecute);
  }

  [Fact]
  public void Strategy_CannotExecute_WhenConditionNotMet()
  {
    // Arrange
    var strategy = new TestStrategy { ShouldExecute = false };
    var context = new TickContext();
    var actor = new IdleComponent();

    // Act
    var canExecute = strategy.CanExecute(context, actor);

    // Assert
    Assert.False(canExecute);
  }

  [Fact]
  public void Strategy_Execute_SetsWasExecuted()
  {
    // Arrange
    var strategy = new TestStrategy();
    var context = new TickContext();
    var actor = new IdleComponent();

    // Act
    strategy.Execute(context, actor);

    // Assert
    Assert.True(strategy.WasExecuted);
  }
}
EOF

# tests/IdleCOP.Data.Tests/IdleCOP.Data.Tests.csproj
cat > tests/IdleCOP.Data.Tests/IdleCOP.Data.Tests.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="coverlet.collector" Version="6.0.0" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="xunit" Version="2.6.1" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.3" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\src\IdleCOP.Data\IdleCOP.Data.csproj" />
  </ItemGroup>

  <ItemGroup>
    <Using Include="Xunit" />
  </ItemGroup>

</Project>
EOF

# tests/IdleCOP.Data.Tests/EntityTests.cs
cat > tests/IdleCOP.Data.Tests/EntityTests.cs << 'EOF'
using IdleCOP.Data.Entities;
using IdleCOP.Data.DTOs;

namespace IdleCOP.Data.Tests;

public class EntityTests
{
  [Fact]
  public void ActorEntity_HasDefaultValues()
  {
    // Arrange & Act
    var entity = new ActorEntity();

    // Assert
    Assert.Equal(Guid.Empty, entity.Id);
    Assert.Equal(string.Empty, entity.Name);
    Assert.Equal(0, entity.Level);
    Assert.Equal(0, entity.ProfileKey);
  }

  [Fact]
  public void ActorEntity_CanSetProperties()
  {
    // Arrange
    var id = Guid.NewGuid();

    // Act
    var entity = new ActorEntity
    {
      Id = id,
      Name = "Test Actor",
      Level = 10,
      ProfileKey = 5
    };

    // Assert
    Assert.Equal(id, entity.Id);
    Assert.Equal("Test Actor", entity.Name);
    Assert.Equal(10, entity.Level);
    Assert.Equal(5, entity.ProfileKey);
  }

  [Fact]
  public void CharacterDTO_HasDefaultValues()
  {
    // Arrange & Act
    var dto = new CharacterDTO();

    // Assert
    Assert.Equal(Guid.Empty, dto.Id);
    Assert.Equal(string.Empty, dto.Name);
    Assert.Equal(0, dto.Level);
    Assert.Equal(0, dto.MaxHealth);
    Assert.Equal(0, dto.CurrentHealth);
  }

  [Fact]
  public void CharacterDTO_CanSetProperties()
  {
    // Arrange
    var id = Guid.NewGuid();

    // Act
    var dto = new CharacterDTO
    {
      Id = id,
      Name = "Test Character",
      Level = 25,
      MaxHealth = 1000,
      CurrentHealth = 750
    };

    // Assert
    Assert.Equal(id, dto.Id);
    Assert.Equal("Test Character", dto.Name);
    Assert.Equal(25, dto.Level);
    Assert.Equal(1000, dto.MaxHealth);
    Assert.Equal(750, dto.CurrentHealth);
  }
}
EOF

echo ""
echo "=== Project Initialization Complete ==="
echo ""
echo "All files have been created. Next steps:"
echo "1. Run 'dotnet restore' to restore NuGet packages"
echo "2. Run 'dotnet build' to build the solution"
echo "3. Run 'dotnet test' to run all unit tests"
echo ""
