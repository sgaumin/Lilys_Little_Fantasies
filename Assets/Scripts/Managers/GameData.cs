using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
	public static GameData Instance { get; private set; }

	public int DayCount { get; private set; }

	public float InsanityLevel { get; private set; }

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
		DayCount = 0;
		InsanityLevel = LevelManager.Instance.StartInsanityLevel;
	}

	public void SaveData() => InsanityLevel = HUD.Instance.Insanity;

	public void NextDay() => DayCount++;
}
