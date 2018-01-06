using System;
using UnityEngine;
using Vuforia;

/// <summary>
/// 如何不要改DefaultTrackableEventHandler 也可以追蹤的方法就是繼承追蹤事件，然後註冊就可以了
/// 
/// Net framework 已定義常用的委派型別
/// 如果不需要回傳值的委派可使用 System.Action
/// 如果需要回傳值的委派可使用 System.Func
/// </summary>
public class TrackableEvent : MonoBehaviour, ITrackableEventHandler {

    /// <summary>
    /// 追蹤到物件事件
    /// </summary>
    public event Action OnTrackingFound = ()=> { };

    /// <summary>
    /// 追蹤到物件事件，會回傳值。目前是回傳 Boolean
    /// </summary>
    public event Func<bool> OnTrackingFoundAndReturn = () => { return true; };

    /// <summary>
    /// 無追蹤到物件事件
    /// </summary>
    public event Action OnTrackingLost = () => { };

    /// <summary>
    /// 無追蹤到物件事件，會回傳值 。目前是回傳 Boolean
    /// </summary>
    public event Func<bool> OnTrackingLostAndReturn = () => { return false; };

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if(newStatus == TrackableBehaviour.Status.DETECTED || 
            newStatus == TrackableBehaviour.Status.TRACKED || 
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
            OnTrackingFoundAndReturn();
        }
        else
        {
            OnTrackingLost();
            OnTrackingLostAndReturn();
        }
    }

    void Start () {

        // 註冊追蹤事件
        GetComponent<TrackableBehaviour>().RegisterTrackableEventHandler(this);

    }

}
