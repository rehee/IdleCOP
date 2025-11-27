using Xunit;
using Idle.Utility.Entities;

namespace Idle.Utility.Tests.Entities;

/// <summary>
/// BaseEntity 的测试用具体实现
/// </summary>
public class TestEntity : BaseEntity
{
  public override string? Description => "Test description";
}

/// <summary>
/// BaseEntity 测试
/// </summary>
public class BaseEntityTests
{
  [Fact]
  public void BaseEntity_NewInstance_HasUniqueId()
  {
    // Arrange & Act
    var entity1 = new TestEntity();
    var entity2 = new TestEntity();

    // Assert
    Assert.NotEqual(entity1.Id, entity2.Id);
    Assert.NotEqual(Guid.Empty, entity1.Id);
  }

  [Fact]
  public void BaseEntity_Name_CanBeSetAndGet()
  {
    // Arrange
    var entity = new TestEntity();
    const string expectedName = "Test Entity Name";

    // Act
    entity.Name = expectedName;

    // Assert
    Assert.Equal(expectedName, entity.Name);
  }

  [Fact]
  public void BaseEntity_Id_CanBeSetAndGet()
  {
    // Arrange
    var entity = new TestEntity();
    var expectedId = Guid.NewGuid();

    // Act
    entity.Id = expectedId;

    // Assert
    Assert.Equal(expectedId, entity.Id);
  }

  [Fact]
  public void BaseEntity_ImplementsIWithName()
  {
    // Arrange
    var entity = new TestEntity();
    entity.Name = "Test";

    // Assert
    Assert.IsAssignableFrom<IWithName>(entity);
    Assert.Equal("Test", ((IWithName)entity).Name);
    Assert.Equal("Test description", ((IWithName)entity).Description);
  }

  [Fact]
  public void BaseEntity_Description_ReturnsExpectedValue()
  {
    // Arrange
    var entity = new TestEntity();

    // Assert
    Assert.Equal("Test description", entity.Description);
  }
}
