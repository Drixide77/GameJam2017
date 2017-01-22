using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeToBlackEffect : MonoBehaviour {

	public Texture Black2DTexture;
	public float fadespeed = 0.2f;
	public int drawDepth = -1000;

	private float alpha = 1.0f;
	private int fadeDir = -1;

	public void OnGUI(){
		alpha += fadeDir * fadespeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color(GUI.color.r, GUI.color.g,GUI.color.b,alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),Black2DTexture);
	}

	/*/ Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/
}
