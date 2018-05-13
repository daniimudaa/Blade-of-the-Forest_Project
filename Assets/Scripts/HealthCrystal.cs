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
		if (col.gameObject.tag == "Sword") 
		{
			player.GetComponent<Player> ().AddingHealth ();

			//play crystal break sound
			//play crystal break particles
			Destroy (gameObject, 1f);
		}
	}
}
