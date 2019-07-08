using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
	public const string startScene = "StartScreen";
	public const string bedroomGameScene = "Bedroom";
	public const string nightmareGameScene = "Nightmare";
	public const string dayScene = "Day";
	public const string creditsScene = "Credits";

	public static LevelLoader Instance { get; private set; }

	protected void Awake() => Instance = this;

	public void ReloadLevel()
	{
		LevelClear();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void LoadNextLevel()
	{
		LevelClear();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void LoadStartScene()
	{
		LevelClear();
		SceneManager.LoadScene(startScene);
	}

	public void LoadDayScene()
	{
		LevelClear();
		SceneManager.LoadScene(dayScene);
	}

	public void LoadCredits()
	{
		LevelClear();
		SceneManager.LoadScene(creditsScene);
	}

	public void LoadBedroom()
	{
		LevelClear();
		SceneManager.LoadScene(bedroomGameScene);
	}

	public void LoadNightmare()
	{
		LevelClear();
		SceneManager.LoadScene(nightmareGameScene);
	}

	public void QuitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
	}

	private void LevelClear() => DOTween.Clear(true);
}