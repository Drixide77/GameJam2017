using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Mathf.Abs (transform.position.x) <= Mathf.Abs (player.transform.position.x)) {
			//Win the game!
			SceneManager.LoadScene ("EndCreditsMenu");
		}
	}
}
