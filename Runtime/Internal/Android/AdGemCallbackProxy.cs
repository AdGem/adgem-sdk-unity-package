using UnityEngine;

namespace AdGem.Runtime
{
	internal class AdGemCallbackProxy : AndroidJavaProxy
	{
		private readonly OfferwallDelegate _offerwallDelegate;

		internal AdGemCallbackProxy(OfferwallDelegate offerwallDelegate)
			: base("com.adgem.unitybridge.AdGemCallbackProxy")
		{
			_offerwallDelegate = offerwallDelegate;
		}

		private void onOfferWallLoadingStarted()
		{
			AdGemAsyncCallbackHelper.Instance.Queue(() =>
			{
				_offerwallDelegate.OnLoadingStarted.Invoke();
			});
		}

		private void onOfferWallLoadingFinished()
		{
			AdGemAsyncCallbackHelper.Instance.Queue(() =>
			{
				_offerwallDelegate.OnLoadingFinished.Invoke();
			});
		}

		private void onOfferWallLoadingFailed(string error)
		{
			AdGemAsyncCallbackHelper.Instance.Queue(() =>
			{
				_offerwallDelegate.OnLoadingFailed.Invoke(error);
			});
		}

		private void onOfferWallRewardReceived(int amount)
		{
			AdGemAsyncCallbackHelper.Instance.Queue(() =>
			{
				_offerwallDelegate.OnRewardReceived.Invoke(amount);
			});
		}

		private void onOfferWallClosed()
		{
			AdGemAsyncCallbackHelper.Instance.Queue(() =>
			{
				_offerwallDelegate.OnClosed.Invoke();
			});
		}
	}
}