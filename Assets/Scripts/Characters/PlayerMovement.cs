using DG.Tweening;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
	public static PlayerMovement Instance { get; private set; }

	[SerializeField] private float runSpeed = 40f;
	[SerializeField] private Transform particleSpawn;

	[SerializeField] private PaintScript paintPrefab;
	[SerializeField] private int maxNumberOfParticles = 10;

	[Header("Audio")]
	[SerializeField] private AudioClip attackSound;
	[SerializeField] private AudioClip walkSound;
	[SerializeField] private AudioClip[] hitSound;

	private AudioSource audioSource;
	private bool canAttack;
	private CharacterController2D controller;
	private Animator animator;
	private float horizontalMove = 0f;
	private bool isJumping;
	private SpriteRenderer spriteRenderer;
	private bool canMove;

	public RuntimeAnimatorController CurrentAnimatorController
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
		audioSource = GetComponent<AudioSource>();
		controller = GetComponent<CharacterController2D>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		canMove = true;
		canAttack = true;
	}

	private void Update()
	{
		if (!canMove)
		{
			animator?.SetFloat("Speed", 0f);
			return;
		}

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		animator?.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump") && !isJumping)
		{
			isJumping = true;
			animator?.SetBool("IsJumping", true);
		}

		if (Input.GetButtonDown("Attack") && GameSystem.Instance.LevelType == LevelTypes.Nightmare && canAttack)
		{
			canAttack = false;
			animator?.SetTrigger("Attack");
		}
	}

	public void OnLanding()
	{
		animator?.SetBool("IsJumping", false);
		isJumping = false;
	}

	public void Hitted()
	{
		//Audio
		audioSource.clip = hitSound[Random.Range(0, hitSound.Length)];
		audioSource.Play();

		animator?.SetTrigger("IsHitted");

		Sequence Anim = DOTween.Sequence();
		Anim.Append(spriteRenderer.DOColor(Color.black, 0.08f)).Append(spriteRenderer.DOColor(Color.white, 0.08f))
			.Append(spriteRenderer.DOColor(Color.black, 0.08f)).Append(spriteRenderer.DOColor(Color.white, 0.08f));
		Anim.Play();
	}

	private void FixedUpdate()
	{
		if (canMove)
		{
			controller.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
		}
	}

	public void LaunchParticules() => StartCoroutine(SpawnParticles());

	public void ResetAttack() => canAttack = true;

	private IEnumerator SpawnParticles()
	{
		//Audio
		audioSource.clip = attackSound;
		audioSource.Play();

		int numberOfParticles = 1;
		for (int i = 0; i < maxNumberOfParticles; i++)
		{
			yield return new WaitForSeconds(0.02f);
			PaintScript paint = Instantiate(paintPrefab, particleSpawn.position + new Vector3(0, 0.6f - 0.1f * numberOfParticles), Quaternion.identity);
			paint.Launch(new Vector2(0.45f * Random.value * transform.localScale.x, 0.45f * Random.value));
			numberOfParticles++;
		}

		ResetAttack();
	}

	public void EnableMoving(bool enableMove)
	{
		if (!enableMove)
		{
			controller.StopMovement();
		}

		canMove = enableMove;
	}

	public void PlayFootSound()
	{
		//Audio
		audioSource.clip = walkSound;
		audioSource.Play();
	}
}
