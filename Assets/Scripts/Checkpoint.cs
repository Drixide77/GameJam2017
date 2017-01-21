using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public GameObject player;
	public Vector3 offset = new Vector3 (0, 0, 0);
	private Player playerScript;
	bool activated = false;
	public Sprite active;
	SpriteRenderer sr;
	AudioSource ads;
	// Use this for initialization
	void Start () {
		ads = GetComponent<AudioSource> ();
		sr = GetComponent<SpriteRenderer> ();
		playerScript = player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!activated) {
			if (Mathf.Abs (transform.position.x) <= Mathf.Abs (player.transform.position.x)) {
				print ("Checkpoint!");
				playerScript.SetRespawn (transform.position + offset);
				activated = true;
				sr.sprite = active;
				ads.Play ();
			}
		}
	}


}
