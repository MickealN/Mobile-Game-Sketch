using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {



	//PlayerController player;
	[SerializeField] GameObject player;
	[SerializeField] float cameraMovementFactor = 0.1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//todo: If there is tiny distance betweent the camera and player, snap the camera directly onto the player
		//and don't do the following calculation
		//Cover [camera movement factor] the distance between x and y each frame
		transform.position += new Vector3(
			((player.transform.position.x - transform.position.x)*cameraMovementFactor), 
			((player.transform.position.y - transform.position.y)*cameraMovementFactor), 
			0
		);
	}
}
