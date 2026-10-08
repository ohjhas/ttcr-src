using System;
using System.Collections;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class Coroutiner : MonoBehaviour
	{
		public static void StartAsyncOperation(Func<IEnumerator> action)
		{
			GameObject gameObject = new GameObject("Coroutiner");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			Coroutiner coroutiner = gameObject.AddComponent<Coroutiner>();
			coroutiner.SetAction(action);
		}

		public void SetAction(Func<IEnumerator> action)
		{
			StartCoroutine(PerformAction(action));
		}

		private IEnumerator PerformAction(Func<IEnumerator> action)
		{
			yield return action();
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}
}
