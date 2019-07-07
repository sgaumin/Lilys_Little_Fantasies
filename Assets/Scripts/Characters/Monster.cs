using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class Monster : MonoBehaviour
{
	[SerializeField] private float animationDuration = 2;
	[SerializeField] private float maxMovingDistance = 0;
	[SerializeField] private MonsterType monsterType = MonsterType.Static;
	[SerializeField] private float forceAmount;
	[SerializeField] private int lifePoints;
	[SerializeField] private Color colorDeath;

	[Header("Sounds")]
	[SerializeField] private AudioClip hitSound;
	[SerializeField] private AudioClip deathSound;

	private AudioSource audioSource;
	private Sequence sequence = null;
	private Collider2D[] collider2D;
	private SpriteRenderer spriteRenderer;
	private Vector3 startPos;
	private int lifePointTemp;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		collider2D = GetComponents<Collider2D>();

		float durationQuarter = animationDuration / 4;
		float halfDistance = maxMovingDistance / 2;

		startPos = transform.localPosition;
		lifePointTemp = lifePoints;

		switch (monsterType)
		{
			case MonsterType.Vertical:
				{
					sequence = DOTween.Sequence();
					sequence.Append(transform.DOLocalMoveY(transform.position.y - halfDistance, durationQuarter).SetEase(Ease.Linear))
						.Append(transform.DOLocalMoveY(transform.position.y + halfDistance, durationQuarter * 2).SetEase(Ease.Linear))
						.Append(transform.DOLocalMoveY(transform.position.y, durationQuarter).SetEase(Ease.Linear))
						.SetLoops(-1);
					sequence.Play();
				}
				break;
			case MonsterType.Horizontal:
				{
					sequence = DOTween.Sequence();
					sequence.Append(transform.DOLocalMoveX(startPos.x - halfDistance, durationQuarter).SetEase(Ease.Linear))
						.Append(transform.DOLocalMoveX(startPos.x, durationQuarter).SetEase(Ease.Linear))
						.Append(transform.DOLocalMoveX(startPos.x + halfDistance, durationQuarter).SetEase(Ease.Linear))
						.Append(transform.DOLocalMoveX(startPos.x, durationQuarter).SetEase(Ease.Linear))
						.SetLoops(-1);
					sequence.Play();
				}
				break;
		}
	}

	public void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.CompareTag("Painting"))
		{
			sequence = DOTween.Sequence();
			sequence.Append(spriteRenderer.DOColor(colorDeath, 0.1f)).Append(spriteRenderer.DOColor(Color.white, 0.1f));
			sequence.Play();

			lifePoints--;

			if (lifePoints == 0)
			{
				GameObject flower = ResourceManager.Instance.GetObject(ObjectType.Flower);
				if (flower != null)
				{
					flower.transform.SetParent(transform.parent);
					flower.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0f);
				}

				GameObject smoke = ResourceManager.Instance.GetObject(ObjectType.Smoke);
				if (smoke != null)
				{
					smoke.transform.position = transform.position;
				}

				StartCoroutine(Death());
			}
			else
			{
				// Audio
				audioSource.clip = hitSound;
				audioSource.Play();
			}
		}

		if (collider.CompareTag("Player"))
		{
			Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
			bool pushOnLeft = (transform.position.x - collider.transform.position.x) > 0f;
			Vector2 direction = pushOnLeft ? Vector2.left : Vector2.right;
			direction += Vector2.up;

			rb?.AddForce(direction * forceAmount);
			collider.GetComponent<PlayerMovement>().Hitted();
		}
	}

	private IEnumerator Death()
	{
		// Audio
		audioSource.clip = deathSound;
		audioSource.Play();
		spriteRenderer.enabled = false;
		foreach (var collider in collider2D)
		{
			collider.enabled = false;
		}

		yield return new WaitForSeconds(1f);

		Destroy(gameObject);
	}

	public void OnDestroy()
	{
		if (sequence != null)
		{
			sequence.Kill();
			sequence = null;
		}
	}
}
