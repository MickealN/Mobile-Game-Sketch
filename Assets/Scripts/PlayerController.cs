using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Experimental.UIElements.StyleEnums;
using UnityEditor.Graphs;


public class PlayerController : MoveableObject {


    // Use this for initialization
    void Start () {
		objectsRigidbody = GetComponent<Rigidbody2D>();
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
        mouseDown = true;
		killV = true;
    }

    private void OnMouseUp() {
        mouseDown = false;
		killV = false;
		calculateNewVelocity();
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
		

		
}
