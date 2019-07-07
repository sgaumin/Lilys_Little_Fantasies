using DG.Tweening;
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

	private SpriteRenderer spriteRenderer;

	protected void Start() => spriteRenderer = GetComponentInChildren<SpriteRenderer>();

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

	public void Hitted()
	{
		animator?.SetTrigger("IsHitted");

		Sequence Anim = DOTween.Sequence();
		Anim.Append(spriteRenderer.DOColor(Color.black, 0.08f)).Append(spriteRenderer.DOColor(Color.white, 0.08f))
			.Append(spriteRenderer.DOColor(Color.black, 0.08f)).Append(spriteRenderer.DOColor(Color.white, 0.08f));
		Anim.Play();
	}

	void FixedUpdate()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
