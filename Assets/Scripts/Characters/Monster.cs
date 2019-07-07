using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour
{
	[SerializeField] private float animationDuration = 2;
	[SerializeField] private float maxMovingDistance = 0;
	[SerializeField] private MonsterType monsterType = MonsterType.Static;

	private Sequence sequence = null;
	private Vector3 startPos;

	void Start()
	{
		float durationQuarter = animationDuration / 4;
		float halfDistance = maxMovingDistance / 2;

		startPos = transform.localPosition;

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
		Debug.Log(" Collision with monster");
		if (collider.gameObject.CompareTag("Player")) // KJ : This is mock. You have to put the name of the brush/bullete
		{
			if (monsterType != MonsterType.Vertical)
			{
				GameObject flower = ResourceManager.Instance.GetObject(ObjectType.Flower);
				if (flower != null)
				{
					flower.transform.position = this.transform.position;
				}
			}

			GameObject smoke = ResourceManager.Instance.GetObject(ObjectType.Smoke);
			if (smoke != null)
			{
				smoke.transform.position = this.transform.position;
			}

			Object.Destroy(this.gameObject);
		}
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
