using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackAndForth : MonoBehaviour {
	public float speed = 3.0f;
	public float maxZ = 2.0f;
	public float minZ = -2.0f; // coordinates the object moves between

	private int _direction = -1; // direction object is currently moving in
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0, 0, _direction * speed * Time.deltaTime);

		bool bounced = false;
		if (transform.position.z > maxZ || transform.position.z < minZ) {
			_direction = -_direction;
			bounced = true;
		}
		if (bounced) {
			transform.Translate (0, 0, _direction * speed * Time.deltaTime);
		}
	}
}
