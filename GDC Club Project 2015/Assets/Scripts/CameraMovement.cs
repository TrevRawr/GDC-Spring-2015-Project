using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	GameObject player;
	public float cameraLagRate;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		followPlayerWithCameraLag ();
	}

	private void followPlayerWithCameraLag(){
		Vector3 playerPosition = new Vector3 (player.transform.position.x / cameraLagRate, player.transform.position.y / cameraLagRate, transform.position.z);
		transform.position = playerPosition;
	}

	private void lookAtPlayer(){
		transform.LookAt (player.transform.position);
	}
}
