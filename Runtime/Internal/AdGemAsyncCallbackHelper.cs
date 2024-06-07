using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdGemUnity.Runtime
{
	internal class AdGemAsyncCallbackHelper : MonoBehaviour
	{
#nullable enable
		private readonly object _queueLock = new object();
		private readonly List<Action> _queuedActions = new List<Action>();
		private readonly List<Action> _executingActions = new List<Action>();
		private static AdGemAsyncCallbackHelper? _instance;

		private const string TAG = nameof(AdGemAsyncCallbackHelper);

		public static AdGemAsyncCallbackHelper Instance
		{
			get
			{
				if (_instance != null)
					return _instance;

				_instance = new GameObject(nameof(AdGemAsyncCallbackHelper))
					.AddComponent<AdGemAsyncCallbackHelper>();

				DontDestroyOnLoad(_instance.gameObject);

				return _instance;
			}
		}

		public void Queue(Action? action)
		{
			if (action == null)
			{
				Debug.LogWarning($"{TAG} Trying to queue null action");
				return;
			}

			if (_instance == null)
			{
				Debug.LogWarning($"{TAG} Instance is null. Will not queue action.");
				return;
			}

			lock (_instance._queueLock)
			{
				_instance._queuedActions.Add(action);
			}
		}

		private void Update()
		{
			MoveQueuedActionsToExecuting();
			while (_executingActions.Count > 0)
			{
				var action = _executingActions[0];
				_executingActions.RemoveAt(0);
				action();
			}
		}

		private void MoveQueuedActionsToExecuting()
		{
			lock (_queueLock)
			{
				while (_queuedActions.Count > 0)
				{
					var action = _queuedActions[0];
					_executingActions.Add(action);
					_queuedActions.RemoveAt(0);
				}
			}
		}
#nullable disable
	}
}