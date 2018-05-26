using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PressController : MonoBehaviour {
	bool redirectActive;
	PlayerController parent;
	SpriteRenderer sprite; 
	[SerializeField] float circleSize = 1;

	// Use this for initialization
	void Start () {
		parent = GetComponentInParent<PlayerController>();
		sprite = GetComponent<SpriteRenderer>(); //Will float in a circle around where the player is pressing
	}
	

	void Update () {

		if(parent.mouseDown){
			parent.clickedInWorld.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
			parent.clickedInWorld.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
		}

		//If the player is clicking (time controller) and the sprite isn't visible, make it visible
		if(parent.mouseDown && !sprite.enabled){
			transform.localPosition = parent.getPointOnCircle(parent.clickedInWorld.x, parent.clickedInWorld.y, circleSize);
			sprite.enabled = true;

		//if the player is clicking and the sprite is visible, update its position to be under the mouse
		} else if(parent.mouseDown && sprite.enabled){
			transform.localPosition = parent.getPointOnCircle(parent.clickedInWorld.x, parent.clickedInWorld.y, circleSize);

		//If player is no longer clicking the sprite is still enabled
		} else if(!parent.mouseDown && sprite.enabled){
			sprite.enabled = false;
		}
	}
}
