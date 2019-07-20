using DG.Tweening;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
	public static GameSystem Instance { get; private set; }

	[SerializeField] private LevelTypes levelTypes;

	public GameStates GameState { get; private set; } = GameStates.Play;

	public LevelTypes LevelType { get => levelTypes; }

	protected void Awake()
	{
		Instance = this;
		DOTween.Init();
		DOTween.defaultAutoPlay = AutoPlay.None;
		DOTween.defaultAutoKill = false;
	}

	protected void Update()
	{
		if (Input.GetButtonDown("Quit"))
		{
			LevelLoader.Instance.QuitGame();
		}
	}
}