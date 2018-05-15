using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour 
{
	public Animator bossAnim;
	public GameObject player;
	public Transform playerTransform;
	public bool attackPlayer;
	public float moveSpeed;

	//boss health
	public Slider healthBar;
	public float CurrentHealth { get; set;}
	public float MaxHealth { get; set;}
	public bool gainhealth;

	void Start () 
	{
		MaxHealth = 200;
		CurrentHealth = MaxHealth;
		healthBar.value = CalculateHealth();
		bossAnim = gameObject.GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerTransform = player.transform;
	}

	void Update ()
	{
		if (CurrentHealth <= 0) 
		{
			Die ();
		}
	}
	
	//if player's sword hits the boss
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Sword") 
		{
			DamageHealth (20);
		}

		if (col.gameObject.tag == "Ult") 
		{
			DamageHealth (50);
		}
	}

	//might have to put in seperate script
	//might also work in OnCollisionEnter?
//	void OnTriggerEnter(Collider col_01)
//	{
//		if (col_01.gameObject.tag == "Ult") 
//		{
//			TakeDamage ();
//		}
//	}

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
		bossAnim.SetBool ("pursuit", false);
	}

	void DamageHealth (float damagevalue)
	{
		CurrentHealth -= damagevalue;
		healthBar.value = CalculateHealth();
	}

	void Die()
	{
		//play death sound
		bossAnim.SetBool("Die", true);
		playerTransform = null;
		//play death particles
		moveSpeed = 0;
		//Destroy (gameObject, 3f);
	}

	IEnumerator MovetoPlayer()
	{
		transform.LookAt (playerTransform);
		bossAnim.SetBool ("pursuit", true); 
		transform.position += transform.forward * moveSpeed *Time.deltaTime;
		yield return null;
	}

	IEnumerator AttackPlayer()
	{
		bossAnim.SetBool ("pursuit", false);
		bossAnim.SetBool ("attack", true); 
		yield return new WaitForSeconds (1f);
		bossAnim.SetBool ("attack", false);
		attackPlayer = false;
		yield return new WaitForSeconds (5f);
		yield return null;
	}

	float CalculateHealth()
	{
		return CurrentHealth / MaxHealth;
	}
}
