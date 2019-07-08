using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
	public static GameData Instance { get; private set; }

	public int DayCount { get; private set; }

	public float SanityLevel { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void InitializeData()
	{
		DayCount = LevelManager.Instance.DaysToFinish;
		SanityLevel = LevelManager.Instance.StartInsanityLevel;
	}

	public void SaveData() => SanityLevel = HUD.Instance.Sanity;

	public void NextDay()
	{
		DayCount--;
		if (DayCount == 0)
		{
			LevelLoader.Instance.LoadNextLevel();
		}
		else
		{
			LevelLoader.Instance.LoadDayScene();
		}
	}
}
