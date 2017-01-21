using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {


	public Button playButton;
	public Button exitButton;
	public Button selectedButton;

	bool firstIsSelected;
	// Use this for initialization
	void Start () {
		selectedButton = playButton;
		firstIsSelected = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Vertical"))
	}
}
