using Idle.Core.Components;
using Idle.Core.Context;

namespace IdleCOP.Gameplay.Instructions;

/// <summary>
/// Component representing an instruction set (collection of instructions).
/// </summary>
public class InstructionSetComponent : IdleComponent
{
  /// <summary>
  /// Gets or sets the instruction set name.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the description.
  /// </summary>
  public string Description { get; set; } = string.Empty;

  /// <summary>
  /// Gets or sets the version.
  /// </summary>
  public int Version { get; set; } = 1;

  /// <summary>
  /// Initializes a new instance of the InstructionSetComponent class.
  /// </summary>
  public InstructionSetComponent() : base()
  {
  }

  /// <summary>
  /// Initializes a new instance of the InstructionSetComponent class with the specified id.
  /// </summary>
  /// <param name="id">The unique identifier.</param>
  public InstructionSetComponent(Guid id) : base(id)
  {
  }

  /// <summary>
  /// Gets instructions sorted by priority (highest first).
  /// </summary>
  /// <returns>Instructions sorted by priority.</returns>
  public IEnumerable<InstructionComponent> GetSortedInstructions()
  {
    return GetChildren<InstructionComponent>()
      .Where(i => i.IsEnabled)
      .OrderByDescending(i => i.Priority);
  }
}
