using System;
using System.Collections.Generic;
using UnityEngine;

namespace O7SDK
{
	public static class O7SDKTypesMarshaling
	{
		[Serializable]
		private struct JsonWrapper<T>
		{
			public List<T> list;
		}

		public const string EMPTY_JSON_ARRAY = "[]";

		public const string EMPTY_JSON_OBJECT = "{}";

		public static bool ToBoolean(string str)
		{
			bool result;
			return bool.TryParse(str ?? string.Empty, out result) && result;
		}

		public static int ToInt(string str)
		{
			int result;
			int.TryParse(str ?? string.Empty, out result);
			return result;
		}

		public static List<T> ToList<T>(string jsonArray)
		{
			string json = string.Format("{{\"list\":{0}}}", jsonArray);
			return JsonUtility.FromJson<JsonWrapper<T>>(json).list;
		}

		public static string ToStringOrNull<T>(this T? nullable) where T : struct
		{
			return (!nullable.HasValue) ? null : nullable.ToString();
		}

		public static T GetMarshalingComponent<T>(string gameObjectName) where T : MonoBehaviour
		{
			GameObject gameObject = GameObject.Find(gameObjectName);
			if (gameObject == null)
			{
				gameObject = new GameObject(gameObjectName);
				gameObject.hideFlags = HideFlags.HideInHierarchy;
				UnityEngine.Object.DontDestroyOnLoad(gameObject);
			}
			T val = gameObject.GetComponent<T>();
			if (val == null)
			{
				val = gameObject.AddComponent<T>();
			}
			return val;
		}

		public static ulong ToNullableFlags(bool? p0 = null, bool? p1 = null, bool? p2 = null, bool? p3 = null, bool? p4 = null, bool? p5 = null, bool? p6 = null, bool? p7 = null, bool? p8 = null, bool? p9 = null, bool? p10 = null, bool? p11 = null, bool? p12 = null, bool? p13 = null, bool? p14 = null, bool? p15 = null)
		{
			ulong num = 0uL;
			num |= ((p0 == true) ? 1uL : 0uL);
			num |= (uint)((p1 == true) ? 2 : 0);
			num |= (uint)((p2 == true) ? 4 : 0);
			num |= (uint)((p3 == true) ? 8 : 0);
			num |= (uint)((p4 == true) ? 16 : 0);
			num |= (uint)((p5 == true) ? 32 : 0);
			num |= (uint)((p6 == true) ? 64 : 0);
			num |= (uint)((p7 == true) ? 128 : 0);
			num |= (uint)((p8 == true) ? 256 : 0);
			num |= (uint)((p9 == true) ? 512 : 0);
			num |= (uint)((p10 == true) ? 1024 : 0);
			num |= (uint)((p11 == true) ? 2048 : 0);
			num |= (uint)((p12 == true) ? 4096 : 0);
			num |= (uint)((p13 == true) ? 8192 : 0);
			num |= (uint)((p14 == true) ? 16384 : 0);
			return num | (uint)((p15 == true) ? 32768 : 0);
		}
	}
}
