using AGUI.StackedUI;
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

        internal void Awake() =>
            Instance = this;

        public void Update()
        {
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

            skidRay.transform.Find("Text").GetComponent<TMPro.TextMeshProUGUI>().text = "Skid Ray";
            toggle = skidRay.transform.Find("Container/ShowNames_Toggle").GetComponent<ToggleButton3D>();
            toggle.BOHIGPFGJBA = false;
            toggle.MPKFGBJBDBH = null;
        }
    }
}
