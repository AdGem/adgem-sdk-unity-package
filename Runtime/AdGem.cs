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
		/// Identifies whether AdGem is ready to show offer wall via the <see cref="ShowOfferwall"/>
		/// </summary>
		/// <returns>True if AdGem is ready to show offer wall, false otherwise.</returns>
		public static bool IsOfferwallReady()
		{
			if (_implementation == null)
				return false;

			return _implementation.IsOfferwallReady();
		}

		/// <summary>
		/// Get detailed state of the offer wall.
		/// </summary>
		/// <returns>offer wall state as defined in <see cref="State"/></returns>
		public static State GetOfferwallState()
		{
			if (_implementation == null)
				return State.DISABLED;

			return _implementation.GetOfferwallState();
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

		/// <summary>
		/// Returns last known error.
		/// </summary>
		/// <returns>last known error. Error state is notified via <see cref="OfferwallDelegate.OnLoadingFailed"/></returns>
		public static string GetError()
		{
			if (_implementation == null)
				return string.Empty;

			return _implementation.GetError();
		}

		private static readonly IAdGem? _implementation
#if UNITY_ANDROID && !UNITY_EDITOR
				= new AdGemAndroid(OfferwallCallback)
#elif UNITY_IOS && !UNITY_EDITOR
				= new AdGemIos()
#endif
			;
#nullable disable
	}
}