using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Experimental.UIElements.StyleEnums;
using UnityEditor.Graphs;


public class PlayerController : MoveableObject {

	[SerializeField] SpriteRenderer playerColor;

	[SerializeField] private int dashCharges = 0;
	[SerializeField] private int dashPower = 2;

    // Use this for initialization
    void Start () {
		objectsRigidbody = GetComponent<Rigidbody2D>();
		auraLight = GetComponentInChildren<Light>();
	}




	void Update () {

		speedCap();
		if(killV){
			killVelocity();
		}

		if(bashSequence != 0){
			bash();
		}
	}


    private void OnMouseDown() {
		if(dashCharges > 0){
			mouseDown = true;
			killV = true;
		}
        
    }

    private void OnMouseUp() {
		if(dashCharges > 0){
			dashSetup();

		}
    }
		
	private void dashSetup(){
		bashFrame = 0;			//Setup the bash startpoint

		//This is a bad line of code. It establishes an "enemy" position to use
		//even through there is no enemy. Its done to repurpose bash phase 2
		enemyPosition = objectsRigidbody.transform.position;

		//Set the point to dash to by getting an area in a circle around the player at a fixed distance
		//Then factor in the location by adding the transform
		dashPoint = -getPointOnCircle(clickedInWorld.x, clickedInWorld.y, dashPower) + transform.position;
		bashSequence = 2;		//Kick off the second half of the bash sequence
		mouseDown = false;
		killV = false;
		dashCharges--;
	}

	private void killVelocityHack(){
		killV = true;
	}

	private void calculateNewVelocity(){
		Vector3 movementVector = getPointOnCircle(clickedInWorld.x, clickedInWorld.y, 1f); //In theory, presscontroller has updated these values accordingly
		//Why 50? Because 50 is a magic number that brings it up to a velocity of 1. 
		objectsRigidbody.AddForce(-50 * movementVector * movementFactor);
	}

	private void sharePosition(Vector3 pos){
		enemyPosition = pos;
	}
	private void shareDashPoint(Vector3 pos){
		dashPoint = pos; 
	}


	private void completeBash(Vector2 direction){
		bashVector = direction;
		killV = false;
		bashFrame = 0f;
		bashSequence = 1;
	}

	public new void setColor(Color c){
		playerColor.color = c;
		base.setColor(c);
	}
		

	private void addDashCharges(int charges){
		dashCharges += charges;
	}

	private void addCheckpoint(){
		
	}


		
}
