using UnityEngine;

public enum MoveDirections
{
	Right,
	Left,
	Up,
	Down
}

public class MovingInDirection : MonoBehaviour
{
	public MoveDirections moveDirection = MoveDirections.Left;

	private Vector3 direction;

	protected void Start()
	{
		switch (moveDirection)
		{
			case MoveDirections.Right:
				direction = Vector3.right;
				break;
			case MoveDirections.Left:
				direction = Vector3.left;
				break;
			case MoveDirections.Up:
				direction = Vector3.up;
				break;
			case MoveDirections.Down:
				direction = Vector3.down;
				break;
			default:
				break;
		}
	}

	private void Update()
		=> transform.position += direction * LevelGenerator.Instance.ChunckSpeed * Time.deltaTime;
}
