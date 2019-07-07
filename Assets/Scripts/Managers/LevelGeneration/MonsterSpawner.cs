using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
	[SerializeField] private Monster[] monsterPrefabs;

	private void Start()
		=> Instantiate(monsterPrefabs[Random.Range(0, monsterPrefabs.Length)], transform.position, Quaternion.identity, transform);
}
