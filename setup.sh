#!/bin/bash
# IdleCOP Project Setup Script
# This script creates the necessary directory structure and project files for IdleCOP

set -e

echo "Creating IdleCOP project structure..."

# Create Idle.Utility project structure
mkdir -p Idle.Utility/Helpers

# Create Idle.Core project structure
mkdir -p Idle.Core/Components
mkdir -p Idle.Core/Profiles
mkdir -p Idle.Core/Context
mkdir -p Idle.Core/Repository
mkdir -p Idle.Core/DI

# Create IdleCOP.Gameplay project structure
mkdir -p IdleCOP.Gameplay/Combat
mkdir -p IdleCOP.Gameplay/Skills
mkdir -p IdleCOP.Gameplay/Equipment
mkdir -p IdleCOP.Gameplay/Maps
mkdir -p IdleCOP.Gameplay/Instructions

# Create IdleCOP.AI project structure
mkdir -p IdleCOP.AI/Strategies

# Create IdleCOP.Data project structure
mkdir -p IdleCOP.Data/Entities
mkdir -p IdleCOP.Data/DTOs
mkdir -p IdleCOP.Data/Configs

# Create IdleCOP.Client.Web project structure
mkdir -p IdleCOP.Client.Web/Pages
mkdir -p IdleCOP.Client.Web/Shared
mkdir -p IdleCOP.Client.Web/wwwroot

# Create config directories
mkdir -p configs/equipment
mkdir -p configs/skills
mkdir -p configs/effects
mkdir -p configs/strategies
mkdir -p configs/maps

echo "Creating project files..."

# Create Idle.Utility.csproj
cat > Idle.Utility/Idle.Utility.csproj << 'EOF'
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <RootNamespace>Idle.Utility</RootNamespace>
    <AssemblyName>Idle.Utility</AssemblyName>
    <Description>Common utility library for IdleCOP - independent of game logic</Description>
  </PropertyGroup>

</Project>
EOF

# Create Idle.Core.csproj
cat > Idle.Core/Idle.Core.csproj << 'EOF'
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
EOF

# Create IdleCOP.Gameplay.csproj
cat > IdleCOP.Gameplay/IdleCOP.Gameplay.csproj << 'EOF'
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
EOF

# Create IdleCOP.AI.csproj
cat > IdleCOP.AI/IdleCOP.AI.csproj << 'EOF'
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
EOF

# Create IdleCOP.Data.csproj
cat > IdleCOP.Data/IdleCOP.Data.csproj << 'EOF'
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
EOF

# Create IdleCOP.Client.Web.csproj
cat > IdleCOP.Client.Web/IdleCOP.Client.Web.csproj << 'EOF'
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
EOF

echo "Directory structure and project files created successfully!"
echo ""
echo "Next steps:"
echo "1. Run 'dotnet restore' to restore packages"
echo "2. Run 'dotnet build' to build the solution"
echo "3. Run 'dotnet run --project IdleCOP.Client.Web' to start the web client"
