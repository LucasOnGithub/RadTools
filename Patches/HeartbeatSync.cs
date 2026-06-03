using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Pun;
using RadTools.Libraries.RadiumWrapper;
using UnityEngine;

namespace RadTools.Patches;

internal class HeartbeatSync
{
    internal const bool enabled = true;

    [HarmonyPatch(typeof(APKFOPPGBFH), nameof(APKFOPPGBFH.ACHEHBDMAJI))]
    internal class RoomLoadError
    {
        [HarmonyPrefix]
        private static void Prefix(string MOPOMPHBHKF)
        {
            if (enabled && MOPOMPHBHKF != null && MOPOMPHBHKF == "RecNet presence out-of-sync!")
            {
                Notifications.Notify("Heartbeat out of sync", Color.red);
            }
        }
    }
}