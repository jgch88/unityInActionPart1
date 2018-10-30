using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAIBehaviour : MonoBehaviour {

	[SerializeField] private GameObject fireballPrefab;
	public float speed = 2.0f;
	public float obstacleRange = 0.5f;
	private GameObject _fireball;

	public const float baseSpeed = 3.0f;

	void Awake() {
		// the method OnSpeedChanged has a value, so Messenger<float> is used
		Messenger<float>.AddListener (GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}

	void OnDestroy() {
		Messenger<float>.RemoveListener (GameEvent.SPEED_CHANGED, OnSpeedChanged);
	}

	private void OnSpeedChanged(float value) {
		speed = baseSpeed * value;
	}

	// We need this variable to prevent AI from moving when it's "dead"
	private bool _isAlive; // we should use Finite State Machines (or state design pattern) rather than
	// a huge bunch of if/else statements

	// Use this for initialization
	void Start () {
		_isAlive = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (_isAlive) {
			transform.Translate (0, 0, speed * Time.deltaTime);
		}
			
		// a ray pointing in the same direction as the character
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;
		// Spherecast has a "width", whereas a raycast is an infinitely thin ray
		if (Physics.SphereCast (ray, 0.75f, out hit)) {
			GameObject hitObject = hit.transform.gameObject;
			if (hitObject.GetComponent<PlayerBehaviour> ()) {
				if (_fireball == null) {
					_fireball = Instantiate (fireballPrefab) as GameObject;
					// place it in front of enemy
					_fireball.transform.position = transform.TransformPoint (Vector3.forward * 1.5f);
					// point it in the same direction as enemy (this.transform.rotation has this omitted)
					_fireball.transform.rotation = transform.rotation;
				}
			}
			else if (hit.distance < obstacleRange) {
				// rotate to a semi-random new direction
				float angle = Random.Range (-110, 110);
				transform.Rotate (0, angle, 0);
			}
		}
	}

	public void SetAlive(bool alive) {
		_isAlive = alive;
	}
}
