namespace IdleCOP.Gameplay.Actors.Monsters;

/// <summary>
/// 哥布林首领 Profile
/// </summary>
public class GoblinChiefProfile : MonsterProfile
{
  /// <inheritdoc/>
  public override EnumMonster ProfileType => EnumMonster.GoblinChief;

  /// <inheritdoc/>
  public override string? Name => "哥布林首领";

  /// <inheritdoc/>
  public override string? Description => "哥布林部落的首领";

  /// <inheritdoc/>
  public override int BaseMaxHealth => 150;

  /// <inheritdoc/>
  public override int BasePhysicalDamageMin => 12;

  /// <inheritdoc/>
  public override int BasePhysicalDamageMax => 20;

  /// <inheritdoc/>
  public override float BaseAttackSpeed => 0.7f;
}
