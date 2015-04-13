using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody rigidBody;
	public float forwardMoveSpeed;
	public float forwardAcceleration;
	public int verticalSpeed;
	public int horizontalSpeed;

	//mobile
	public float tiltOffset;
	public float verticalTiltSpeed;
	public float horizontalTiltSpeed;
	public int boostStrength;
	private bool boostReady = true;
	public int maxSpeed;

	//bounds
	public int maxVerticalDistance;
	public int maxHorizontalDistance;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate(){
		//Movement ();
		tiltMovement ();
		boost ();
	}

	private void Movement(){
		clampMovement ();
		float verticalMovement = Input.GetAxis ("Vertical");
		float horizontalMovement = Input.GetAxis ("Horizontal");
		rigidBody.velocity = new Vector3 (horizontalSpeed * horizontalMovement , verticalSpeed * verticalMovement, forwardMoveSpeed);
		forwardMoveSpeed += forwardAcceleration;
	}

	
	//clamp player's movement so they can't move beyond certain x and y coordinates
	private void clampMovement(){
		rigidBody.position = new Vector3 (Mathf.Clamp (rigidBody.position.x, -maxHorizontalDistance, maxHorizontalDistance), Mathf.Clamp (rigidBody.position.y, -maxVerticalDistance, maxVerticalDistance), rigidBody.position.z);     
	}

	private void tiltMovement(){
		clampMovement ();
		Vector3 movementSpeed = Input.acceleration;
		float verticalMovement = -movementSpeed.y - tiltOffset/100;
		float horizontalMovement = movementSpeed.x;

		//force movement not working yet
//		Vector2 currSpeed = new Vector2 (rigidBody.velocity.x, rigidBody.velocity.y);
//		if (currSpeed.magnitude < maxSpeed) {
//			rigidBody.AddForce(new Vector3(horizontalTiltSpeed * horizontalMovement, verticalTiltSpeed * verticalMovement, forwardMoveSpeed));
//		}
		rigidBody.position += new Vector3 (horizontalTiltSpeed * horizontalMovement , verticalTiltSpeed * verticalMovement, forwardMoveSpeed);
		forwardMoveSpeed += forwardAcceleration;
	}

	private void boost(){
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			//Debug.Log (touch.deltaPosition);
			Vector2 boost = touch.deltaPosition;
			if (boost != Vector2.zero && boostReady) {
				Debug.Log ("Boost!");
				boost.Normalize();
				rigidBody.AddForce(boost * boostStrength);
				//disable boost
				boostReady = false;
				Invoke ("resetBoost", 3);
			}
		}
	}

	private void resetBoost(){
		boostReady = true;
		rigidBody.velocity = new Vector3 (0, 0, rigidBody.velocity.z);
	}
}
