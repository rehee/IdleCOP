namespace Idle.Core;

/// <summary>
/// 随机数生成器接口
/// </summary>
public interface IRandom
{
  /// <summary>
  /// 返回非负随机整数
  /// </summary>
  int Next();

  /// <summary>
  /// 返回在 [0, maxValue) 范围内的非负随机整数
  /// </summary>
  int Next(int maxValue);

  /// <summary>
  /// 返回在 [minValue, maxValue) 范围内的随机整数
  /// </summary>
  int Next(int minValue, int maxValue);

  /// <summary>
  /// 返回在 [0.0, 1.0) 范围内的随机浮点数
  /// </summary>
  float NextFloat();

  /// <summary>
  /// 返回在 [0.0, 1.0) 范围内的随机双精度浮点数
  /// </summary>
  double NextDouble();
}
