using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerController : MoveableObject {




	[SerializeField] float movementFactor = 10;

    // Use this for initialization
    void Start () {
		objectsRigidbody =  GetComponent<Rigidbody2D>();
    }

	void Update () {

		if(killV){
			killVelocity();
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
		//clickedWorldX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
		//clickedWorldY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
		Vector3 movementVector = getPointOnCircle(clickedInWorld.x, clickedInWorld.y, 1f); //In theory, presscontroller has updated these values accordingly
		//Why 50? Because 50 is a magic number that brings it up to a velocity of 1. 
		objectsRigidbody.AddForce(-50 * movementVector * movementFactor);
	}

	private void completeBash(Vector2 direction){
		killV = false;
		objectsRigidbody.AddForce(50 * direction * movementFactor);

		/*
		 * Idea. Move player to enemyies location then past it in an incredibly aggressive lerp. 
		 * Will give the bash a snappy feeling. 
		Vector2 direction = ob[0];
		Transform enemyPos = ob[1];
		objectsRigidbody.transform.position = enemyPos.transform.position;
		*/

	}
		
	//This function is full of fucky math. It find the intersection of a line through the origin to a point, and a circle around the origin. 
	public Vector3 getPointOnCircle(float pressX, float pressY, float circleSize){

		float rise = pressY - transform.position.y;
		float run = pressX - transform.position.x;

		float m = Math.Abs(rise / run);

		float xOnC;
		float yOnC;


		xOnC = (float) Math.Sqrt((1*circleSize) / (m*m + 1));
		yOnC = (float) Math.Sqrt((1*circleSize) - (xOnC*xOnC));

		if(rise < 0){
			yOnC = yOnC * -1;
		}
		if(run < 0){
			xOnC = xOnC*-1;
		}

		return new Vector3(xOnC, yOnC, 0);
	}
		
}
