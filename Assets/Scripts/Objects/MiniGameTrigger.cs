using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
	private enum MiniGameState
	{
		None , 
		ShowButton , 
		PlayGame
	}

	[SerializeField] MiniGameType miniGameType = MiniGameType.Book;
	[SerializeField] Transform target = null;

	private MiniGameButton miniGameButton = null;
	private BedroomMiniGame miniGameBar = null; 

	private MiniGameState miniGameState = MiniGameState.None; 

	public void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.CompareTag("Player"))
		{
			var buttonObject = ResourceManager.Instance.GetObject(ObjectType.MiniGameButton) ;
			if (buttonObject != null)
			{
				miniGameButton = buttonObject.GetComponent<MiniGameButton>();
				miniGameButton?.Initialize();

				if (target != null)
					miniGameButton.transform.position = target.position;

				miniGameState = MiniGameState.ShowButton; 
			}
		}
	}

	public void OnTriggerExit(Collider other)
	{
		if (miniGameButton != null)
		{
			miniGameButton.DestroyObject();
		}
	}

	private void Update()
	{
		if (Input.GetButtonDown("X"))
		{
			switch (miniGameState)
			{
				case MiniGameState.ShowButton:
					{
						if (miniGameButton != null)
						{
							miniGameButton.DestroyObject();
						}

						var gameBar = ResourceManager.Instance.GetObject(ObjectType.MiniGame);
						if (gameBar != null)
						{
							miniGameBar = gameBar.GetComponent<BedroomMiniGame>(); 

							switch (miniGameType)
							{

								case MiniGameType.Toy:
									{
										miniGameBar.Initialize( 1 , 10 , 1); 
									}
									break;
								case MiniGameType.Book:
									{
										miniGameBar.Initialize(2, 5, 1);
									}
									break;
							}

						}

						miniGameState = MiniGameState.PlayGame;  
					}
					break;
				case MiniGameState.PlayGame:
					{
						if( miniGameBar !=null )
						{
							miniGameBar.Stop();
							bool result  = miniGameBar.GetResult(); 
							if( result )
							{
								// Do something to insanity bar
							}
						}
					}
					break; 
			}

			
		}
	}
	
}
