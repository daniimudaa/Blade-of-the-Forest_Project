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
		moveDirection = new Vector3 (Input.GetAxis ("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis ("Vertical") * moveSpeed);

		//jump only once - when grounded
		if (controller.isGrounded) 
		{
			if (Input.GetButtonDown ("Jump")) 
			{
				moveDirection.y = jumpForce;
			}
		}

		moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
		controller.Move (moveDirection * Time.deltaTime);
	}
}
