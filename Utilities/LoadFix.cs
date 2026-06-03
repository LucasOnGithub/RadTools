using BepInEx.Unity.IL2CPP.Utils.Collections;
using Photon.Pun;
using Photon.Realtime;
using RadTools.Core;
using RadTools.Libraries.RadiumWrapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RadTools.Utilities
{
    [Instance.AddOnAwake]
    internal class LoadFix : MonoWrap
    {
        internal static LoadFix Instance;
        private static int disconnects = 0;
        private static float disconnectTime = 0f;

        internal static bool enableMod;

        internal void Awake() =>
            Instance = this;

        public override void OnDisconnected(DisconnectCause cause)
        {
            if (!enableMod)
                return;

            if (Time.time > disconnectTime)
                disconnects = 0;

            disconnects += 1;
            disconnectTime = Time.time + 30f;
            if (disconnects >= 5)
            {
                disconnects = 0;
                IEnumerator KickMeInASecond()
                {
                    yield return new WaitForSeconds(0.5f);
                    Photon.Pun.PhotonNetwork.Disconnect();
                    while (!PhotonNetwork.InRoom) yield return null;
                    yield return new WaitForSeconds(2f);
                    Notifications.Notify("Automatically sent to OfflineDorm: too many disconnects", Color.red, 5f);
                }

                StartCoroutine(KickMeInASecond().WrapToIl2Cpp());
            }
            Notifications.Notify($"Photon Disconnect for {cause}\nCount: {disconnects}/5");
        }
    }
}
