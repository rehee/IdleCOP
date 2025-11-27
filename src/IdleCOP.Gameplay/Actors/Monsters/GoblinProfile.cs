namespace IdleCOP.Gameplay.Actors.Monsters;

/// <summary>
/// 哥布林 Profile
/// </summary>
public class GoblinProfile : MonsterProfile
{
  /// <inheritdoc/>
  public override EnumMonster ProfileType => EnumMonster.Goblin;

  /// <inheritdoc/>
  public override string? Name => "哥布林";

  /// <inheritdoc/>
  public override string? Description => "弱小但数量众多的哥布林";

  /// <inheritdoc/>
  public override int BaseMaxHealth => 30;

  /// <inheritdoc/>
  public override int BasePhysicalDamageMin => 3;

  /// <inheritdoc/>
  public override int BasePhysicalDamageMax => 6;

  /// <inheritdoc/>
  public override float BaseAttackSpeed => 1.5f;
}
