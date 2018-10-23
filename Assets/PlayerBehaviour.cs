using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
	private int _health;

	// Use this for initialization
	void Start () {
		_health = 5;
	}
		
	public void Hurt(int damage) {
		_health -= damage;
		Debug.Log ("Player Health: " + _health);
	}
}