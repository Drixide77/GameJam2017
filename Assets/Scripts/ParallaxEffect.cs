using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour {

	public Transform[] backgrounds;
	private float[] parallaxScales;

	public float smoothing;

	private Transform camRef;
	private Vector3 camLastPos;
	// Use this for initialization
	void Start () {
		camRef = Camera.main.transform;
		smoothing = 1.0f;
		camLastPos = camRef.position;
		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; ++i) {
			parallaxScales [i] = backgrounds [i].position.z*1.0f*0.3f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < backgrounds.Length; ++i) {
			float parallax = (camLastPos.x - camRef.position.x) * parallaxScales [i];
			float backgroundTargetX = backgrounds[i].position.x + parallax;
			Vector3 newBackgroundPos = new Vector3 (backgroundTargetX, backgrounds[i].position.y, backgrounds[i].position.z);
			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, newBackgroundPos, smoothing*Time.deltaTime);
		}
		camLastPos = camRef.position;
	}
}
