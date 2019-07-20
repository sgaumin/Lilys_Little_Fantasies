using System.Collections;
using TMPro;
using UnityEngine;

public class DayScreen : MonoBehaviour
{
	[SerializeField] private float durationBeforeNextDay;

	private TextMeshProUGUI dayText;

	private void Start()
	{
		dayText = GetComponentInChildren<TextMeshProUGUI>();

		if (GameData.DayCount > 1)
		{
			dayText.text = GameData.DayCount.ToString() + " days before...";
		}
		else
		{
			dayText.text = GameData.DayCount.ToString() + " day before...";
		}

		StartCoroutine(LoadNextDay());
	}

	private IEnumerator LoadNextDay()
	{
		yield return new WaitForSeconds(durationBeforeNextDay);
		LevelLoader.Instance.LoadBedroom();
	}
}
