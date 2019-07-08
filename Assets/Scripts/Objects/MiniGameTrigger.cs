using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
	private enum MiniGameState
	{
		None,
		ShowButton,
		PlayGame
	}

	[SerializeField] GameObject minigamePrefab = null;
	[SerializeField] GameObject buttonPrefab = null;


	[SerializeField] MiniGameType miniGameType = MiniGameType.Book;
	[SerializeField] Transform buttonPosition = null;
	[SerializeField] Transform barPosition = null;

	[SerializeField] float sanityPoints = 0.1f;

	private MiniGameButton miniGameButton = null;
	private BedroomMiniGame miniGameBar = null;

	private MiniGameState miniGameState = MiniGameState.None;

	public void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			var buttonObject = GameObject.Instantiate(buttonPrefab);

			if (buttonObject != null)
			{
				miniGameButton = buttonObject.GetComponent<MiniGameButton>();
				miniGameButton?.Initialize();

				if (buttonPosition != null)
					miniGameButton.transform.position = buttonPosition.position;

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

						var gameBar = GameObject.Instantiate(minigamePrefab);
						if (gameBar != null)
						{
							if (barPosition != null)
								gameBar.transform.position = barPosition.position;

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
								HUD.Instance.Sanity += sanityPoints;
							}

							GameObject.Destroy(miniGameBar.gameObject);
						}

						PlayerMovement.Instance.EnableMoving(true);
						miniGameBar = null;
						miniGameState = MiniGameState.None;
					}
					break;
			}
		}
	}

}
