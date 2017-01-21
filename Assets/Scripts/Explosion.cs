using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	float ttl = 0.25f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ttl -= Time.deltaTime;
		if (ttl < 0.0f)
			Destroy (gameObject);
	}
}
