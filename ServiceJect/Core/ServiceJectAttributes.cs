
using System;

/// <summary>
/// Bu attribute, sınıfın ServiceJect sistemine servis olarak otomatik kaydedilmesini sağlar.
/// Geliştiren: Fatih AKDENİZ
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class ServiceJectRegisterAttribute : Attribute { }

/// <summary>
/// Bu attribute, field veya property'sine servis otomatik inject edilmesini sağlar.
/// Geliştiren: Fatih AKDENİZ
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class GetServiceAttribute : Attribute { }
