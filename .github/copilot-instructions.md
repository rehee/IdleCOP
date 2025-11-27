# Copilot Instructions for IdleCOP

This document provides instructions for GitHub Copilot when working with the IdleCOP codebase.

## Project Overview

IdleCOP is an ARPG idle game built with C# and Bootstrap Blazor. It features equipment systems, skill systems, instruction/AI systems, and procedurally generated maps.

## Technology Stack

- **Language**: C# (.NET 8.0+)
- **Frontend**: Bootstrap Blazor
- **Desktop**: WPF + Blazor Hybrid
- **Mobile**: MAUI + Blazor Hybrid
- **Storage**: SQLite (local) / IndexedDB (web) / PostgreSQL (server)

## Code Style Guidelines

### Indentation

- Use **2 spaces** for indentation, not tabs.

### File Organization

- **One class per file**: Each `.cs` file should contain only one class
- **Enums**: Place all enum types in the `Enums/` directory of the appropriate project
- **Enum naming**: Use `Enum[Type]` naming (e.g., `EnumMonster`, `EnumPlayer`, `EnumMap`) without suffix like "Profile"

### Naming Conventions

| Type | Convention | Example |
|------|------------|---------|
| Enums | Prefix with `Enum`, default value `NotSpecified = 0` | `EnumQuality`, `EnumMonster`, `EnumMap` |
| Entities | Suffix with `Entity` | `ActorEntity`, `SkillEntity` |
| DTOs | Suffix with `DTO` | `CharacterDTO`, `SkillDTO` |
| Helper/Extension Methods | Suffix with `Helper`, place in `Helpers/` directory | `TickHelper`, `CombatStatsHelper` |
| Private Members | camelCase, no `_` prefix | `private readonly IService service;` |
| Public Members | PascalCase | `public int ProfileKey { get; set; }` |
| Id Fields | Use `Guid` for all Entity/DTO/Component IDs | `public Guid Id { get; set; }` |

### Example Enum

```csharp
public enum EnumQuality
{
  NotSpecified = 0,
  Normal,
  Magic,
  Rare,
  Legendary,
  Unique
}
```

### Example Service Class

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
```

## Architecture Patterns

### Component-Profile Pattern

IdleCOP uses a Component-Profile pattern where:

- **Component (IdleComponent)**: Contains instance data and state
- **Profile (IdleProfile)**: Contains logic as a singleton, processes components

```csharp
// Non-generic Profile base class
public abstract class IdleProfile : IWithName
{
  public abstract int Key { get; }
  public virtual int? KeyOverride { get; }
  public abstract string? Name { get; }
  public abstract string? Description { get; }
}

// Generic Profile with Enum constraint
public abstract class IdleProfile<TEnum> : IdleProfile where TEnum : struct, Enum
{
  public abstract TEnum ProfileType { get; }
  public override int Key => Convert.ToInt32(ProfileType);
}
```

### Profile Hierarchy Example

```csharp
// Base Profile (non-generic)
public abstract class ActorProfile : IdleProfile { ... }

// Generic Actor Profile with Enum constraint  
public abstract class ActorProfile<TEnum> : ActorProfile where TEnum : struct, Enum
{
  public abstract TEnum ProfileType { get; }
  public override int Key => Convert.ToInt32(ProfileType);
}

// Base Monster Profile (generic)
public abstract class MonsterProfile : ActorProfile<EnumMonster> { ... }

// Concrete Monster Profile (one per file)
public class SkeletonWarriorProfile : MonsterProfile
{
  public override EnumMonster ProfileType => EnumMonster.SkeletonWarrior;
  public override string? Name => "骷髅战士";
  // ... other properties
}
```

### Method Signature Correspondence

Profile methods must correspond to Component methods, but with an additional `IdleComponent` parameter:

| Component Method | Profile Method |
|-----------------|----------------|
| `OnTick(TickContext context)` | `OnTick(IdleComponent component, TickContext context)` |

### Data Persistence

- Persistent data is stored as `XXXEntity`
- Entities generate `IdleComponent` instances based on their corresponding Profile
- Storage varies by platform (database, IndexedDB, etc.)

## Project Structure

```
src/
├── Idle.Utility/           # Common utility library (no game dependencies)
│   └── Helpers/            # Helper classes (XXXHelper)
├── Idle.Core/              # Core base library (generic gameplay framework)
│   ├── Components/         # Component system base classes
│   ├── Profiles/           # Profile singleton logic
│   ├── Context/            # TickContext battle context
│   ├── Repository/         # IRepository/IContext data access abstractions
│   └── DI/                 # Dependency injection abstractions
├── IdleCOP.Gameplay/       # COP game implementation
│   ├── Combat/             # Combat system
│   ├── Skills/             # Skill system
│   ├── Equipment/          # Equipment system
│   ├── Maps/               # Map system
│   └── Instructions/       # Instruction system
├── IdleCOP.AI/             # AI and behavior
│   └── Strategies/         # Configurable strategy list
├── IdleCOP.Data/           # Data management
│   ├── Entities/           # Persistence entities
│   ├── DTOs/               # Data transfer objects
│   └── Configs/            # Configuration files
└── IdleCOP.Client/         # Client applications
    ├── Web/                # Blazor Web
    ├── Desktop/            # WPF Blazor Hybrid
    └── Mobile/             # MAUI Blazor Hybrid
```

## Commit Message Format

Follow [Conventional Commits](https://www.conventionalcommits.org/):

```
<type>(<scope>): <subject>
```

Types: `feat`, `fix`, `docs`, `style`, `refactor`, `test`, `chore`

Example:
```
feat(equipment): add affix generation system
```

## Documentation

- Public APIs must have XML documentation comments
- Complex logic should have inline comments
- README and design documents are written in Chinese
