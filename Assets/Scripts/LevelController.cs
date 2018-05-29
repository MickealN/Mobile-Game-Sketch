using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : LiterallyJustAnEnum {

	[SerializeField] private int totalCheckpoints = 3;


	public bool[] checkpointTracker;

	void Start () {
		checkpointTracker = new bool[totalCheckpoints]; 
	}
		
	//A simple table
	private void OnCheckpoint(ObjectType checkpointType){
		switch(checkpointType){
		case ObjectType.Blue:
			checkpointTracker[0] = true;
			break;

		case ObjectType.Green:
			checkpointTracker[1] = true;
			break;

		case ObjectType.Red:
			checkpointTracker[2] = true;
			break;
		}
		checkIfDone();
	}

	private void checkIfDone(){
		bool done = true;
		foreach(bool b in checkpointTracker){
			if(b != true){
				done = false;
			}
		}

		if(done == true){
			print("Done!");
		} else {
			print("Not done yet!");
		}

	}

}
