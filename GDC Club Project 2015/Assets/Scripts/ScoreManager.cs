using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {
	private float time = 0;
	private int score = 0;
	public UnityEngine.UI.Text scoreText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		score = (int)time;
		scoreText.text = "Score: " + score;
	}
}
