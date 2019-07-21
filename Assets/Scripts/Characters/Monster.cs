using DG.Tweening;
using System.Linq;
using UnityEngine;

public class Monster : MonoBehaviour
{
	[SerializeField] private float animationDuration = 2;
	[SerializeField] private float maxMovingDistance = 0;
	[SerializeField] private MonsterMovementType monsterMovement = MonsterMovementType.Static;
	[SerializeField] private float forceAmount;
	[SerializeField] private int lifePoints;
	[SerializeField] private Color colorDeath;
	[SerializeField] private float insanityAmount = 0.04f;

	[Header("Sounds")]
	[SerializeField] private AudioExpress hitSound;
	[SerializeField] private AudioExpress deathSound;

	private Sequence sequenceMovement;
	private Sequence sequenceHit;

	private SpriteRenderer spriteRenderer;
	private Vector3 startPos;
	private int lifePointTemp;

	private void Start()
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		float durationQuarter = animationDuration / 4;
		float halfDistance = maxMovingDistance / 2;

		startPos = transform.localPosition;
		lifePointTemp = lifePoints;

		switch (monsterMovement)
		{
			case MonsterMovementType.Vertical:
				{
					sequenceMovement = DOTween.Sequence();
					sequenceMovement.Append(transform.DOLocalMoveY(transform.position.y - halfDistance, durationQuarter).SetEase(Ease.Linear))
						.Append(transform.DOLocalMoveY(transform.position.y + halfDistance, durationQuarter * 2).SetEase(Ease.Linear))
						.Append(transform.DOLocalMoveY(transform.position.y, durationQuarter).SetEase(Ease.Linear))
						.SetLoops(-1);
					sequenceMovement.Play();
				}
				break;
			case MonsterMovementType.Horizontal:
				{
					sequenceMovement = DOTween.Sequence();
					sequenceMovement.Append(transform.DOLocalMoveX(startPos.x - halfDistance, durationQuarter).SetEase(Ease.Linear))
						.Append(transform.DOLocalMoveX(startPos.x, durationQuarter).SetEase(Ease.Linear))
						.Append(transform.DOLocalMoveX(startPos.x + halfDistance, durationQuarter).SetEase(Ease.Linear))
						.Append(transform.DOLocalMoveX(startPos.x, durationQuarter).SetEase(Ease.Linear))
						.SetLoops(-1);
					sequenceMovement.Play();
				}
				break;
		}
	}

	public void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.CompareTag("Painting"))
		{
			lifePoints--;

			if (lifePoints == 0)
			{
				sequenceHit?.Kill();
				sequenceMovement?.Kill();

				GameObject flower = ResourceManager.Instance.GetObject(ObjectType.Flower);
				if (flower != null)
				{
					flower.transform.SetParent(transform.parent);
					flower.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, 0f);
				}

				// Audio
				if (GameSystem.Instance.GameState == GameStates.Play)
				{
					deathSound?.Play(gameObject);
				}

				Destroy(gameObject);
			}
			else
			{
				sequenceHit = DOTween.Sequence();
				sequenceHit?.Append(spriteRenderer?.DOColor(colorDeath, 0.1f)).Append(spriteRenderer?.DOColor(Color.white, 0.1f));
				sequenceHit?.Play();

				// Audio
				if (GameSystem.Instance.GameState == GameStates.Play)
				{
					hitSound?.Play(gameObject);
				}
			}
		}

		if (collider.CompareTag("Player"))
		{
			bool pushOnLeft = (transform.position.x - collider.transform.position.x) > 0f;
			Vector2 direction = pushOnLeft ? Vector2.left : Vector2.right;
			direction += Vector2.up;

			collider.GetComponent<PlayerMovement>().Hit(insanityAmount, direction * forceAmount);
		}
	}

	public void OnDestroy()
	{
		sequenceHit?.Kill();
		sequenceMovement?.Kill();
	}
}
