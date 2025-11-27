#!/bin/bash

# Setup script for IdleCOP
# This script creates project directories and files that couldn't be created by the automated system

set -e

echo "Setting up IdleCOP project structure..."

# Create IdleCOP.Client.Console project
mkdir -p IdleCOP.Client.Console
cat > IdleCOP.Client.Console/IdleCOP.Client.Console.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <RootNamespace>IdleCOP.Client.Console</RootNamespace>
    <AssemblyName>IdleCOP.Client.Console</AssemblyName>
    <Description>Console client for IdleCOP - experimental features</Description>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\IdleCOP.Gameplay\IdleCOP.Gameplay.csproj" />
    <ProjectReference Include="..\IdleCOP.AI\IdleCOP.AI.csproj" />
    <ProjectReference Include="..\IdleCOP.Data\IdleCOP.Data.csproj" />
  </ItemGroup>

</Project>
EOF

cat > IdleCOP.Client.Console/Program.cs << 'EOF'
using Idle.Core.Context;
using IdleCOP.Gameplay.Combat;
using IdleCOP.Gameplay.Skills;
using IdleCOP.Gameplay.Maps;
using IdleCOP.Data.Enums;

namespace IdleCOP.Client.Console;

/// <summary>
/// Console application for experimental features.
/// </summary>
public class Program
{
  /// <summary>
  /// Entry point for the console application.
  /// </summary>
  /// <param name="args">Command line arguments.</param>
  public static void Main(string[] args)
  {
    System.Console.WriteLine("IdleCOP Console - Experimental Features");
    System.Console.WriteLine("========================================");
    System.Console.WriteLine();

    // Demo: Create a simple battle scenario
    DemoBattle();

    System.Console.WriteLine();
    System.Console.WriteLine("Press any key to exit...");
    System.Console.ReadKey();
  }

  private static void DemoBattle()
  {
    System.Console.WriteLine("Demo: Simple Battle Scenario");
    System.Console.WriteLine("----------------------------");

    // Create a map
    var map = new MapComponent
    {
      Name = "Demo Dungeon",
      Seed = "demo-seed-123",
      Level = 1,
      Width = 100,
      Height = 100,
      MaxBattleTicks = 30 * 60 // 60 seconds at 30 ticks/second
    };

    // Create player
    var player = new ActorComponent
    {
      Name = "Hero",
      Level = 1,
      Faction = EnumFaction.Creator,
      MaxHealth = 100,
      CurrentHealth = 100,
      MaxEnergy = 50,
      CurrentEnergy = 50,
      PositionX = 10,
      PositionY = 10
    };

    // Add a skill to player
    var fireball = new SkillComponent
    {
      ProfileKey = 1,
      SkillType = EnumSkillType.Active,
      CooldownTicks = 30, // 1 second
      ResourceCost = 10
    };
    player.AddChild(fireball);

    // Create enemy
    var enemy = new ActorComponent
    {
      Name = "Skeleton",
      Level = 1,
      Faction = EnumFaction.Enemy,
      MaxHealth = 50,
      CurrentHealth = 50,
      MaxEnergy = 0,
      CurrentEnergy = 0,
      PositionX = 20,
      PositionY = 10
    };

    // Create tick context
    using var context = new TickContext
    {
      MaxTick = map.MaxBattleTicks,
      Map = map
    };

    context.CreatorFaction.Add(player);
    context.EnemyFaction.Add(enemy);

    System.Console.WriteLine($"Map: {map.Name} (Level {map.Level})");
    System.Console.WriteLine($"Player: {player.Name} - HP: {player.CurrentHealth}/{player.MaxHealth}");
    System.Console.WriteLine($"Enemy: {enemy.Name} - HP: {enemy.CurrentHealth}/{enemy.MaxHealth}");
    System.Console.WriteLine();

    // Simulate a few ticks
    for (int i = 0; i < 5; i++)
    {
      context.CurrentTick++;
      player.OnTick(context);
      enemy.OnTick(context);

      System.Console.WriteLine($"Tick {context.CurrentTick}: Player HP={player.CurrentHealth}, Enemy HP={enemy.CurrentHealth}");

      // Simulate player dealing damage to enemy
      if (fireball.IsReady && player.ConsumeEnergy(fireball.ResourceCost))
      {
        int damage = 15;
        enemy.TakeDamage(damage);
        fireball.TriggerCooldown();
        System.Console.WriteLine($"  -> Player uses Fireball! Deals {damage} damage.");
      }

      if (!enemy.IsAlive)
      {
        System.Console.WriteLine("  -> Enemy defeated!");
        context.IsBattleOver = true;
        context.Result = EnumBattleResult.Victory;
        break;
      }
    }

    System.Console.WriteLine();
    System.Console.WriteLine($"Battle Result: {context.Result}");
  }
}
EOF

