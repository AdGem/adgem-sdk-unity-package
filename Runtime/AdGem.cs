namespace AdGemUnity.Runtime
{
#nullable enable
	/// <summary>
	/// Main entry point for communications with AdGem SDK.
	/// </summary>
	public static class AdGem
	{
		/// <summary>
		/// Callback to receive updates about the offer wall.
		/// </summary>
		public static OfferwallDelegate OfferwallCallback { get; } = new OfferwallDelegate();

		/// <summary>
		/// Sets metadata specific to the user of this application.
		/// Important: has to be called before any other SDK calls are made.
		/// </summary>
		/// <param name="metadata">Extra information about the player.</param>
		public static void SetPlayerMetaData(PlayerMetadata metadata)
		{
			if (_implementation == null)
				return;

			_implementation.SetPlayerMetaData(metadata);
		}

		/// <summary>
		/// Shows offer wall.
		/// Offer wall status callbacks (including even when offer wall is closed) will be delivered via the <see cref="OfferwallCallback"/>
		/// </summary>
		public static void ShowOfferwall()
		{
			if (_implementation == null)
				return;

			_implementation.ShowOfferwall();
		}

		private static readonly IAdGem? _implementation
#if UNITY_ANDROID && !UNITY_EDITOR
				= new AdGemAndroid(OfferwallCallback)
#elif UNITY_IOS && !UNITY_EDITOR
				= new AdGemIos(OfferwallCallback)
#endif
			;
#nullable disable
	}
}