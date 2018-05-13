using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	//health variables and values
	public Slider healthBar;
	public float CurrentHealth { get; set;}
	public float MaxHealth { get; set;}
	public bool gainhealth;

	//stamina variables and values
	public Slider StaminaBar;
	public float CurrentStamina { get; set;}
	public float MaxStamina { get; set;}
	public bool gainStamina;
	public bool takeStamina;

	//magic variables and values
	public Slider MagicBar;
	public float CurrentMagic { get; set;}
	public float MaxMagic { get; set;}
	public bool collectMagic;
	public bool takeMagic;
	public bool swordUlt;


	void Start () 
	{
		//Time.timeScale = 1;
		MaxHealth = 100;
		MaxStamina = 100;
		MaxMagic = 100;
		CurrentStamina = MaxStamina;
		StaminaBar.value = CalculateStamina ();
		CurrentHealth = MaxHealth;
		attacking = false;
		controller = GetComponent<CharacterController>();
		anim = GetComponent<Animator> ();
		healthBar.value = CalculateHealth();
	}
	
	void Update () 
	{
		collectMagic = true;
		gainStamina = true;

		if (Input.GetMouseButtonDown (0)) 
		{
			if (CurrentStamina >= 1) 
			{
				attacking = true;
				StartCoroutine (Attacking ());
			}
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

		if (CurrentHealth <= 0) 
		{
			Die ();
		}

		if (CurrentHealth >= 99) { gainhealth = false;}
		if (CurrentHealth <= 99) { gainhealth = true;}

		if (CurrentStamina >= 99) { gainStamina = false;}
		if (CurrentStamina <= 99 && CurrentStamina >= 1) {gainStamina = true;}
		if (CurrentStamina <= 1) {takeStamina = false;}
		if (CurrentStamina >= 19) {takeStamina = true;}

		if (CurrentMagic >= 99 ) { collectMagic = false;}
		if (CurrentMagic <= 99 && CurrentMagic >= 1) { collectMagic = true;}
		if (CurrentMagic >= 49) { swordUlt = true;} 
		if (CurrentMagic <= 49) { swordUlt = false;}

		if (Input.GetMouseButtonDown (1)) 
		{
			if (swordUlt) 
			{
				StartCoroutine (SwordAnim ());
			} 
		}

		//here for testing what would be enemy and self variable uses
		if (Input.GetKeyDown (KeyCode.X)) 
		{
			DamageHealth (10);
		}

	}

	IEnumerator Attacking()
	{
		anim.SetBool ("Attack", true);
		sword.GetComponent<Collider>().enabled = true;
		yield return new WaitForSeconds (1f);

		attacking = false;
		TakeStamina (20);
		yield return null;
	}

	void NotAttacking()
	{
		sword.GetComponent<Collider>().enabled = false; 
	}

	void Die()
	{
		CurrentHealth = 0;
		anim.SetBool ("death", true);
		//Time.timeScale = 0;
		//playdeath animation
		//play death sound
		//menu.setactive to restart level
		print ("You Died");
	}

	void DamageHealth (float damagevalue)
	{
		CurrentHealth -= damagevalue;
		healthBar.value = CalculateHealth();
	}

	void AddHealth (float healthvalue)
	{
		if (gainhealth)
		{
			CurrentHealth += healthvalue;
			healthBar.value = CalculateHealth ();
		}
	}

	void AddStamina(float stamValue)
	{
		if (gainStamina) 
		{
			CurrentStamina += stamValue;
			StaminaBar.value = CalculateStamina();
		}
	}

	void TakeStamina(float staminValue)
	{
		if (takeStamina) 
		{
			CurrentStamina -= staminValue;
			StaminaBar.value = CalculateStamina();
		}
	}

	void AddMagic(float magValue)
	{
		//if you are able to collect magic then add values
		if (collectMagic) 
		{
			CurrentMagic += magValue;
			MagicBar.value = CalculateMagic();
		}
	}

	void TakeMagic(float magicValue)
	{
		if (takeMagic) 
		{
			CurrentMagic -= magicValue;
			MagicBar.value = CalculateMagic();
		}
	}

	IEnumerator SwordAnim()
	{
		attacking = true;
		takeMagic = true;
		TakeMagic (50);
		//sword ult sound
		//sword ult particles
		anim.SetBool ("SwordUlt", true); 
		yield return new WaitForSeconds (1f);
		anim.SetBool ("SwordUlt", false);
		swordUlt = false;
		yield return null;
	}

//	if collect crystals then add
	public void AddingHealth() 
	{
		AddHealth (10);
	}
	public void AddingStamina() 
	{
		AddStamina (40);
	}
	public void AddingMagic()
	{
		AddMagic (25);
	}


	float CalculateHealth()
	{
		return CurrentHealth / MaxHealth;
	}
	float CalculateStamina()
	{
		return CurrentStamina / MaxStamina;
	}
	float CalculateMagic()
	{
		return CurrentMagic / MaxMagic;
	}
}
