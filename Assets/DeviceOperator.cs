using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour {

	public float radius = 1.5f; // how far away to operate devices from

	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			Collider[] hitColliders = 
				// return an array of all objects that are within a given distance of a given position
				Physics.OverlapSphere (transform.position, radius);
			foreach (Collider hitCollider in hitColliders) {
				Vector3 direction = hitCollider.transform.position - transform.position;
				if (Vector3.Dot (transform.forward, direction) > .5f) {
					// call Operate() on the interactive device's scripts
					hitCollider.SendMessage ("Operate",
						SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
}
