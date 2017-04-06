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
  if(Input.GetMouseButtonUp(0))
#elif UNITY_ANDROID || UNITY_IPHONE 	// 由手機為平台
  if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
#endif
{
	CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
}


默認追蹤事件處理器 DefaultTrackableEventHandler.cs
=================
當追蹤到圖片要做什麼事?
private void OnTrackingFound()

當沒追蹤到圖片要做什麼事?
private void OnTrackingLost()

所以在角本內建立一個 IsFinding 的布林變數，可以讓場景的其它物件知道，我的相機是追蹤中圖片。
