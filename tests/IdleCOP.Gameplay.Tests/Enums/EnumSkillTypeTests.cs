using Xunit;
using IdleCOP.Gameplay;

namespace IdleCOP.Gameplay.Tests.Enums;

/// <summary>
/// EnumSkillType 测试
/// </summary>
public class EnumSkillTypeTests
{
  [Fact]
  public void EnumSkillType_NotSpecified_IsDefault()
  {
    // Assert
    Assert.Equal(0, (int)EnumSkillType.NotSpecified);
  }

  [Fact]
  public void EnumSkillType_ContainsAllExpectedValues()
  {
    // Assert
    var values = Enum.GetValues<EnumSkillType>();
    Assert.Contains(EnumSkillType.NotSpecified, values);
    Assert.Contains(EnumSkillType.Active, values);
    Assert.Contains(EnumSkillType.Support, values);
    Assert.Contains(EnumSkillType.Trigger, values);
  }

  [Fact]
  public void EnumSkillType_DefaultValue_IsNotSpecified()
  {
    // Arrange
    EnumSkillType skillType = default;

    // Assert
    Assert.Equal(EnumSkillType.NotSpecified, skillType);
  }

  [Fact]
  public void EnumSkillType_AllValuesAreDistinct()
  {
    // Arrange
    var values = Enum.GetValues<EnumSkillType>();

    // Assert
    Assert.Equal(values.Length, values.Distinct().Count());
  }
}
