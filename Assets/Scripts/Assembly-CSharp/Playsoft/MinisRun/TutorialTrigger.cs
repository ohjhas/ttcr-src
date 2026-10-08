using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public abstract class TutorialTrigger : ScriptableObject
	{
		[SerializeField]
		private bool isTriggered;

		public bool IsEnabled { get; set; }

		public bool IsTriggered
		{
			get
			{
				return IsEnabled && isTriggered;
			}
			set
			{
				if (IsEnabled)
				{
					isTriggered = value;
					NotifyTriggered();
				}
			}
		}

		public TutorialBaseStep Step { get; set; }

		public event Action OnTriggerChanged;

		private void NotifyTriggered()
		{
			if (this.OnTriggerChanged != null)
			{
				this.OnTriggerChanged();
			}
		}

		protected virtual void OnDisable()
		{
			isTriggered = false;
		}
	}
}
