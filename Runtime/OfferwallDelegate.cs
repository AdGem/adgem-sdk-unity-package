using UnityEngine.Events;

namespace AdGemUnity.Runtime
{
	/// <summary>
	/// Class encapsulating offerwall callbacks.
	/// </summary>
	public class OfferwallDelegate
	{
		/// <summary>
		/// Notifies that the offer wall loading has started.
		/// </summary>
		public UnityEvent OnLoadingStarted { get; } = new UnityEvent();

		/// <summary>
		/// Notifies that the offer wall has been loaded.
		/// </summary>
		public UnityEvent OnLoadingFinished { get; } = new UnityEvent();

		/// <summary>
		/// Notifies that the offer wall has failed to load.
		/// </summary>
		public UnityEvent<string> OnLoadingFailed { get; } = new UnityEvent<string>();

		/// <summary>
		/// Notifies to reward user for the action they performed in the offer wall.
		/// </summary>
		public UnityEvent<int> OnRewardReceived { get; } = new UnityEvent<int>();

		/// <summary>
		/// Notifies that the offer wall has been closed.
		/// </summary>
		public UnityEvent OnClosed { get; } = new UnityEvent();
	}
}