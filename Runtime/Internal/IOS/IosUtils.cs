using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace AdGemUnity.Runtime
{
	internal static class IosUtils
	{
		internal static IntPtr GetPointer(this object obj)
		{
			return obj == null ? IntPtr.Zero : GCHandle.ToIntPtr(GCHandle.Alloc(obj));
		}

		private static T Cast<T>(this IntPtr instancePtr)
		{
			var instanceHandle = GCHandle.FromIntPtr(instancePtr);
			if (!(instanceHandle.Target is T castedTarget))
			{
				throw new InvalidCastException("Failed to cast IntPtr");
			}

			return castedTarget;
		}

		[MonoPInvokeCallback(typeof(ActionVoidCallbackDelegate))]
		internal static void ActionVoidCallback(IntPtr actionPtr)
		{
			if (Debug.isDebugBuild)
			{
				Debug.Log("ActionVoidCallback");
			}

			if (actionPtr != IntPtr.Zero)
			{
				var action = actionPtr.Cast<Action>();
				action();
			}
		}

		[MonoPInvokeCallback(typeof(ActionStringCallbackDelegate))]
		internal static void ActionStringCallback(IntPtr actionPtr, string data)
		{
			if (Debug.isDebugBuild)
			{
				Debug.Log("ActionStringCallback: " + data);
			}

			if (actionPtr != IntPtr.Zero)
			{
				var action = actionPtr.Cast<Action<string>>();
				action(data);
			}
		}

		[MonoPInvokeCallback(typeof(ActionIntCallbackDelegate))]
		internal static void ActionIntCallback(IntPtr actionPtr, int data)
		{
			if (Debug.isDebugBuild)
			{
				Debug.Log("ActionIntCallback: " + data);
			}

			if (actionPtr != IntPtr.Zero)
			{
				var action = actionPtr.Cast<Action<int>>();
				action(data);
			}
		}

		internal delegate void ActionVoidCallbackDelegate(IntPtr actionPtr);

		internal delegate void ActionStringCallbackDelegate(IntPtr actionPtr, string data);

		internal delegate void ActionIntCallbackDelegate(IntPtr actionPtr, int data);
	}
}