using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

	// Use this for initialization
	public GameObject hazard;//obstacle to be spawned, defined in inspector
	public GameObject ufo;//ufo to be spawned, defined in inspector
	public Vector3 spawnValues;//location range for which the objects to spawn
	
	public int hazardCount;//number of obstacles per wave
	public float spawnWait;//how long to wait between spawning obstacles
	public float startWait;//how long until the objects start spawning
	public float waveWait;//how long it is between 'waves,' which is defined by spawning the number in hazardcount, then incrementing
	public int ufoProbability;//0 to 100. Odds of ufo showing up.

	private GameObject player;
	public float asteroidToCharacterDistance;
	public float asteroidSpeed = 100;
	private int wallchance;

	void Start () 
	{
		StartCoroutine  (Spawnwaves());//the IEnumurator is a coroutine that generates obstacles. not reliant on update()
		player = GameObject.Find ("Player");
	}

	void Update(){
		moveSpawner ();
	}
	
	// Update is called once per frame
	IEnumerator Spawnwaves()
	{ 
		yield return new WaitForSeconds (startWait);
		while(true)
		{	
			int ufochance = Random.Range(0, 100);
			if (ufochance < ufoProbability){
				Vector3 ufoSpawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y) , spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				
				GameObject newUfo = (GameObject)Instantiate(ufo, ufoSpawnPosition, spawnRotation);
				Rigidbody ufoRigidBody = newUfo.GetComponent<Rigidbody>();
				ufoRigidBody.AddForce(-Vector3.forward * asteroidSpeed);
				
				yield return new WaitForSeconds (0);
			}
			for(int i=0; i<hazardCount; i++)
			{			
				Vector3 asteroidSpawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y) , spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;

				GameObject asteroid = (GameObject)Instantiate(hazard, asteroidSpawnPosition, spawnRotation);
				Rigidbody asteroidRigidBody = asteroid.GetComponent<Rigidbody>();
				asteroidRigidBody.AddForce(-Vector3.forward * asteroidSpeed);

				yield return new WaitForSeconds (spawnWait);
			}

			hazardCount++;
			wallchance = Random.Range(0, 100);
			if (wallchance>50)
			{
				for(int i=0; i<hazardCount/2; i++)
				{			
					Vector3 asteroidSpawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), Random.Range(-spawnValues.y, spawnValues.y) , spawnValues.z);
					Quaternion spawnRotation = Quaternion.identity;
					
					GameObject asteroid = (GameObject)Instantiate(hazard, asteroidSpawnPosition, spawnRotation);
					Rigidbody asteroidRigidBody = asteroid.GetComponent<Rigidbody>();
					asteroidRigidBody.AddForce(-Vector3.forward * asteroidSpeed);
					
					yield return new WaitForSeconds (0);
				}

			}
			yield return new WaitForSeconds (waveWait);
		}
	}

	private void moveSpawner(){
		Vector3 playerPosition = player.transform.position;
		Vector3 spawnerPosition = new Vector3(transform.position.x, transform.position.y, playerPosition.z + asteroidToCharacterDistance);
		transform.position = spawnerPosition;
	}



}
