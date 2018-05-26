using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour {
	bool redirectActive;
	PlayerController parent;
	SpriteRenderer sprite; 


	void Start () {
		parent = GetComponentInParent<PlayerController>();
		sprite = GetComponent<SpriteRenderer>();
	}
		
	void Update () {

		//If the player is clicking (time controller) and the sprite isn't visible, make it visible
		if(parent.mouseDown && !sprite.enabled){
			transform.position = parent.transform.position;
			sprite.enabled = true;

		//if the player is clicking and the sprite is visible, update its position to be under the mouse
		} else if(parent.mouseDown && sprite.enabled){
			transform.position = parent.transform.position;
		
		//If player is no longer clicking the sprite is still enabled
		} else if(!parent.mouseDown && sprite.enabled){
			sprite.enabled = false;
		}
	}
}