using UnityEngine;
using UnityEngine.Scripting;

namespace AdGemUnity.Runtime
{
	[Preserve]
	public class AdGemCallbackProxy : AndroidJavaProxy
	{
		private readonly OfferwallDelegate _offerwallDelegate;

		internal AdGemCallbackProxy(OfferwallDelegate offerwallDelegate)
			: base("com.adgem.unitybridge.AdGemCallbackProxy")
		{
			_offerwallDelegate = offerwallDelegate;
		}

		[Preserve]
		public void onOfferwallLoadingStarted()
		{
			AdGemAsyncCallbackHelper.Instance.Queue(() =>
			{
				_offerwallDelegate.OnLoadingStarted.Invoke();
			});
		}

		[Preserve]
		public void onOfferwallLoadingFinished()
		{
			AdGemAsyncCallbackHelper.Instance.Queue(() =>
			{
				_offerwallDelegate.OnLoadingFinished.Invoke();
			});
		}

		[Preserve]
		public void onOfferwallLoadingFailed(string error)
		{
			AdGemAsyncCallbackHelper.Instance.Queue(() =>
			{
				_offerwallDelegate.OnLoadingFailed.Invoke(error);
			});
		}

		[Preserve]
		public void onOfferwallRewardReceived(int amount)
		{
			AdGemAsyncCallbackHelper.Instance.Queue(() =>
			{
				_offerwallDelegate.OnRewardReceived.Invoke(amount);
			});
		}

		[Preserve]
		public void onOfferwallClosed()
		{
			AdGemAsyncCallbackHelper.Instance.Queue(() =>
			{
				_offerwallDelegate.OnClosed.Invoke();
			});
		}
	}
}