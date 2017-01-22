using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offset;

	bool hasLeftAuxiliar;
	bool hasRightAuxiliar;

	public bool reverseScale;

	public float spriteWidth;
	public Camera cam;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		reverseScale = false;
		offset = 2;
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();
		spriteWidth = sr.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasLeftAuxiliar || !hasRightAuxiliar) {
			float camExtension = cam.orthographicSize * (Screen.width / Screen.height);
			float LeftVisibleZone = transform.position.x - spriteWidth / 2.0f - camExtension;
			float RightVisibleZone = transform.position.x + spriteWidth / 2.0f - camExtension;
			if (transform.position.x >= RightVisibleZone - offset && !hasRightAuxiliar) {
				InstantiateNewSprite (1);
				hasRightAuxiliar = true;
			}
			if (transform.position.x <= LeftVisibleZone + offset && !hasLeftAuxiliar) {
				InstantiateNewSprite (-1);
				hasLeftAuxiliar = true;
			}

		}
	}

	void InstantiateNewSprite(int LeftOrRight){
		Vector3 newPosition= new Vector3(transform.position.x + spriteWidth * LeftOrRight,transform.position.y,transform.position.z);
		Transform newSprite = (Transform)Instantiate (transform, newPosition,Quaternion.identity);
		if (reverseScale) {
			newSprite.localScale = new Vector3 (newSprite.localScale.x*-1,newSprite.localScale.y,newSprite.localScale.z);
		}
		newSprite.parent = transform;
		if (LeftOrRight < 0) {
			newSprite.GetComponent<Tiling> ().hasLeftAuxiliar = true;
		} else {
			newSprite.GetComponent<Tiling> ().hasRightAuxiliar = true;		
		}
	}
}
