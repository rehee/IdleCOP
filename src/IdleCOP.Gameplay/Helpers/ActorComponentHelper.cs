using Idle.Core;
using Idle.Core.DTOs;
using IdleCOP.Gameplay.Actors;

namespace IdleCOP.Gameplay.Helpers;

/// <summary>
/// ActorComponent 辅助方法
/// </summary>
public static class ActorComponentHelper
{
  /// <summary>
  /// 从DTO创建ActorComponent
  /// </summary>
  /// <param name="dto">角色DTO</param>
  /// <param name="faction">阵营</param>
  /// <returns>ActorComponent实例</returns>
  public static ActorComponent FromDTO(CharacterDTO dto, EnumFaction faction)
  {
    return new ActorComponent
    {
      Id = dto.Id,
      Name = dto.Name,
      Level = dto.Level,
      ActorType = dto.ActorType,
      ProfileKey = dto.ProfileKey,
      Faction = faction,
      CombatStats = dto.CombatStats.Clone()
    };
  }
}
