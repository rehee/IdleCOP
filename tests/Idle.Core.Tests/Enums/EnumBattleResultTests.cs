using Xunit;
using Idle.Core;

namespace Idle.Core.Tests.Enums;

/// <summary>
/// EnumBattleResult 测试
/// </summary>
public class EnumBattleResultTests
{
  [Fact]
  public void EnumBattleResult_NotSpecified_IsDefault()
  {
    // Assert
    Assert.Equal(0, (int)EnumBattleResult.NotSpecified);
  }

  [Fact]
  public void EnumBattleResult_ContainsAllExpectedValues()
  {
    // Assert
    var values = Enum.GetValues<EnumBattleResult>();
    Assert.Contains(EnumBattleResult.NotSpecified, values);
    Assert.Contains(EnumBattleResult.Victory, values);
    Assert.Contains(EnumBattleResult.Defeat, values);
    Assert.Contains(EnumBattleResult.Timeout, values);
    Assert.Contains(EnumBattleResult.Draw, values);
    Assert.Contains(EnumBattleResult.Error, values);
    Assert.Contains(EnumBattleResult.PlayerExit, values);
  }

  [Fact]
  public void EnumBattleResult_DefaultValue_IsNotSpecified()
  {
    // Arrange
    EnumBattleResult result = default;

    // Assert
    Assert.Equal(EnumBattleResult.NotSpecified, result);
  }
}
