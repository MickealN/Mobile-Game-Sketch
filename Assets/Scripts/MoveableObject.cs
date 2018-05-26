using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour {

	public void printSomething(){
		print("test");

	}

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



}
