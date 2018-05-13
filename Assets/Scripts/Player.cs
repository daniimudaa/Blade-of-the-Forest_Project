using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	public float moveSpeed;
	public float jumpForce;
	public CharacterController controller;

	private Vector3 moveDirection; 
	public float gravityScale;

	private Animator anim;

	public bool doubleJump;
	public bool attacking;

	public GameObject sword;

	void Start () 
	{
		attacking = false;
		controller = GetComponent<CharacterController>();
		anim = GetComponent<Animator> ();
	}
	
	void Update () 
	{
		if (Input.GetMouseButtonDown (0)) 
		{
			attacking = true;
			StartCoroutine (Attacking ());
		} 

		if (!attacking)
		{
			anim.SetBool ("Attack", false); 
			NotAttacking ();
		}


		//character movement
		float yStore = moveDirection.y;
		moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis ("Horizontal"));
		moveDirection = moveDirection.normalized * moveSpeed; 
		moveDirection.y = yStore;

		//jump only once - when grounded
		if (controller.isGrounded) 
		{
			moveDirection.y = 0f;
			doubleJump = false;

			if (Input.GetButtonDown ("Jump")) 
			{
				moveDirection.y = jumpForce;
			}
		}

		if (!controller.isGrounded) 
		{
			doubleJump = true;
			if (doubleJump)
			{
				//controller.velocity (Vector3.up * jumpForce);
				anim.SetBool ("doubleJump", true);
			}
		}

		anim.SetBool ("isGrounded", controller.isGrounded);


		//add jump force
		moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
		controller.Move (moveDirection * Time.deltaTime);

		anim.SetFloat ("walk", (Mathf.Abs (Input.GetAxis ("Vertical"))));
		anim.SetFloat ("walksideways", (Mathf.Abs (Input.GetAxis ("Horizontal"))));
	}

	IEnumerator Attacking()
	{
		anim.SetBool ("Attack", true);
		sword.GetComponent<Collider>().enabled = true;
		print ("Sword col on");
		yield return new WaitForSeconds (1f);

		attacking = false;
		yield return null;
	}

	void NotAttacking()
	{
		sword.GetComponent<Collider>().enabled = false; 
		print ("Sword col off");

	}
}
