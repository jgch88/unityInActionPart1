using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour {

	// method called in RayShooter : target.ReactToHit();
	public void ReactToHit() {
		StartCoroutine (Die ());
	}

	// Coroutine
	private IEnumerator Die() {
		this.transform.Rotate (-75, 0, 0); // check out "tweens" to make smooth object animation

		yield return new WaitForSeconds (2);

		// the "this" keyword is optional...
		// "this" refers to only the script component
		// "this.gameObject" refers to the object this script is attached to.
		// Destroy the gameObject, not "this"!
		Destroy (this.gameObject);
	}
		
}
