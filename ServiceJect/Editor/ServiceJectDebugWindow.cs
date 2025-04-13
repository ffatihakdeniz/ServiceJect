
using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// ServiceJect sisteminde kayıtlı servisleri ve sahnedeki inject edilmiş objeleri gösteren debug penceresidir.
/// Menü: Tools → ServiceJect → Debug
/// Geliştiren: Fatih AKDENİZ
/// </summary>
public class ServiceJectDebugWindow : EditorWindow
{
    private Vector2 _scroll;
    private List<string> _registeredServices;

    [MenuItem("Tools/ServiceJect/Debug")]
    public static void ShowWindow()
    {
        GetWindow<ServiceJectDebugWindow>("ServiceJect Debug");
    }

    private void OnEnable()
    {
        RefreshServiceList();
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("📦 Kayıtlı Servisler", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        if (_registeredServices == null || _registeredServices.Count == 0)
        {
            EditorGUILayout.HelpBox("Henüz kayıtlı bir servis bulunamadı.", MessageType.Info);
            if (GUILayout.Button("Yenile"))
            {
                RefreshServiceList();
            }
            return;
        }

        _scroll = EditorGUILayout.BeginScrollView(_scroll);
        foreach (var service in _registeredServices)
        {
            EditorGUILayout.LabelField("- " + service);
        }
        EditorGUILayout.EndScrollView();

        EditorGUILayout.Space(10);
        if (GUILayout.Button("Servis Listesini Yenile"))
        {
            RefreshServiceList();
        }
    }

    private void RefreshServiceList()
    {
        _registeredServices = new List<string>();

        var field = typeof(ServiceJect).GetField("_services", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        if (field != null)
        {
            var dict = field.GetValue(null) as Dictionary<Type, object>;
            if (dict != null)
            {
                foreach (var entry in dict)
                {
                    _registeredServices.Add(entry.Key.FullName);
                }
            }
        }
    }
}
