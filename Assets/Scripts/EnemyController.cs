using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Experimental.U2D;

public class EnemyController : MoveableObject {

	public enum EnemyType{
		normal, 
		emitted, 
		smart
	}


	[SerializeField] SpriteRenderer arrow;
	Vector3 angleVector;
	float arrowAngle;
	[SerializeField] float timeToExist = 1;
	[SerializeField] EnemyType enemyType = EnemyType.normal;
	[SerializeField] GameObject playerReference;
	[SerializeField] float bashThreshhold = 5;


	void Start () {
		objectsRigidbody = gameObject.GetComponent<Rigidbody2D>();
		playerReference = GameObject.FindGameObjectWithTag("Player");
		if(enemyType == EnemyType.emitted){
			//todo: dont' self destroy if player is clicking
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
			angleVector = new Vector3(clickedInWorld.x, clickedInWorld.y, 0) - gameObject.transform.position;
			arrowAngle = (float) 90f + Mathf.Rad2Deg*Mathf.Atan2(angleVector.y, angleVector.x);
			Quaternion q = Quaternion.identity;
			q.eulerAngles = new Vector3(0, 0, arrowAngle);
			arrow.transform.rotation = q;
		}
	}


	private void OnMouseDown(){
		print("clicked enemy");
		if(Vector2.Distance(gameObject.transform.position, playerReference.transform.position) < bashThreshhold){
			startBash();
		}
	}


	private void OnMouseUp(){
		arrow.enabled = false;
		mouseDown = false;
		killV = false;
		completeBash();
	}


	private void startBash(){
		//Stop the players motion
		//Stop the projectiles motion
		//Draw Arrow dynamically over the mouse
			//Set Mousedown to true
			//Create Arrow object childed to enemy
		playerReference.SendMessage("killVelocityHack");
		killV = true;
		mouseDown = true;
		arrow.enabled = true;
		Physics2D.IgnoreCollision(playerReference.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), true);
	}

	private void completeBash(){
		//Direction is normallized so the x and y components sum to 1
		//TODO: Bug. Sometimes when the two objects glance each other at a 45 degree angle,
		//The object goes flying off way faster than intended. Investigate later. 
		Vector2 direction = new Vector2(angleVector.x/Math.Abs(angleVector.x + angleVector.y), angleVector.y/Math.Abs(angleVector.x + angleVector.y));
		objectsRigidbody.AddForce(direction*-50);
		playerReference.SendMessage("completeBash", direction);
		Invoke("reenableCollision", 3);
	}

	//Note that if collisions are renabled while they are on top of each other, this can cause serious movement bugs
	private void reenableCollision(){
		Physics2D.IgnoreCollision(playerReference.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), false);
	}

	public void setEnemyProperties(EnemyType t, float time){
		enemyType = t;
		timeToExist = time;
	}
}