# Create test projects
mkdir -p Idle.Utility.Tests
cat > Idle.Utility.Tests/Idle.Utility.Tests.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>Idle.Utility.Tests</RootNamespace>
    <AssemblyName>Idle.Utility.Tests</AssemblyName>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="xunit" Version="2.6.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.4">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Idle.Utility\Idle.Utility.csproj" />
  </ItemGroup>

</Project>
EOF

cat > Idle.Utility.Tests/TickHelperTests.cs << 'EOF'
using Idle.Utility.Helpers;
using Xunit;

namespace Idle.Utility.Tests;

public class TickHelperTests
{
  [Fact]
  public void SecondsToTicks_WithDefaultTickRate_ReturnsCorrectValue()
  {
    var result = TickHelper.SecondsToTicks(1.0f);
    Assert.Equal(30, result);
  }

  [Fact]
  public void SecondsToTicks_WithCustomTickRate_ReturnsCorrectValue()
  {
    var result = TickHelper.SecondsToTicks(1.0f, 60);
    Assert.Equal(60, result);
  }

  [Fact]
  public void TicksToSeconds_WithDefaultTickRate_ReturnsCorrectValue()
  {
    var result = TickHelper.TicksToSeconds(30);
    Assert.Equal(1.0f, result);
  }

  [Fact]
  public void MillisecondsToTicks_ReturnsCorrectValue()
  {
    var result = TickHelper.MillisecondsToTicks(1000);
    Assert.Equal(30, result);
  }

  [Fact]
  public void TicksToMilliseconds_ReturnsCorrectValue()
  {
    var result = TickHelper.TicksToMilliseconds(30);
    Assert.Equal(1000, result);
  }
}
EOF

cat > Idle.Utility.Tests/RandomHelperTests.cs << 'EOF'
using Idle.Utility.Helpers;
using Xunit;

namespace Idle.Utility.Tests;

public class RandomHelperTests
{
  [Fact]
  public void NextFloat_ReturnsValueInRange()
  {
    var random = new Random(42);
    var result = random.NextFloat(0f, 10f);
    Assert.InRange(result, 0f, 10f);
  }

  [Fact]
  public void RollChance_With100Percent_AlwaysReturnsTrue()
  {
    var random = new Random(42);
    for (int i = 0; i < 10; i++)
    {
      Assert.True(random.RollChance(100f));
    }
  }

  [Fact]
  public void RollChance_With0Percent_AlwaysReturnsFalse()
  {
    var random = new Random(42);
    for (int i = 0; i < 10; i++)
    {
      Assert.False(random.RollChance(0f));
    }
  }

  [Fact]
  public void PickRandom_ReturnsElementFromList()
  {
    var random = new Random(42);
    var list = new List<int> { 1, 2, 3, 4, 5 };
    var result = random.PickRandom(list);
    Assert.Contains(result, list);
  }

  [Fact]
  public void PickRandom_WithEmptyList_ThrowsArgumentException()
  {
    var random = new Random(42);
    var list = new List<int>();
    Assert.Throws<ArgumentException>(() => random.PickRandom(list));
  }

  [Fact]
  public void Shuffle_ShufflesList()
  {
    var random = new Random(42);
    var list = new List<int> { 1, 2, 3, 4, 5 };
    var original = new List<int>(list);
    random.Shuffle(list);

    // List should still contain the same elements
    Assert.Equal(original.Count, list.Count);
    foreach (var item in original)
    {
      Assert.Contains(item, list);
    }
  }
}
EOF

