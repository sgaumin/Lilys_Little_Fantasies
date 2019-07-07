using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : MonoBehaviour
{
	[SerializeField] private float animationDuration = 2;
	[SerializeField] private float maxMovingDistance = 0;
	//	[SerializeField] private Animator animator = null;
	[SerializeField] private MonsterType mosnterType = MonsterType.Static;

	private Sequence sequence = null; 

	// Start is called before the first frame update
	void Start()
	{
		float durationQuarter = animationDuration / 4;
		float halfDistance = maxMovingDistance / 2;

		switch (mosnterType)
		{
			case MonsterType.Vertical:
				{
					sequence = DOTween.Sequence();
					sequence.Append(transform.DOMoveY(transform.position.y - halfDistance, durationQuarter).SetEase(Ease.Linear));
					sequence.Append(transform.DOMoveY(transform.position.y + halfDistance, durationQuarter * 2).SetEase(Ease.Linear));
					sequence.Append(transform.DOMoveY(transform.position.y, durationQuarter).SetEase(Ease.Linear));
					sequence.SetLoops(-1);
					sequence.Play();
				}
				break;
			case MonsterType.Horizontal:
				{
					sequence = DOTween.Sequence();
					sequence.Append(transform.DOMoveX(transform.position.x - halfDistance, durationQuarter).SetEase(Ease.Linear));
					sequence.Append(transform.DOMoveX(transform.position.x + halfDistance, durationQuarter * 2).SetEase(Ease.Linear));
					sequence.Append(transform.DOMoveX(transform.position.x, durationQuarter).SetEase(Ease.Linear));

					sequence.SetLoops(-1);
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
			if (mosnterType != MonsterType.Vertical)
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

	public void OnTriggerExit2D(Collider2D collider)
	{
		Debug.Log(" OnCollisionExit2D");
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
