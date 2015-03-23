using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

	// Use this for initialization
	public GameObject hazard;//obstacle to be spawned, defined in inspector
	public Vector3 spawnValues;//location range for which the objects to spawn
	
	public int hazardCount;//number of obstacles per wave
	public float spawnWait;//how long to wait between spawning obstacles
	public float startWait;//how long until the objects start spawning
	public float waveWait;//how long it is between 'waves,' which is defined by spawning the number in hazardcount, then incrementing
	void Start () 
	{
		StartCoroutine  (Spawnwaves());//the IEnumurator is a coroutine that generates obstacles. not reliant on update()
	}
	
	// Update is called once per frame
	IEnumerator Spawnwaves()
	{ 
		
		yield return new WaitForSeconds (startWait);
		while(true)
		{	
			for(int i=0; i<hazardCount; i++)
			{			
				Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate(hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			
			yield return new WaitForSeconds (waveWait);
			
		}
	}




}
