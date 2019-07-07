using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public static HUD Instance { get; private set; }

	[SerializeField] private TextMeshProUGUI timer;
	[SerializeField] private Slider insanityBar;
	[SerializeField] private InsanityText insanityText;

	private float time;
	private Sequence timerSequence;
	private Sequence animBarText;
	private bool timerWarning;

	protected void Awake() => Instance = this;

	protected void Start()
	{
		Tween timerAnim1 = timer.DOColor(Color.red, 0.1f);
		Tween timerAnim2 = timer.DOColor(Color.white, 0.1f);

		timerSequence = DOTween.Sequence();
		timerSequence.Append(timerAnim1).Append(timerAnim2).Insert(0f, timer.transform.DOScale(1.2f, 0.2f)).Insert(0f, timer.transform.DOScale(1f, 0.2f)).SetLoops(-1);
		timerSequence.Pause();
		timerWarning = false;

		//TO ERASE
		editInsanityBar(0.5f);
	}

	protected void Update()
	{
		time += Time.deltaTime;

		string minutes = Mathf.Floor(time / 60).ToString("00");
		string seconds = Mathf.Floor(time % 60).ToString("00");

		timer.text = minutes + ":" + seconds;


		if (Mathf.Floor(time % 60) >= 5f && !timerWarning)
		{
			editInsanityBar(0.5f);
		}
		if (Mathf.Floor(time % 60) >= 8f && !timerWarning)
		{
			editInsanityBar(0.1f);
		}
		if (Mathf.Floor(time % 60) >= 10f && !timerWarning)
		{
			editInsanityBar(0.9f);
		}
		if (Mathf.Floor(time % 60) >= 45f && !timerWarning)
		{
			timerWarning = true;
			timerSequence.Play();
		}
		if (insanityBar.value==0f)
		{
			Debug.Log("GameOver Scene + return main Screen");
		}
		if (insanityBar.value<=0.25)
		{
			insanityText.switchState(false);
		}
		else
		{
			insanityText.switchState(true);
		}
	}

	public void editInsanityBar(float value)
	{
		insanityBar.DOValue(value, 0.5f).Play();
	}
}
