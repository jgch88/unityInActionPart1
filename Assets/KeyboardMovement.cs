using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour {
	public float speed = 3.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float deltaX = Input.GetAxis ("Horizontal") * speed; 
		// "Horizontal/Vertical" is an indirect abstract name for keyboard mapping WASD
		float deltaZ = Input.GetAxis ("Vertical") * speed;

		// Time.deltaTime is the amount of time between frames
		// This makes it "frame rate independent" (for different GPU/FPS speeds)
		transform.Translate (deltaX * Time.deltaTime, 0, deltaZ * Time.deltaTime);
	}
}
