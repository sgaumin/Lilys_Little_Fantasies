using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class MiniGameButton : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer = null;
	[SerializeField] private float animatinoDuration = 1;

	private Tweener spriteAnimation = null; 
	
	public void Initialize()
	{
		if(spriteRenderer != null )
		{
			Color color = spriteRenderer.color;
			color.a = 0;
			spriteRenderer.color = color;

			spriteAnimation = spriteRenderer.DOFade( 1 ,  animatinoDuration ).SetLoops( 8 , LoopType.Yoyo);
			
		}
	}

	public void DestroyObject()
	{
		if( spriteAnimation != null )
		{
			spriteAnimation.Kill(); 
		}

		GameObject.Destroy(this.gameObject); 
	}


}