cat > Idle.Utility.Tests/GuidHelperTests.cs << 'EOF'
using Idle.Utility.Helpers;
using Xunit;

namespace Idle.Utility.Tests;

public class GuidHelperTests
{
  [Fact]
  public void NewId_ReturnsNonEmptyGuid()
  {
    var id = GuidHelper.NewId();
    Assert.NotEqual(Guid.Empty, id);
  }

  [Fact]
  public void IsEmpty_WithEmptyGuid_ReturnsTrue()
  {
    var id = Guid.Empty;
    Assert.True(id.IsEmpty());
  }

  [Fact]
  public void IsEmpty_WithNonEmptyGuid_ReturnsFalse()
  {
    var id = Guid.NewGuid();
    Assert.False(id.IsEmpty());
  }

  [Fact]
  public void IsNullOrEmpty_WithNull_ReturnsTrue()
  {
    Guid? id = null;
    Assert.True(id.IsNullOrEmpty());
  }

  [Fact]
  public void IsNullOrEmpty_WithEmpty_ReturnsTrue()
  {
    Guid? id = Guid.Empty;
    Assert.True(id.IsNullOrEmpty());
  }

  [Fact]
  public void IsNullOrEmpty_WithValidGuid_ReturnsFalse()
  {
    Guid? id = Guid.NewGuid();
    Assert.False(id.IsNullOrEmpty());
  }
}
EOF

mkdir -p Idle.Core.Tests
cat > Idle.Core.Tests/Idle.Core.Tests.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>Idle.Core.Tests</RootNamespace>
    <AssemblyName>Idle.Core.Tests</AssemblyName>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="xunit" Version="2.6.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.4">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\Idle.Core\Idle.Core.csproj" />
  </ItemGroup>

</Project>
EOF

cat > Idle.Core.Tests/IdleComponentTests.cs << 'EOF'
using Idle.Core.Components;
using Idle.Core.Context;
using Xunit;

namespace Idle.Core.Tests;

public class TestComponent : IdleComponent
{
  public int TickCount { get; set; }

  public override void OnTick(TickContext context)
  {
    TickCount++;
    base.OnTick(context);
  }
}

public class IdleComponentTests
{
  [Fact]
  public void Constructor_GeneratesUniqueId()
  {
    var component1 = new TestComponent();
    var component2 = new TestComponent();
    Assert.NotEqual(component1.Id, component2.Id);
  }

  [Fact]
  public void SetParent_AddsToParentChildren()
  {
    var parent = new TestComponent();
    var child = new TestComponent();

    child.SetParent(parent);

    Assert.Contains(child, parent.Children);
    Assert.Equal(parent, child.Parent);
  }

  [Fact]
  public void RemoveParent_RemovesFromParentChildren()
  {
    var parent = new TestComponent();
    var child = new TestComponent();
    child.SetParent(parent);

    child.RemoveParent();

    Assert.DoesNotContain(child, parent.Children);
    Assert.Null(child.Parent);
  }

  [Fact]
  public void AddChild_SetsParent()
  {
    var parent = new TestComponent();
    var child = new TestComponent();

    parent.AddChild(child);

    Assert.Contains(child, parent.Children);
    Assert.Equal(parent, child.Parent);
  }

  [Fact]
  public void OnTick_ProcessesChildren()
  {
    var parent = new TestComponent();
    var child = new TestComponent();
    parent.AddChild(child);

    using var context = new TickContext();
    parent.OnTick(context);

    Assert.Equal(1, parent.TickCount);
    Assert.Equal(1, child.TickCount);
  }

  [Fact]
  public void GetChild_ReturnsFirstMatchingChild()
  {
    var parent = new TestComponent();
    var child1 = new TestComponent();
    var child2 = new TestComponent();
    parent.AddChild(child1);
    parent.AddChild(child2);

    var result = parent.GetChild<TestComponent>();

    Assert.Equal(child1, result);
  }

