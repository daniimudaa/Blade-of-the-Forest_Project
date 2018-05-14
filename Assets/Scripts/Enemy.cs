using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	Animator enemyAnim;
	public GameObject player;
	public Transform playerTransform;
	public GameObject horn;
	public bool attackPlayer;
	public float MoveSpeed;

	void Start () 
	{
		enemyAnim = gameObject.GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerTransform = player.transform;
	}

	//if player's sword hits the enemy
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Sword") 
		{
			//play death sound
			enemyAnim.SetBool("death", true);
			playerTransform = null;
			//play death particles
			MoveSpeed = 0;
			Destroy (gameObject, 3f);
		}
	}

	void OnTriggerStay (Collider other)
	{
		if (other.transform.tag == "Player")
		{
			StartCoroutine(MovetoPlayer());

			//if player is close enough for attack then attack
			if (Vector3.Distance(transform.position, player.transform.position) <= 2) 
			{
				print ("Attack player");
				StartCoroutine(AttackPlayer ());
				StopCoroutine(AttackPlayer ());
			}
		}
	}

	void OnTriggerExit ()
	{
		attackPlayer = false;
		enemyAnim.SetBool ("pursuit", false);
	}

	IEnumerator MovetoPlayer()
	{
		transform.LookAt (playerTransform);
		enemyAnim.SetBool ("pursuit", true); 
		transform.position += transform.forward * MoveSpeed *Time.deltaTime;
		yield return null;
	}

	IEnumerator AttackPlayer()
	{
		enemyAnim.SetBool ("pursuit", false);
		enemyAnim.SetBool ("attack", true); 
		yield return new WaitForSeconds (1f);
		enemyAnim.SetBool ("attack", false);
		attackPlayer = false;
		yield return new WaitForSeconds (5f);
		yield return null;
	}
}
