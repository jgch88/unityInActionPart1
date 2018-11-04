using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceTrigger : MonoBehaviour {
	[SerializeField] private GameObject[] targets;

	public bool requireKey;

	void OnTriggerEnter (Collider other) {
		if (requireKey && Managers.Inventory.equippedItem != "key") {
			return;
		}

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
