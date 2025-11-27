namespace IdleCOP.Gameplay.Actors.Monsters;

/// <summary>
/// 骷髅法师 Profile
/// </summary>
public class SkeletonMageProfile : MonsterProfile
{
  /// <inheritdoc/>
  public override EnumMonster ProfileType => EnumMonster.SkeletonMage;

  /// <inheritdoc/>
  public override string? Name => "骷髅法师";

  /// <inheritdoc/>
  public override string? Description => "使用魔法的骷髅法师";

  /// <inheritdoc/>
  public override int BaseMaxHealth => 40;

  /// <inheritdoc/>
  public override int BasePhysicalDamageMin => 10;

  /// <inheritdoc/>
  public override int BasePhysicalDamageMax => 18;

  /// <inheritdoc/>
  public override float BaseAttackSpeed => 0.6f;
}
