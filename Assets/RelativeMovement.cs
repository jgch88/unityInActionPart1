using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RelativeMovement : MonoBehaviour {
	
	[SerializeField] private Transform target;
	public float rotSpeed = 15.0f;

	// moving
	public float moveSpeed = 6.0f;
	private CharacterController _charController;

	// jumping
	public float jumpSpeed = 15.0f;
	public float gravity = -9.8f;
	public float terminalVelocity = -10.0f;
	public float minFall = -1.5f;
	private float _vertSpeed;

	// sliding when not directly on flat ground
	private ControllerColliderHit _contact;

	void Start () {
		_charController = GetComponent<CharacterController> ();
		_vertSpeed = minFall;
	}

	void Update () {
		Vector3 movement = Vector3.zero;

		float horInput = Input.GetAxis ("Horizontal");
		float vertInput = Input.GetAxis ("Vertical");

		// horizontal movement
		if (horInput != 0 || vertInput != 0) {
			movement.x = horInput * moveSpeed;
			movement.z = vertInput * moveSpeed;
			movement = Vector3.ClampMagnitude (movement, moveSpeed);

			Quaternion tmp = target.rotation;
			target.eulerAngles = new Vector3 (0, target.eulerAngles.y, 0);
			movement = target.TransformDirection (movement);
			target.rotation = tmp;

			// using Lerp to smoothen the turning
			Quaternion direction = Quaternion.LookRotation (movement);
			transform.rotation = Quaternion.Lerp (transform.rotation,
				direction, rotSpeed * Time.deltaTime);

		}

		// raycast a short distance down to check that player is "on flat ground"
		// and not floating off a ledge/standing on a very steep slope
		bool hitGround = false;
		RaycastHit hit;
		if (_vertSpeed < 0 && Physics.Raycast (transform.position, Vector3.down, out hit)) {
			float check = (_charController.height + _charController.radius) / 1.9f; // how far below the player to raycast and detect "ground"
			// Height of controller (height without rounded ends), plus rounded ends. Divide by 2 because ray is cast from the middle of the player
			// But we don't use exactly 2, use 1.9 instead because we really want the ray to extend a little bit past the character.
			hitGround = hit.distance <= check;
		}

		// vertical movement
		if (hitGround) {
			if (Input.GetButtonDown ("Jump")) {
				_vertSpeed = jumpSpeed;
			} else {
				// this "minFall" is like weight (and that's why it's not 0), the character is always
				// pressing downward while running
				// this is crucial for running up and down uneven terrain
				_vertSpeed = minFall;
			}
		} else {
			_vertSpeed += gravity * 5 * Time.deltaTime;
			if (_vertSpeed < terminalVelocity) {
				_vertSpeed = terminalVelocity;
			}

			// nudge the character to slide down ledges/steep faces
			if (_charController.isGrounded) {
				if (Vector3.Dot (movement, _contact.normal) < 0) {
					movement = _contact.normal * moveSpeed;
				} else {
					movement += _contact.normal * moveSpeed;
				}
			}
		}
		movement.y = _vertSpeed;

		movement *= Time.deltaTime;
		_charController.Move (movement);
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		_contact = hit;
	}
}
