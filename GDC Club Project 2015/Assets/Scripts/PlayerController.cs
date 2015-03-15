using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody rigidBody;
	public float forwardMoveSpeed;
	public float forwardAcceleration;
	public int verticalSpeed;
	public int horizontalSpeed;

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
		Movement ();
	}

	private void Movement(){
		//clamp player's movement so they can't move beyond certain x and y coordinates
		rigidBody.position = new Vector3 (Mathf.Clamp (rigidBody.position.x, -maxHorizontalDistance, maxHorizontalDistance), Mathf.Clamp (rigidBody.position.y, -maxVerticalDistance, maxVerticalDistance), rigidBody.position.z);     
		
		float verticalMovement = Input.GetAxis ("Vertical");
		float horizontalMovement = Input.GetAxis ("Horizontal");
		rigidBody.velocity = new Vector3 (horizontalSpeed * horizontalMovement , verticalSpeed * verticalMovement, forwardMoveSpeed);
		forwardMoveSpeed += forwardAcceleration;
	}
}
