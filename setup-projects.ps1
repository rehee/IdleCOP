# Setup script for IdleCOP (PowerShell)
# This script creates project directories and files that couldn't be created by the automated system

Write-Host "Setting up IdleCOP project structure..."

# Create folder structure for Idle.Utility
Write-Host "Organizing Idle.Utility folder structure..."
New-Item -ItemType Directory -Force -Path "Idle.Utility/Helpers" | Out-Null
New-Item -ItemType Directory -Force -Path "Idle.Utility/Randoms" | Out-Null
New-Item -ItemType Directory -Force -Path "Idle.Utility/Repository" | Out-Null
New-Item -ItemType Directory -Force -Path "Idle.Utility/Container" | Out-Null
New-Item -ItemType Directory -Force -Path "Idle.Utility/Commons" | Out-Null

# Move Idle.Utility files to proper folders
if (Test-Path "Idle.Utility/MathHelper.cs") { Move-Item "Idle.Utility/MathHelper.cs" "Idle.Utility/Helpers/" -Force }
if (Test-Path "Idle.Utility/TickHelper.cs") { Move-Item "Idle.Utility/TickHelper.cs" "Idle.Utility/Helpers/" -Force }
if (Test-Path "Idle.Utility/RandomHelper.cs") { Move-Item "Idle.Utility/RandomHelper.cs" "Idle.Utility/Helpers/" -Force }
if (Test-Path "Idle.Utility/GuidHelper.cs") { Move-Item "Idle.Utility/GuidHelper.cs" "Idle.Utility/Helpers/" -Force }

# Create folder structure for Idle.Core
Write-Host "Organizing Idle.Core folder structure..."
New-Item -ItemType Directory -Force -Path "Idle.Core/Enums" | Out-Null
New-Item -ItemType Directory -Force -Path "Idle.Core/Components" | Out-Null
New-Item -ItemType Directory -Force -Path "Idle.Core/Profiles" | Out-Null
New-Item -ItemType Directory -Force -Path "Idle.Core/Commons" | Out-Null

# Move Idle.Core files to proper folders
if (Test-Path "Idle.Core/EnumBattleResult.cs") { Move-Item "Idle.Core/EnumBattleResult.cs" "Idle.Core/Enums/" -Force }
if (Test-Path "Idle.Core/IdleComponent.cs") { Move-Item "Idle.Core/IdleComponent.cs" "Idle.Core/Components/" -Force }
if (Test-Path "Idle.Core/IdleProfile.cs") { Move-Item "Idle.Core/IdleProfile.cs" "Idle.Core/Profiles/" -Force }
if (Test-Path "Idle.Core/TickContext.cs") { Move-Item "Idle.Core/TickContext.cs" "Idle.Core/Commons/" -Force }

# Move interfaces from Idle.Core to Idle.Utility based on review feedback
if (Test-Path "Idle.Core/IRandom.cs") { Move-Item "Idle.Core/IRandom.cs" "Idle.Utility/Randoms/" -Force }
if (Test-Path "Idle.Core/IRepository.cs") { Move-Item "Idle.Core/IRepository.cs" "Idle.Utility/Repository/" -Force }
if (Test-Path "Idle.Core/IServiceContainer.cs") { Move-Item "Idle.Core/IServiceContainer.cs" "Idle.Utility/Container/" -Force }
if (Test-Path "Idle.Core/IWithName.cs") { Move-Item "Idle.Core/IWithName.cs" "Idle.Utility/Commons/" -Force }

# Create folder structure for IdleCOP.Data
Write-Host "Organizing IdleCOP.Data folder structure..."
New-Item -ItemType Directory -Force -Path "IdleCOP.Data/Enums" | Out-Null
New-Item -ItemType Directory -Force -Path "IdleCOP.Data/Entities" | Out-Null
New-Item -ItemType Directory -Force -Path "IdleCOP.Data/DTOs" | Out-Null

# Move IdleCOP.Data files to proper folders
if (Test-Path "IdleCOP.Data/EnumQuality.cs") { Move-Item "IdleCOP.Data/EnumQuality.cs" "IdleCOP.Data/Enums/" -Force }
if (Test-Path "IdleCOP.Data/EnumSkillType.cs") { Move-Item "IdleCOP.Data/EnumSkillType.cs" "IdleCOP.Data/Enums/" -Force }
if (Test-Path "IdleCOP.Data/EnumAffixType.cs") { Move-Item "IdleCOP.Data/EnumAffixType.cs" "IdleCOP.Data/Enums/" -Force }
if (Test-Path "IdleCOP.Data/EnumResourceType.cs") { Move-Item "IdleCOP.Data/EnumResourceType.cs" "IdleCOP.Data/Enums/" -Force }
if (Test-Path "IdleCOP.Data/EnumDurationType.cs") { Move-Item "IdleCOP.Data/EnumDurationType.cs" "IdleCOP.Data/Enums/" -Force }
if (Test-Path "IdleCOP.Data/EnumFaction.cs") { Move-Item "IdleCOP.Data/EnumFaction.cs" "IdleCOP.Data/Enums/" -Force }
if (Test-Path "IdleCOP.Data/EnumActorType.cs") { Move-Item "IdleCOP.Data/EnumActorType.cs" "IdleCOP.Data/Enums/" -Force }

