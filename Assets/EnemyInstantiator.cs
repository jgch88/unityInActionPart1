using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour {

	// enemyPrefab stores the prefab, _enemy stores the instance of the prefab, note the distinction

	[SerializeField] private GameObject enemyPrefab;
	// public fields show up in the Unity Inspector by default
	// private fields need [SerializeField] to make them show up
	private GameObject _enemy;

	void Update () {

		// this condition works because Destroy() probably lets garbage collection remove its reference
		// actually, it's not garbage collection -> Unity handles GameObjects via references in a scene graph.
		// the == null is actually important! overloaded by Unity to return True when we've Destroy()ed the object.
		if (_enemy == null) {
			// the Instantiate method instantiates the Enemy prefab in the enemyPrefab field as a GameObject
			// in C#, the "as" keyword is used for typecasting, here Object is casted to GameObject

			_enemy = Instantiate (enemyPrefab) as GameObject;
			// Vector3 needs float explicitly declared with f behind
			_enemy.transform.position = new Vector3 (1, 2, -2.25f);
			float angle = Random.Range (0, 360);
			_enemy.transform.Rotate (0, angle, 0);
		}
	}
}
