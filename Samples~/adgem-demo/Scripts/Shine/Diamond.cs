using UnityEngine;

public class Diamond : MonoBehaviour
{
	[SerializeField] private Transform[] anchors;
	[SerializeField] private Shine shine;

	private void Start()
	{
		ShineBright();
	}

	private void ShineBright()
	{
		shine.Play(anchors[Random.Range(0, anchors.Length)].position);
		Invoke(nameof(ShineBright), Random.Range(3f, 7f));
	}
}