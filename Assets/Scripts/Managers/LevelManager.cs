using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class LevelManager : MonoBehaviour
{
	public static LevelManager Instance { get; private set; }

	[Header("Initialization")]
	[SerializeField] private float startInsanityLevel;
	[SerializeField] private float timeByScene;

	[Header("Controllers")]
	[SerializeField] private AnimatorController bedroomAnimator;
	[SerializeField] private AnimatorController nightmareAnimator;

	public float TimeByScene => timeByScene;

	public float StartInsanityLevel => startInsanityLevel;

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
		}
	}

	public void GameSceneTransition()
	{
		// We need to save game data from here
		GameData.Instance.SaveData();

		switch (GameSystem.Instance.LevelType)
		{
			case LevelTypes.Nightmare:
				LevelLoader.Instance.LoadBedroom();
				break;
			case LevelTypes.Bedroom:
				LevelLoader.Instance.LoadNightmare();
				break;
		}
	}
}