using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller = null ;
	public Animator animator = null ;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	// Update is called once per frame
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if( animator != null )
			animator?.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;

			if ( animator != null )
				animator?.SetBool("IsJumping", true);
		}

		/*
		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}
		*/
	}

	public void OnLanding ()
	{
		if( animator != null )
			animator?.SetBool("IsJumping", false);
	}

	public void OnCrouching (bool isCrouching)
	{
//		animator?.SetBool("IsCrouching", isCrouching);
	}

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
