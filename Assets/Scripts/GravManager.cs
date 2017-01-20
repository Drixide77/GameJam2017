using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravManager : MonoBehaviour {

	[HideInInspector] 
	public bool InvertedGrav;
	public float TimeUntilGravWave;

	// Use this for initialization
	void Start () {
		InvertedGrav = false;
		TimeUntilGravWave = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
		TimeUntilGravWave -= Time.deltaTime;
		if (TimeUntilGravWave < 0.0f) {
			if (InvertedGrav)
				InvertedGrav = false;
			else
				InvertedGrav = true;
			TimeUntilGravWave = 5.0f;
			Debug.Log ("Gravity Inverted: " + InvertedGrav.ToString());
		}
	}

	void PlayGravWaveSound(){
		//Do stuff here
	}
}
