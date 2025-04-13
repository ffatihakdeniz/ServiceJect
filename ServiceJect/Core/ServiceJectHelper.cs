
using System;

/// <summary>
/// ServiceJect sistemine ait reflection işlemlerini kolaylaştıran yardımcı sınıftır.
/// Geliştiren: Fatih AKDENİZ
/// </summary>
public static class ServiceJectHelper
{
    public const string GET_SERVICE_METHOD_NAME = "GetService";
    public const string REGISTER_SERVICE_METHOD_NAME = "RegisterService";

    public static object GetServiceByType(Type type)
    {
        return typeof(ServiceJect)
            .GetMethod(GET_SERVICE_METHOD_NAME)
            .MakeGenericMethod(type)
            .Invoke(null, null);
    }

    public static void RegisterServiceByType(Type type, object instance)
    {
        typeof(ServiceJect)
            .GetMethod(REGISTER_SERVICE_METHOD_NAME)
            .MakeGenericMethod(type)
            .Invoke(null, new object[] { instance });
    }
}
