using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
	public static LevelGenerator Instance { get; private set; }

	[SerializeField] private GameObject[] chuncks;
	[SerializeField] private Transform spawn;
	[SerializeField] private float chunckSpeed;
	[SerializeField] private float difficultyFactor = 1.1f;


	public float ChunckSpeed { get; private set; }

	protected void Awake() => Instance = this;

	protected void Start()
		=> ChunckSpeed = chunckSpeed + Mathf.Log(LevelManager.Instance.DaysToFinish - GameData.DayCount + 1, 2f) * difficultyFactor;


	public void Generate()
		=> Instantiate(chuncks[Random.Range(0, chuncks.Length)], spawn.transform.position, Quaternion.identity, transform);
}
