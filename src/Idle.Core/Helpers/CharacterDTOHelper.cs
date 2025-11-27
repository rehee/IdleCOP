using Idle.Core.DTOs;

namespace Idle.Core.Helpers;

/// <summary>
/// 角色DTO帮助类
/// </summary>
public static class CharacterDTOHelper
{
  /// <summary>
  /// 创建玩家角色DTO
  /// </summary>
  public static CharacterDTO CreatePlayer(string name, int level = 1)
  {
    return new CharacterDTO
    {
      Name = name,
      Level = level,
      ActorType = EnumActorType.Player,
      CombatStats = CombatStatsHelper.CreateDefault(level)
    };
  }

  /// <summary>
  /// 创建怪物角色DTO
  /// </summary>
  public static CharacterDTO CreateMonster(string name, int level, int profileKey)
  {
    return new CharacterDTO
    {
      Name = name,
      Level = level,
      ActorType = EnumActorType.Monster,
      ProfileKey = profileKey,
      CombatStats = CombatStatsHelper.CreateDefault(level)
    };
  }
}
