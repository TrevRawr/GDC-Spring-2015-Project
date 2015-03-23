using UnityEngine;
using System.Collections;

public class RandomRotater : MonoBehaviour {

	public float tumble;
	public float lifetime=10.0f;
	void Start()
	{
		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere*tumble;
		
	}
	void Update()
	{
		lifetime-=Time.deltaTime;
		if(lifetime<0)
		{
			Destroy (this.gameObject);
		}
	}

}
