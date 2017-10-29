using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CameraTrack : MonoBehaviour {

	void Start () {

        

        //vuforia 5.6
        //TrackerManager.Instance.GetTracker<ImageTracker>().Stop();
        //TrackerManager.Instance.GetTracker<ImageTracker>().Start();

        //TrackerManager.Instance.GetTracker(Tracker.Type.IMAGE_TRACKER).Stop();
        //TrackerManager.Instance.GetTracker(Tracker.Type.IMAGE_TRACKER).Start();

        //mTrackableBehaviour.UnregisterTrackableEventHandler(this);
        //mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }
	
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            TrackerManager.Instance.GetTracker<ObjectTracker>().Stop();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            TrackerManager.Instance.GetTracker<ObjectTracker>().Start();
        }

	}
}
