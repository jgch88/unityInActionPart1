using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // for GUI framework

public class RayShooter : MonoBehaviour {
	private Camera _camera;
	// Use this for initialization
	void Start () {
		_camera = GetComponent<Camera> ();

		// Hide the mouse at the center of the screen (removed for GUI purposes)
		// Cursor.lockState = CursorLockMode.Locked;
		// Cursor.visible = false;
	}

	// Use Basic UI (unity also has advanced UI)
	// OnGUI() is a MonoBehaviour built in method
	// runs every frame right after 3D scene is rendered, 
	// so it appears like something is in between the 3D scene
	// and the camera (or on top of the 3D scene)
	void OnGUI() {
		int size = 12; // This is the size of the Rect, not the text size
		float posX = _camera.pixelWidth / 2 - size / 4;
		float posY = _camera.pixelHeight / 2 - size / 2;
		// drawing a crosshair using a text label
		GUI.Label (new Rect (posX, posY, size, size), "*");
	}
	
	// Update is called once per frame
	void Update () {
		// Check that we aren't clicking on a GUI object (prevent shooting while interacting with GUI)
		if (Input.GetMouseButtonDown (0) && !EventSystem.current.IsPointerOverGameObject()) {
			Vector3 screenCenterPoint = new Vector3 (_camera.pixelWidth / 2, _camera.pixelHeight / 2);
			// create ray from center of the camera screen
			Ray ray = _camera.ScreenPointToRay (screenCenterPoint);
			RaycastHit hit; // RaycastHit data structure stores information about the intersection of the ray
			// Needs Physics.Raycast() to mutate data on where the intersection happened and which object was intersected

			// the out in the argument is like C++ passing in by reference, as opposed to copying it into scope?
			if (Physics.Raycast (ray, out hit)) {
				GameObject hitObject = hit.transform.gameObject; // retrieve the object that ray hit
				ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>(); // This is because we created our own C# ReactiveTarget script
				// this method checks that the object has this Component/Script attached to it, else returns null
				// elif / switch issues come to mind when you have multiple script detection (probably has an elegant design pattern to solve this)
				// we could also do hitObject.GetComponent<KeyboardMovement> or GetComponent<MouseLook> which was done in Section 2
				if (target != null) {
					Debug.Log ("Target Hit: " + hit.point);
					target.ReactToHit (); // calling method on the target, which we need to define in <ReactiveTarget> script
				} else {
					StartCoroutine (ShowHitLocationUsingSphere (hit.point));
				}
			} // else if Physics.Raycast returns false, it didn't hit any object (e.g. it hit the "sky")
		}
	}

	// IEnumerator is a Coroutine data type
	private IEnumerator ShowHitLocationUsingSphere(Vector3 pos) {
		GameObject sphere = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		sphere.transform.position = pos;

		// yield tells coroutine where to pause
		// this is really interesting syntax, returning (to prevent blocking) and also not needing
		// a callback function for a timer
		yield return new WaitForSeconds (1);
		// Coroutines aren't asynchronous -> they hand back program flow but pick it up again from that point in the
		// next frame, so they seem to run in the background of a program through
		// a repeated cycle of running PARTWAY (I guess WaitForSeconds(1) splits this out over several frames)
		// then returning to the rest of the program.
		// e.g. for 60 FPS, line 37 is executed 60 times, before line 44... probably some sort of timeDelta thing going on.

		// really high level abstraction: this is just a backgroundWait(1)
		Destroy (sphere);
	}

}
