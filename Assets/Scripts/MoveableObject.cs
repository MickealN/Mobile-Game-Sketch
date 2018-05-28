using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveableObject : MonoBehaviour {

	public void printSomething(){
		print("test");

	}


	public float movementFactor = 10;

	private Vector3 originPosition; 

	public float lerpFactor = 0.1f;
	public bool bashSequence = false;
	public float bashFrame = 0;


	public Vector3 dashPoint = Vector3.negativeInfinity;
	public Vector2 bashVector;
	public Vector3 enemyPosition;


	public Rigidbody2D objectsRigidbody;
	public bool killV = false;
	public bool mouseDown;
	public Vector2 clickedInWorld = Vector2.zero;

	public float velocityKillThreshold;
	public float velocitySlowdown;



	public void killVelocity(){

		if(Mathf.Abs(objectsRigidbody.velocity.x) <= velocityKillThreshold && Mathf.Abs(objectsRigidbody.velocity.y) <= velocityKillThreshold ){
			objectsRigidbody.velocity = Vector2.zero;
		} else {
			objectsRigidbody.velocity = new Vector2((-1*objectsRigidbody.velocity.x)*velocitySlowdown, (-1*objectsRigidbody.velocity.y)*velocitySlowdown);
		}
	}

	public void bash(){
		//TODO: Add maybe one or two intermediary frames of the player blinking to the enemies position
		//It might be a bit too agressive right now. 
		if(bashFrame == 0f){
			originPosition = enemyPosition;
			objectsRigidbody.transform.position = enemyPosition;
		}


		bashFrame += lerpFactor;
		//TODO: Consider a different kind of interpolation for the dash so it more smoothly blends into the follow up movement
		//objectsRigidbody.transform.position = Vector3.Lerp(objectsRigidbody.transform.position, dashPoint, bashFrame);
		objectsRigidbody.transform.position = Vector3.Lerp(originPosition, dashPoint, bashFrame);

		if(bashFrame >= 1) {
			objectsRigidbody.AddForce(50 * bashVector * movementFactor);
			bashSequence = false;
		}	
	}




	//This function is full of fucky math. It find the intersection of a line through the origin to a point, and a circle around the origin. 
	public Vector3 getPointOnCircle(float pressX, float pressY, float circleSize){

		float rise = pressY - transform.position.y;
		float run = pressX - transform.position.x;

		float m = Math.Abs(rise / run);

		float xOnC = (float) Math.Sqrt((1*circleSize) / (m*m + 1));
		float yOnC = (float) Math.Sqrt((1*circleSize) - (xOnC*xOnC));

		if(rise < 0){
			yOnC = yOnC * -1;
		}
		if(run < 0){
			xOnC = xOnC*-1;
		}

		return new Vector3(xOnC, yOnC, 0);
	}
}