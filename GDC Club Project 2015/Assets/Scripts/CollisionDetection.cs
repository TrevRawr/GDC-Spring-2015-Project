using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {
	GameObject explosionParticles;
	public int secondsTillExplosion;
	GameObject mainCamera;
	GameObject gameOverScreen;
	GameObject playerUI;
	bool gameOver = false;

	public AudioClip explosionSound;
	private AudioSource explosionSource;

	private AudioSource backgroundMusic;

	private int score;

	// Use this for initialization
	void Start () {
		explosionParticles = GameObject.Find ("Explosion");
		explosionParticles.SetActive (false);
		mainCamera = GameObject.Find("Main Camera");
		gameOverScreen = GameObject.Find ("Game Over Screen");
		gameOverScreen.SetActive (false);
		playerUI = GameObject.Find ("Player UI");
		explosionSource = GetComponent<AudioSource> ();
		backgroundMusic = GameObject.Find ("BackGround Music").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		RestartGame ();
	}

	void OnCollisionEnter(Collision collision){
		explosionParticles.SetActive(true);
		ParentCamera ();
		//DetachWings ();
		setScore ();
		playerUI.SetActive (false);
		gameOver = true;
		backgroundMusic.Stop ();
		Invoke ("Die", secondsTillExplosion);
	}

	void ParentCamera(){
		mainCamera.transform.parent = gameObject.transform;
	}

	void DetachWings(){
		GameObject wings = GameObject.Find ("AircraftWingsJet");
		wings.transform.SetParent (null);
		wings.AddComponent<Rigidbody> ();
		Rigidbody wingsBody = wings.GetComponent<Rigidbody> ();
		wingsBody.useGravity = false;
	}

	void setScore() {
		if (!gameOver) {
			score = GameObject.Find ("GameManager").GetComponent<ScoreManager> ().getCurrentScore ();
			explosionSource.PlayOneShot (explosionSound);
		}	
	}

	void Die(){
		gameOverScreen.SetActive (true);
		UnityEngine.UI.Text scoreText = gameOverScreen.GetComponentsInChildren<UnityEngine.UI.Text> ()[1];
		scoreText.text = "Your Score: " + score.ToString ();
	}

	void RestartGame ()
	{
		if (gameOver) {
			if (Input.GetKey (KeyCode.Space)) {
				int currLevel = Application.loadedLevel;
				Application.LoadLevel (currLevel);
			}
		}
	}
}
