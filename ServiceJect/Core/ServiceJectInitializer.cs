
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// ServiceJect sisteminin Unity tarafında başlatılmasını ve injection işlemlerini yöneten sınıftır.
/// Geliştiren: Fatih AKDENİZ
/// </summary>
[DefaultExecutionOrder(-99)]
public class ServiceJectInitializer : MonoBehaviour
{
    [Tooltip("Sadece InjectableMonoBehaviour türetilmiş sınıflar inject edilsin.")]
    public bool useOnlyServiceJectMonoBehaviour = false;

    [Tooltip("Inactive GameObject'ler de aramaya dahil edilsin.")]
    public bool useInactiveGameObject = true;

    private void Awake()
    {
        AutoRegisterServices();
        AutoInjectServices();
    }

    private void AutoRegisterServices()
    {
        var allTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(asm => asm.GetTypes())
            .Where(t => t.GetCustomAttribute<ServiceJectRegisterAttribute>() != null);

        foreach (var type in allTypes)
        {
            if (ServiceJect.IsRegisteredService(type))
                continue;

            if (!typeof(MonoBehaviour).IsAssignableFrom(type))
            {
                object instance = Activator.CreateInstance(type);
                ServiceJectHelper.RegisterServiceByType(type, instance);
            }
            else
            {
                var monoInstances = FindObjectsByType(
                    type,
                    useInactiveGameObject ? FindObjectsInactive.Include : FindObjectsInactive.Exclude,
                    FindObjectsSortMode.None
                );

                if (monoInstances.Length > 0)
                {
                    ServiceJectHelper.RegisterServiceByType(type, monoInstances[0]);
                }
                else
                {
                    Debug.LogWarning($"[ServiceJect] Could not find instance of {type.Name} in scene. Please add it to the scene.");
                }
            }
        }
    }

    private void AutoInjectServices()
    {
        var injectableMonos = FindObjectsByType<InjectableMonoBehaviour>(
            useInactiveGameObject ? FindObjectsInactive.Include : FindObjectsInactive.Exclude,
            FindObjectsSortMode.None
        ).Cast<MonoBehaviour>();

        var monoSet = new HashSet<MonoBehaviour>(injectableMonos);

        if (!useOnlyServiceJectMonoBehaviour)
        {
            var allMonos = FindObjectsByType<MonoBehaviour>(
                useInactiveGameObject ? FindObjectsInactive.Include : FindObjectsInactive.Exclude,
                FindObjectsSortMode.None
            );

            foreach (var mono in allMonos)
            {
                if (!monoSet.Contains(mono))
                    monoSet.Add(mono);
            }
        }

        foreach (var mono in monoSet)
        {
            InjectMembers(mono);
        }
    }

    private void InjectMembers(MonoBehaviour mono)
    {
        var type = mono.GetType();
        var members = type.GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
            .Where(m => m.GetCustomAttribute<GetServiceAttribute>() != null);

        foreach (var member in members)
        {
            Type serviceType = member switch
            {
                FieldInfo f => f.FieldType,
                PropertyInfo p when p.CanWrite => p.PropertyType,
                _ => null
            };

            if (serviceType == null) continue;

            var service = ServiceJectHelper.GetServiceByType(serviceType);
            if (service == null)
            {
                Debug.LogWarning($"[ServiceJect] Injection failed for {type.Name}: Service not found for {serviceType.Name}");
                continue;
            }

            if (member is FieldInfo field)
                field.SetValue(mono, service);
            else if (member is PropertyInfo prop)
                prop.SetValue(mono, service);
        }
    }
}
