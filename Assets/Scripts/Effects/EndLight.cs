using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLight : MonoBehaviour
{
	[SerializeField] private SpriteRenderer sprite;
	Tweener sinusMovement;
	Tweener sinusMovement2;
	Tweener sinusMovement3;
	Tweener horizontalMovement;
	Sequence sinus;
	Sequence anim;

	void Start()
	{
		sinusMovement = sprite.transform.DOLocalMoveY(-1.5f, 1f);
		sinusMovement.SetEase(Ease.InOutSine);

		sinusMovement2 = sprite.transform.DOLocalMoveY(1.5f, 2f);
		sinusMovement2.SetEase(Ease.InOutSine);

		sinusMovement3 = sprite.transform.DOLocalMoveY(0f, 1f);
		sinusMovement3.SetEase(Ease.InOutSine);

		horizontalMovement = sprite.transform.DOMove(new Vector3(-7.5f, 0, 0), 60f);
		sinus = DOTween.Sequence().Append(sinusMovement)
			.Append(sinusMovement2)
			.Append(sinusMovement3)
			.SetLoops(-1)
			.Play();

		//anim = DOTween.Sequence();
		//anim.Append(sinusMovement).Append(sinusMovement2);
		//anim.Play();
	}

	void Update()
	{
		this.transform.position += new Vector3(-1, 0) * 0.33f * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			LevelManager.Instance.GameSceneTransition();
		}

		if (collision.gameObject.CompareTag("Generator"))
		{
			LevelLoader.Instance.LoadGameOver();
		}
	}
}
