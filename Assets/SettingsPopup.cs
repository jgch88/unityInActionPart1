using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The UI window that pops up after clicking the gear icon
public class SettingsPopup : MonoBehaviour {

	public void Open () {
		gameObject.SetActive (true);
	}
	
	public void Close () {
		gameObject.SetActive (false);
	}

	public void OnSubmitName (string name) {
		Debug.Log (name);
	}

	public void OnSpeedValue (float speed) {
		Debug.Log ("Speed: " + speed);
		// Broadcast the event SPEED_CHANGED along with a float value
		Messenger<float>.Broadcast (GameEvent.SPEED_CHANGED, speed);
	}
}
