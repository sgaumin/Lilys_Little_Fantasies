using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI title;
	[SerializeField] private TextMeshProUGUI start;
	[SerializeField] private TextMeshProUGUI bracket1;
	[SerializeField] private TextMeshProUGUI bracket2;

	private Sequence titleAnim;
	private Sequence startAnim;
	private Sequence bracketsAnim;

	protected void Start()
	{
		titleAnim = DOTween.Sequence().Append(title.transform.DOScale(1.2f, 1f)).Append(title.transform.DOScale(1f, 1f)).SetLoops(-1).Play();
		startAnim = DOTween.Sequence().Append(start.transform.DOScale(1.05f, 1f)).Append(start.transform.DOScale(1f, 1f)).SetLoops(-1).Play();
		bracketsAnim = DOTween.Sequence().Append(bracket1.transform.DOLocalMoveX(-520, 0.3f)).Append(bracket1.transform.DOLocalMoveX(-500, 0.3f)).Append(bracket1.transform.DOLocalMoveX(-500, 0.3f)).Append(bracket2.transform.DOLocalMoveX(520, 0.3f)).Append(bracket2.transform.DOLocalMoveX(500, 0.3f)).SetLoops(-1);
	}

	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GameData.Instance.InitializeData();
			LevelLoader.Instance.LoadNextLevel();
		}
	}
}
