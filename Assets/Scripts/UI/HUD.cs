using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public static HUD Instance { get; private set; }

	[SerializeField] private TextMeshProUGUI timerText;
	[SerializeField] private Slider insanityBar;
	[SerializeField] private InsanityText insanityText;

	private Sequence timerSequence;
	private Sequence animBarText;

	public float TimeInScene { get; private set; }

	public float Insanity { get => insanityBar.value; set => SetInsanity(value); }

	protected void Awake() => Instance = this;

	protected void Start()
	{
		TimeInScene = 0;

		Tween timerAnim1 = timerText.DOColor(Color.red, 0.1f);
		Tween timerAnim2 = timerText.DOColor(Color.white, 0.1f);

		timerSequence = DOTween.Sequence();
		timerSequence.Append(timerAnim1).Append(timerAnim2).Insert(0f, timerText.transform.DOScale(1.2f, 0.2f))
			.Insert(0f, timerText.transform.DOScale(1f, 0.2f))
			.SetLoops(-1);

		timerSequence.Pause();

		SetInsanity(0.5f);
	}

	protected void Update()
	{
		TimeInScene += Time.deltaTime;

		// Display Timer
		string minutes = Mathf.Floor(TimeInScene / 60).ToString("00");
		string seconds = Mathf.Floor(TimeInScene % 60).ToString("00");
		timerText.text = minutes + ":" + seconds;

		if (Insanity == 0f)
		{
			Debug.Log("GameOver Scene + return main Screen");
		}

		if (Insanity <= 0.25)
		{
			insanityText.switchState(false);
		}
		else
		{
			insanityText.switchState(true);
		}

		if (TimeInScene >= LevelManager.Instance.TimeByScene)
		{
			LevelManager.Instance.GameSceneTransition();
		}
	}

	private void SetInsanity(float value) => insanityBar.DOValue(value, 0.5f).Play();
}
