using UnityEngine;
using System.Collections;

public class lockerAttack : MonoBehaviour {

	Animator enemyAnimator;

	//attacking
	void Start()
	{
		enemyAnimator = GetComponentInChildren<Animator>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player"){
			enemyAnimator.SetBool("isAttacking", true);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.tag == "Player"){
			enemyAnimator.SetBool("isAttacking", false);
		}
	}
}