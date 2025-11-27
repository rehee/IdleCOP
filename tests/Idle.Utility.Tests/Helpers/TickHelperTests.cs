using Xunit;
using Idle.Utility.Helpers;

namespace Idle.Utility.Tests.Helpers;

/// <summary>
/// TickHelper 测试
/// </summary>
public class TickHelperTests
{
  [Fact]
  public void DefaultTickRate_Is30()
  {
    // Assert
    Assert.Equal(30, TickHelper.DefaultTickRate);
  }

  [Theory]
  [InlineData(1, 30)]
  [InlineData(2, 60)]
  [InlineData(0.5f, 15)]
  [InlineData(0, 0)]
  public void SecondsToTicks_WithDefaultTickRate_ConvertsCorrectly(float seconds, int expectedTicks)
  {
    // Act
    var result = TickHelper.SecondsToTicks(seconds);

    // Assert
    Assert.Equal(expectedTicks, result);
  }

  [Theory]
  [InlineData(1, 60, 60)]
  [InlineData(2, 60, 120)]
  [InlineData(1, 10, 10)]
  public void SecondsToTicks_WithCustomTickRate_ConvertsCorrectly(float seconds, int tickRate, int expectedTicks)
  {
    // Act
    var result = TickHelper.SecondsToTicks(seconds, tickRate);

    // Assert
    Assert.Equal(expectedTicks, result);
  }

  [Theory]
  [InlineData(30, 1f)]
  [InlineData(60, 2f)]
  [InlineData(15, 0.5f)]
  [InlineData(0, 0f)]
  public void TicksToSeconds_WithDefaultTickRate_ConvertsCorrectly(int ticks, float expectedSeconds)
  {
    // Act
    var result = TickHelper.TicksToSeconds(ticks);

    // Assert
    Assert.Equal(expectedSeconds, result, 4);
  }

  [Theory]
  [InlineData(60, 60, 1f)]
  [InlineData(120, 60, 2f)]
  [InlineData(10, 10, 1f)]
  public void TicksToSeconds_WithCustomTickRate_ConvertsCorrectly(int ticks, int tickRate, float expectedSeconds)
  {
    // Act
    var result = TickHelper.TicksToSeconds(ticks, tickRate);

    // Assert
    Assert.Equal(expectedSeconds, result, 4);
  }

  [Fact]
  public void SecondsToTicks_AndTicksToSeconds_AreInverse()
  {
    // Arrange
    float originalSeconds = 5.5f;

    // Act
    var ticks = TickHelper.SecondsToTicks(originalSeconds);
    var resultSeconds = TickHelper.TicksToSeconds(ticks);

    // Assert - Due to integer conversion, we check approximate equality
    Assert.True(Math.Abs(resultSeconds - originalSeconds) < 0.1f);
  }
}
