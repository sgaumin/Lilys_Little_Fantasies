using UnityEngine;

public class Bedroom : MonoBehaviour
{
	[Header("Windows")]
	[SerializeField] private GameObject windowRaining;
	[SerializeField] private GameObject windowCalm;

	[Header("Paintings")]
	[SerializeField] private GameObject paintingMonster;
	[SerializeField] private GameObject paintingEmpty;

	protected void Start()
	{
		if (LevelManager.Instance.LevelType == LevelTypes.Bedroom)
		{
			windowRaining.SetActive(true);
			windowCalm.SetActive(false);

			paintingMonster.SetActive(true);
			paintingEmpty.SetActive(false);
		}
		else
		{
			windowRaining.SetActive(false);
			windowCalm.SetActive(true);

			paintingMonster.SetActive(false);
			paintingEmpty.SetActive(true);
		}
	}
}
