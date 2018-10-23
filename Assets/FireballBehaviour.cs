using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehaviour : MonoBehaviour {
	public float speed = 3.0f;
	public int damage = 1;
	
	// Which sequence of scripts Updates() in the engine?
	// There is Edit -> Project Settings -> Script Execution Order
	void Update () {
		transform.Translate (0, 0, speed * Time.deltaTime);
	}

	void OnTriggerEnter (Collider other) {
		Debug.Log ("Fireball Collision");
		PlayerBehaviour player = other.GetComponent<PlayerBehaviour> ();
		if (player != null) {
			Debug.Log("Player hit");
		}
		Destroy(this.gameObject);
	}

}
