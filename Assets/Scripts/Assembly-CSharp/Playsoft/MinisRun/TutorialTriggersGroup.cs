using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "TriggersGroup", menuName = "Tutorial/Triggers/TriggersGroup")]
	public sealed class TutorialTriggersGroup : TutorialTrigger
	{
		private enum Conditions
		{
			All = 0,
			Any = 1,
			Not = 2
		}

		[SerializeField]
		private Conditions condition;

		[SerializeField]
		private List<TutorialTrigger> triggers;

		public List<TutorialTrigger> Triggers
		{
			get
			{
				return triggers;
			}
		}

		private bool GetTriggerState()
		{
			if (triggers.Count == 0)
			{
				return false;
			}
			switch (condition)
			{
			case Conditions.All:
			{
				for (int num3 = triggers.Count - 1; num3 >= 0; num3--)
				{
					TutorialTrigger tutorialTrigger3 = triggers[num3];
					if (!(tutorialTrigger3 != null) || !tutorialTrigger3.IsTriggered)
					{
						return false;
					}
				}
				return true;
			}
			case Conditions.Any:
			{
				for (int num2 = triggers.Count - 1; num2 >= 0; num2--)
				{
					TutorialTrigger tutorialTrigger2 = triggers[num2];
					if (tutorialTrigger2 != null && tutorialTrigger2.IsTriggered)
					{
						return true;
					}
				}
				return false;
			}
			case Conditions.Not:
			{
				for (int num = triggers.Count - 1; num >= 0; num--)
				{
					TutorialTrigger tutorialTrigger = triggers[num];
					if (tutorialTrigger != null && tutorialTrigger.IsTriggered)
					{
						return false;
					}
				}
				return true;
			}
			default:
				return false;
			}
		}

		private void OnEnable()
		{
			if (triggers == null)
			{
				return;
			}
			if (triggers.Count == 0)
			{
				Debug.LogWarning("[Tutorial] Triggers list is empty");
				return;
			}
			foreach (TutorialTrigger trigger in triggers)
			{
				if (!(null == trigger))
				{
					trigger.OnTriggerChanged += TriggerChanged;
				}
			}
		}

		private void TriggerChanged()
		{
			base.IsTriggered = GetTriggerState();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			foreach (TutorialTrigger trigger in triggers)
			{
				if (!(trigger == null))
				{
					trigger.OnTriggerChanged -= TriggerChanged;
				}
			}
		}
	}
}
