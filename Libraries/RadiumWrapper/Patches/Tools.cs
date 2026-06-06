using HarmonyLib;
using System;
using static UnityEngine.GridBrushBase;

namespace RadTools.Libraries.RadiumWrapper.Patches;

internal static class Tools
{
    internal static event Action<Tool> ToolInstantiated;

    [HarmonyPatch(typeof(Tool), nameof(Tool.Start))]
    private static class PostStart
    {
        [HarmonyPostfix]
        private static void Postfix(Tool __instance)
        {
            ToolInstantiated?.Invoke(__instance);
        }
    }
}