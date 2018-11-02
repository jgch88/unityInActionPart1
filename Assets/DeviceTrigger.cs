using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour {
	[SerializeField] private GameObject[] targets;

	void OnTriggerEnter (Collider other) {
		// only player can trigger door open
		if (other.GetComponent<CharacterController> () != null) {
			foreach (GameObject target in targets) {
				target.SendMessage ("Activate");
			}
		}
	}
	
	void OnTriggerExit (Collider other) {
		if (other.GetComponent<CharacterController> () != null) {
			foreach (GameObject target in targets) {
				target.SendMessage ("Deactivate");
			}
		}
	}
}
