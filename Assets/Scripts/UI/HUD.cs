using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	public static HUD Instance { get; private set; }

	[SerializeField] private TextMeshProUGUI timerText;
	[SerializeField] private Slider sanityBar;
	[SerializeField] private InsanityText sanityText;

	private Sequence timerSequence;
	private Sequence animBarText;
	private bool timeOut;
	private bool isWarning;

	public float TimeInScene { get; private set; }

	public float Sanity { get => sanityBar.value; set => SetInsanity(value); }

	protected void Awake() => Instance = this;

	protected void Start()
	{
		TimeInScene = 0;
		Sanity = GameData.SanityLevel;

		Tween timerAnim1 = timerText.DOColor(Color.red, 0.1f);
		Tween timerAnim2 = timerText.DOColor(Color.white, 0.1f);

		timerSequence = DOTween.Sequence();
		timerSequence.Append(timerAnim1).Append(timerAnim2).Insert(0f, timerText.transform.DOScale(1.2f, 0.2f))
			.Insert(0f, timerText.transform.DOScale(1f, 0.2f))
			.SetLoops(-1);

		timerSequence.Pause();
	}

	protected void Update()
	{
		TimeInScene += Time.deltaTime;

		// Display Timer
		string minutes = Mathf.Floor(TimeInScene / 60).ToString("00");
		string seconds = Mathf.Floor(TimeInScene % 60).ToString("00");
		timerText.text = minutes + ":" + seconds;

		if (TimeInScene >= LevelManager.Instance.TimeByScene * 0.75f && !isWarning)
		{
			isWarning = true;
			timerSequence.Play();
		}

		if (TimeInScene >= LevelManager.Instance.TimeByScene)
		{
			if (!timeOut)
			{
				if (LevelManager.Instance.LevelType == LevelTypes.Nightmare)
				{
					LevelManager.Instance.SpawnNightmareLight();
				}
				else
				{
					LevelManager.Instance.GameSceneTransition();
				}

				timeOut = true;
			}
		}
	}

	private void SetInsanity(float value)
	{
		sanityBar.DOValue(value, 0.5f).Play();
		if (sanityBar.value <= 0f)
		{
			LevelManager.Instance.GameOver();
		}
	}
}
