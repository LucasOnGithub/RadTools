using Il2CppInterop.Runtime;
using RadTools.Libraries.RadiumWrapper;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace RadTools.Core;

public class Instance : MonoWrap
{
    internal static Instance instance;

    internal void Awake()
    {
        instance = this;
        Logger.Log(@$"RadTools {Constants.GUID} {Constants.Version} by cc0");

        var types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && t.GetCustomAttribute<AddOnAwake>() != null);
        foreach (var type in types)
            gameObject.AddComponent(Il2CppType.From(type));

        if (!PlayerPrefs.HasKey("RadTools.Load"))
        {
            PlayerPrefs.SetInt("RadTools.Load", 1);
            Notifications.Notify($"RadTools {Constants.Version} load success\nThis notification will never show again");
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class AddOnAwake : Attribute
    {
    }

    public interface IPlugin
    {
        string Name { get; }
        string Version { get; }

        void Initialize();
    }
}