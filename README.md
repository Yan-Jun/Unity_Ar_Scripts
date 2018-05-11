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

Vuforia - Camera Focus Modes
https://library.vuforia.com/articles/Solution/Working-with-the-Camera#Camera-Focus-Modes

```C#
using UnityEngine;
using System.Collections;
using Vuforia;

public class CameraFocusController : MonoBehaviour {

	// 選擇模式
	[SerializeField]
	private CameraDevice.FocusMode FocusMode = CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO;

    void Start()
    {
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
    
		.
		.
		.

}
```



默認追蹤事件處理器 DefaultTrackableEventHandler.cs
=================
當追蹤到圖片要做什麼事?
private void OnTrackingFound()

當沒追蹤到圖片要做什麼事?
private void OnTrackingLost()

所以在角本內建立一個 IsFinding 的布林變數，可以讓場景的其它物件知道，我的相機是否追蹤中圖片。



自訂追蹤事件處理器 TrackableEvent.cs
=================
如何不要改DefaultTrackableEventHandler.cs 也可以追蹤的方法就是繼承追蹤事件，然後註冊就可以。
腳本內有提供使用　Net framework　已定義常用的委派型別來呼叫
```C#

using System;
using UnityEngine;
using Vuforia;

public class TrackableEvent : MonoBehaviour, ITrackableEventHandler {

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if(newStatus == TrackableBehaviour.Status.DETECTED || 
            newStatus == TrackableBehaviour.Status.TRACKED || 
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            /// If is found to call custom methods...
        }
        else
        {
            /// If is lost to call custom methods...
        }
    }

    void Start () {

        // Register trackable event.
        GetComponent<TrackableBehaviour>().RegisterTrackableEventHandler(this);

    }

```


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
	// 補償感測器是指加速度計、磁力計、陀螺儀
	Input.compensateSensors = true;	
	// 更新陀螺儀的間格時間
	Input.gyro.updateInterval = 0.01f;
}
	
void Update () {
	// 目標攝影機依照陀螺儀旋轉
        transform.Rotate(0, -Input.gyro.rotationRateUnbiased.y, 0);
 }

```

相機追蹤 CameraTrack.cs
=================

```C#

//vuforia old version (舊版本)
TrackerManager.Instance.GetTracker<ImageTracker>().Stop();
TrackerManager.Instance.GetTracker<ImageTracker>().Start();

TrackerManager.Instance.GetTracker(Tracker.Type.IMAGE_TRACKER).Stop();
TrackerManager.Instance.GetTracker(Tracker.Type.IMAGE_TRACKER).Start();

mTrackableBehaviour.UnregisterTrackableEventHandler(this);
mTrackableBehaviour.RegisterTrackableEventHandler(this);
	
// vuforia new version (新版本)
TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
TrackerManager.Instance.GetTracker<ObjectTracker>().Start();

// 當前狀態
TrackerManager.Instance.GetTracker<ObjectTracker>().IsActive

```

擴展追蹤 ExtendedTrackableHandler.cs
=================
用腳本開關的擴展追蹤方法，若要修改擴展追蹤，需要將追蹤關閉才能修改。

擴展追蹤
https://library.vuforia.com/articles/Training/Extended-Tracking.html

```C#
// 追蹤行為物件
[SerializeField]
private TrackableBehaviour _trackableBehaviour;

// 開啟擴展追蹤方法
public void OnEnableExtendeTracking()
{
	ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
	tracker.Stop();
	((ImageTarget)_trackableBehaviour.Trackable).StartExtendedTracking();
	tracker.Start();
}

// 關閉擴展追蹤方法
public void OnDisableExtendeTracking()
{

	ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
	tracker.Stop();
	((ImageTarget)_trackableBehaviour.Trackable).StopExtendedTracking();
	tracker.Start();
}
```
