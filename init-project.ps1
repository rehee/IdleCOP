# IdleCOP 完整项目初始化脚本 (PowerShell)
# 此脚本将创建所有必要的目录和文件

$ErrorActionPreference = "Stop"

Write-Host "=== IdleCOP Project Initialization ===" -ForegroundColor Cyan
Write-Host ""

# Create all directories
Write-Host "Creating directory structure..." -ForegroundColor Yellow

$directories = @(
  "src/Idle.Utility/Helpers",
  "src/Idle.Core/Components",
  "src/Idle.Core/Profiles",
  "src/Idle.Core/Context",
  "src/Idle.Core/Repository",
  "src/Idle.Core/DI",
  "src/IdleCOP.Gameplay/Combat",
  "src/IdleCOP.Gameplay/Skills",
  "src/IdleCOP.Gameplay/Equipment",
  "src/IdleCOP.Gameplay/Maps",
  "src/IdleCOP.Gameplay/Instructions",
  "src/IdleCOP.AI/Strategies",
  "src/IdleCOP.Data/Entities",
  "src/IdleCOP.Data/DTOs",
  "src/IdleCOP.Data/Configs",
  "src/IdleCOP.Console",
  "tests/Idle.Utility.Tests",
  "tests/Idle.Core.Tests",
  "tests/IdleCOP.Gameplay.Tests",
  "tests/IdleCOP.AI.Tests",
  "tests/IdleCOP.Data.Tests"
)

foreach ($dir in $directories) {
  New-Item -ItemType Directory -Path $dir -Force | Out-Null
}

Write-Host "Directories created." -ForegroundColor Green
Write-Host ""

# ========================================
# Idle.Utility Project
# ========================================
Write-Host "Creating Idle.Utility project..." -ForegroundColor Yellow

@'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>Idle.Utility</RootNamespace>
    <Description>通用工具库（独立，无游戏依赖）</Description>
  </PropertyGroup>

</Project>
'@ | Set-Content -Path "src/Idle.Utility/Idle.Utility.csproj"

@'
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
'@ | Set-Content -Path "src/Idle.Utility/Helpers/TickHelper.cs"

# ========================================
# Idle.Core Project
# ========================================
Write-Host "Creating Idle.Core project..." -ForegroundColor Yellow

@'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>Idle.Core</RootNamespace>
    <Description>核心基础库（通用玩法框架）</Description>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Idle.Utility\Idle.Utility.csproj" />
  </ItemGroup>

</Project>
'@ | Set-Content -Path "src/Idle.Core/Idle.Core.csproj"

@'
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
'@ | Set-Content -Path "src/Idle.Core/Components/IdleComponent.cs"

@'
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
'@ | Set-Content -Path "src/Idle.Core/Profiles/IdleProfile.cs"

@'
using Idle.Core.Components;

namespace Idle.Core.Context;

/// <summary>
/// Tick 执行上下文，包含战斗状态信息
/// </summary>
public class TickContext : IDisposable
{
  public int CurrentTick { get; set; }
  public int MaxTick { get; set; }
  public bool IsBattleOver { get; set; }
  public EnumBattleResult Result { get; set; } = EnumBattleResult.NotSpecified;
  public List<IdleComponent> CreatorFaction { get; } = new();
  public List<IdleComponent> EnemyFaction { get; } = new();
  public List<IdleComponent> Projectiles { get; } = new();
  public IRandom? BattleRandom { get; set; }
  public IRandom? ItemRandom { get; set; }

  public void Dispose()
  {
    CreatorFaction.Clear();
    EnemyFaction.Clear();
    Projectiles.Clear();
    GC.SuppressFinalize(this);
  }
}

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

public interface IRandom
{
  int Next();
  int Next(int maxValue);
  int Next(int minValue, int maxValue);
  float NextFloat();
  double NextDouble();
}
'@ | Set-Content -Path "src/Idle.Core/Context/TickContext.cs"

@'
using System.Linq.Expressions;

namespace Idle.Core.Repository;

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
'@ | Set-Content -Path "src/Idle.Core/Repository/IRepository.cs"

@'
namespace Idle.Core.Repository;

public interface IDataContext : IDisposable
{
  Task SaveChangesAsync();
}
'@ | Set-Content -Path "src/Idle.Core/Repository/IDataContext.cs"

# ========================================
# IdleCOP.Gameplay Project
# ========================================
Write-Host "Creating IdleCOP.Gameplay project..." -ForegroundColor Yellow

@'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>IdleCOP.Gameplay</RootNamespace>
    <Description>COP 游戏玩法实现</Description>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Idle.Core\Idle.Core.csproj" />
  </ItemGroup>

