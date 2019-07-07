using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class BedroomMiniGame : MonoBehaviour
{

	[SerializeField] private RectTransform rectTransform = null;
	[SerializeField] private RectTransform innerBoxRectTransform = null;
	[SerializeField] private Transform target = null; 

	[SerializeField] private float animationDuration = 1;
	[SerializeField] private Ease animationEase = Ease.Linear;
	[SerializeField] private float aimMaxPercent = 10;

	private Sequence sequenceAnim = null;
	
    // Start is called before the first frame update
    void Start()
    {
		Initialize( ); 

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Initialize( float sizePercent = 1 , float animPercent = 10 ,  float duration = 1  , Ease ease = Ease.Linear )
	{
		animationDuration = duration;
		animationEase = ease;
		sequenceAnim = null;
		aimMaxPercent = 10; 

		if (rectTransform != null)
		{
			float width = rectTransform.rect.width * sizePercent;
			rectTransform.sizeDelta = new Vector2(width, rectTransform.rect.height);
		}

		if( innerBoxRectTransform != null )
		{
			float innerBoxHalfWidth = innerBoxRectTransform.rect.width / 2 ; 
			if( target != null )
			{
				Vector3 position = target.localPosition;
				//				position.x = UnityEngine.Random.Range(-innerBoxHalfWidth, innerBoxHalfWidth); 
				position.x = -innerBoxHalfWidth; 
				target.localPosition = position;
				
				sequenceAnim = DOTween.Sequence();
				sequenceAnim.Append(target.DOLocalMoveX(target.localPosition.x + innerBoxRectTransform.rect.width, duration).SetEase(ease));
				sequenceAnim.Append(target.DOLocalMoveX(target.localPosition.x , duration).SetEase(ease));
				sequenceAnim.SetLoops(-1);

			}
		}


	}

	public void Stop()
	{
		if(sequenceAnim != null)
		{
			sequenceAnim.Kill();
			sequenceAnim = null; 
		}

		Debug.Log( GetResult().ToString()) ; 
	}

	public bool GetResult()
	{
		float targetLastXPos = target.localPosition.x;
		float aimDistance = aimMaxPercent * innerBoxRectTransform.rect.width / 2;

		Debug.Log(targetLastXPos + "  " + aimDistance);
		if (targetLastXPos > -aimDistance && targetLastXPos < aimDistance)
			return true;

		return false; 
	}
}
