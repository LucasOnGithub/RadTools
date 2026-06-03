using System;
using RecRoom.Core;
using UnityEngine;
using Valve.VR;

namespace RadTools.Libraries.RadiumWrapper;

public class PlayerRoot
{
    #region Parts and Objects

    private static GameObject _playerObject;

    public static GameObject PlayerObject
    {
        get
        {
            if (_playerObject == null)
                _playerObject = GameObject.Find("PlayerRoot/[Player](Clone)_local");

            return _playerObject;
        }
        set => _playerObject = value;
    }

    private static Camera _screenCamera;

    public static Camera PlayerCamera
    {
        get
        {
            if (_screenCamera == null)
                _screenCamera = GameObject.Find("GameRoot/Startup/Core Systems/[RR CameraRig]/ScreenModeCamera")
                    .GetComponent<Camera>();

            return _screenCamera;
        }
        set => _screenCamera = value;
    }

    public static Player PlayerInstance => PlayerObject?.GetComponent<Player>();

    public static WatchUI PlayerWatch => GameObject
        .Find("PlayerRoot/[Player](Clone)_local/TrackingSpace/WatchMenu/VisualRoot/[PlayerWatchMenu](Clone)")
        .GetComponent<WatchUI>();

    public static PlayerTrackingSpace TrackingSpace => PlayerInstance.trackingSpace;

    public static PlayerHead Head => PlayerInstance.head;

    public static PlayerHand LeftHand => PlayerInstance.leftHand;

    public static PlayerHand RightHand => PlayerInstance.rightHand;

    public static Rigidbody PlayerRigidbody => TrackingSpace.GetComponent<Rigidbody>();

    public static Vector3 World2Player(Vector3 world)
    {
        return world - Head.transform.position + TrackingSpace.transform.position;
    }

    #endregion

    #region Events and Interactions

    public static event Action<string> SendChatMessage;

    /// <summary>
    ///     NEVER RUN THIS METHOD. This is only for internal use to invoke the SendChatMessage event when the player chats.
    /// </summary>
    internal static void ChatMessageSent(string message)
    {
        SendChatMessage?.Invoke(message);
    }

    #endregion

    #region Inputs

    public static bool LeftHandPoint => LeftHand.CHNPKMIOAGG == PlayerHand.ALGBCDINEKF.PointGesture;
    public static bool RightHandPoint => RightHand.CHNPKMIOAGG == PlayerHand.ALGBCDINEKF.PointGesture;

    public static bool LeftHandGrab => SteamVR_Actions.default_SelfScale.GetState(SteamVR_Input_Sources.LeftHand);
    public static bool RightHandGrab => SteamVR_Actions.default_SelfScale.GetState(SteamVR_Input_Sources.RightHand);

    public static bool LeftHandTrigger =>
        SteamVR_Actions.default_Trigger.GetAxis(SteamVR_Input_Sources.LeftHand) > 0.5f || Input.GetMouseButton(0);

    public static bool RightHandTrigger =>
        SteamVR_Actions.default_Trigger.GetAxis(SteamVR_Input_Sources.RightHand) > 0.5f || Input.GetMouseButton(1);

    #endregion
}