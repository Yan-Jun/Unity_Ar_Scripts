using System;
using UnityEngine;
using Vuforia;

public class ExtendedTrackableHandler : MonoBehaviour
{
    [SerializeField]
    private TrackableBehaviour _trackableBehaviour;

    private void Update()
    {
        // Test
        if (Input.GetKeyDown(KeyCode.F2)) OnEnableExtendeTracking();
        else if (Input.GetKeyDown(KeyCode.F3)) OnDisableExtendeTracking();
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
