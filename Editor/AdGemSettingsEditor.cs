using UnityEditor;

namespace AdGem.Editor
{
	[CustomEditor(typeof(AdGemSettings))]
	public class AdGemSettingsEditor : UnityEditor.Editor
	{
		[MenuItem("Window/AdGem", false, 1000)]
		public static void Edit()
		{
			Selection.activeObject = AdGemSettings.GetInstance();
		}
	}
}