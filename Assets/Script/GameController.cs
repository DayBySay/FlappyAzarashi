using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

	enum State {
		Ready,
		Play,
		GameOver
	}

	State state;
	int score;

	public AzarashiController azarashi;
	public GameObject blocks;
	public Text scoreLabel;
	public Text stateLabel;

	private FluctInterstitial interstitial;

	void Start() {
		Ready ();
		interstitial = gameObject.AddComponent<FluctInterstitial>();
	}

	void LateUpdate() {
		switch (state) {
		case State.Ready:
			if (Input.GetButtonDown ("Fire1")) {
				GameStart ();
			}
			break;
		case State.Play:
			if (azarashi.isDead) {
				GameOver ();
			}
			break;
		case State.GameOver:
			if (Input.GetButtonDown ("Fire1")) {
				Reload ();
			}
			break;
		}
	}

	void Ready() {
		state = State.Ready;
		azarashi.SetSteerActive (false);
		blocks.SetActive (false);

		scoreLabel.text = "Score : " + 0;

		stateLabel.gameObject.SetActive (true);
		stateLabel.text = "Ready";
	}

	void GameStart() {
		state = State.Play;
		azarashi.SetSteerActive (true);
		blocks.SetActive (true);
		stateLabel.text = "";
	}

	void GameOver() {
		state = State.GameOver;
		ScrollObject[] scrollObjects = GameObject.FindObjectsOfType<ScrollObject> ();

		foreach (ScrollObject so in scrollObjects) {
			so.enabled = false;
		}

		stateLabel.gameObject.SetActive (true);
		stateLabel.text = "Game Over";

		interstitial.ShowInterstitial("0000004738");
	}

	void Reload() {
		Application.LoadLevel (Application.loadedLevel);
	}

	public void IncreaseScore() {
		score++;
		scoreLabel.text = "Score : " + score;
	}
}
