# Setup script for IdleCOP (PowerShell)
# This script creates project directories and files that couldn't be created by the automated system

Write-Host "Setting up IdleCOP project structure..."

# Create IdleCOP.Client.Console project
New-Item -ItemType Directory -Force -Path "IdleCOP.Client.Console" | Out-Null

@'
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
'@ | Set-Content "IdleCOP.Client.Console\IdleCOP.Client.Console.csproj"

@'
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
    for ($i = 0; $i -lt 5; $i++)
    {
      context.CurrentTick++;
      player.OnTick(context);
      enemy.OnTick(context);

      System.Console.WriteLine($"Tick {context.CurrentTick}: Player HP={player.CurrentHealth}, Enemy HP={enemy.CurrentHealth}");

      // Simulate player dealing damage to enemy
      if (fireball.IsReady && player.ConsumeEnergy(fireball.ResourceCost))
      {
        $damage = 15;
        enemy.TakeDamage($damage);
        fireball.TriggerCooldown();
        System.Console.WriteLine($"  -> Player uses Fireball! Deals {$damage} damage.");
      }

      if (!enemy.IsAlive)
      {
        System.Console.WriteLine("  -> Enemy defeated!");
        context.IsBattleOver = $true;
        context.Result = EnumBattleResult.Victory;
        break;
      }
    }

    System.Console.WriteLine();
    System.Console.WriteLine($"Battle Result: {context.Result}");
  }
}
'@ | Set-Content "IdleCOP.Client.Console\Program.cs"

# Create test project directories and files
$testProjects = @(
    @{Name = "Idle.Utility.Tests"; Reference = "..\Idle.Utility\Idle.Utility.csproj"},
    @{Name = "Idle.Core.Tests"; Reference = "..\Idle.Core\Idle.Core.csproj"},
    @{Name = "IdleCOP.Gameplay.Tests"; Reference = "..\IdleCOP.Gameplay\IdleCOP.Gameplay.csproj"},
    @{Name = "IdleCOP.AI.Tests"; Reference = "..\IdleCOP.AI\IdleCOP.AI.csproj"},
    @{Name = "IdleCOP.Data.Tests"; Reference = "..\IdleCOP.Data\IdleCOP.Data.csproj"}
)

foreach ($project in $testProjects) {
    New-Item -ItemType Directory -Force -Path $project.Name | Out-Null

    $csprojContent = @"
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>$($project.Name)</RootNamespace>
    <AssemblyName>$($project.Name)</AssemblyName>
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
    <ProjectReference Include="$($project.Reference)" />
  </ItemGroup>

</Project>
"@
    $csprojContent | Set-Content "$($project.Name)\$($project.Name).csproj"
}

# Create placeholder test file for each test project
@'
using Xunit;

namespace Idle.Utility.Tests;

public class PlaceholderTests
{
    [Fact]
    public void Placeholder_Test()
    {
        Assert.True(true);
    }
}
'@ | Set-Content "Idle.Utility.Tests\PlaceholderTests.cs"

@'
using Xunit;

namespace Idle.Core.Tests;

public class PlaceholderTests
{
    [Fact]
    public void Placeholder_Test()
    {
        Assert.True(true);
    }
}
'@ | Set-Content "Idle.Core.Tests\PlaceholderTests.cs"

@'
using Xunit;

namespace IdleCOP.Gameplay.Tests;

public class PlaceholderTests
{
    [Fact]
    public void Placeholder_Test()
    {
        Assert.True(true);
    }
}
'@ | Set-Content "IdleCOP.Gameplay.Tests\PlaceholderTests.cs"

@'
using Xunit;

namespace IdleCOP.AI.Tests;

public class PlaceholderTests
{
    [Fact]
    public void Placeholder_Test()
    {
        Assert.True(true);
    }
}
'@ | Set-Content "IdleCOP.AI.Tests\PlaceholderTests.cs"

@'
using Xunit;

namespace IdleCOP.Data.Tests;

public class PlaceholderTests
{
    [Fact]
    public void Placeholder_Test()
    {
        Assert.True(true);
    }
}
'@ | Set-Content "IdleCOP.Data.Tests\PlaceholderTests.cs"

Write-Host "Setup complete!"
Write-Host ""
Write-Host "You can now build and test the project with:"
Write-Host "  dotnet restore"
Write-Host "  dotnet build"
Write-Host "  dotnet test"
