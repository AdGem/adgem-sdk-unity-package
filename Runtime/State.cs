namespace AdGemUnity.Runtime
{
	/// <summary>
	/// Possible states of the Offerwall.
	/// </summary>
	public enum State
	{
		DISABLED = -1,
		NEEDS_INITIALIZATION = 0,
		INITIALIZING = 1,
		NEEDS_CAMPAIGN_REFRESH = 2,
		REFRESHING_CAMPAIGN = 3,
		NEEDS_DOWNLOAD = 4,
		DOWNLOADING = 5,
		READY = 6
	}
}