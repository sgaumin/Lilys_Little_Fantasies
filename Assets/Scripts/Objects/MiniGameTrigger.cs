using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MiniGameTrigger : MonoBehaviour
{
	private enum MiniGameState
	{
		None,
		ShowButton,
		PlayGame
	}

	[SerializeField] private GameObject minigamePrefab = null;
	[SerializeField] private GameObject buttonPrefab = null;


	[SerializeField] private MiniGameType miniGameType = MiniGameType.Book;
	[SerializeField] private Transform buttonPosition = null;
	[SerializeField] private Transform barPosition = null;

	[SerializeField] private float sanityPoints = 0.1f;

	[Header("Audio")]
	[SerializeField] private AudioClip miniGameSucceed;
	[SerializeField] private AudioClip miniGameFailed;

	private AudioSource audioSource;
	private MiniGameButton miniGameButton = null;
	private BedroomMiniGame miniGameBar = null;

	private MiniGameState miniGameState = MiniGameState.None;

	protected void Start() => audioSource = GetComponent<AudioSource>();

	public void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			GameObject buttonObject = Instantiate(buttonPrefab);

			if (buttonObject != null)
			{
				miniGameButton = buttonObject.GetComponent<MiniGameButton>();
				miniGameButton?.Initialize();

				if (buttonPosition != null)
				{
					miniGameButton.transform.position = buttonPosition.position;
				}

				miniGameState = MiniGameState.ShowButton;
			}
		}
	}

	public void OnTriggerExit2D(Collider2D collider)
	{
		if (miniGameButton != null)
		{
			miniGameButton.DestroyObject();
			miniGameButton = null;
		}

		miniGameState = MiniGameState.None;
	}

	private void Update()
	{
		if (Input.GetButtonDown("BedroomTrigger"))
		{
			switch (miniGameState)
			{
				case MiniGameState.ShowButton:
					{
						if (miniGameButton != null)
						{
							miniGameButton.DestroyObject();
							miniGameButton = null;
						}

						GameObject gameBar = Instantiate(minigamePrefab);
						if (gameBar != null)
						{
							if (barPosition != null)
							{
								gameBar.transform.position = barPosition.position;
							}

							gameBar.transform.SetParent(HUD.Instance.transform);
							gameBar.transform.localScale = Vector3.one;

							miniGameBar = gameBar.GetComponent<BedroomMiniGame>();

							switch (miniGameType)
							{
								case MiniGameType.Toy:
									{
										miniGameBar.Initialize(1, 0.1f, 1);
									}
									break;
								case MiniGameType.Book:
									{
										miniGameBar.Initialize(2, 0.05f, 1);
									}
									break;
							}

							PlayerMovement.Instance.EnableMoving(false);
						}

						miniGameState = MiniGameState.PlayGame;
					}
					break;
				case MiniGameState.PlayGame:
					{
						if (miniGameBar != null)
						{
							miniGameBar.Stop();
							bool result = miniGameBar.GetResult();
							if (result)
							{
								audioSource.clip = miniGameSucceed;
								HUD.Instance.Sanity += sanityPoints;
							}
							else
							{
								audioSource.clip = miniGameFailed;
							}

							Destroy(miniGameBar.gameObject);
						}

						// Audio
						audioSource.Play();

						PlayerMovement.Instance.EnableMoving(true);
						miniGameBar = null;
						miniGameState = MiniGameState.None;
					}

					break;
			}
		}
	}
}
