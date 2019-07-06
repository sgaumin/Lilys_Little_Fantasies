using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI timer;
	private float time;
	private Sequence timerSequence;
	private bool timerWarning;
	// Start is called before the first frame update
	void Start()
    {
		DOTween.defaultAutoPlay = AutoPlay.None;
		DOTween.defaultAutoKill = false;
		Tween timerAnim1 = timer.DOColor(Color.red, 0.1f);
		Tween timerAnim2 = timer.DOColor(Color.white, 0.1f);
		timerSequence = DOTween.Sequence();
		timerSequence.Append(timerAnim1).Append(timerAnim2).Insert(0f, timer.transform.DOScale(1.2f,0.2f)).Insert(0f, timer.transform.DOScale(1f, 0.2f)).SetLoops(-1);
		timerWarning = false;
	}

    // Update is called once per frame
    void Update()
    {
		time += Time.deltaTime;

		string minutes = Mathf.Floor(time / 60).ToString("00");
		string seconds = Mathf.Floor(time % 60).ToString("00");

		timer.text = minutes + ":" + seconds;

		if (Mathf.Floor(time%60)>=45f&&!timerWarning)
		{
			timerWarning = true;
			timerSequence.Play();
		}
	}
}
