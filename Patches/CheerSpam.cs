using CircuitsV2.Utilities;
using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Pun;
using RadTools.Libraries.RadiumWrapper;
using System.Collections.Generic;
using UnityEngine;

namespace RadTools.Patches;

internal class CheerSpam
{
    internal const bool enabled = true;

    [HarmonyPatch(typeof(PlayerProgression), nameof(PlayerProgression.RpcPlayLevelUpFeedback))]
    internal class AntiCheerPatch
    {
        private static Dictionary<int, List<float>> lastCheerTimes = new();
        private static Dictionary<int, float> blockTimes = new();

        [HarmonyPrefix]
        private static bool Prefix(PlayerProgression __instance, PlayerProgression.NIAKDKNGBEJ MBAJNJHNODE)
        {
            if (enabled)
            {
                if (blockTimes.GetValueOrDefault(__instance.owner.ActorNumber, 0f) < Time.time)
                    return false;

                var callTimestamps = lastCheerTimes.GetValueOrDefault(__instance.owner.ActorNumber, new List<float>());

                callTimestamps.Add(Time.time);
                callTimestamps.RemoveAll(t => Time.time - t > 1);

                lastCheerTimes[__instance.owner.ActorNumber] = callTimestamps;

                if (callTimestamps.Count > 15)
                {
                    Notifications.Notify($"Player {__instance.owner.NickName} is spamming cheers; more than 15 in 1 second. Ignoring further calls for 30 seconds.", Color.red, 3f);
                    blockTimes[__instance.owner.ActorNumber] = Time.time + 30f;
                    return false;
                }
            }
            return true;
        }
    }
}