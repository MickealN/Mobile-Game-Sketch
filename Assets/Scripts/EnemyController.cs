﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Experimental.U2D;
using System.Runtime.ConstrainedExecution;

public class EnemyController : MoveableObject {

	public enum EnemyType{
		normal, 
		emitted, 
		smart
	}



	Vector3 angleVector;				//Where the player clicked in worldspace if the enemy was centered at 0
	float arrowAngle;					//The arrows angle in degrees
	bool bashLegallyStarted = false;	//So the player doesn't always bash on click release
	Vector3 dashPointVector;			//Point to dash too
	Vector2 velocityBackup;				//Back the players velocity up so it's magnitude can be restored afte the bash

	[SerializeField] SpriteRenderer arrow;
	[SerializeField] float timeToExist = 1;
	[SerializeField] EnemyType enemyType = EnemyType.normal;
	[SerializeField] GameObject playerReference;
	[SerializeField] float bashThreshhold = 5;
	[SerializeField] float dashRange = 2;





	void Start () {
		objectsRigidbody = gameObject.GetComponent<Rigidbody2D>();
		playerReference = GameObject.FindGameObjectWithTag("Player");
		auraLight = gameObject.GetComponentInChildren<Light>();
		auraLight.color = color;

		if(enemyType == EnemyType.emitted){
			//TODO: dont' self destroy if player is clicking
			Destroy(gameObject, timeToExist);
		}
	}



	void Update () {

		if(mouseDown){
			clickedInWorld.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
			clickedInWorld.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
		}
			
		if(killV){
			killVelocity();
		}

		if(arrow.enabled){
			rotateArrow();
		}

		if(bashSequence != 0){
			bash();
		}

	}




	private void OnMouseDown(){
		if(Vector2.Distance(gameObject.transform.position, playerReference.transform.position) < bashThreshhold){
			startBash();
		}
	}


	private void OnMouseUp(){
		mouseDown = false;
		if(bashLegallyStarted){
			completeBash();
		}
	}

	//No touching it works
	void rotateArrow(){
		angleVector = new Vector3(clickedInWorld.x, clickedInWorld.y, 0) - gameObject.transform.position;
		arrowAngle = (float) 90f + Mathf.Rad2Deg * Mathf.Atan2(angleVector.y, angleVector.x);
		Quaternion q = Quaternion.identity;
		q.eulerAngles = new Vector3(0, 0, arrowAngle);
		arrow.transform.rotation = q;
	}


	/*
	 * Check Colors, if invalid combination, cancel the bash with a differnt codeblock
	 * Flag the bash as legal (so mouse up doesn't call second half)
	 * Take a backup of the velocity to the magnitude can be restored on redirect
	 * Kill the players velocity
	 * Kill the enemies velocity
	 * Set mousedown to true so the arrow updates its rotation in realtime
	 * Disable collisions so the player can bash through without issue
	 */
	private void startBash(){
		if(playerReference.GetComponent<PlayerController>().getColor() != getColor()){
			//Do extra cannot bash animation stuffs here
			return;
		}
			
		bashLegallyStarted = true;
		velocityBackup = objectsRigidbody.velocity;
		playerReference.SendMessage("killVelocityHack");
		killV = true;
		mouseDown = true;
		arrow.enabled = true;
		Physics2D.IgnoreCollision(playerReference.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), true);
	}

	/*
	 * Reset bash flag
	 * Disable the arrow
	 * Allow the objects to move (shut off killV)
	 * Normallize the directions magnitude to be fractions of 1
	 */ 

	private void completeBash(){
		bashLegallyStarted = false;
		arrow.enabled = false;
		killV = false;

		//Direction is normallized so the x and y components sum to 1
		Vector2 direction = new Vector2(angleVector.x / (Math.Abs(angleVector.x) + Math.Abs(angleVector.y)), angleVector.y / (Math.Abs(angleVector.x) + Math.Abs(angleVector.y)));

		dashPointVector = getPointOnCircle(clickedInWorld.x, clickedInWorld.y, dashRange);
		//Factor is the enemies position to the dashpoint
		dashPoint = dashPointVector + transform.position;

		//This is an ugly fix, but since sendmessage doesn't support multiple parameters, two methods is easier than building an object for data transfer
		playerReference.SendMessage("sharePosition", transform.position);
		playerReference.SendMessage("shareDashPoint", dashPoint);
		playerReference.SendMessage("completeBash", direction);


		/*
		 * This block of code is to give the enemy a bash in the opposite direction
		 * Upon testing, it felt too agressive
		 * Consider retoring the enemy's velocity instead. 
		bashFrame = 0f;
		bashVector = -direction;

		//Set enemyposition to the same position so the blink doesn't move it
		//Set the dashpoint to negative so it flies off in the opposite direction
		enemyPosition = transform.position;
		dashPoint = -dashPointVector + transform.position;
		bashSequence = true;
		*/


		//TODO: Give the bash a little bit of knockback if not a projectile/enemy is not in motion
		//If giving back the bash, remove this addforce.
		objectsRigidbody.velocity = -direction * velocityBackup.magnitude;

		

		//TODO: Verify this is okay later on
		Invoke("reenableCollision", 1);
	}
		

	//Note that if collisions are renabled while they are on top of each other, this can cause serious movement bugs
	private void reenableCollision(){
		Physics2D.IgnoreCollision(playerReference.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), false);
	}


	//Used by the emitter to setup the enemy
	public void setEnemyProperties(EnemyType t, float time){
		enemyType = t;
		timeToExist = time;
	}

}
