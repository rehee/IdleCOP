# IdleCOP Project Setup Script for Windows
# This script creates the necessary directory structure and project files for IdleCOP

Write-Host "Creating IdleCOP project structure..." -ForegroundColor Green

# Create Idle.Utility project structure
New-Item -ItemType Directory -Force -Path "Idle.Utility/Helpers" | Out-Null

# Create Idle.Core project structure
New-Item -ItemType Directory -Force -Path "Idle.Core/Components" | Out-Null
New-Item -ItemType Directory -Force -Path "Idle.Core/Profiles" | Out-Null
New-Item -ItemType Directory -Force -Path "Idle.Core/Context" | Out-Null
New-Item -ItemType Directory -Force -Path "Idle.Core/Repository" | Out-Null
New-Item -ItemType Directory -Force -Path "Idle.Core/DI" | Out-Null

# Create IdleCOP.Gameplay project structure
New-Item -ItemType Directory -Force -Path "IdleCOP.Gameplay/Combat" | Out-Null
New-Item -ItemType Directory -Force -Path "IdleCOP.Gameplay/Skills" | Out-Null
New-Item -ItemType Directory -Force -Path "IdleCOP.Gameplay/Equipment" | Out-Null
New-Item -ItemType Directory -Force -Path "IdleCOP.Gameplay/Maps" | Out-Null
New-Item -ItemType Directory -Force -Path "IdleCOP.Gameplay/Instructions" | Out-Null

# Create IdleCOP.AI project structure
New-Item -ItemType Directory -Force -Path "IdleCOP.AI/Strategies" | Out-Null

# Create IdleCOP.Data project structure
New-Item -ItemType Directory -Force -Path "IdleCOP.Data/Entities" | Out-Null
New-Item -ItemType Directory -Force -Path "IdleCOP.Data/DTOs" | Out-Null
New-Item -ItemType Directory -Force -Path "IdleCOP.Data/Configs" | Out-Null

# Create IdleCOP.Client.Web project structure
New-Item -ItemType Directory -Force -Path "IdleCOP.Client.Web/Pages" | Out-Null
New-Item -ItemType Directory -Force -Path "IdleCOP.Client.Web/Shared" | Out-Null
New-Item -ItemType Directory -Force -Path "IdleCOP.Client.Web/wwwroot" | Out-Null

# Create config directories
New-Item -ItemType Directory -Force -Path "configs/equipment" | Out-Null
New-Item -ItemType Directory -Force -Path "configs/skills" | Out-Null
New-Item -ItemType Directory -Force -Path "configs/effects" | Out-Null
New-Item -ItemType Directory -Force -Path "configs/strategies" | Out-Null
New-Item -ItemType Directory -Force -Path "configs/maps" | Out-Null

Write-Host "Creating project files..." -ForegroundColor Green

# Create Idle.Utility.csproj
@'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>Idle.Utility</RootNamespace>
    <AssemblyName>Idle.Utility</AssemblyName>
    <Description>Common utility library for IdleCOP - independent of game logic</Description>
  </PropertyGroup>

</Project>
'@ | Set-Content -Path "Idle.Utility/Idle.Utility.csproj"

# Create Idle.Core.csproj
@'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>Idle.Core</RootNamespace>
    <AssemblyName>Idle.Core</AssemblyName>
    <Description>Core base library for IdleCOP - generic gameplay framework</Description>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Idle.Utility\Idle.Utility.csproj" />
  </ItemGroup>

</Project>
'@ | Set-Content -Path "Idle.Core/Idle.Core.csproj"

# Create IdleCOP.Gameplay.csproj
@'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>IdleCOP.Gameplay</RootNamespace>
    <AssemblyName>IdleCOP.Gameplay</AssemblyName>
    <Description>IdleCOP game implementation</Description>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Idle.Core\Idle.Core.csproj" />
  </ItemGroup>

</Project>
'@ | Set-Content -Path "IdleCOP.Gameplay/IdleCOP.Gameplay.csproj"

# Create IdleCOP.AI.csproj
@'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>IdleCOP.AI</RootNamespace>
    <AssemblyName>IdleCOP.AI</AssemblyName>
    <Description>AI and behavior system for IdleCOP</Description>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Idle.Core\Idle.Core.csproj" />
  </ItemGroup>

</Project>
'@ | Set-Content -Path "IdleCOP.AI/IdleCOP.AI.csproj"

# Create IdleCOP.Data.csproj
@'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>IdleCOP.Data</RootNamespace>
    <AssemblyName>IdleCOP.Data</AssemblyName>
    <Description>Data management for IdleCOP</Description>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\Idle.Core\Idle.Core.csproj" />
  </ItemGroup>

</Project>
'@ | Set-Content -Path "IdleCOP.Data/IdleCOP.Data.csproj"

# Create IdleCOP.Client.Web.csproj
@'
<Project Sdk="Microsoft.NET.Sdk.BlazorWebAssembly">

  <PropertyGroup>
    <RootNamespace>IdleCOP.Client.Web</RootNamespace>
    <AssemblyName>IdleCOP.Client.Web</AssemblyName>
    <Description>Blazor WebAssembly client for IdleCOP</Description>
  </PropertyGroup>

  <ItemGroup>
    <ProjectReference Include="..\IdleCOP.Gameplay\IdleCOP.Gameplay.csproj" />
    <ProjectReference Include="..\IdleCOP.AI\IdleCOP.AI.csproj" />
    <ProjectReference Include="..\IdleCOP.Data\IdleCOP.Data.csproj" />
  </ItemGroup>

</Project>
'@ | Set-Content -Path "IdleCOP.Client.Web/IdleCOP.Client.Web.csproj"

Write-Host ""
Write-Host "Directory structure and project files created successfully!" -ForegroundColor Green
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Yellow
Write-Host "1. Run 'dotnet restore' to restore packages"
Write-Host "2. Run 'dotnet build' to build the solution"
Write-Host "3. Run 'dotnet run --project IdleCOP.Client.Web' to start the web client"
