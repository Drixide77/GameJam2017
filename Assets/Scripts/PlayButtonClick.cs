﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonClick : MonoBehaviour {

	AudioSource audiosource;
	public AudioClip press;
	public AudioClip select;
	private bool PlaySelected;
	private bool GoneUp;
	private bool GoneDown;


	public void Click(){
		audiosource.PlayOneShot (press);
		SceneManager.LoadScene ("scene0");
	}

	void Start(){
		audiosource = GetComponent<AudioSource> ();
		PlaySelected = true;
		GoneUp = false;
		GoneDown = false;
	}

	void Update(){
		if ((Input.GetAxisRaw ("Vertical") >= 0.25) && !GoneUp) {
			audiosource.PlayOneShot (select);
			PlaySelected = true;
			GoneUp = true;
			if (GoneDown) GoneDown =false;
		}
		else if((Input.GetAxisRaw ("Vertical") < -0.25)&& !GoneDown) {
			audiosource.PlayOneShot (select);
			PlaySelected = false;
			GoneDown = true;
			if (GoneUp) GoneUp =false;
		}
		
		if (Input.GetButtonDown("Jump") && PlaySelected){
			print ("Entered here");
			Click ();
		}
	}
}
