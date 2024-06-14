using System.Collections.Generic;
#if UNITY_IOS && !UNITY_EDITOR
using System;
using System.Runtime.InteropServices;
#endif

namespace AdGemUnity.Runtime
{
	internal class AdGemIos : IAdGem
	{
		internal AdGemIos(OfferwallDelegate offerwallDelegate)
		{
#if UNITY_IOS && !UNITY_EDITOR
			Action loadStarted = () => { offerwallDelegate.OnLoadingStarted.Invoke(); };
			Action loadFinished = () => { offerwallDelegate.OnLoadingFinished.Invoke(); };
			Action<string> loadError = error => { offerwallDelegate.OnLoadingFailed.Invoke(error); };
			Action<int> rewardReceived = reward => { offerwallDelegate.OnRewardReceived.Invoke(reward); };
			Action closed = () => { offerwallDelegate.OnClosed.Invoke(); };

			_initDelegate(
				IosUtils.ActionVoidCallback, loadStarted.GetPointer(),
				IosUtils.ActionVoidCallback, loadFinished.GetPointer(),
				IosUtils.ActionStringCallback, loadError.GetPointer(),
				IosUtils.ActionIntCallback, rewardReceived.GetPointer(),
				IosUtils.ActionVoidCallback, closed.GetPointer());
#endif
		}

		~AdGemIos()
		{
#if UNITY_IOS && !UNITY_EDITOR
			_unregisterDelegate();
#endif
		}

		public void ShowOfferwall()
		{
#if UNITY_IOS && !UNITY_EDITOR
			_showOfferwall();
#endif
		}

		public void SetPlayerMetaData(PlayerMetadata metadata)
		{
#if UNITY_IOS && !UNITY_EDITOR
			var dateString = metadata.createdAt != DateTime.MinValue
				? metadata.createdAt.ToString("YYYY-MM-dd HH:mm:ss")
				: string.Empty;

			var fields = metadata.customFields;

			_setMetadata(metadata.id, metadata.age, (int)metadata.gender, metadata.level, metadata.placement,
				dateString, metadata.isPayer, metadata.iapTotalUsd,
				GetItemAt(fields, 0), GetItemAt(fields, 1), GetItemAt(fields, 2), GetItemAt(fields, 3), GetItemAt(fields, 4));
#endif
		}

		private string GetItemAt(IReadOnlyList<string> items, int index)
		{
			return items.Count > index ? items[index] : string.Empty;
		}

#if UNITY_IOS && !UNITY_EDITOR
		[DllImport("__Internal")]
		private static extern void _unregisterDelegate();

		[DllImport("__Internal")]
		private static extern void _initDelegate(
			IosUtils.ActionVoidCallbackDelegate onLoadStarted, IntPtr onLoadStartedPtr,
			IosUtils.ActionVoidCallbackDelegate onLoadFinished, IntPtr onLoadFinishedPtr,
			IosUtils.ActionStringCallbackDelegate onLoadError, IntPtr onLoadErrorPtr,
			IosUtils.ActionIntCallbackDelegate onReward, IntPtr onRewardPtr,
			IosUtils.ActionVoidCallbackDelegate onClosed, IntPtr onClosedPtr);

		[DllImport("__Internal")]
		private static extern void _showOfferwall();

		[DllImport("__Internal")]
		private static extern void _setMetadata(string playerId, int age, int gender,
			int level, long placement, string createdAt, bool isPayer, float iapTotal,
			string custom1, string custom2, string custom3, string custom4, string custom5);
#endif
	}
}