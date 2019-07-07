using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController2D controller = null;
	public Animator animator = null;

	public float runSpeed = 40f;

	public PaintScript paintPrefab;

	[SerializeField] private Transform particleSpawn;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

	private SpriteRenderer spriteRenderer;

	private int numberOfParticles;
	private int MAX_numberOfParticles;

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
			numberOfParticles = 0;
			MAX_numberOfParticles = 10;
			StartCoroutine(SpawnParticles());
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

	private IEnumerator SpawnParticles()
	{
		numberOfParticles++;
		PaintScript paint;

		for (int i = 0; i < MAX_numberOfParticles; i++)
		{
			yield return new WaitForSeconds(0.05f);
			paint = Instantiate(paintPrefab, particleSpawn.position + new Vector3(0, 0.5f - 0.15f * numberOfParticles), Quaternion.identity);
			paint.Launch(new Vector2(0.45f * Random.value * transform.localScale.x, 0.45f * Random.value));
			numberOfParticles++;
		}
	}
}
