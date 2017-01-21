using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonClick : MonoBehaviour {

	AudioSource audiosource;
	public AudioClip press;
	private bool PlaySelected;
	private bool GoneUp;
	private bool GoneDown;

	public void Click(){
		audiosource.PlayOneShot (press);
		Application.Quit ();
	}

	void Start(){
		audiosource = GetComponent<AudioSource> ();
		PlaySelected = false;
	}

	void Update(){
		if ((Input.GetAxisRaw ("Vertical") >= 0.25) && !GoneUp) {
			PlaySelected = true;
			GoneUp = true;
			if (GoneDown) GoneDown =false;
		}
		else if((Input.GetAxisRaw ("Vertical") < -0.25)&& !GoneDown) {
			PlaySelected = false;
			GoneDown = true;
			if (GoneUp) GoneUp =false;
		}
		if (Input.GetButtonDown("Jump") && !PlaySelected){
			Click ();
		}
	}
}
