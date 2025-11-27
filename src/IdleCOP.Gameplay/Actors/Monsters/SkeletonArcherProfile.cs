namespace IdleCOP.Gameplay.Actors.Monsters;

/// <summary>
/// 骷髅弓手 Profile
/// </summary>
public class SkeletonArcherProfile : MonsterProfile
{
  /// <inheritdoc/>
  public override EnumMonster ProfileType => EnumMonster.SkeletonArcher;

  /// <inheritdoc/>
  public override string? Name => "骷髅弓手";

  /// <inheritdoc/>
  public override string? Description => "远程攻击的骷髅弓手";

  /// <inheritdoc/>
  public override int BaseMaxHealth => 50;

  /// <inheritdoc/>
  public override int BasePhysicalDamageMin => 8;

  /// <inheritdoc/>
  public override int BasePhysicalDamageMax => 14;

  /// <inheritdoc/>
  public override float BaseAttackSpeed => 1.0f;
}
