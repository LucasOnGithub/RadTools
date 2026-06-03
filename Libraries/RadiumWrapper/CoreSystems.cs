using UnityEngine;

namespace RadTools.Libraries.RadiumWrapper;

public static class CoreSystems
{
    private static GameObject _coreSystemObject;

    public static GameObject CoreObject
    {
        get
        {
            if (_coreSystemObject == null)
                _coreSystemObject = GameObject.Find("GameRoot/Startup/Core Systems");

            return _coreSystemObject;
        }
        set => _coreSystemObject = value;
    }

    internal static RecRoomSceneManager SceneManager =>
        RecRoomSceneManager._OCDLBPHJMOP_k__BackingField;

    public static GameObject GetCoreSystem(string name)
    {
        return CoreObject.transform.Find(name).gameObject;
    }
}