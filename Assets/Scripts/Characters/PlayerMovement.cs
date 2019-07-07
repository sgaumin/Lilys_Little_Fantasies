using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public static PlayerMovement Instance { get; private set; }

	[SerializeField] private float runSpeed = 40f;
	[SerializeField] private Transform particleSpawn;

	[SerializeField] private PaintScript paintPrefab;
	[SerializeField] private int maxNumberOfParticles = 10;

	private bool canAttack;
	private CharacterController2D controller;
	private Animator animator;
	private float horizontalMove = 0f;
	private bool jump = false;
	private SpriteRenderer spriteRenderer;

	public AnimatorController CurrentAnimatorController
	{
		set
		{
			animator = GetComponentInChildren<Animator>();
			animator.runtimeAnimatorController = value;
		}
	}

	protected void Awake() => Instance = this;

	protected void Start()
	{
		controller = GetComponent<CharacterController2D>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		canAttack = true;
	}

	void Update()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		animator?.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator?.SetBool("IsJumping", true);
		}

		if (Input.GetButtonDown("Attack") && GameSystem.Instance.LevelType == LevelTypes.Nightmare && canAttack)
		{
			animator?.SetTrigger("Attack");
		}
	}

	public void LaunchParticules()
	{
		StartCoroutine(SpawnParticles());
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
		controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
		jump = false;
	}

	private IEnumerator SpawnParticles()
	{
		canAttack = false;

		int numberOfParticles = 1;
		for (int i = 0; i < maxNumberOfParticles; i++)
		{
			yield return new WaitForSeconds(0.02f);
			PaintScript paint = Instantiate(paintPrefab, particleSpawn.position + new Vector3(0, 0.6f - 0.1f * numberOfParticles), Quaternion.identity);
			paint.Launch(new Vector2(0.45f * Random.value * transform.localScale.x, 0.45f * Random.value));
			numberOfParticles++;
		}

		canAttack = true;
	}
}