</Project>
'@ | Set-Content -Path "src/IdleCOP.Gameplay/IdleCOP.Gameplay.csproj"

@'
namespace IdleCOP.Gameplay.Combat;

public class BattleSeed
{
  public Guid BattleGuid { get; set; }
  public string MapId { get; set; } = string.Empty;
  public string Version { get; set; } = string.Empty;
  public List<Guid> CreatorCharacterIds { get; set; } = new();
  public List<Guid> EnemyCharacterIds { get; set; } = new();
}
'@ | Set-Content -Path "src/IdleCOP.Gameplay/Combat/BattleSeed.cs"

@'
namespace IdleCOP.Gameplay.Equipment;

public enum EnumQuality
{
  NotSpecified = 0,
  Normal = 1,
  Magic = 2,
  Rare = 3,
  Legendary = 4,
  Unique = 5
}
'@ | Set-Content -Path "src/IdleCOP.Gameplay/Equipment/EnumQuality.cs"

@'
namespace IdleCOP.Gameplay.Skills;

public enum EnumSkillType
{
  NotSpecified = 0,
  Active = 1,
  Support = 2,
  Trigger = 3
}
'@ | Set-Content -Path "src/IdleCOP.Gameplay/Skills/EnumSkillType.cs"

# ========================================
# IdleCOP.AI Project
# ========================================
Write-Host "Creating IdleCOP.AI project..." -ForegroundColor Yellow

@'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>IdleCOP.AI</RootNamespace>
    <Description>AI与行为系统</Description>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Idle.Core\Idle.Core.csproj" />
  </ItemGroup>

</Project>
'@ | Set-Content -Path "src/IdleCOP.AI/IdleCOP.AI.csproj"

@'
using Idle.Core.Components;
using Idle.Core.Context;

namespace IdleCOP.AI.Strategies;

public interface IStrategy
{
  int Priority { get; }
  bool CanExecute(TickContext context, IdleComponent actor);
  void Execute(TickContext context, IdleComponent actor);
}
'@ | Set-Content -Path "src/IdleCOP.AI/Strategies/IStrategy.cs"

# ========================================
# IdleCOP.Data Project
# ========================================
Write-Host "Creating IdleCOP.Data project..." -ForegroundColor Yellow

@'
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
'@ | Set-Content -Path "src/IdleCOP.Data/IdleCOP.Data.csproj"

@'
namespace IdleCOP.Data.Entities;

public class ActorEntity
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public int Level { get; set; }
  public int ProfileKey { get; set; }
}
'@ | Set-Content -Path "src/IdleCOP.Data/Entities/ActorEntity.cs"

@'
namespace IdleCOP.Data.DTOs;

public class CharacterDTO
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public int Level { get; set; }
  public int MaxHealth { get; set; }
  public int CurrentHealth { get; set; }
}
'@ | Set-Content -Path "src/IdleCOP.Data/DTOs/CharacterDTO.cs"

# ========================================
# IdleCOP.Console Project
# ========================================
Write-Host "Creating IdleCOP.Console project..." -ForegroundColor Yellow

@'
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
'@ | Set-Content -Path "src/IdleCOP.Console/IdleCOP.Console.csproj"

@'
using Idle.Utility.Helpers;
using Idle.Core.Components;
using IdleCOP.Gameplay.Combat;

namespace IdleCOP.Console;

public class Program
{
  public static void Main(string[] args)
  {
    System.Console.WriteLine("=== IdleCOP Console ===");
    System.Console.WriteLine();

    // Test TickHelper
    var ticks = TickHelper.SecondsToTicks(2.5f);
    System.Console.WriteLine($"2.5 seconds = {ticks} ticks");

    // Test Component
    var component = new IdleComponent();
    System.Console.WriteLine($"Component ID: {component.Id}");

    // Test BattleSeed
    var seed = new BattleSeed { MapId = "test_map" };
    System.Console.WriteLine($"Battle Map: {seed.MapId}");

    System.Console.WriteLine();
    System.Console.WriteLine("Press any key to exit...");
    System.Console.ReadKey();
  }
}
'@ | Set-Content -Path "src/IdleCOP.Console/Program.cs"

# ========================================
# Test Projects
# ========================================
Write-Host "Creating test projects..." -ForegroundColor Yellow

# Idle.Utility.Tests
@'
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
'@ | Set-Content -Path "tests/Idle.Utility.Tests/Idle.Utility.Tests.csproj"

@'
using Idle.Utility.Helpers;

namespace Idle.Utility.Tests;

public class TickHelperTests
{
  [Fact]
  public void SecondsToTicks_WithDefaultRate_ReturnsCorrectTicks()
  {
    var ticks = TickHelper.SecondsToTicks(2.0f);
    Assert.Equal(60, ticks);
  }

