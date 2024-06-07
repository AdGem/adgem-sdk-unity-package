using System;
using System.IO;
using System.Text;
using System.Xml;
using UnityEditor.Android;
using UnityEngine;

namespace AdGemUnity.Editor
{
	public class AdGemBuildPostprocess : IPostGenerateGradleAndroidProject
	{
		public int callbackOrder { get; } = 117;

		private const string NODE_NAME = "meta-data";
		private const string NAME_VALUE = "com.adgem.Config";
		private const string RESOURCE_VALUE = "@xml/adgem_config";

		public void OnPostGenerateGradleAndroidProject(string path)
		{
			try
			{
				if (!ModifyManifest(path))
					Debug.LogError("AdGem SDK will not work correctly.");

				var settings = AdGemSettings.GetInstance();
				if (string.IsNullOrEmpty(settings.AppId))
					Debug.LogError("App ID is not set in the AdGem Settings. AdGem SDK will not work correctly.");

				SaveConfigXml(path, settings);
			}
			catch (Exception e)
			{
				Debug.LogException(e);
				Debug.LogError("AdGem SDK will not work correctly");
			}
		}

		private bool ModifyManifest(string path)
		{
			var manifestPath = Path.Combine(path, "src/main/AndroidManifest.xml");
			if (!File.Exists(manifestPath))
			{
				Debug.LogError("Could not find Android Manifest file.");
				return false;
			}

			var manifest = new XmlDocument();
			using (var reader = new XmlTextReader(manifestPath))
			{
				if (!reader.Read())
				{
					Debug.LogError("Could not read Android Manifest file.");
					return false;
				}

				manifest.Load(reader);
			}

			var applicationElement = (XmlElement) manifest.SelectSingleNode("manifest/application");
			if (applicationElement == null)
			{
				Debug.LogError("Could not find application node in Android Manifest.");
				return false;
			}

			if (HasAddedNode(applicationElement))
				return true;

			AddNode(manifest, applicationElement);

			using (var writer = new XmlTextWriter(manifestPath, Encoding.UTF8) {Formatting = Formatting.Indented})
			{
				manifest.Save(writer);
			}

			return true;
		}

		private bool HasAddedNode(XmlElement applicationElement)
		{
			var hasNode = false;

			foreach (XmlNode childNode in applicationElement.ChildNodes)
			{
				if (childNode.Name != NODE_NAME)
					continue;

				var attributes = childNode.Attributes;
				if (attributes == null || attributes.Count < 1)
					continue;

				foreach (XmlAttribute attribute in attributes)
				{
					if (attribute.Value != NAME_VALUE)
						continue;

					hasNode = true;
					break;
				}
			}

			return hasNode;
		}

		private void AddNode(XmlDocument manifest, XmlElement applicationElement)
		{
			const string NAMESPACE = "http://schemas.android.com/apk/res/android";

			var node = applicationElement.AppendChild(manifest.CreateElement(NODE_NAME));

			var nameAttribute = manifest.CreateAttribute("android", "name", NAMESPACE);
			nameAttribute.Value = NAME_VALUE;

			var resourceAttribute = manifest.CreateAttribute("android", "resource", NAMESPACE);
			resourceAttribute.Value = RESOURCE_VALUE;

			node.Attributes!.Append(nameAttribute);
			node.Attributes!.Append(resourceAttribute);
		}

		private void SaveConfigXml(string path, AdGemSettings settings)
		{
			var configPath = Path.Combine(path, "src/main/res/xml/adgem_config.xml");
			Directory.CreateDirectory(Path.GetDirectoryName(configPath)!);

			const string CONTENT_FORMAT =
				"<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<adgem-configuration\napplicationId=\"{0}\"\nofferWallEnabled=\"true\"\nlockOrientation=\"true\"\ndebuggable=\"{1}\" />";
			var content = string.Format(CONTENT_FORMAT, settings.AppId, settings.IsDebug.ToString().ToLower());

			File.WriteAllText(configPath, content);
		}
	}
}