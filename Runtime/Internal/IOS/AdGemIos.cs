namespace AdGem.Runtime
{
	internal class AdGemIos : IAdGem
	{
		internal AdGemIos()
		{
			//TODO: bind offerwall callbacks
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