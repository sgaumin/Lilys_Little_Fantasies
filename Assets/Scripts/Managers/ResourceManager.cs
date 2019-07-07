using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class ResourceManager : MonoBehaviour
{
	static public ResourceManager Instance { private set; get; }

	[SerializeField] private List<GameObject> prefabs = null;

	private Dictionary<string, List<GameObject>> objectList = new Dictionary<string, List<GameObject>>();

	protected void Awake()
	{
		Instance = this;
	}


	private GameObject GetPrefab(string typeName)
	{
		return prefabs?.Find(x => x.name == typeName);
	}

	public GameObject GetObject<T>(T type) where T : struct, IConvertible
	{
		string typeName = type.ToString();
		if (!objectList.ContainsKey(typeName))
			objectList.Add(typeName, new List<GameObject>());

		return GetObject(objectList[typeName], typeName);
	}


	public M GetObject<M, T>(T type)
		where M : MonoBehaviour
		where T : struct, IConvertible
	{
		string typeName = type.ToString();
		if (!objectList.ContainsKey(typeName))
			objectList.Add(typeName, new List<GameObject>());

		GameObject obj = GetObject(objectList[typeName], typeName);
		return obj?.GetComponent<M>();
	}

	private GameObject GetObject(List<GameObject> list, string typeName)
	{
		// if the pool is Empty You have to instantiate from a prefab 
		if (list.Count == 0)
		{
			GameObject prefab = GetPrefab(typeName);

			if (prefab != null)
			{
				// Instantiating from a prefab
				GameObject newObject = Instantiate(prefab.gameObject);
				newObject.transform.SetParent(transform, false);
				newObject.transform.localScale = Vector3.one;
				return newObject;
			}
			else // Return ull if there is no prefab in the list with type == objectType
				return default(GameObject);
		}
		else
		{
			// if the pool is not empty extract one and return it back
			int lastCellIndex = list.Count - 1;
			GameObject obj = list[lastCellIndex];
			list.RemoveAt(lastCellIndex);
			obj.SetActive(true);
			return obj;
		}

	}

	// This functions return the objects back to the pool so they can be used for further need 
	public void ReturnToRepository<T>(GameObject obj, T type) where T : struct, IConvertible
	{
		if (obj == null)
			return;
		// change objects parent and reset it
		obj.transform.SetParent(this.transform);
		obj.gameObject.SetActive(false);

		// add object to its related list

		string typeName = type.ToString();

		if (!objectList.ContainsKey(typeName))
			objectList.Add(typeName, new List<GameObject>());

		objectList[typeName].Add(obj.gameObject);
	}
}