using Xunit;
using IdleCOP.Gameplay;

namespace IdleCOP.Gameplay.Tests.Enums;

/// <summary>
/// EnumQuality 测试
/// </summary>
public class EnumQualityTests
{
  [Fact]
  public void EnumQuality_NotSpecified_IsDefault()
  {
    // Assert
    Assert.Equal(0, (int)EnumQuality.NotSpecified);
  }

  [Fact]
  public void EnumQuality_ContainsAllExpectedValues()
  {
    // Assert
    var values = Enum.GetValues<EnumQuality>();
    Assert.Contains(EnumQuality.NotSpecified, values);
    Assert.Contains(EnumQuality.Normal, values);
    Assert.Contains(EnumQuality.Magic, values);
    Assert.Contains(EnumQuality.Rare, values);
    Assert.Contains(EnumQuality.Legendary, values);
    Assert.Contains(EnumQuality.Unique, values);
  }

  [Fact]
  public void EnumQuality_DefaultValue_IsNotSpecified()
  {
    // Arrange
    EnumQuality quality = default;

    // Assert
    Assert.Equal(EnumQuality.NotSpecified, quality);
  }

  [Fact]
  public void EnumQuality_HasCorrectOrder()
  {
    // The order should be: NotSpecified < Normal < Magic < Rare < Legendary < Unique
    Assert.True((int)EnumQuality.NotSpecified < (int)EnumQuality.Normal);
    Assert.True((int)EnumQuality.Normal < (int)EnumQuality.Magic);
    Assert.True((int)EnumQuality.Magic < (int)EnumQuality.Rare);
    Assert.True((int)EnumQuality.Rare < (int)EnumQuality.Legendary);
    Assert.True((int)EnumQuality.Legendary < (int)EnumQuality.Unique);
  }
}
