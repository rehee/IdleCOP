namespace Idle.Utility.Container;

/// <summary>
/// Interface for service providers.
/// </summary>
public interface IServiceContainer
{
  /// <summary>
  /// Registers a service with its implementation type.
  /// </summary>
  /// <typeparam name="TService">The service interface type.</typeparam>
  /// <typeparam name="TImplementation">The implementation type.</typeparam>
  void Register<TService, TImplementation>()
    where TService : class
    where TImplementation : class, TService;

  /// <summary>
  /// Registers a singleton service instance.
  /// </summary>
  /// <typeparam name="TService">The service type.</typeparam>
  /// <param name="instance">The service instance.</param>
  void RegisterSingleton<TService>(TService instance) where TService : class;

  /// <summary>
  /// Resolves a service by its type.
  /// </summary>
  /// <typeparam name="TService">The service type to resolve.</typeparam>
  /// <returns>The service instance.</returns>
  TService Resolve<TService>() where TService : class;

  /// <summary>
  /// Resolves a service by its type.
  /// </summary>
  /// <param name="serviceType">The service type to resolve.</param>
  /// <returns>The service instance.</returns>
  object Resolve(Type serviceType);

  /// <summary>
  /// Checks if a service is registered.
  /// </summary>
  /// <typeparam name="TService">The service type.</typeparam>
  /// <returns>True if the service is registered.</returns>
  bool IsRegistered<TService>() where TService : class;
}
