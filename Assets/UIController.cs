using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	[SerializeField] private Text scoreLabel;
	[SerializeField] private SettingsPopup settingsPopup;
	private int _score;

	// Awake and OnDestroy are part of MonoBehaviour
	// Probably used for garbage collection to prevent dangling references when Adding/Removing listeners
	void Awake() {
		// Declare which method responds to ENEMY_HIT event
		Messenger.AddListener (GameEvent.ENEMY_HIT, OnEnemyHit);
	}

	void OnDestroy() {
		// When object is destroyed, clean up listener to avoid errors
		Messenger.RemoveListener (GameEvent.ENEMY_HIT, OnEnemyHit);
	}

	void Start() {
		_score = 0;
		scoreLabel.text = _score.ToString ();

		settingsPopup.Close ();
	}

	private void OnEnemyHit() {
		_score += 1;
		scoreLabel.text = _score.ToString ();
	}

	public void OnOpenSettings() {
		settingsPopup.Open ();
	}
}
