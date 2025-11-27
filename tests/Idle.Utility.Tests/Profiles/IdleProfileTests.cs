using Xunit;
using Idle.Utility.Profiles;

namespace Idle.Utility.Tests.Profiles;

/// <summary>
/// IdleProfile 的测试用具体实现
/// </summary>
public class TestProfile : IdleProfile
{
  private readonly int key;
  private readonly int? keyOverride;
  private readonly string? name;
  private readonly string? description;

  public TestProfile(int key, string? name, string? description, int? keyOverride = null)
  {
    this.key = key;
    this.name = name;
    this.description = description;
    this.keyOverride = keyOverride;
  }

  public override int Key => key;
  public override int? KeyOverride => keyOverride;
  public override string? Name => name;
  public override string? Description => description;
}

/// <summary>
/// IdleProfile 测试
/// </summary>
public class IdleProfileTests
{
  [Fact]
  public void IdleProfile_ImplementsIWithName()
  {
    // Arrange
    var profile = new TestProfile(1, "Test", "Description");

    // Assert
    Assert.IsAssignableFrom<IWithName>(profile);
  }

  [Fact]
  public void IdleProfile_Key_ReturnsExpectedValue()
  {
    // Arrange
    const int expectedKey = 42;
    var profile = new TestProfile(expectedKey, "Test", "Description");

    // Assert
    Assert.Equal(expectedKey, profile.Key);
  }

  [Fact]
  public void IdleProfile_Name_ReturnsExpectedValue()
  {
    // Arrange
    const string expectedName = "Test Profile";
    var profile = new TestProfile(1, expectedName, "Description");

    // Assert
    Assert.Equal(expectedName, profile.Name);
  }

  [Fact]
  public void IdleProfile_Description_ReturnsExpectedValue()
  {
    // Arrange
    const string expectedDescription = "Test Description";
    var profile = new TestProfile(1, "Test", expectedDescription);

    // Assert
    Assert.Equal(expectedDescription, profile.Description);
  }

  [Fact]
  public void EffectiveKey_WhenKeyIsNonZero_ReturnsKey()
  {
    // Arrange
    var profile = new TestProfile(42, "Test", "Description", keyOverride: 100);

    // Act
    var effectiveKey = profile.EffectiveKey;

    // Assert
    Assert.Equal(42, effectiveKey);
  }

  [Fact]
  public void EffectiveKey_WhenKeyIsZeroAndKeyOverrideExists_ReturnsKeyOverride()
  {
    // Arrange
    var profile = new TestProfile(0, "Test", "Description", keyOverride: 100);

    // Act
    var effectiveKey = profile.EffectiveKey;

    // Assert
    Assert.Equal(100, effectiveKey);
  }

  [Fact]
  public void EffectiveKey_WhenKeyIsZeroAndNoKeyOverride_ReturnsZero()
  {
    // Arrange
    var profile = new TestProfile(0, "Test", "Description");

    // Act
    var effectiveKey = profile.EffectiveKey;

    // Assert
    Assert.Equal(0, effectiveKey);
  }

  [Fact]
  public void IdleProfile_KeyOverride_DefaultsToNull()
  {
    // Arrange
    var profile = new TestProfile(1, "Test", "Description");

    // Assert
    Assert.Null(profile.KeyOverride);
  }
}
