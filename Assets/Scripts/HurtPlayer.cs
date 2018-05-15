using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour 
{
	public GameObject player;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player");
	}

	void OnTriggerEnter(Collider col)
	{
		//if player collides with enemy trigger then hurt player
		if (col.transform.tag == "Player") 
		{
			//hurt player
			StartCoroutine(hurtingPlayer());
			StopCoroutine(hurtingPlayer());
		}
	}

	IEnumerator hurtingPlayer()
	{
		player.GetComponent<Player> ().HurtPlayer();

		yield return new WaitForSeconds (4f);

		GetComponent<BoxCollider>().enabled = false;
		GetComponent<BoxCollider>().enabled = true;

		yield return null;
	}
}
