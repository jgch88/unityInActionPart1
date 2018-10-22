using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {
	public float speed = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Space.Self follows rotation about the object's own axis
		// Space.World follows rotation about the non rotated world's fixed axis
		transform.Rotate (0, speed, 0);
	}
}
