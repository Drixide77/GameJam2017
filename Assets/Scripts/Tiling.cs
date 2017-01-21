using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offset;

	bool hasLeftAuxiliar;
	bool hasRightAuxiliar;

	public float spriteWidth;
	public Camera cam;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		offset = 2;
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		spriteWidth = sr.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasLeftAuxiliar || !hasRightAuxiliar) {
			float camExtension = cam.orthographicSize * (Screen.width / Screen.height);
			float LeftVisibleZone = transform.position.x + spriteWidth / 2.0f - camExtension;
			float RightVisibleZone = transform.position.x - spriteWidth / 2.0f - camExtension;
			if (transform.position.x >= RightVisibleZone && !hasRightAuxiliar) {
			
			}
			if (transform.position.x <= LeftVisibleZone && !hasRightAuxiliar) {

			}

		}
	}

	void InstantiateNewSprite(int LeftOrRight){
		Vector3 newPosition;
	}
}
