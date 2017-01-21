using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravManager : MonoBehaviour {

	[HideInInspector] 
	public float TimeUntilGravWave;
	public GameObject player;
	private Player playerScript;
	// Use this for initialization
	void Start () {
		TimeUntilGravWave = 5.0f;
		playerScript = player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		TimeUntilGravWave -= Time.deltaTime;
		if (TimeUntilGravWave < 0.0f) {
			playerScript.InvertGravity();
			TimeUntilGravWave = 5.0f;
		}
	}

	void PlayGravWaveSound(){
		//Do stuff here
	}
}
