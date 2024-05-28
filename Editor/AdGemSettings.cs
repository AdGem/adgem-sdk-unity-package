using System.IO;
using UnityEditor;
using UnityEngine;

namespace AdGem.Editor
{
	public class AdGemSettings : ScriptableObject
	{
		[SerializeField]
		public string AppId;
		[SerializeField]
		public bool IsDebug;

		internal static AdGemSettings GetInstance()
		{
			const string fileName = "AdGemSettings";
			var fullPath = $"Assets/Resources/AdGem/{fileName}.asset";
			Directory.CreateDirectory(Path.GetDirectoryName(fullPath)!);
			var asset = Resources.Load<AdGemSettings>($"AdGem/{fileName}");
			if (asset != null)
				return asset;

			Debug.Log("Creating AdGem settings asset...");
			asset = CreateInstance<AdGemSettings>();

			AssetDatabase.CreateAsset(asset, fullPath);
			AssetDatabase.SaveAssets();

			return asset;
		}
	}
}