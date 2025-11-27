namespace IdleCOP.Gameplay.Actors.Monsters;

/// <summary>
/// 骷髅战士 Profile
/// </summary>
public class SkeletonWarriorProfile : MonsterProfile
{
  /// <inheritdoc/>
  public override EnumMonster ProfileType => EnumMonster.SkeletonWarrior;

  /// <inheritdoc/>
  public override string? Name => "骷髅战士";

  /// <inheritdoc/>
  public override string? Description => "普通的骷髅战士，使用近战攻击";

  /// <inheritdoc/>
  public override int BaseMaxHealth => 80;

  /// <inheritdoc/>
  public override int BasePhysicalDamageMin => 6;

  /// <inheritdoc/>
  public override int BasePhysicalDamageMax => 12;

  /// <inheritdoc/>
  public override float BaseAttackSpeed => 0.8f;
}
