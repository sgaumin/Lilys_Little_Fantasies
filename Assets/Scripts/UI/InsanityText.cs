using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class InsanityText : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI text;

	Sequence sequence1;
	Sequence animBarText;

	void Start()
	{
		DOTween.defaultAutoPlay = AutoPlay.None;
		DOTween.defaultAutoKill = false;

		Tween anim1 = text.DOColor(Color.clear, 0.5f);
		Tween anim2 = text.DOColor(Color.white, 1f);
		sequence1 = DOTween.Sequence();
		sequence1.Append(anim1).Append(anim2).SetLoops(-1);
		sequence1.Pause();

		Tweener anim21 = text.DOColor(Color.red, 0.1f);
		Tweener anim22 = text.DOColor(Color.white, 0.1f);
		animBarText = DOTween.Sequence();
		animBarText.Append(anim21).Append(anim22).Insert(0f, text.transform.DOScale(1.2f, 0.2f)).Insert(0f, text.transform.DOScale(1f, 0.2f)).SetLoops(-1).Pause();
	}

	public void SwitchState(bool value)
	{
		if (value)
		{
			sequence1.PlayForward();
			animBarText.Pause();
		}
		else
		{
			sequence1.Pause();
			animBarText.PlayForward();
		}
	}
}
