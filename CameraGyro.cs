using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraGyro : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Input.gyro.enabled = true;
	Input.compensateSensors = true;		// Compensated sensors are accelerometer, compass, gyroscope.
	Input.gyro.updateInterval = 0.01f;	// Update gyroscope data interval
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, -Input.gyro.rotationRateUnbiased.y, 0);
    }
}
