﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaCrystal : MonoBehaviour 
{

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	//if player's sword hits the crystal
	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "Sword") 
		{
			//play crystal break sound
			//play crystal break particles
			print("Absorbed Stamina Crystal");
			Destroy (gameObject, 1f);
		}
	}
}
