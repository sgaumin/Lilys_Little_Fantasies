public static class GameData
{
	public static int DayCount { get; private set; }

	public static float SanityLevel { get; private set; }

	public static void InitializeData()
	{
		DayCount = LevelManager.Instance.DaysToFinish;
		SanityLevel = LevelManager.Instance.StartInsanityLevel;
	}

	public static void SaveData() => SanityLevel = HUD.Instance.Sanity;

	public static void NextDay()
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
