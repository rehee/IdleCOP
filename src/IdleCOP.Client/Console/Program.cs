// IdleCOP 控制台应用程序
// 用于测试和实验性功能

using Idle.Utility;
using Idle.Utility.Helpers;
using Idle.Core.Context;

namespace IdleCOP.Client.Console;

/// <summary>
/// 控制台应用程序入口
/// </summary>
public class Program
{
  /// <summary>
  /// 程序入口点
  /// </summary>
  public static void Main(string[] args)
  {
    System.Console.WriteLine("IdleCOP - 控制台测试应用");
    System.Console.WriteLine("========================");
    System.Console.WriteLine();

    // 示例: 使用 GameRandom
    var random = new GameRandom(42);
    System.Console.WriteLine($"随机数测试 (种子: 42):");
    System.Console.WriteLine($"  Next(): {random.Next()}");
    System.Console.WriteLine($"  Next(100): {random.Next(100)}");
    System.Console.WriteLine($"  Next(10, 20): {random.Next(10, 20)}");
    System.Console.WriteLine($"  NextFloat(): {random.NextFloat():F4}");
    System.Console.WriteLine($"  NextDouble(): {random.NextDouble():F4}");
    System.Console.WriteLine();

    // 示例: 使用 TickHelper
    System.Console.WriteLine($"Tick 转换测试:");
    System.Console.WriteLine($"  默认帧率: {TickHelper.DefaultTickRate} FPS");
    System.Console.WriteLine($"  1秒 = {TickHelper.SecondsToTicks(1)} Ticks");
    System.Console.WriteLine($"  60 Ticks = {TickHelper.TicksToSeconds(60)} 秒");
    System.Console.WriteLine();

    // 示例: 使用 TickContext
    using var context = new TickContext
    {
      MaxTick = TickHelper.SecondsToTicks(60),
      BattleRandom = new GameRandom(123)
    };
    System.Console.WriteLine($"战斗上下文测试:");
    System.Console.WriteLine($"  当前 Tick: {context.CurrentTick}");
    System.Console.WriteLine($"  最大 Tick: {context.MaxTick}");
    System.Console.WriteLine($"  战斗结束: {context.IsBattleOver}");
    System.Console.WriteLine();

    System.Console.WriteLine("测试完成!");
  }
}
