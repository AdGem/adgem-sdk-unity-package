using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdGem.Runtime
{
	internal static class AndroidUtils
	{
		private static AndroidJavaObject _activity;

		internal static AndroidJavaObject Activity
		{
			get
			{
				if (_activity != null)
					return _activity;

				var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				_activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

				return _activity;
			}
		}

		internal static AndroidJavaObject ToJavaList<T, TJAVA>(this List<T> items, Func<T, TJAVA> converter)
		{
			var list = new AndroidJavaObject("java.util.ArrayList");

			if (items == null || items.Count == 0)
			{
				return list;
			}

			foreach (var item in items)
			{
				list.Call<bool>("add", converter(item));
			}

			return list;
		}
	}
}