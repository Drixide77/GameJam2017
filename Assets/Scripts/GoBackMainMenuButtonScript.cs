using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackMainMenuButtonScript : MonoBehaviour {

	AudioSource audiosource;
	public AudioClip press;

	public void Click(){
		audiosource.PlayOneShot (press);
		SceneManager.LoadScene ("MainMenu");
	}

	void Start(){
		audiosource = GetComponent<AudioSource> ();
	}

	void Update(){
		if (Input.GetButtonDown("Jump")){
			Click ();
		}
	}
}
