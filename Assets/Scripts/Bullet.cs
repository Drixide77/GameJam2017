using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


	public float bulletVelocity;
	private BoxCollider2D bulletCollider;
	private int bulletDirection;
	private float bulletTimeToLive;
	// Use this for initialization
	void Start () {
		bulletCollider = GetComponent<BoxCollider2D>();
		bulletTimeToLive = 3.0f;
	}

	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector3 (bulletVelocity*bulletDirection*Time.deltaTime, 0.0f,0.0f));
		bulletTimeToLive -= Time.deltaTime;
		if (bulletTimeToLive < 0.0f) {
			DestroyObject (gameObject);
		}

	}

	public void SetDirection(int dir){
		bulletDirection = dir;
	}


}