  [Fact]
  public void GetChildren_ReturnsAllMatchingChildren()
  {
    var parent = new TestComponent();
    var child1 = new TestComponent();
    var child2 = new TestComponent();
    parent.AddChild(child1);
    parent.AddChild(child2);

    var result = parent.GetChildren<TestComponent>().ToList();

    Assert.Equal(2, result.Count);
    Assert.Contains(child1, result);
    Assert.Contains(child2, result);
  }
}
EOF

cat > Idle.Core.Tests/TickContextTests.cs << 'EOF'
using Idle.Core.Context;
using Xunit;

namespace Idle.Core.Tests;

public class TickContextTests
{
  [Fact]
  public void Constructor_InitializesDefaults()
  {
    using var context = new TickContext();

    Assert.Equal(0, context.CurrentTick);
    Assert.Equal(0, context.MaxTick);
    Assert.False(context.IsBattleOver);
    Assert.Equal(EnumBattleResult.NotSpecified, context.Result);
    Assert.Equal(30, context.TickRate);
  }

  [Fact]
  public void Dispose_ClearsCollections()
  {
    var context = new TickContext();
    context.CreatorFaction.Add(new TestComponent());
    context.EnemyFaction.Add(new TestComponent());
    context.Projectiles.Add(new TestComponent());

    context.Dispose();

    Assert.Empty(context.CreatorFaction);
    Assert.Empty(context.EnemyFaction);
    Assert.Empty(context.Projectiles);
    Assert.Null(context.Map);
  }
}
EOF

mkdir -p IdleCOP.Gameplay.Tests
cat > IdleCOP.Gameplay.Tests/IdleCOP.Gameplay.Tests.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>IdleCOP.Gameplay.Tests</RootNamespace>
    <AssemblyName>IdleCOP.Gameplay.Tests</AssemblyName>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="xunit" Version="2.6.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.4">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\IdleCOP.Gameplay\IdleCOP.Gameplay.csproj" />
  </ItemGroup>

</Project>
EOF

cat > IdleCOP.Gameplay.Tests/ActorComponentTests.cs << 'EOF'
using IdleCOP.Gameplay.Combat;
using IdleCOP.Data.Enums;
using Xunit;

namespace IdleCOP.Gameplay.Tests;

public class ActorComponentTests
{
  [Fact]
  public void IsAlive_WithPositiveHealth_ReturnsTrue()
  {
    var actor = new ActorComponent { CurrentHealth = 100 };
    Assert.True(actor.IsAlive);
  }

  [Fact]
  public void IsAlive_WithZeroHealth_ReturnsFalse()
  {
    var actor = new ActorComponent { CurrentHealth = 0 };
    Assert.False(actor.IsAlive);
  }

  [Fact]
  public void TakeDamage_ReducesHealth()
  {
    var actor = new ActorComponent { CurrentHealth = 100, MaxHealth = 100 };

    var actualDamage = actor.TakeDamage(30);

    Assert.Equal(30, actualDamage);
    Assert.Equal(70, actor.CurrentHealth);
  }

  [Fact]
  public void TakeDamage_DoesNotExceedCurrentHealth()
  {
    var actor = new ActorComponent { CurrentHealth = 50, MaxHealth = 100 };

    var actualDamage = actor.TakeDamage(100);

    Assert.Equal(50, actualDamage);
    Assert.Equal(0, actor.CurrentHealth);
  }

  [Fact]
  public void Heal_IncreasesHealth()
  {
    var actor = new ActorComponent { CurrentHealth = 50, MaxHealth = 100 };

    var actualHeal = actor.Heal(30);

    Assert.Equal(30, actualHeal);
    Assert.Equal(80, actor.CurrentHealth);
  }

  [Fact]
  public void Heal_DoesNotExceedMaxHealth()
  {
    var actor = new ActorComponent { CurrentHealth = 80, MaxHealth = 100 };

    var actualHeal = actor.Heal(50);

    Assert.Equal(20, actualHeal);
    Assert.Equal(100, actor.CurrentHealth);
  }

