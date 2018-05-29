using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Graphs;
using System;

public class AddDashCharge : LiterallyJustAnEnum {

	[SerializeField] ObjectType type;
	private enum ChargeType{
		Single,
		Recharging
	}

	[SerializeField] private int charges = 1; 
	[SerializeField] private ChargeType chargeType = ChargeType.Single; 


	void OnTriggerEnter2D(Collider2D c){
		if(c.gameObject.tag != "Player"){
			return;
		}

		switch(chargeType){
		case ChargeType.Single:

			c.SendMessage("addDashCharges", charges);
			Destroy(gameObject);
			break;

		case ChargeType.Recharging:
			break;
		}

	}
}
