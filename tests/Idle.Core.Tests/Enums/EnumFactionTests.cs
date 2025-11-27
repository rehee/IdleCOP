using Xunit;
using Idle.Core;

namespace Idle.Core.Tests.Enums;

/// <summary>
/// EnumFaction 测试
/// </summary>
public class EnumFactionTests
{
  [Fact]
  public void EnumFaction_NotSpecified_IsDefault()
  {
    // Assert
    Assert.Equal(0, (int)EnumFaction.NotSpecified);
  }

  [Fact]
  public void EnumFaction_ContainsAllExpectedValues()
  {
    // Assert
    var values = Enum.GetValues<EnumFaction>();
    Assert.Contains(EnumFaction.NotSpecified, values);
    Assert.Contains(EnumFaction.Creator, values);
    Assert.Contains(EnumFaction.Enemy, values);
  }

  [Fact]
  public void EnumFaction_DefaultValue_IsNotSpecified()
  {
    // Arrange
    EnumFaction faction = default;

    // Assert
    Assert.Equal(EnumFaction.NotSpecified, faction);
  }

  [Fact]
  public void EnumFaction_Creator_IsNotSameAsEnemy()
  {
    // Assert
    Assert.NotEqual(EnumFaction.Creator, EnumFaction.Enemy);
  }
}