if (Test-Path "IdleCOP.Data/BaseEntity.cs") { Move-Item "IdleCOP.Data/BaseEntity.cs" "IdleCOP.Data/Entities/" -Force }
if (Test-Path "IdleCOP.Data/ActorEntity.cs") { Move-Item "IdleCOP.Data/ActorEntity.cs" "IdleCOP.Data/Entities/" -Force }
if (Test-Path "IdleCOP.Data/SkillEntity.cs") { Move-Item "IdleCOP.Data/SkillEntity.cs" "IdleCOP.Data/Entities/" -Force }
if (Test-Path "IdleCOP.Data/EquipmentEntity.cs") { Move-Item "IdleCOP.Data/EquipmentEntity.cs" "IdleCOP.Data/Entities/" -Force }
if (Test-Path "IdleCOP.Data/AffixEntity.cs") { Move-Item "IdleCOP.Data/AffixEntity.cs" "IdleCOP.Data/Entities/" -Force }
if (Test-Path "IdleCOP.Data/StrategyEntity.cs") { Move-Item "IdleCOP.Data/StrategyEntity.cs" "IdleCOP.Data/Entities/" -Force }

if (Test-Path "IdleCOP.Data/BaseDTO.cs") { Move-Item "IdleCOP.Data/BaseDTO.cs" "IdleCOP.Data/DTOs/" -Force }
if (Test-Path "IdleCOP.Data/CharacterDTO.cs") { Move-Item "IdleCOP.Data/CharacterDTO.cs" "IdleCOP.Data/DTOs/" -Force }
if (Test-Path "IdleCOP.Data/SkillDTO.cs") { Move-Item "IdleCOP.Data/SkillDTO.cs" "IdleCOP.Data/DTOs/" -Force }
if (Test-Path "IdleCOP.Data/EquipmentDTO.cs") { Move-Item "IdleCOP.Data/EquipmentDTO.cs" "IdleCOP.Data/DTOs/" -Force }
if (Test-Path "IdleCOP.Data/AffixDTO.cs") { Move-Item "IdleCOP.Data/AffixDTO.cs" "IdleCOP.Data/DTOs/" -Force }
if (Test-Path "IdleCOP.Data/BattleSeedDTO.cs") { Move-Item "IdleCOP.Data/BattleSeedDTO.cs" "IdleCOP.Data/DTOs/" -Force }

# Create folder structure for IdleCOP.Gameplay
Write-Host "Organizing IdleCOP.Gameplay folder structure..."
New-Item -ItemType Directory -Force -Path "IdleCOP.Gameplay/Combat" | Out-Null
New-Item -ItemType Directory -Force -Path "IdleCOP.Gameplay/Skills" | Out-Null
New-Item -ItemType Directory -Force -Path "IdleCOP.Gameplay/Equipment" | Out-Null
New-Item -ItemType Directory -Force -Path "IdleCOP.Gameplay/Maps" | Out-Null
New-Item -ItemType Directory -Force -Path "IdleCOP.Gameplay/Instructions" | Out-Null

# Move IdleCOP.Gameplay files to proper folders
if (Test-Path "IdleCOP.Gameplay/ActorComponent.cs") { Move-Item "IdleCOP.Gameplay/ActorComponent.cs" "IdleCOP.Gameplay/Combat/" -Force }
if (Test-Path "IdleCOP.Gameplay/SkillComponent.cs") { Move-Item "IdleCOP.Gameplay/SkillComponent.cs" "IdleCOP.Gameplay/Skills/" -Force }
if (Test-Path "IdleCOP.Gameplay/EquipmentComponent.cs") { Move-Item "IdleCOP.Gameplay/EquipmentComponent.cs" "IdleCOP.Gameplay/Equipment/" -Force }
if (Test-Path "IdleCOP.Gameplay/AffixComponent.cs") { Move-Item "IdleCOP.Gameplay/AffixComponent.cs" "IdleCOP.Gameplay/Equipment/" -Force }
if (Test-Path "IdleCOP.Gameplay/MapComponent.cs") { Move-Item "IdleCOP.Gameplay/MapComponent.cs" "IdleCOP.Gameplay/Maps/" -Force }
if (Test-Path "IdleCOP.Gameplay/ZoneComponent.cs") { Move-Item "IdleCOP.Gameplay/ZoneComponent.cs" "IdleCOP.Gameplay/Maps/" -Force }
if (Test-Path "IdleCOP.Gameplay/InstructionComponent.cs") { Move-Item "IdleCOP.Gameplay/InstructionComponent.cs" "IdleCOP.Gameplay/Instructions/" -Force }
if (Test-Path "IdleCOP.Gameplay/InstructionSetComponent.cs") { Move-Item "IdleCOP.Gameplay/InstructionSetComponent.cs" "IdleCOP.Gameplay/Instructions/" -Force }

# Create folder structure for IdleCOP.AI
Write-Host "Organizing IdleCOP.AI folder structure..."
New-Item -ItemType Directory -Force -Path "IdleCOP.AI/Strategies" | Out-Null

# Move IdleCOP.AI files to proper folders
if (Test-Path "IdleCOP.AI/IStrategy.cs") { Move-Item "IdleCOP.AI/IStrategy.cs" "IdleCOP.AI/Strategies/" -Force }
if (Test-Path "IdleCOP.AI/StrategyManager.cs") { Move-Item "IdleCOP.AI/StrategyManager.cs" "IdleCOP.AI/Strategies/" -Force }

# Remove old placeholder and obsolete files
if (Test-Path "Idle.Utility/Placeholder.cs") { Remove-Item "Idle.Utility/Placeholder.cs" -Force }
if (Test-Path "IdleCOP.Data/Entities.cs") { Remove-Item "IdleCOP.Data/Entities.cs" -Force }
if (Test-Path "IdleCOP.Data/DTOs.cs") { Remove-Item "IdleCOP.Data/DTOs.cs" -Force }
if (Test-Path "IdleCOP.Data/Enums.cs") { Remove-Item "IdleCOP.Data/Enums.cs" -Force }

Write-Host "Folder structure reorganization complete!"
Write-Host ""

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
