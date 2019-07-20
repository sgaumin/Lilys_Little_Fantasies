using DG.Tweening;
using UnityEngine;

public class MiniGameButton : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer = null;
	[SerializeField] private float animationDuration = 1;

	private Sequence sequence = null;

	public void Initialize()
	{
		if (spriteRenderer != null)
		{
			sequence = DOTween.Sequence();
			sequence.Append(transform.DOScale(1.3f, animationDuration).SetEase(Ease.Linear));
			sequence.Append(transform.DOScale(1, animationDuration).SetEase(Ease.Linear));
			sequence.SetLoops(-1);
			sequence.Play();

		}
	}

	public void DestroyObject()
	{
		if (sequence != null)
		{
			sequence.Kill();
		}
		Destroy(this.gameObject);
	}
}
