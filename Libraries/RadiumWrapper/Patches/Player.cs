using HarmonyLib;
using RecRoom.Players;

namespace RadTools.Libraries.RadiumWrapper.Patches;

internal static class Player
{
    public static bool skipMessageIfCommand = false;

    [HarmonyPatch(typeof(PlayerEmotes), nameof(PlayerEmotes.LocalSendChatMessage))]
    private static class SendChat
    {
        [HarmonyPrefix]
        private static void Prefix(string NGPMADFHHKP, bool KJHKAODNCGE)
        {
            PlayerRoot.ChatMessageSent(NGPMADFHHKP);
        }
    }
}