namespace IdleCOP.Gameplay.Actors.Players;

/// <summary>
/// 基础玩家 Profile
/// </summary>
public class BasicPlayerProfile : PlayerProfile
{
  /// <inheritdoc/>
  public override EnumPlayer ProfileType => EnumPlayer.BasicPlayer;

  /// <inheritdoc/>
  public override string? Name => "基础玩家";

  /// <inheritdoc/>
  public override string? Description => "基础玩家角色，具有均衡的属性";

  /// <inheritdoc/>
  public override int BaseMaxHealth => 150;

  /// <inheritdoc/>
  public override int BaseMaxEnergy => 100;

  /// <inheritdoc/>
  public override int BasePhysicalDamageMin => 8;

  /// <inheritdoc/>
  public override int BasePhysicalDamageMax => 15;

  /// <inheritdoc/>
  public override float BaseAttackSpeed => 1.2f;
}
