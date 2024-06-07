using UnityEngine;

public class Rotator : MonoBehaviour
{
	[SerializeField] private Transform trans;
	[SerializeField] private float speed;

	private void Update()
	{
		trans.Rotate(0, 0 , Time.deltaTime * speed);
	}
}