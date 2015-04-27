using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody rigidBody;

	//if you want the dhip to move forward, use these
	public float forwardMoveSpeed;
	public float forwardAcceleration;

	//mobile tilt controls
	//note that right now, tilt speed is also being used as the arrow key control speed too
	public float tiltSpeed;
	public float tiltOffset;

	//boost
	public int boostStrength;
	public float boostRegenerationTime;
	private bool boostReady = true;

	//bounds
	public int maxVerticalDistance;
	public int maxHorizontalDistance;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate(){
		clampMovement();
		if (boostReady) {
			tiltMovement ();
			Movement ();
			boost ();
		}
	}

	private void Movement(){
		float verticalMovement = Input.GetAxis ("Vertical");
		float horizontalMovement = Input.GetAxis ("Horizontal");
		Vector3 movementVelocity = new Vector3 (horizontalMovement, verticalMovement, 0);
		rigidBody.AddForce (movementVelocity * tiltSpeed - rigidBody.velocity);
	}

	private void moveForward(){
		forwardMoveSpeed += forwardAcceleration;
	}

	
	//clamp player's movement so they can't move beyond certain x and y coordinates
	private void clampMovement(){
		rigidBody.position = new Vector3 (Mathf.Clamp (rigidBody.position.x, -maxHorizontalDistance, maxHorizontalDistance), Mathf.Clamp (rigidBody.position.y, -maxVerticalDistance, maxVerticalDistance), rigidBody.position.z);     
	}

	private void tiltMovement(){
		Vector3 movementVelocity = Input.acceleration;

		//we don't want the ship moving forwards or backwards
		//only up and down, left and right
		movementVelocity.z = 0;

		rigidBody.AddForce (movementVelocity * tiltSpeed - rigidBody.velocity);
	}

	private void boost(){
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			//Debug.Log (touch.deltaPosition);
			Vector2 boostDirection = touch.deltaPosition;
			if (boostDirection != Vector2.zero && boostReady) {
				Debug.Log ("Boost!");
				boostDirection.Normalize();
				rigidBody.velocity = Vector3.zero;
				rigidBody.AddForce(boostDirection * boostStrength);
				//disable boost
				boostReady = false;
				Invoke ("resetBoost", boostRegenerationTime);
			}
		}
	}

	private void resetBoost(){
		boostReady = true;
		rigidBody.velocity = new Vector3 (0, 0, rigidBody.velocity.z);
	}
}
