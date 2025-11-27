using Idle.Core.Context;
using Idle.Utility.Commons;

namespace Idle.Core.Components;

/// <summary>
/// Base class for all game components.
/// </summary>
public abstract class IdleComponent
{
  /// <summary>
  /// Gets the unique identifier of this component.
  /// </summary>
  public Guid Id { get; protected set; }

  /// <summary>
  /// Gets or sets the profile key for this component.
  /// </summary>
  public int ProfileKey { get; set; }

  /// <summary>
  /// Gets or sets the parent component.
  /// </summary>
  public IdleComponent? Parent { get; protected set; }

  /// <summary>
  /// Gets the list of child components.
  /// </summary>
  public List<IdleComponent> Children { get; } = new();

  /// <summary>
  /// Initializes a new instance of the IdleComponent class.
  /// </summary>
  protected IdleComponent()
  {
    Id = Guid.NewGuid();
  }

  /// <summary>
  /// Initializes a new instance of the IdleComponent class with the specified id.
  /// </summary>
  /// <param name="id">The unique identifier.</param>
  protected IdleComponent(Guid id)
  {
    Id = id;
  }

  /// <summary>
  /// Sets the parent of this component.
  /// </summary>
  /// <param name="newParent">The new parent component.</param>
  public virtual void SetParent(IdleComponent? newParent)
  {
    if (Parent != null)
    {
      Parent.Children.Remove(this);
    }

    Parent = newParent;

    if (newParent != null)
    {
      newParent.Children.Add(this);
    }
  }

  /// <summary>
  /// Removes this component from its parent.
  /// </summary>
  public virtual void RemoveParent()
  {
    SetParent(null);
  }

  /// <summary>
  /// Called each game tick.
  /// </summary>
  /// <param name="context">The tick context.</param>
  public virtual void OnTick(TickContext context)
  {
    // Process children
    foreach (var child in Children.ToList())
    {
      child.OnTick(context);
    }
  }

  /// <summary>
  /// Gets a child component of the specified type.
  /// </summary>
  /// <typeparam name="T">The type of component to get.</typeparam>
  /// <returns>The first matching component or null.</returns>
  public T? GetChild<T>() where T : IdleComponent
  {
    return Children.OfType<T>().FirstOrDefault();
  }

  /// <summary>
  /// Gets all child components of the specified type.
  /// </summary>
  /// <typeparam name="T">The type of components to get.</typeparam>
  /// <returns>All matching components.</returns>
  public IEnumerable<T> GetChildren<T>() where T : IdleComponent
  {
    return Children.OfType<T>();
  }

  /// <summary>
  /// Adds a child component.
  /// </summary>
  /// <param name="child">The child component to add.</param>
  public void AddChild(IdleComponent child)
  {
    child.SetParent(this);
  }

  /// <summary>
  /// Removes a child component.
  /// </summary>
  /// <param name="child">The child component to remove.</param>
  public void RemoveChild(IdleComponent child)
  {
    if (Children.Contains(child))
    {
      child.RemoveParent();
    }
  }
}
