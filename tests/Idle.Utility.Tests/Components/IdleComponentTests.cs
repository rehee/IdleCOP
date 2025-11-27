using Xunit;
using Idle.Utility.Components;

namespace Idle.Utility.Tests.Components;

/// <summary>
/// IdleComponent 测试
/// </summary>
public class IdleComponentTests
{
  [Fact]
  public void IdleComponent_NewInstance_HasUniqueId()
  {
    // Arrange & Act
    var component1 = new IdleComponent();
    var component2 = new IdleComponent();

    // Assert
    Assert.NotEqual(component1.Id, component2.Id);
    Assert.NotEqual(Guid.Empty, component1.Id);
  }

  [Fact]
  public void IdleComponent_NewInstance_HasNullParent()
  {
    // Arrange & Act
    var component = new IdleComponent();

    // Assert
    Assert.Null(component.Parent);
  }

  [Fact]
  public void IdleComponent_NewInstance_HasEmptyChildren()
  {
    // Arrange & Act
    var component = new IdleComponent();

    // Assert
    Assert.Empty(component.Children);
  }

  [Fact]
  public void SetParent_WithNewParent_SetsParentAndAddsToChildren()
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
  public void SetParent_WithNull_RemovesFromPreviousParent()
  {
    // Arrange
    var parent = new IdleComponent();
    var child = new IdleComponent();
    child.SetParent(parent);

    // Act
    child.SetParent(null);

    // Assert
    Assert.Null(child.Parent);
    Assert.DoesNotContain(child, parent.Children);
  }

  [Fact]
  public void SetParent_WithDifferentParent_MovesChild()
  {
    // Arrange
    var oldParent = new IdleComponent();
    var newParent = new IdleComponent();
    var child = new IdleComponent();
    child.SetParent(oldParent);

    // Act
    child.SetParent(newParent);

    // Assert
    Assert.Equal(newParent, child.Parent);
    Assert.DoesNotContain(child, oldParent.Children);
    Assert.Contains(child, newParent.Children);
  }

  [Fact]
  public void RemoveParent_RemovesParentRelationship()
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
  public void IdleComponent_ProfileKey_CanBeSetAndGet()
  {
    // Arrange
    var component = new IdleComponent();
    const int expectedKey = 123;

    // Act
    component.ProfileKey = expectedKey;

    // Assert
    Assert.Equal(expectedKey, component.ProfileKey);
  }

  [Fact]
  public void SetParent_MultipleChildren_AllHaveSameParent()
  {
    // Arrange
    var parent = new IdleComponent();
    var child1 = new IdleComponent();
    var child2 = new IdleComponent();
    var child3 = new IdleComponent();

    // Act
    child1.SetParent(parent);
    child2.SetParent(parent);
    child3.SetParent(parent);

    // Assert
    Assert.Equal(3, parent.Children.Count);
    Assert.All(new[] { child1, child2, child3 }, c => Assert.Equal(parent, c.Parent));
  }
}
