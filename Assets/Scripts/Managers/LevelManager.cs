using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance { get; private set; }

	[Header("Initialization")]
	[SerializeField] private float startSanityLevel;
	[SerializeField] private float timeByScene;
	[SerializeField] private int daysToFinish;
	[SerializeField] private AudioExpress jump;

	[Header("Nightmare")]
	[SerializeField] private EndLight endLightPrefab;
	[SerializeField] private Transform lightSpawn;

	[Header("Controllers")]
	[SerializeField] private RuntimeAnimatorController bedroomAnimator;
	[SerializeField] private RuntimeAnimatorController nightmareAnimator;

	public float TimeByScene => timeByScene;

	public float StartInsanityLevel => startSanityLevel;

	public int DaysToFinish => daysToFinish;

	private void Awake() => Instance = this;

	private void Start()
	{

		switch (GameSystem.Instance.LevelType)
		{
			case LevelTypes.Nightmare:
				Transition.Instance.FadIn();
				PlayerMovement.Instance.CurrentAnimatorController = nightmareAnimator as RuntimeAnimatorController;
				break;
			case LevelTypes.Bedroom:
				Transition.Instance.FadIn();
				PlayerMovement.Instance.CurrentAnimatorController = bedroomAnimator as RuntimeAnimatorController;
				break;
			case LevelTypes.Day:
				break;
			case LevelTypes.Others:
				break;
		}
	}

	public void GameSceneTransition()
	{
		// We need to save game data from here
		GameData.Instance.SaveData();

		switch (GameSystem.Instance.LevelType)
		{
			case LevelTypes.Nightmare:
				StartCoroutine(NightmareLoading());
				break;
			case LevelTypes.Bedroom:
				StartCoroutine(BedroomLoading());
				break;
			case LevelTypes.Day:
				break;
			case LevelTypes.Others:
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
		GameData.Instance.NextDay();
	}
}