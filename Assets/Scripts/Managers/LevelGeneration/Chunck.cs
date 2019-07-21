using UnityEngine;

public class Chunck : MonoBehaviour
{
	[SerializeField, Range(0f, 1f)] private float difficulty;

	public float Difficulty => difficulty;
}
