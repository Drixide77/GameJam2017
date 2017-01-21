using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public GameObject player;
	private Player playerScript;
	// Use this for initialization
	void Start () {
		playerScript = player.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(transform.position.x) == Mathf.Abs(player.transform.position.x)){
			// playerScript.SetRespawn(transform.position);
		}
	}


}
