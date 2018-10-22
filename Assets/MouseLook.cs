using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {
	public enum RotationAxes {
		MouseXandY = 0,
		MouseX = 1,
		MouseY = 2
	}
	public RotationAxes axes = RotationAxes.MouseXandY;
	public float sensitivityHorizontalAngle = 9.0f;
	public float sensitivityVerticalAngle = 9.0f;

	public float minimumVerticalAngle = -89.0f;
	public float maximumVerticalAngle = 89.0f;

	private float _rotationX = 0;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (axes == RotationAxes.MouseX) {
			// Unity Input API
			// https://docs.unity3d.com/ScriptReference/Input.GetAxis.html

			// Rotate() increments the current rotation, like add 5 to angle
			// It also automatically handles exceeding 360, and creating new Vectors since
			// transform is read only
			transform.Rotate (0, Input.GetAxis("Mouse X") * sensitivityHorizontalAngle, 0);
		} else if (axes == RotationAxes.MouseY) {
			// _rotationX is a placeholder variable for the angle itself
			_rotationX -= Input.GetAxis ("Mouse Y") * sensitivityVerticalAngle;
			_rotationX = Mathf.Clamp (_rotationX, minimumVerticalAngle, maximumVerticalAngle);

			float rotationY = transform.localEulerAngles.y;

			// localEulerAngles sets the current angle directly relative to parent's axes (roll/pitch/yaw)
			// https://docs.unity3d.com/ScriptReference/Transform-localEulerAngles.html

			// the alternative is to use localRotation which uses quaternion notation...
			transform.localEulerAngles = new Vector3 (_rotationX, rotationY, 0); // transform's vector is read only make a new Vector
		} else {
		}
	}
}
