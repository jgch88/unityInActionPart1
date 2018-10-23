using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour {
	private Camera _camera;
	// Use this for initialization
	void Start () {
		_camera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 screenCenterPoint = new Vector3 (_camera.pixelWidth / 2, _camera.pixelHeight / 2);
			// create ray from center of the camera screen
			Ray ray = _camera.ScreenPointToRay (screenCenterPoint);
			RaycastHit hit; // RaycastHit data structure stores information about the intersection of the ray
			// Needs Physics.Raycast() to mutate data on where the intersection happened and which object was intersected

			// the out in the argument is like C++ passing in by reference, as opposed to copying it into scope?
			if (Physics.Raycast (ray, out hit)) {
				Debug.Log ("Hit " + hit.point);
			} // else if Physics.Raycast returns false, it didn't hit any object (e.g. it hit the "sky")
		}
	}
}
