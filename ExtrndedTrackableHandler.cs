using System;
using UnityEngine;
using Vuforia;

public class ExtrndedTrackableHandler : MonoBehaviour
{
    [SerializeField]
    private TrackableBehaviour _trackableBehaviour;

    private void Start()
    {

    }

    public void OnEnableExtendeTracking()
    {
        ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        tracker.Stop();
        ((ImageTarget)_trackableBehaviour.Trackable).StartExtendedTracking();
        tracker.Start();
    }

    public void OnDisableExtendeTracking()
    {

        ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
        tracker.Stop();
        ((ImageTarget)_trackableBehaviour.Trackable).StopExtendedTracking();
        tracker.Start();
    }

}
