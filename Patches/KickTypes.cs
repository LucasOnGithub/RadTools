using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Pun;
using RadTools.Libraries.RadiumWrapper;
using UnityEngine;

namespace RadTools.Patches;

internal class KickTypes
{
    internal const bool enabled = true;

    [HarmonyPatch(typeof(SessionManager), nameof(SessionManager.BootLocalPlayerToDormRoom))]
    internal class BootPlayer
    {
        [HarmonyPrefix]
        private static void Prefix(string LLOKHPDCJBH)
        {
            Notifications.Notify($"Kicking to dorm room: {LLOKHPDCJBH}", Color.red);
        }
    }

    [HarmonyPatch(typeof(SessionManager), nameof(SessionManager.LogoutToBootScene))]
    internal class LogoutBoot
    {
        [HarmonyPrefix]
        private static void Prefix()
        {
            Notifications.Notify($"Logged out to boot scene", Color.red);
        }
    }

    [HarmonyPatch(typeof(SessionManager), nameof(SessionManager.TryApplicationQuit))]
    internal class TryApplicationQuit
    {
        [HarmonyPrefix]
        private static void Prefix()
        {
            Notifications.Notify($"Application quitting", Color.red);
        }
    }

    [HarmonyPatch(typeof(CFLMJLFBOKH), nameof(CFLMJLFBOKH.FJGJCBMKIBL))]
    internal class RecNetLogout
    {
        [HarmonyPrefix]
        private static void Prefix()
        {
            Notifications.Notify($"RecNet logout request", Color.red);
        }
    }

    [HarmonyPatch(typeof(PhotonNetwork), nameof(PhotonNetwork.OnEvent))]
    internal class OnEvent
    {
        [HarmonyPrefix]
        private static void Prefix(EventData photonEvent)
        {
            if (photonEvent.Code == PunEvent.CloseConnection)
                Notifications.Notify($"CloseConnection sent, either cheater or server request", Color.yellow);
        }
    }
}