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

	void Start () 
	{
		controller = GetComponent<CharacterController>();
	}
	
	void Update () 
	{
		//character movement
		float yStore = moveDirection.y;
		moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis ("Horizontal"));
		moveDirection = moveDirection.normalized * moveSpeed; 
		moveDirection.y = yStore;

		//jump only once - when grounded
		if (controller.isGrounded) 
		{
			moveDirection.y = 0f;

			if (Input.GetButtonDown ("Jump")) 
			{
				moveDirection.y = jumpForce;
			}
		}

		//add jump force
		moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
		controller.Move (moveDirection * Time.deltaTime);
	}
}
