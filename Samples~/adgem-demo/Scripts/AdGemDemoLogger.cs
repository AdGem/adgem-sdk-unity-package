using TMPro;
using UnityEngine;

public class AdGemDemoLogger : MonoBehaviour
{
	[SerializeField] private TMP_Text prefab;
	[SerializeField] private Transform contentHolder;

	public void LogMessage(string message)
	{
		Debug.Log(message);

		var text = Instantiate(prefab, contentHolder);
		text.text = message;
		text.color = Color.white;
	}

	public void LogError(string message)
	{
		Debug.LogError(message);

		var text = Instantiate(prefab, contentHolder);
		text.text = message;
		text.color = Color.red;
	}
}