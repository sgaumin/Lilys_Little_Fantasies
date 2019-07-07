using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI title;
	[SerializeField] private TextMeshProUGUI start;
	[SerializeField] private TextMeshProUGUI bracket1;
	[SerializeField] private TextMeshProUGUI bracket2;

	private Sequence titleAnim;
	private Sequence startAnim;
	// Start is called before the first frame update
	void Start()
    {
		titleAnim = DOTween.Sequence().Append(title.transform.DOScale(1.2f, 1f)).Append(title.transform.DOScale(1f,1f)).SetLoops(-1).Play();
		startAnim = DOTween.Sequence().Append(start.transform.DOScale(1.2f, 1f)).Append(start.transform.DOScale(1f, 1f)).SetLoops(-1).Play();
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
