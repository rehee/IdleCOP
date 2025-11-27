using Xunit;

namespace Idle.Utility.Tests;

/// <summary>
/// GameRandom 测试
/// </summary>
public class GameRandomTests
{
  [Fact]
  public void GameRandom_SameSeed_ProducesSameSequence()
  {
    // Arrange
    var random1 = new GameRandom(42);
    var random2 = new GameRandom(42);

    // Act & Assert
    for (int i = 0; i < 10; i++)
    {
      Assert.Equal(random1.Next(), random2.Next());
    }
  }

  [Fact]
  public void Next_ReturnsNonNegativeValue()
  {
    // Arrange
    var random = new GameRandom(42);

    // Act & Assert
    for (int i = 0; i < 100; i++)
    {
      Assert.True(random.Next() >= 0);
    }
  }

  [Fact]
  public void Next_WithMaxValue_ReturnsValueInRange()
  {
    // Arrange
    var random = new GameRandom(42);
    const int maxValue = 100;

    // Act & Assert
    for (int i = 0; i < 100; i++)
    {
      var result = random.Next(maxValue);
      Assert.True(result >= 0 && result < maxValue);
    }
  }

  [Fact]
  public void Next_WithMinMaxValue_ReturnsValueInRange()
  {
    // Arrange
    var random = new GameRandom(42);
    const int minValue = 10;
    const int maxValue = 20;

    // Act & Assert
    for (int i = 0; i < 100; i++)
    {
      var result = random.Next(minValue, maxValue);
      Assert.True(result >= minValue && result < maxValue);
    }
  }

  [Fact]
  public void NextFloat_ReturnsValueInZeroToOneRange()
  {
    // Arrange
    var random = new GameRandom(42);

    // Act & Assert
    for (int i = 0; i < 100; i++)
    {
      var result = random.NextFloat();
      Assert.True(result >= 0.0f && result < 1.0f);
    }
  }

  [Fact]
  public void NextDouble_ReturnsValueInZeroToOneRange()
  {
    // Arrange
    var random = new GameRandom(42);

    // Act & Assert
    for (int i = 0; i < 100; i++)
    {
      var result = random.NextDouble();
      Assert.True(result >= 0.0 && result < 1.0);
    }
  }

  [Fact]
  public void GameRandom_DefaultConstructor_CreatesInstance()
  {
    // Arrange & Act
    var random = new GameRandom();

    // Assert
    Assert.NotNull(random);
    Assert.True(random.Next() >= 0);
  }
}
