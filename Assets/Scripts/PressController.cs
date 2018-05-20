using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PressController : MonoBehaviour {
	bool redirectActive;
	PlayerController parent;
	SpriteRenderer sprite; 
	Vector2 clickedInWorld = Vector2.zero;
	[SerializeField] float circleSize = 1;

	// Use this for initialization
	void Start () {
		parent = GetComponentInParent<PlayerController>();
		sprite = GetComponent<SpriteRenderer>(); //Will float in a circle around where the player is pressing
		
	}
	

	void Update () {



		if(parent.mouseDown){
			clickedInWorld.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
			clickedInWorld.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
		}

		//If the player is clicking (time controller) and the sprite isn't visible, make it visible
		if(parent.mouseDown && !sprite.enabled){
			transform.localPosition = getPointOnCircle(clickedInWorld.x, clickedInWorld.y);
			sprite.enabled = true;

		//if the player is clicking and the sprite is visible, update its position to be under the mouse
		} else if(parent.mouseDown && sprite.enabled){
			transform.localPosition = getPointOnCircle(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

		//If player is no longer clicking the sprite is still enabled
		} else if(!parent.mouseDown && sprite.enabled){
			sprite.enabled = false;
		}
	}

	//Todo: Refactor these methods to properly inherit functions 
	private Vector3 getPointOnCircle(float pressX, float pressY){

		float rise = pressY - parent.transform.position.y;
		float run = pressX - parent.transform.position.x;

		float m = Math.Abs(rise / run);

		float xOnC;
		float yOnC;


		xOnC = (float) Math.Sqrt(circleSize / (m*m + 1));
		yOnC = (float) Math.Sqrt(circleSize - (xOnC*xOnC));

		if(rise < 0){
			yOnC = yOnC * -1;
		}


		if(run < 0){
			xOnC = xOnC*-1;
		}

		return new Vector3(xOnC, yOnC, 0);
	}


}
