using UnityEngine;

public class Bedroom : MonoBehaviour
{
	[SerializeField] private GameObject windowRaining;
	[SerializeField] private GameObject windowCalm;

	protected void Start()
	{
		if (LevelManager.Instance.LevelType == LevelTypes.Bedroom)
		{
			windowRaining.SetActive(true);
			windowCalm.SetActive(false);
		}
		else
		{
			windowRaining.SetActive(false);
			windowCalm.SetActive(true);
		}
	}
}
