using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	//camera look at variables
	public Transform target;
	public Vector3 offset;
	public bool useOffsetValues;

	//camera rotation variables
	public float rotateSpeed;

	public Transform pivot;


	void Start () 
	{
		if (!useOffsetValues) 
		{
			//camera position offset
			offset = target.position - transform.position;
		}

		pivot.transform.position = target.transform.position;
		pivot.transform.parent = target.transform;

		Cursor.lockState = CursorLockMode.Locked;
	}
	
	void LateUpdate () 
	{
		//Get the X position of the mouse & rotate target
		float horizontal = Input.GetAxis ("Mouse X") * rotateSpeed;
		target.Rotate (0, horizontal, 0);

		//get the Y position of Mouse & rotate the pivot
		float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
		pivot.Rotate (-vertical, 0, 0);

		//limit up & down camera rotation
		if (pivot.rotation.eulerAngles.x > 80f && pivot.rotation.eulerAngles.x < 180) 
		{
			pivot.rotation = Quaternion.Euler (80f, 0, 0);
		}

		if (pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 315) 
		{
			pivot.rotation = Quaternion.Euler (315f, 0, 0);
		}

		//move the camera based on current target rotation & offset
		float desiredYAngle = target.eulerAngles.y;
		float desiredXAngle = pivot.eulerAngles.x;
		Quaternion rotation = Quaternion.Euler (desiredXAngle, desiredYAngle, 0);
		transform.position = target.position - (rotation * offset);

		if (transform.position.y < target.position.y) 
		{
			transform.position = new Vector3 (transform.position.x, target.position.y, transform.position.z);
		}

		//camera position from player, look at the player
		transform.LookAt (target);
	}
}
