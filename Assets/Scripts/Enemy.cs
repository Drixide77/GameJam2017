using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Sprite idle0;
	public Sprite idle1;

	public GameObject explosion;

	float animCD = 0.25f;
	private SpriteRenderer sr;
	int frame = 0;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		animCD -= Time.deltaTime;
		if (animCD < 0.0f) {
			animCD = 0.25f;
			frame = (frame + 1) % 2;
			if (frame == 0)
				sr.sprite = idle0;
			else sr.sprite = idle1;
		}
	}

	public void getDestroyed(){
		print("Enemy got destroyed");
		Instantiate (explosion, transform.position, Quaternion.identity);
		Destroy(gameObject);
		print("Destroyed");
	}
}
