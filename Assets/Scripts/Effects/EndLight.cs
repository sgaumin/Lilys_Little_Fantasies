using DG.Tweening;
using UnityEngine;

public class EndLight : MonoBehaviour
{
	[SerializeField] private SpriteRenderer sprite;
	[SerializeField] private AudioExpress transition;

	private bool hasBeenTouchedByPlayer;
	private Tweener sinusMovement;
	private Tweener sinusMovement2;
	private Tweener horizontalMovement;
	private Sequence sinus;

	private void Start()
	{
		sinusMovement = sprite.transform.DOLocalMoveY(-1.5f, 1f).SetEase(Ease.InOutSine);
		sinusMovement2 = sprite.transform.DOLocalMoveY(1.5f, 1f).SetEase(Ease.InOutSine);
		horizontalMovement = sprite.transform.DOMove(new Vector3(-7.5f, 0, 0), 50f);

		sinus = DOTween.Sequence()
			.Append(sinusMovement)
			.Append(sinusMovement2)
			.SetLoops(-1)
			.Play();
	}

	private void Update() => transform.position += new Vector3(-1, 0) * 0.33f * Time.deltaTime;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player") && !hasBeenTouchedByPlayer)
		{
			transition.Play(gameObject);

			hasBeenTouchedByPlayer = true;
			LevelManager.Instance.GameSceneTransition();
		}

		if (collision.gameObject.CompareTag("Generator"))
		{
			LevelLoader.Instance.LoadGameOver();
		}
	}
}
