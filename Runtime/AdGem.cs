namespace AdGem.Runtime
{
	/// <summary>
	/// Main entry point for communications with AdGem SDK.
	/// </summary>
	public class AdGem
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

		/// <summary>
		/// Callback to receive updates about the offer wall.
		/// </summary>
		public static OfferwallDelegate OfferwallCallback { get; } = new OfferwallDelegate();

		/// <summary>
		/// Initializes the SDK.
		///
		/// Should be called before any other calls to the SDK are made.
		/// </summary>
		public static void Init()
		{
		}

		/// <summary>
		/// Identifies whether AdGem is ready to show offer wall via the <see cref="ShowOfferwall"/>
		/// </summary>
		/// <returns>True if AdGem is ready to show offer wall, false otherwise.</returns>
		public static bool IsOfferwallReady()
		{
			return false;
		}

		/// <summary>
		/// Get detailed state of the offer wall.
		/// </summary>
		/// <returns>offer wall state as defined in <see cref="State"/></returns>
		public static State GetOfferwallState()
		{
			return State.DISABLED;
		}

		/// <summary>
		/// Shows offer wall.
		/// Offer wall status callbacks (including even when offer wall is closed) will be delivered via the <see cref="OfferwallCallback"/>
		/// </summary>
		public static void ShowOfferwall()
		{
		}

		/// <summary>
		/// Returns last known error.
		/// </summary>
		/// <returns>last known error. Error state is notified via <see cref="OfferwallDelegate.OnLoadingFailed"/></returns>
		public static string GetError()
		{
			return string.Empty;
		}

		/// <summary>
		/// Sets additional metadata specific to the user of this application.
		/// </summary>
		/// <param name="metadata">Extra information about the player.</param>
		public static void SetPlayerMetaData(PlayerMetadata metadata)
		{
		}
	}
}