  [Fact]
  public void ConsumeEnergy_ReturnsTrueWhenSufficient()
  {
    var actor = new ActorComponent { CurrentEnergy = 50, MaxEnergy = 50 };

    var result = actor.ConsumeEnergy(30);

    Assert.True(result);
    Assert.Equal(20, actor.CurrentEnergy);
  }

  [Fact]
  public void ConsumeEnergy_ReturnsFalseWhenInsufficient()
  {
    var actor = new ActorComponent { CurrentEnergy = 20, MaxEnergy = 50 };

    var result = actor.ConsumeEnergy(30);

    Assert.False(result);
    Assert.Equal(20, actor.CurrentEnergy);
  }
}
EOF

cat > IdleCOP.Gameplay.Tests/SkillComponentTests.cs << 'EOF'
using IdleCOP.Gameplay.Skills;
using IdleCOP.Data.Enums;
using Idle.Core.Context;
using Xunit;

namespace IdleCOP.Gameplay.Tests;

public class SkillComponentTests
{
  [Fact]
  public void IsReady_WithZeroCooldown_ReturnsTrue()
  {
    var skill = new SkillComponent { RemainingCooldownTicks = 0 };
    Assert.True(skill.IsReady);
  }

  [Fact]
  public void IsReady_WithRemainingCooldown_ReturnsFalse()
  {
    var skill = new SkillComponent { RemainingCooldownTicks = 10 };
    Assert.False(skill.IsReady);
  }

  [Fact]
  public void TriggerCooldown_SetsRemainingCooldown()
  {
    var skill = new SkillComponent { CooldownTicks = 30, RemainingCooldownTicks = 0 };

    skill.TriggerCooldown();

    Assert.Equal(30, skill.RemainingCooldownTicks);
  }

  [Fact]
  public void OnTick_DecreasesCooldown()
  {
    var skill = new SkillComponent { CooldownTicks = 30, RemainingCooldownTicks = 10 };
    using var context = new TickContext();

    skill.OnTick(context);

    Assert.Equal(9, skill.RemainingCooldownTicks);
  }

  [Fact]
  public void OnTick_DoesNotDecreaseBelowZero()
  {
    var skill = new SkillComponent { CooldownTicks = 30, RemainingCooldownTicks = 0 };
    using var context = new TickContext();

    skill.OnTick(context);

    Assert.Equal(0, skill.RemainingCooldownTicks);
  }
}
EOF

mkdir -p IdleCOP.AI.Tests
cat > IdleCOP.AI.Tests/IdleCOP.AI.Tests.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>IdleCOP.AI.Tests</RootNamespace>
    <AssemblyName>IdleCOP.AI.Tests</AssemblyName>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="xunit" Version="2.6.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.4">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\IdleCOP.AI\IdleCOP.AI.csproj" />
  </ItemGroup>

</Project>
EOF

cat > IdleCOP.AI.Tests/StrategyManagerTests.cs << 'EOF'
using IdleCOP.AI.Strategies;
using Idle.Core.Components;
using Idle.Core.Context;
using Xunit;

namespace IdleCOP.AI.Tests;

public class TestStrategy : StrategyBase
{
  private readonly int priority;
  private readonly bool canExecute;

  public bool WasExecuted { get; private set; }

  public override int Priority => priority;

  public TestStrategy(int priority, bool canExecute = true)
  {
    this.priority = priority;
    this.canExecute = canExecute;
  }

  public override bool CanExecute(TickContext context, IdleComponent actor)
  {
    return canExecute;
  }

  public override void Execute(TickContext context, IdleComponent actor)
  {
    WasExecuted = true;
  }
}

public class TestComponent : IdleComponent { }

public class StrategyManagerTests
{
  [Fact]
  public void AddStrategy_AddsStrategyToList()
  {
    var manager = new StrategyManager();
    var strategy = new TestStrategy(10);

    manager.AddStrategy(strategy);

    Assert.Contains(strategy, manager.GetStrategies());
  }

  [Fact]
  public void RemoveStrategy_RemovesStrategyFromList()
  {
    var manager = new StrategyManager();
    var strategy = new TestStrategy(10);
    manager.AddStrategy(strategy);

    manager.RemoveStrategy(strategy);

    Assert.DoesNotContain(strategy, manager.GetStrategies());
  }

