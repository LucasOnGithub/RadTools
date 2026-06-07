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
    internal class ClearPhotonCache : MonoWrap
    {
        internal static ClearPhotonCache Instance;

        internal void Awake()
        {
            Instance = this;
            if (PlayerPrefs.HasKey("VoiceCloudBestRegion"))
            {
                PlayerPrefs.DeleteKey("VoiceCloudBestRegion");
                PlayerPrefs.Save();
            }
            if (PlayerPrefs.HasKey("PUNCloudBestRegion"))
            {
                PlayerPrefs.DeleteKey("PUNCloudBestRegion");
                PlayerPrefs.Save();
            }
        }
    }
}
