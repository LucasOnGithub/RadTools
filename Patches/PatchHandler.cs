using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;

namespace RadTools.Patches;

internal class PatchHandler
{
    private static Harmony instance;
    internal static bool IsPatched { get; private set; }
    internal static int PatchErrors { get; private set; }

    internal static bool CriticalPatchFailed { get; private set; }

    internal static void PatchAll()
    {
        if (IsPatched) return;
        instance ??= new Harmony(Constants.GUID);

        var harmonyPatchTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsClass && t.GetCustomAttribute<HarmonyPatch>() != null);
        foreach (var type in harmonyPatchTypes)
            try
            {
                instance.CreateClassProcessor(type).Patch();
            }
            catch (Exception ex)
            {
                PatchErrors++;
                if (type.GetCustomAttribute<SecurityPatch>() != null)
                    CriticalPatchFailed = true;

                Console.WriteLine($"Failed to patch {type.FullName}: {ex}");
            }

        var patchCount = harmonyPatchTypes.Count();
        Logger.Log($"Patched classes {patchCount - PatchErrors}/{patchCount} classes; {PatchErrors} errors");

        IsPatched = true;
    }

    internal static void UnpatchAll()
    {
        if (instance == null || !IsPatched) return;
        instance.UnpatchSelf();
        IsPatched = false;
        instance = null;
    }

    internal static void ApplyPatch(Type targetClass, string methodName, MethodInfo prefix = null,
        MethodInfo postfix = null, Type[] parameterTypes = null)
    {
        var original =
            (parameterTypes == null
                ? targetClass.GetMethod(methodName,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                : targetClass.GetMethod(methodName,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null,
                    parameterTypes, null)) ??
            throw new Exception($"Method '{methodName}' not found on {targetClass.FullName}");
        instance.Patch(original,
            prefix != null ? new HarmonyMethod(prefix) : null,
            postfix != null ? new HarmonyMethod(postfix) : null);
    }

    internal static void RemovePatch(Type targetClass, string methodName, Type[] parameterTypes = null)
    {
        var original =
            (parameterTypes == null
                ? targetClass.GetMethod(methodName,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
                : targetClass.GetMethod(methodName,
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static, null,
                    parameterTypes, null)) ??
            throw new Exception($"Method '{methodName}' not found on {targetClass.FullName}");
        instance.Unpatch(original, HarmonyPatchType.All, instance.Id);
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    internal class SecurityPatch : Attribute
    {
    }
}