  [Fact]
  public void GetStrategies_ReturnsSortedByPriority()
  {
    var manager = new StrategyManager();
    var lowPriority = new TestStrategy(10);
    var highPriority = new TestStrategy(100);
    manager.AddStrategy(lowPriority);
    manager.AddStrategy(highPriority);

    var strategies = manager.GetStrategies().ToList();

    Assert.Equal(highPriority, strategies[0]);
    Assert.Equal(lowPriority, strategies[1]);
  }

  [Fact]
  public void ExecuteFirst_ExecutesHighestPriorityStrategy()
  {
    var manager = new StrategyManager();
    var lowPriority = new TestStrategy(10);
    var highPriority = new TestStrategy(100);
    manager.AddStrategy(lowPriority);
    manager.AddStrategy(highPriority);

    using var context = new TickContext();
    var actor = new TestComponent();

    var result = manager.ExecuteFirst(context, actor);

    Assert.True(result);
    Assert.True(highPriority.WasExecuted);
    Assert.False(lowPriority.WasExecuted);
  }

  [Fact]
  public void ExecuteFirst_SkipsStrategiesThatCannotExecute()
  {
    var manager = new StrategyManager();
    var cannotExecute = new TestStrategy(100, canExecute: false);
    var canExecute = new TestStrategy(10, canExecute: true);
    manager.AddStrategy(cannotExecute);
    manager.AddStrategy(canExecute);

    using var context = new TickContext();
    var actor = new TestComponent();

    var result = manager.ExecuteFirst(context, actor);

    Assert.True(result);
    Assert.False(cannotExecute.WasExecuted);
    Assert.True(canExecute.WasExecuted);
  }

  [Fact]
  public void ExecuteAll_ExecutesAllExecutableStrategies()
  {
    var manager = new StrategyManager();
    var strategy1 = new TestStrategy(100);
    var strategy2 = new TestStrategy(10);
    var cannotExecute = new TestStrategy(50, canExecute: false);
    manager.AddStrategy(strategy1);
    manager.AddStrategy(strategy2);
    manager.AddStrategy(cannotExecute);

    using var context = new TickContext();
    var actor = new TestComponent();

    var count = manager.ExecuteAll(context, actor);

    Assert.Equal(2, count);
    Assert.True(strategy1.WasExecuted);
    Assert.True(strategy2.WasExecuted);
    Assert.False(cannotExecute.WasExecuted);
  }
}
EOF

mkdir -p IdleCOP.Data.Tests
cat > IdleCOP.Data.Tests/IdleCOP.Data.Tests.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>IdleCOP.Data.Tests</RootNamespace>
    <AssemblyName>IdleCOP.Data.Tests</AssemblyName>
    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.8.0" />
    <PackageReference Include="xunit" Version="2.6.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.5.4">
      <PrivateAssets>all</PrivateAssets>
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    </PackageReference>
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\IdleCOP.Data\IdleCOP.Data.csproj" />
  </ItemGroup>

</Project>
EOF

cat > IdleCOP.Data.Tests/EnumsTests.cs << 'EOF'
using IdleCOP.Data.Enums;
using Xunit;

namespace IdleCOP.Data.Tests;

public class EnumsTests
{
  [Fact]
  public void EnumQuality_DefaultValue_IsNotSpecified()
  {
    var quality = default(EnumQuality);
    Assert.Equal(EnumQuality.NotSpecified, quality);
  }

  [Fact]
  public void EnumSkillType_DefaultValue_IsNotSpecified()
  {
    var skillType = default(EnumSkillType);
    Assert.Equal(EnumSkillType.NotSpecified, skillType);
  }

  [Fact]
  public void EnumAffixType_DefaultValue_IsNotSpecified()
  {
    var affixType = default(EnumAffixType);
    Assert.Equal(EnumAffixType.NotSpecified, affixType);
  }

  [Fact]
  public void EnumFaction_DefaultValue_IsNotSpecified()
  {
    var faction = default(EnumFaction);
    Assert.Equal(EnumFaction.NotSpecified, faction);
  }

