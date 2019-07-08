using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance { get; private set; }

	[Header("Initialization")]
	[SerializeField] private float startSanityLevel;
	[SerializeField] private float timeByScene;
	[SerializeField] private int daysToFinish;

	[Header("Nightmare")]
	[SerializeField] private EndLight endLightPrefab;
	[SerializeField] private Transform lightSpawn;

	[Header("Controllers")]
	[SerializeField] private AnimatorController bedroomAnimator;
	[SerializeField] private AnimatorController nightmareAnimator;

	public float TimeByScene => timeByScene;

	public float StartInsanityLevel => startSanityLevel;

	public int DaysToFinish => daysToFinish;

	private void Awake() => Instance = this;

	private void Start()
	{
		switch (GameSystem.Instance.LevelType)
		{
			case LevelTypes.Nightmare:
				PlayerMovement.Instance.CurrentAnimatorController = nightmareAnimator;
				break;
			case LevelTypes.Bedroom:
				PlayerMovement.Instance.CurrentAnimatorController = bedroomAnimator;
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
				GameData.Instance.NextDay();
				break;
			case LevelTypes.Bedroom:
				LevelLoader.Instance.LoadNightmare();
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
}