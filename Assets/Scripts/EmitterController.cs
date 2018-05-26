using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitterController : MonoBehaviour {

	[SerializeField] EnemyController enemy;
	[SerializeField] float timeToExist = 0;
	[Tooltip("Multiplied by magic number 50 because it's addForce")][SerializeField] Vector2 emitterDirection = new Vector2(0, 0);


	void Update () {
		//Just use space to fire an enemy for now
		if(Input.GetKeyDown(KeyCode.Space)) {
			emit();
		}
	}

	private void emit(){
		EnemyController tempEnemy = Instantiate(enemy, this.transform.position, Quaternion.identity);
		tempEnemy.GetComponent<Rigidbody2D>().AddForce(emitterDirection*50);
		tempEnemy.setEnemyProperties(EnemyController.EnemyType.emitted, timeToExist);
	}
}
