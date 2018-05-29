using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : LiterallyJustAnEnum {



	private SpriteRenderer sprite;
	private LevelController levelController;
	[SerializeField] private ObjectType checkpointType;// = CheckpointType.Default;



	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
		levelController = FindObjectOfType<LevelController>();

		//I really should probably just use colors, but I might extend them to not solid colors eventually, so I want the framework
		switch(checkpointType){
			case ObjectType.Blue:
				sprite.color = Color.blue;
				break;
			case ObjectType.Green:
				sprite.color = Color.green;
				break;
			case ObjectType.Red:
				sprite.color = Color.red;
				break;
		}
	}

	void OnTriggerEnter2D(Collider2D c){
		if(c.gameObject.tag != "Player"){
			return;
		}
		levelController.SendMessage("OnCheckpoint", checkpointType);
	}


}
