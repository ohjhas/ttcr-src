using UnityEngine;
using UnityEngine.Events;

namespace Playsoft.MinisRun
{
	public class SimpleEvent : MonoBehaviour
	{
		[SerializeField]
		private bool executeOnAwake;

		[SerializeField]
		private UnityEvent simpleEvent;

		private void Awake()
		{
			if (executeOnAwake)
			{
				simpleEvent.Invoke();
			}
		}

		public void Execute()
		{
			simpleEvent.Invoke();
		}
	}
}
