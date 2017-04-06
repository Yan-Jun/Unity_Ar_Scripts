using UnityEngine;
using System.Collections;
using Vuforia;

public class CameraFocusController : MonoBehaviour {

	public static bool m_bIsFocus;

	// Use this for initialization
	void Start()
	{
		m_bIsFocus = false;
		//CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
	}

	// Update is called once per frame
	void Update()
	{
		//if (m_bIsFocus)
		#if UNITY_EDITOR
			if(Input.GetMouseButtonUp(0))
		#elif UNITY_ANDROID || UNITY_IPHONE
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
		#endif
		{
			//CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
			CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
		}
	}
}