  [Fact]
  public void TicksToSeconds_WithDefaultRate_ReturnsCorrectSeconds()
  {
    var seconds = TickHelper.TicksToSeconds(90);
    Assert.Equal(3.0f, seconds);
  }

  [Fact]
  public void DefaultTickRate_IsThirty()
  {
    Assert.Equal(30, TickHelper.DefaultTickRate);
  }
}
'@ | Set-Content -Path "tests/Idle.Utility.Tests/TickHelperTests.cs"

# Idle.Core.Tests
@'
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
'@ | Set-Content -Path "tests/Idle.Core.Tests/Idle.Core.Tests.csproj"

@'
using Idle.Core.Components;

namespace Idle.Core.Tests;

public class IdleComponentTests
{
  [Fact]
  public void NewComponent_HasUniqueId()
  {
    var c1 = new IdleComponent();
    var c2 = new IdleComponent();
    Assert.NotEqual(c1.Id, c2.Id);
  }

  [Fact]
  public void SetParent_AddsChildToParent()
  {
    var parent = new IdleComponent();
    var child = new IdleComponent();
    child.SetParent(parent);
    Assert.Contains(child, parent.Children);
  }

  [Fact]
  public void RemoveParent_SetsParentToNull()
  {
    var parent = new IdleComponent();
    var child = new IdleComponent();
    child.SetParent(parent);
    child.RemoveParent();
    Assert.Null(child.Parent);
  }
}
'@ | Set-Content -Path "tests/Idle.Core.Tests/IdleComponentTests.cs"

# IdleCOP.Gameplay.Tests
@'
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
'@ | Set-Content -Path "tests/IdleCOP.Gameplay.Tests/IdleCOP.Gameplay.Tests.csproj"

@'
using IdleCOP.Gameplay.Combat;
using IdleCOP.Gameplay.Equipment;

namespace IdleCOP.Gameplay.Tests;

public class BattleSeedTests
{
  [Fact]
  public void NewBattleSeed_HasDefaultValues()
  {
    var seed = new BattleSeed();
    Assert.Equal(string.Empty, seed.MapId);
  }
}

public class EnumTests
{
  [Fact]
  public void EnumQuality_HasCorrectValues()
  {
    Assert.Equal(0, (int)EnumQuality.NotSpecified);
    Assert.Equal(1, (int)EnumQuality.Normal);
  }
}
'@ | Set-Content -Path "tests/IdleCOP.Gameplay.Tests/GameplayTests.cs"

# IdleCOP.AI.Tests
@'
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
'@ | Set-Content -Path "tests/IdleCOP.AI.Tests/IdleCOP.AI.Tests.csproj"

@'
using IdleCOP.AI.Strategies;
using Idle.Core.Components;
using Idle.Core.Context;

namespace IdleCOP.AI.Tests;

public class StrategyTests
{
  private class TestStrategy : IStrategy
  {
    public int Priority => 50;
    public bool CanExecute(TickContext context, IdleComponent actor) => true;
    public void Execute(TickContext context, IdleComponent actor) { }
  }

  [Fact]
  public void Strategy_HasPriority()
  {
    var strategy = new TestStrategy();
    Assert.Equal(50, strategy.Priority);
  }
}
'@ | Set-Content -Path "tests/IdleCOP.AI.Tests/StrategyTests.cs"

# IdleCOP.Data.Tests
@'
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
'@ | Set-Content -Path "tests/IdleCOP.Data.Tests/IdleCOP.Data.Tests.csproj"

@'
using IdleCOP.Data.Entities;
using IdleCOP.Data.DTOs;

namespace IdleCOP.Data.Tests;

public class EntityTests
{
  [Fact]
  public void ActorEntity_HasDefaultValues()
  {
    var entity = new ActorEntity();
    Assert.Equal(string.Empty, entity.Name);
  }

  [Fact]
  public void CharacterDTO_HasDefaultValues()
  {
    var dto = new CharacterDTO();
    Assert.Equal(string.Empty, dto.Name);
  }
}
'@ | Set-Content -Path "tests/IdleCOP.Data.Tests/EntityTests.cs"

Write-Host ""
Write-Host "=== Project Initialization Complete ===" -ForegroundColor Green
Write-Host ""
Write-Host "All files have been created. Next steps:" -ForegroundColor Yellow
Write-Host "1. Run 'dotnet restore' to restore NuGet packages"
Write-Host "2. Run 'dotnet build' to build the solution"
Write-Host "3. Run 'dotnet test' to run all unit tests"
Write-Host ""
