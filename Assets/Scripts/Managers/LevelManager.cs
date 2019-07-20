using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance { get; private set; }

	[SerializeField] private LevelTypes levelTypes;

	[Header("Initialization")]
	[SerializeField] private float startSanityLevel;
	[SerializeField] private float timeByScene;
	[SerializeField] private int daysToFinish;

	[Header("Nightmare")]
	[SerializeField] private EndLight endLightPrefab;
	[SerializeField] private Transform lightSpawn;

	[Header("Controllers")]
	[SerializeField] private RuntimeAnimatorController bedroomAnimator;
	[SerializeField] private RuntimeAnimatorController nightmareAnimator;

	public LevelTypes LevelType { get => levelTypes; }

	public float TimeByScene => timeByScene;

	public float StartInsanityLevel => startSanityLevel;

	public int DaysToFinish => daysToFinish;

	private void Awake() => Instance = this;

	private void Start()
	{
		switch (LevelType)
		{
			case LevelTypes.Nightmare:
				Transition.Instance.FadIn();
				PlayerMovement.Instance.CurrentAnimatorController = nightmareAnimator as RuntimeAnimatorController;
				break;
			case LevelTypes.Bedroom:
				Transition.Instance.FadIn();
				PlayerMovement.Instance.CurrentAnimatorController = bedroomAnimator as RuntimeAnimatorController;
				break;
		}
	}

	public void GameSceneTransition()
	{
		// We need to save game data from here
		GameData.SaveData();

		switch (LevelType)
		{
			case LevelTypes.Nightmare:
				StartCoroutine(NightmareLoading());
				break;
			case LevelTypes.Bedroom:
				StartCoroutine(BedroomLoading());
				break;
		}
	}

	public void GameOver()
	{
		LevelLoader.Instance.LoadGameOver();
	}

	public void SpawnNightmareLight() => Instantiate(endLightPrefab, lightSpawn);

	private IEnumerator BedroomLoading()
	{
		Transition.Instance.FadOut();
		yield return new WaitForSeconds(1f);
		LevelLoader.Instance.LoadNightmare();
	}

	private IEnumerator NightmareLoading()
	{
		Transition.Instance.FadOutWhite();
		yield return new WaitForSeconds(1f);
		GameData.NextDay();
	}
}