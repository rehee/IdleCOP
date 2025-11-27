using Xunit;
using IdleCOP.Data.Entities;
using Idle.Utility;

namespace IdleCOP.Data.Tests.Entities;

/// <summary>
/// ActorEntity 测试
/// </summary>
public class ActorEntityTests
{
  [Fact]
  public void ActorEntity_NewInstance_HasUniqueId()
  {
    // Arrange & Act
    var entity1 = new ActorEntity();
    var entity2 = new ActorEntity();

    // Assert
    Assert.NotEqual(entity1.Id, entity2.Id);
    Assert.NotEqual(Guid.Empty, entity1.Id);
  }

  [Fact]
  public void ActorEntity_InheritsFromBaseEntity()
  {
    // Arrange & Act
    var entity = new ActorEntity();

    // Assert
    Assert.IsAssignableFrom<Idle.Utility.Entities.BaseEntity>(entity);
  }

  [Fact]
  public void ActorEntity_ImplementsIWithName()
  {
    // Arrange & Act
    var entity = new ActorEntity();

    // Assert
    Assert.IsAssignableFrom<IWithName>(entity);
  }

  [Fact]
  public void ActorEntity_Level_DefaultsToZero()
  {
    // Arrange & Act
    var entity = new ActorEntity();

    // Assert
    Assert.Equal(0, entity.Level);
  }

  [Fact]
  public void ActorEntity_Level_CanBeSetAndGet()
  {
    // Arrange
    var entity = new ActorEntity();
    const int expectedLevel = 45;

    // Act
    entity.Level = expectedLevel;

    // Assert
    Assert.Equal(expectedLevel, entity.Level);
  }

  [Fact]
  public void ActorEntity_ProfileKey_DefaultsToZero()
  {
    // Arrange & Act
    var entity = new ActorEntity();

    // Assert
    Assert.Equal(0, entity.ProfileKey);
  }

  [Fact]
  public void ActorEntity_ProfileKey_CanBeSetAndGet()
  {
    // Arrange
    var entity = new ActorEntity();
    const int expectedKey = 100;

    // Act
    entity.ProfileKey = expectedKey;

    // Assert
    Assert.Equal(expectedKey, entity.ProfileKey);
  }

  [Fact]
  public void ActorEntity_CreatedAt_DefaultsToCurrentTime()
  {
    // Arrange
    var beforeCreation = DateTime.UtcNow;

    // Act
    var entity = new ActorEntity();
    var afterCreation = DateTime.UtcNow;

    // Assert
    Assert.True(entity.CreatedAt >= beforeCreation);
    Assert.True(entity.CreatedAt <= afterCreation);
  }

  [Fact]
  public void ActorEntity_UpdatedAt_DefaultsToCurrentTime()
  {
    // Arrange
    var beforeCreation = DateTime.UtcNow;

    // Act
    var entity = new ActorEntity();
    var afterCreation = DateTime.UtcNow;

    // Assert
    Assert.True(entity.UpdatedAt >= beforeCreation);
    Assert.True(entity.UpdatedAt <= afterCreation);
  }

  [Fact]
  public void ActorEntity_Name_CanBeSetAndGet()
  {
    // Arrange
    var entity = new ActorEntity();
    const string expectedName = "Test Actor";

    // Act
    entity.Name = expectedName;

    // Assert
    Assert.Equal(expectedName, entity.Name);
  }

  [Fact]
  public void ActorEntity_Description_ReturnsNull()
  {
    // Arrange & Act
    var entity = new ActorEntity();

    // Assert
    Assert.Null(entity.Description);
  }
}
