using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveableObject : MonoBehaviour {
	
	public Color color;
	public float softSpeedCap;
	public float speedCapFactor;
	public float movementFactor = 10;
	public float blinkFrames = 5;
	public float lerpFactor = 0.1f;


	[HideInInspector] public Vector3 originPosition; 
	[HideInInspector] public int bashSequence = 0;
	[HideInInspector] public float bashFrame = 0;
	[HideInInspector] public float blinkFrame = 0;


	[HideInInspector] public Vector3 lastVelocity;


	[HideInInspector] public Vector3 dashPoint = Vector3.negativeInfinity;
	[HideInInspector] public Vector2 bashVector;
	[HideInInspector] public Vector3 enemyPosition;


	public Rigidbody2D objectsRigidbody;
	[HideInInspector] public bool killV = false;
	[HideInInspector] public bool mouseDown;
	[HideInInspector] public Vector2 clickedInWorld = Vector2.zero;

	public float velocityKillThreshold;
	public float velocitySlowdown = 0.8f;

	[HideInInspector] public Light auraLight;



	public void killVelocity(){

		if(Mathf.Abs(objectsRigidbody.velocity.x) <= velocityKillThreshold && Mathf.Abs(objectsRigidbody.velocity.y) <= velocityKillThreshold ){
			objectsRigidbody.velocity = Vector2.zero;
		} else {
			objectsRigidbody.velocity = new Vector2((objectsRigidbody.velocity.x)*velocitySlowdown, (objectsRigidbody.velocity.y)*velocitySlowdown);
		}
	}

	public void speedCap(){
		//objectsRigidbody.velocity = new Vector2()
		if(objectsRigidbody.velocity.magnitude > softSpeedCap){
			objectsRigidbody.velocity = new Vector2((objectsRigidbody.velocity.x)*speedCapFactor, (objectsRigidbody.velocity.y)*speedCapFactor);
		}
	}


	public void bash(){

		//Stage one is the blink to the enemy. Stage two is the dash away from them
		//TODO: Make stage two follow a log(x) curve
		switch(bashSequence){

		case 1:
			originPosition = objectsRigidbody.transform.position;
			if(++blinkFrame < blinkFrames){
				objectsRigidbody.transform.position = Vector3.Lerp(originPosition, enemyPosition, blinkFrame/blinkFrames);
			} else {
				objectsRigidbody.transform.position = enemyPosition;
				blinkFrame = 0;
				bashSequence = 2;
			}
			break;

		case 2:
			bashFrame += lerpFactor;



			objectsRigidbody.transform.position = Vector3.Lerp(enemyPosition, dashPoint, bashFrame);

			if(bashFrame >= 1){
				
				lastVelocity = Vector3.Lerp(enemyPosition, dashPoint, bashFrame-lerpFactor) - Vector3.Lerp(enemyPosition, dashPoint, bashFrame-(lerpFactor*2));

				//objectsRigidbody.AddForce(50 * bashVector * movementFactor);
				objectsRigidbody.AddForce(50 * new Vector2(lastVelocity.x, lastVelocity.y), ForceMode2D.Impulse);
				//print(objectsRigidbody.velocity.magnitude);
				bashSequence = 0;
			}
			break;
		case 3:
			//For a potential solo dash later. 


			break;

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
		

	public void setColor(Color c){
		color = c;
	}
	public void setLight(Color c){
		auraLight.color = c;
	}
	public Color getColor(){
		return color; //auraLight.color;
	}


}