using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCrystal : MonoBehaviour 
{
	public GameObject player;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	//if player's sword hits the crystal
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Collect") 
		{
			player.GetComponent<Player> ().AddingHealth ();
			player.GetComponent<Player> ().collecting = false;
			//play crystal break sound
			//play crystal break particles
			Destroy (gameObject, 1f);
		}
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			player.GetComponent<Player> ().collecting = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			player.GetComponent<Player> ().collecting = false;
		}
	}
}
