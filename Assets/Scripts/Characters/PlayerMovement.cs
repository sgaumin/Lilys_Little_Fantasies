using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController2D controller = null;
	public Animator animator = null;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	void Update()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		animator?.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator?.SetBool("IsJumping", true);
		}

		if (Input.GetButtonDown("Attack"))
		{
			animator?.SetTrigger("Attack");
		}
	}

	public void OnLanding() => animator?.SetBool("IsJumping", false);

	public void Hitted() => animator?.SetTrigger("IsHitted");

	void FixedUpdate()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
