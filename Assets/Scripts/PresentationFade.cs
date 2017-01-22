using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PresentationFade : MonoBehaviour {

	public float fadespeed = 0.2f;
	public int drawDepth = -1000;
	public Texture Black2DTexture;
	//public Texture Sprite2DTexture;
	private float alpha = 1.0f;
	private int fadeDir = -1;
	//public GameObject button;
	//private PlayButtonClick bs;
	private bool Jump;
	private bool firstFadeDone;

	private int order;
	// Use this for initialization
	void Start () {
		order = 1;
		Jump = false;
		firstFadeDone = false;
		//bs = button.GetComponent<PlayButtonClick> ();

	}



	// Update is called once per frame
	void OnGUI () {
		/*if (bs.playPressed && order < 1) {
			++order;
			//print ("button pressed");
			/*GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),Sprite2DTexture);
			GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),Black2DTexture);*/
		//}*/
		if (Input.GetButtonDown ("Jump")) {
			print ("Pulsado");
			Jump = true;
		}
		if (Jump && firstFadeDone){
			alpha -= fadeDir * fadespeed * Time.deltaTime;
			alpha = Mathf.Clamp01 (alpha);
			GUI.color = new Color(GUI.color.r, GUI.color.g,GUI.color.b,alpha);
			GUI.depth = drawDepth;
			GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),Black2DTexture);
			if (alpha > 0.99f) {
				++order;
				//alpha = 0.0f;
				SceneManager.LoadScene ("scene0");
			}
		}
		if (order == 1) {
			alpha += fadeDir * fadespeed * Time.deltaTime;
			alpha = Mathf.Clamp01 (alpha);
			GUI.color = new Color(GUI.color.r, GUI.color.g,GUI.color.b,alpha);
			GUI.depth = drawDepth;
			GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),Black2DTexture);
			if (alpha < 0.01f) {
				++order;
				alpha = 0.0f;
				firstFadeDone = true;
			}
		}
	}
}
