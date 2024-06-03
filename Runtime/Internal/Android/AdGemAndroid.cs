namespace AdGem.Runtime
{
	internal class AdGemAndroid : IAdGem
	{
		internal AdGemAndroid()
		{
			//TODO: bind proxy
		}

		public bool IsOfferwallReady()
		{
			return false;
		}

		public State GetOfferwallState()
		{
			return State.DISABLED;
		}

		public void ShowOfferwall()
		{
		}

		public string GetError()
		{
			return string.Empty;
		}

		public void SetPlayerMetaData(PlayerMetadata metadata)
		{
		}
	}
}