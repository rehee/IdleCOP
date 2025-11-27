namespace IdleCOP.Gameplay.Actors.Players;

/// <summary>
/// 玩家 Profile 单例集合
/// </summary>
public static class PlayerProfiles
{
  /// <summary>
  /// 基础玩家
  /// </summary>
  public static readonly BasicPlayerProfile BasicPlayer = new();

  /// <summary>
  /// 根据枚举获取 Profile
  /// </summary>
  public static PlayerProfile? GetProfile(EnumPlayer type)
  {
    return type switch
    {
      EnumPlayer.BasicPlayer => BasicPlayer,
      _ => null
    };
  }

  /// <summary>
  /// 根据 Key 获取 Profile
  /// </summary>
  public static PlayerProfile? GetProfileByKey(int key)
  {
    return GetProfile((EnumPlayer)key);
  }
}
