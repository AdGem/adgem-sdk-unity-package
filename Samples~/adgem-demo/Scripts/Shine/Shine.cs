using UnityEngine;

public class Shine : MonoBehaviour
{
	private const float HALF_TIME = 0.4f;
	private float _time;

	public void Play(Vector3 position)
	{
		transform.position = position;
		gameObject.SetActive(true);
		_time = 0;
		transform.localScale = Vector3.zero;
	}

	private void Update()
	{
		_time += Time.deltaTime;
		if (_time > HALF_TIME * 2)
		{
			gameObject.SetActive(false);
			return;
		}

		if (_time <= HALF_TIME)
			transform.localScale = _time / HALF_TIME * Vector3.one;
		else
			transform.localScale = (1 - (_time - HALF_TIME) / HALF_TIME) * Vector3.one;
	}
}