using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraControl : MonoBehaviour {



	//PlayerController player;
	[SerializeField] private int cameraMode;
	public int CameraMode {
		get {
			return cameraMode;
		}
		set {
			if(value > 2){
				throw new ArgumentOutOfRangeException();
			}  else {
				cameraMode = value;
			}
		}
	}



	[SerializeField] GameObject player;
	[SerializeField] float cameraMovementFactor = 0.1f;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	// Update is called once per frame
	void Update () {

		switch(cameraMode) {

		case 0:
			//todo: If there is tiny distance betweent the camera and player, snap the camera directly onto the player
			//and don't do the following calculation
			//Cover [camera movement factor] the distance between x and y each frame
			transform.position += new Vector3(
				((player.transform.position.x - transform.position.x) * cameraMovementFactor), 
				((player.transform.position.y - transform.position.y) * cameraMovementFactor), 
				0
			);
			break;
		
		case 1:

			break;

		case 2:

			break;

		}
	}
}
