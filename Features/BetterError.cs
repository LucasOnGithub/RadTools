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
    internal class BetterError : MonoWrap
    {
        internal static BetterError Instance;
        internal static Dictionary<string, string> errors = new()
        {
            ["(Error: 4)"] = "(ClientTimeout code 4)",
            ["(Error: 32752)"] = "(InsufficientSpace \"There is no more space in this room\" code 32752)",
            ["(Error: 32765)"] = "(Game full code 32765)",
            ["(Error: -1)"] = "(RadTools automated disconnect code -1/invalid)"
        };

        internal void Awake() =>
            Instance = this;

        public void Update()
        {
            var screen = GameObject.Find("PlayerRoot/[Player](Clone)_local/TrackingSpace/WatchMenu/VisualRoot/[PlayerWatchMenu](Clone)/Canvas/Screens/[ConfirmationDialogScreen](Clone)/Body/BodyBG/TextGroup/BodyText");
            if (screen == null)
                return;

            var text = screen.GetComponent<TMPro.TextMeshProUGUI>();
            if (text == null) return;

            foreach (var error in errors)
            {
                if (text.text.Contains(error.Key))
                    text.text = text.text.Replace(error.Key, error.Value);
            }
        }
    }
}
