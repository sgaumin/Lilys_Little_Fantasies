using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class InsanityText : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
		DOTween.defaultAutoPlay = AutoPlay.None;
		DOTween.defaultAutoKill = false;
		Tween anim1 = text.DOColor(new Color(183 / 255f, 113 / 255f, 181 / 255f), 1.25f);
		Tween anim2 = text.DOColor(Color.white, 0.3f);
		Sequence sequence1 = DOTween.Sequence();
		sequence1.Append(anim1).Append(anim2).SetLoops(-1);
		sequence1.PlayForward();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
