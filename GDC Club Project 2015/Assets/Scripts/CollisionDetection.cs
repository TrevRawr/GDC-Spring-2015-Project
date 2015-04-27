using UnityEngine;
using System.Collections;

public class CollisionDetection : MonoBehaviour {
	GameObject explosionParticles;
	public int secondsTillExplosion;
	GameObject mainCamera;

	// Use this for initialization
	void Start () {
		explosionParticles = GameObject.Find ("Explosion");
		explosionParticles.SetActive (false);
		mainCamera = GameObject.Find("Main Camera");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision){
		explosionParticles.SetActive(true);
		ParentCamera ();
		//DetachWings ();
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

	void Die(){
		Debug.Log ("Die");
		Application.LoadLevel ("GameOver");
	}
}
