namespace Idle.Utility;

/// <summary>
/// 游戏随机数生成器实现
/// </summary>
public class GameRandom : IRandom
{
  private readonly Random random;

  /// <summary>
  /// 使用指定种子初始化随机数生成器
  /// </summary>
  public GameRandom(int seed)
  {
    random = new Random(seed);
  }

  /// <summary>
  /// 使用随机种子初始化随机数生成器
  /// </summary>
  public GameRandom()
  {
    random = new Random();
  }

  /// <inheritdoc/>
  public int Next()
  {
    return random.Next();
  }

  /// <inheritdoc/>
  public int Next(int maxValue)
  {
    return random.Next(maxValue);
  }

  /// <inheritdoc/>
  public int Next(int minValue, int maxValue)
  {
    return random.Next(minValue, maxValue);
  }

  /// <inheritdoc/>
  public float NextFloat()
  {
    return (float)random.NextDouble();
  }

  /// <inheritdoc/>
  public double NextDouble()
  {
    return random.NextDouble();
  }
}
