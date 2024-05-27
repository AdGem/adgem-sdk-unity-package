using UnityEditor.Android;

public class AdGemBuildPostprocess : IPostGenerateGradleAndroidProject
{
	public int callbackOrder { get; } = 117;

	public void OnPostGenerateGradleAndroidProject(string path)
	{
	}
}