  [Fact]
  public void EnumResourceType_DefaultValue_IsNotSpecified()
  {
    var resourceType = default(EnumResourceType);
    Assert.Equal(EnumResourceType.NotSpecified, resourceType);
  }
}
EOF

cat > IdleCOP.Data.Tests/EntitiesTests.cs << 'EOF'
using IdleCOP.Data.Entities;
using IdleCOP.Data.Enums;
using Xunit;

namespace IdleCOP.Data.Tests;

public class EntitiesTests
{
  [Fact]
  public void ActorEntity_DefaultValues()
  {
    var actor = new ActorEntity();

    Assert.Equal(Guid.Empty, actor.Id);
    Assert.Equal(string.Empty, actor.Name);
    Assert.Equal(1, actor.Level);
    Assert.Equal(0, actor.Experience);
    Assert.Equal(EnumActorType.NotSpecified, actor.ActorType);
    Assert.Equal(EnumFaction.NotSpecified, actor.Faction);
  }

  [Fact]
  public void SkillEntity_DefaultValues()
  {
    var skill = new SkillEntity();

    Assert.Equal(Guid.Empty, skill.Id);
    Assert.Equal(EnumSkillType.NotSpecified, skill.SkillType);
    Assert.Equal(0, skill.Cooldown);
    Assert.Equal(0, skill.CastTime);
  }

  [Fact]
  public void EquipmentEntity_DefaultValues()
  {
    var equipment = new EquipmentEntity();

    Assert.Equal(Guid.Empty, equipment.Id);
    Assert.Equal(string.Empty, equipment.BaseType);
    Assert.Equal(EnumQuality.NotSpecified, equipment.Quality);
    Assert.False(equipment.IsIdentified);
  }

  [Fact]
  public void BaseEntity_SetsCreatedAtAndUpdatedAt()
  {
    var before = DateTime.UtcNow;
    var entity = new ActorEntity();
    var after = DateTime.UtcNow;

    Assert.InRange(entity.CreatedAt, before, after);
    Assert.InRange(entity.UpdatedAt, before, after);
  }
}
EOF

cat > IdleCOP.Data.Tests/DTOsTests.cs << 'EOF'
using IdleCOP.Data.DTOs;
using IdleCOP.Data.Enums;
using Xunit;

namespace IdleCOP.Data.Tests;

public class DTOsTests
{
  [Fact]
  public void CharacterDTO_DefaultValues()
  {
    var dto = new CharacterDTO();

    Assert.Equal(Guid.Empty, dto.Id);
    Assert.Equal(string.Empty, dto.Name);
    Assert.Equal(0, dto.Level);
    Assert.Equal(string.Empty, dto.Class);
  }

  [Fact]
  public void SkillDTO_DefaultValues()
  {
    var dto = new SkillDTO();

    Assert.Equal(Guid.Empty, dto.Id);
    Assert.Equal(EnumSkillType.NotSpecified, dto.SkillType);
    Assert.Equal(string.Empty, dto.Name);
  }

  [Fact]
  public void EquipmentDTO_DefaultValues()
  {
    var dto = new EquipmentDTO();

    Assert.Equal(Guid.Empty, dto.Id);
    Assert.Equal(string.Empty, dto.BaseType);
    Assert.Equal(EnumQuality.NotSpecified, dto.Quality);
    Assert.Empty(dto.Affixes);
  }

  [Fact]
  public void BattleSeedDTO_DefaultValues()
  {
    var dto = new BattleSeedDTO();

    Assert.Equal(Guid.Empty, dto.Id);
    Assert.Equal(Guid.Empty, dto.BattleGuid);
    Assert.Equal(string.Empty, dto.MapId);
    Assert.Empty(dto.CreatorCharacterIds);
    Assert.Empty(dto.EnemyCharacterIds);
    Assert.Empty(dto.Characters);
  }
}
EOF

echo "Setup complete!"
echo ""
echo "You can now build and test the project with:"
echo "  dotnet restore"
echo "  dotnet build"
echo "  dotnet test"
