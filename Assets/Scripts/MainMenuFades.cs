using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuFades : MonoBehaviour {

	public float fadespeed = 0.2f;
	public int drawDepth = -1000;
	public Texture Black2DTexture;
	private float alpha = 0.0f;
	private int fadeDir = -1;
	public GameObject button;
	private PlayButtonClick bs;

	private bool spriteFadeDone;
	private int order;
	// Use this for initialization
	void Start () {
		order = 0;
		bs = button.GetComponent<PlayButtonClick> ();

	}



	// Update is called once per frame
	void OnGUI () {
		if (bs.playPressed && order < 1) {
			++order;
			//print ("button pressed");
			/*GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),Sprite2DTexture);
			GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),Black2DTexture);*/
		}
		if (order >= 1) {
			alpha -= fadeDir * fadespeed * Time.deltaTime;
			alpha = Mathf.Clamp01 (alpha);
			GUI.color = new Color(GUI.color.r, GUI.color.g,GUI.color.b,alpha);
			GUI.depth = drawDepth;
			GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),Black2DTexture);
			if (alpha > 0.99f) {
				++order;
				alpha = 1.0f;
				//GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),Black2DTexture);
				SceneManager.LoadScene ("Presentation");
			}
		}
		/*if (order == 2) {
			alpha += fadeDir * fadespeed * Time.deltaTime;
			alpha = Mathf.Clamp01 (alpha);
			GUI.color = new Color(GUI.color.r, GUI.color.g,GUI.color.b,alpha);
			GUI.depth = drawDepth;
			GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),Black2DTexture);
			if (alpha < 0.1f) {
				//
			}
		}*/
	}
}
