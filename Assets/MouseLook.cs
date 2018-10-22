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
	public float sensitivtyHorizontal = 9.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (axes == RotationAxes.MouseX) {
			// Unity Input API
			// https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
			transform.Rotate (0, Input.GetAxis("Mouse X"), 0);
		} else if (axes == RotationAxes.MouseY) {
		} else {
		}
	}
}
