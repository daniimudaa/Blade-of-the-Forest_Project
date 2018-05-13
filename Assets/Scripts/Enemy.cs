﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	//if player's sword hits the enemy
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Sword") 
		{
			//play death sound
			//play death animation
			//play death particles
			Destroy (gameObject, 2f);
		}
	}
}
