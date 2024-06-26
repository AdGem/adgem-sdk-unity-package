using System;
using UnityEngine;

namespace AdGemUnity.Runtime
{
	internal class AdGemAndroid : IAdGem
	{
		private readonly AdGemCallbackProxy _proxy;
		private readonly AndroidJavaClass _bridgeClass;

		internal AdGemAndroid(OfferwallDelegate offerwallDelegate)
		{
			_proxy = new AdGemCallbackProxy(offerwallDelegate);
			_bridgeClass = new AndroidJavaClass("com.adgem.unitybridge.Bridge");

			_bridgeClass.CallStatic("registerOfferwallCallback", AndroidUtils.Activity, _proxy);
		}

		~AdGemAndroid()
		{
			_bridgeClass.CallStatic("unregisterOfferwallCallback", AndroidUtils.Activity, _proxy);
		}

		public void ShowOfferwall()
		{
			_bridgeClass.CallStatic("showOfferwall", AndroidUtils.Activity);
		}

		public void SetPlayerMetaData(PlayerMetadata metadata)
		{
			var builder = _bridgeClass.CallStatic<AndroidJavaObject>("getMetadataBuilder", metadata.id);
			if (metadata.gender != PlayerMetadata.Gender.UNKNOWN)
				_bridgeClass.CallStatic("setGender", builder, (int) metadata.gender);
			if (metadata.age >= 0)
				builder.Call<AndroidJavaObject>("age", metadata.age);
			if (metadata.level >= 0)
				builder.Call<AndroidJavaObject>("level", metadata.level);
			if (metadata.placement >= 0)
				builder.Call<AndroidJavaObject>("placement", metadata.placement);
			if (metadata.iapTotalUsd >= 0)
				builder.Call<AndroidJavaObject>("iapTotalUsd", metadata.iapTotalUsd);
			if (metadata.createdAt > DateTime.MinValue)
				builder.Call<AndroidJavaObject>("createdAt", metadata.createdAt.ToString("YYYY-MM-dd HH:mm:ss"));

			builder.Call<AndroidJavaObject>("isPayer", metadata.isPayer);

			if (metadata.customFields.Count > 0)
			{
				var javaFields = metadata.customFields.ToJavaList(value => value);
				_bridgeClass.CallStatic("setCustomFields", builder, javaFields);
			}

			_bridgeClass.CallStatic("setPlayerMetaData", builder);
		}
	}
}