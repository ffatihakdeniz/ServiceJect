
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Uygulama genelinde servisleri merkezi olarak yöneten statik sınıftır.
/// Geliştiren: Fatih AKDENİZ
/// </summary>
public static class ServiceJect
{
    private static readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

    public static void RegisterService<T>(T service) where T : class
    {
        Type type = typeof(T);
        if (IsRegisteredService(type))
        {
            Debug.LogError($"A service of type {type} has already been registered.");
            return;
        }
        _services[type] = service;
    }

    public static T GetService<T>() where T : class
    {
        Type type = typeof(T);
        if (_services.TryGetValue(type, out object service))
        {
            return (T)service;
        }
        Debug.LogError($"No service of type {type} has been registered.");
        return null;
    }

    public static bool TryGetService<T>(out T service) where T : class
    {
        service = GetService<T>();
        return service != null;
    }

    public static bool IsRegisteredService(Type type) => _services.ContainsKey(type);

    public static bool IsRegisteredService<T>() where T : class
    {
        return _services.ContainsKey(typeof(T));
    }

    public static void UnRegisterService<T>() where T : class
    {
        _services.Remove(typeof(T));
        
    }
}
