using DG.Tweening;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
	public static GameSystem Instance { get; private set; }

	public GameStates GameState { get; set; }

	protected void Awake()
	{
		Instance = this;
		DOTween.Init();
		DOTween.defaultAutoPlay = AutoPlay.None;
		DOTween.defaultAutoKill = false;

		GameState = GameStates.Play;
		Time.timeScale = 1f;
	}

	protected void Update()
	{
		if (Input.GetButtonDown("Quit"))
		{
			LevelLoader.Instance.QuitGame();
		}
	}
}