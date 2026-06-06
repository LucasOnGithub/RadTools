using AGUI.StackedUI;
using BepInEx.Unity.IL2CPP.Utils.Collections;
using Photon.Pun;
using Photon.Realtime;
using RadTools.Core;
using RadTools.Extensions;
using RadTools.Libraries.RadiumWrapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.GridBrushBase;

namespace RadTools.Features
{
    [Instance.AddOnAwake]
    internal class SkidRay : MonoWrap
    {
        internal static SkidRay Instance;
        internal static ToggleButton3D toggle;
        internal static bool Active
        {
            get => toggle != null && toggle.BOHIGPFGJBA;
        }

        internal void Awake()
        {
            Instance = this;
            Libraries.RadiumWrapper.Patches.Tools.ToolInstantiated += (tool) =>
            {
                if (!Active)
                    return;

                var photonView = tool.photonView;
                if (photonView == null)
                    return;

                var creator = PhotonNetwork.NetworkingClient.CurrentRoom.GetPlayer(photonView.CreatorActorNr);
                if (creator == null)
                    creator = PhotonNetwork.NetworkingClient.CurrentRoom.GetPlayer(photonView.OwnerActorNr);
                
                if ((new[]
                {
                    "[feedbacktool]",
                    "[makerpen]",
                    "[sharecamera]",
                    "bowstring",
                    "pulltab",
                    "makerpenpalettemoveabletoolmenu",
                    "[giftbox]",
                    "creationshapecontainer"
                }).Any(tool.gameObject.name.ToLower().Contains))
                    return;

                Notifications.Notify($"New prefab {tool.gameObject.name} created by {creator?.NickName ?? "Unknown"}", tool.gameObject.name.ToLower().Contains("consumable") ? Color.white : Color.red);
            };
        }

        private float notificationTime;
        private bool previousEnabled;

        public void Update()
        {
            if (Active && !previousEnabled)
            {
                Notifications.Notify("Notifications for prefabs will appear. Don't leave enabled for long, causes performance drops", Color.yellow, 5f);
            }
            previousEnabled = Active;

            if (Active && Time.time > notificationTime)
            {
                var tools = GameObj.GetAllType<Tool>(1f);
                foreach (var tool in tools)
                {
                    if (tool == null) continue;
                    if (new[] { PlayerRoot.LeftHand, PlayerRoot.RightHand }.Any(x => x.OIDGJKOGAIJ == tool || Vector3.Distance(x.transform.position, tool.gameObject.transform.position) > 1f))
                        continue;

                    if ((new[]
                    {
                        //"[feedbacktool]",
                        //"[makerpen]",
                        //"[sharecamera]",
                        "bowstring",
                        "pulltab",
                        "makerpenpalettemoveabletoolmenu",
                        "[giftbox]",
                        "creationshapecontainer"
                    }).Any(tool.gameObject.name.ToLower().Contains))
                            continue;

                    var photonView = tool.photonView;
                    if (photonView == null)
                        continue;
                    var creator = PhotonNetwork.NetworkingClient.CurrentRoom.GetPlayer(photonView.CreatorActorNr);
                    if (creator == null)
                        creator = PhotonNetwork.NetworkingClient.CurrentRoom.GetPlayer(photonView.OwnerActorNr);

                    notificationTime = Time.time + 1f;
                    Notifications.Notify($"{tool.gameObject.name} created by {creator?.NickName ?? "Unknown"}", tool.gameObject.name.ToLower().Contains("consumable") ? Color.white : Color.red);
                }
            }

            var screen = GameObject.Find("PlayerRoot/[Player](Clone)_local/TrackingSpace/WatchMenu/VisualRoot/[PlayerWatchMenu](Clone)/Canvas/Screens/[SettingsScreen](Clone)/Body/[AdvancedSubscreen]/Options");
            if (screen == null)
                return;

            var showNames = screen.transform.Find("ShowNames");
            if (showNames == null)
                return;

            var skidRay = screen.transform.Find("SkidRay");
            if (skidRay != null)
                return;

            skidRay = Instantiate(showNames);
            skidRay.transform.SetParent(showNames.transform.parent, false);
            skidRay.name = "SkidRay";

            skidRay.transform.Find("Text").GetComponent<TMPro.TextMeshProUGUI>().text = "Skid Ray";
            toggle = skidRay.transform.Find("Container/ShowNames_Toggle").GetComponent<ToggleButton3D>();
            toggle.BOHIGPFGJBA = false;
            toggle.MPKFGBJBDBH = null;
        }
    }
}
