using CircuitsV2.Utilities;
using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Pun;
using RadTools.Libraries.RadiumWrapper;
using System.Collections.Generic;
using UnityEngine;

namespace RadTools.Patches;

internal class NetworkErrorPatch
{
    internal const bool enabled = true;
    internal static string LastError;

    [HarmonyPatch(typeof(PUNNetworkManager), nameof(PUNNetworkManager.OnPhotonJoinRoomFailed))]
    internal class AntiCheerPatch
    {
        [HarmonyPostfix]
        private static void Postfix(short IFGPCAPEOPC, string KGEKPIGKJDA)
        {
            if (enabled)
                LastError = KGEKPIGKJDA;
        }
    }
}