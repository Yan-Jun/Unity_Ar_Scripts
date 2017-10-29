# Unity_Ar_Scripts

相機對焦控制器 CameraFocusController.cs
=================
設定對焦模式為自動對焦
CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

設定對焦模式為手動對焦
CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);

條件式編譯 (Unity文件有說明這些變數是由哪種平台啟動)
https://docs.unity3d.com/Manual/PlatformDependentCompilation.html

#if UNITY_EDITOR  // 由電腦為平台  	
#elif UNITY_ANDROID || UNITY_IPHONE // 由手機為平台
#endif

```C#
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


```



默認追蹤事件處理器 DefaultTrackableEventHandler.cs
=================
當追蹤到圖片要做什麼事?
private void OnTrackingFound()

當沒追蹤到圖片要做什麼事?
private void OnTrackingLost()

所以在角本內建立一個 IsFinding 的布林變數，可以讓場景的其它物件知道，我的相機是否追蹤中圖片。


虛擬按鈕 VirtualBtn.cs
=================
```C#

// 虛擬按鈕
public GameObject _vButton;

// 按下事件
public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
{
	Debug.Log("Pressed");
}

// 放開事件
public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
{
	Debug.Log("Released");
}

void Start () {
	// 註冊按鈕事件
	_vButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
}

```

陀螺儀 CameraGyro.cs
=================

```C#
void Start () {
	// 啟動陀螺儀裝置
	Input.gyro.enabled = true;
}
	
void Update () {
	// 目標攝影機依照陀螺儀旋轉
        transform.Rotate(0, -Input.gyro.rotationRateUnbiased.y, 0);
 }

```

相機追蹤 CameraTrack.cs
=================

```C#

//vuforia 5.6 影像停止追蹤方法
TrackerManager.Instance.GetTracker<ImageTracker>().Stop();
TrackerManager.Instance.GetTracker<ImageTracker>().Start();

TrackerManager.Instance.GetTracker(Tracker.Type.IMAGE_TRACKER).Stop();
TrackerManager.Instance.GetTracker(Tracker.Type.IMAGE_TRACKER).Start();

mTrackableBehaviour.UnregisterTrackableEventHandler(this);
mTrackableBehaviour.RegisterTrackableEventHandler(this);
	
// vuforia 2017.2 影像停止追蹤方法
TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
TrackerManager.Instance.GetTracker<ObjectTracker>().Start();

```
