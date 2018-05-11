using UnityEngine;
using System.Collections;
using Vuforia;

public class CameraFocusController : MonoBehaviour {

	[SerializeField]
    private CameraDevice.FocusMode FocusMode = CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO;

    void Start()
    {
        // Save player prefs focus mode.
        FocusMode = (CameraDevice.FocusMode)PlayerPrefs.GetInt(TT_Manager.KEY_FOCUSMODE, (int)CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

        var vuforia = VuforiaARController.Instance;
        vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        vuforia.RegisterOnPauseCallback(OnPaused);

        // Trigger auto focus mode.
        if(FocusMode == CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO)
            vuforia.RegisterTrackablesUpdatedCallback(OnTriggerAutoUpdate);
    }

    private void OnTriggerAutoUpdate()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonUp(0))
#elif UNITY_ANDROID || UNITY_IPHONE
	if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
#endif
        {
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
        }
    }

    private void OnVuforiaStarted()
    {
        CameraDevice.Instance.SetFocusMode(FocusMode);
    }

    private void OnPaused(bool paused)
    {
        if (!paused) // resumed
        {
            // Set again autofocus mode when app is resumed
            CameraDevice.Instance.SetFocusMode(FocusMode);
        }
    }
}
