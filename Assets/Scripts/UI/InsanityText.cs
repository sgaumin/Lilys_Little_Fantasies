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
	// Start is called before the first frame update
	void Start()
    {
		DOTween.defaultAutoPlay = AutoPlay.None;
		DOTween.defaultAutoKill = false;
		Tween anim1 = text.DOColor(new Color(183 / 255f, 113 / 255f, 181 / 255f), 1.25f);
		Tween anim2 = text.DOColor(Color.white, 0.3f);
		sequence1 = DOTween.Sequence();
		sequence1.Append(anim1).Append(anim2).SetLoops(-1);
		sequence1.PlayForward();

		Tweener anim21 = text.DOColor(Color.red, 0.1f);
		Tweener anim22 = text.DOColor(Color.white, 0.1f);
		animBarText = DOTween.Sequence();
		animBarText.Append(anim21).Append(anim22).Insert(0f, text.transform.DOScale(1.2f, 0.2f)).Insert(0f, text.transform.DOScale(1f, 0.2f)).SetLoops(-1).Pause();
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void switchState(bool value)
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
