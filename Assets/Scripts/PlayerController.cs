using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerController : MonoBehaviour {


	Rigidbody2D playerRigidbody;

	[SerializeField] float movementFactor = 10;

	[SerializeField] float velocityKillThreshold = 0.01f;
	[SerializeField] float velocitySlowdown = 0.1f;

	private bool killV = false;
	public bool mouseDown;

    // Use this for initialization
    void Start () {
        playerRigidbody = GetComponent<Rigidbody2D>();
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

	private void killVelocity(){

		if(Mathf.Abs(playerRigidbody.velocity.x) <= velocityKillThreshold && Mathf.Abs(playerRigidbody.velocity.y) <= velocityKillThreshold ){
			playerRigidbody.velocity = Vector2.zero;
		} else {
			playerRigidbody.velocity = new Vector2((-1*playerRigidbody.velocity.x)*velocitySlowdown, (-1*playerRigidbody.velocity.y)*velocitySlowdown);
		}
	}

	private void calculateNewVelocity(){
		float clickedWorldX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
		float clickedWorldY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
		Vector3 movementVector = getPointOnCircle(clickedWorldX, clickedWorldY);
		//Why 50? Because 50 is a magic number that brings it up to a velocity of 1. 
		playerRigidbody.AddForce(-50 * movementVector * movementFactor);
	}
		
	//Todo: Identical Function. Refactor
	//This function is full of fucky math. It find the intersection of a line through the origin to a point, and a circle around the origin. 
	private Vector3 getPointOnCircle(float pressX, float pressY){

		float rise = pressY - transform.position.y;
		float run = pressX - transform.position.x;

		float m = Math.Abs(rise / run);

		float xOnC;
		float yOnC;


		xOnC = (float) Math.Sqrt(1 / (m*m + 1));
		yOnC = (float) Math.Sqrt(1 - (xOnC*xOnC));

		if(rise < 0){
			yOnC = yOnC * -1;
		}
		if(run < 0){
			xOnC = xOnC*-1;
		}

		return new Vector3(xOnC, yOnC, 0);
	}





}
