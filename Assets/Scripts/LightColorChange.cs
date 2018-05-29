using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.VersionControl;

public class LightColorChange : MonoBehaviour {

	/*
	 * Passing through an object with this script will send the object the color it should turn to. 
	*/


	[SerializeField] Color color;



	void Start(){
		//This is where I will find any childed gameobjects with lights on them, enable those lights, and set their color
		SpriteRenderer[] sr = GetComponentsInChildren<SpriteRenderer>();
		foreach(SpriteRenderer s in sr){
			if(s.name == "ChangePoint"){
				s.color = color;
			}
		}


	}


	void OnTriggerEnter2D(Collider2D c){
		if(c.gameObject.tag == "Player"){
			c.SendMessage("setColor", color);
		}
	}
}
