namespace AdGem.Runtime
{
	internal interface IAdGem
	{
		public bool IsOfferwallReady();
		public State GetOfferwallState();
		public void ShowOfferwall();
		public string GetError();
		public void SetPlayerMetaData(PlayerMetadata metadata);
	}
}