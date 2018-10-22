using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attribute (a C# thing) to ensure CharacterController exists, is mandatory
[RequireComponent(typeof(CharacterController))]
// Attribute to allow this component to show up in Unity Editor's menu 
[AddComponentMenu("Control Script/Keyboard Movement")]
public class KeyboardMovement : MonoBehaviour {
	public float speed = 3.0f;
	public float gravity = -9.8f;

	private CharacterController _charController;
	// Use this for initialization
	void Start () {
		// access the CharacterController component attached to this object
		// to utilize its collision detection behaviour
		_charController = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		float deltaX = Input.GetAxis ("Horizontal") * speed; 
		// "Horizontal/Vertical" is an indirect abstract name for keyboard mapping WASD
		float deltaZ = Input.GetAxis ("Vertical") * speed;

		Vector3 movement = new Vector3 (deltaX, 0, deltaZ);
		movement = Vector3.ClampMagnitude (movement, speed); // because moving diagonally shouldn't be faster than moving forward
		movement.y = gravity;
		movement *= Time.deltaTime;
		movement = transform.TransformDirection (movement); // transform movement from local to global coordinates -> this gives rise to "strafing" behaviour
		// CharacterController's Move() API which detects collisions
		_charController.Move (movement);

		// Time.deltaTime is the amount of time between frames
		// This makes it "frame rate independent" (for different GPU/FPS speeds)
		// transform.Translate (deltaX * Time.deltaTime, 0, deltaZ * Time.deltaTime);
	}
}
