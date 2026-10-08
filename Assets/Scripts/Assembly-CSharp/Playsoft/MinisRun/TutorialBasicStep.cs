using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "TutorialStep", menuName = "Tutorial/TutorialStep")]
	public class TutorialBasicStep : TutorialBaseStep
	{
		private void OnEnable()
		{
			if (base.EnableTrigger != null)
			{
				base.EnableTrigger.OnTriggerChanged += EnableStep;
			}
			if (base.DisableTrigger != null)
			{
				base.DisableTrigger.OnTriggerChanged += DisableStep;
			}
		}

		private void EnableStep()
		{
			if (base.EnableTrigger.IsTriggered)
			{
				Contexts.sharedInstance.state.CreateEntity().AddTutorialStepEnableTriggered(this);
			}
		}

		private void DisableStep()
		{
			if (base.DisableTrigger.IsTriggered)
			{
				Contexts.sharedInstance.state.CreateEntity().AddTutorialStepDisableTriggered(this);
			}
		}

		private void OnDisable()
		{
			if (base.EnableTrigger != null)
			{
				base.EnableTrigger.OnTriggerChanged -= EnableStep;
			}
			if (base.DisableTrigger != null)
			{
				base.DisableTrigger.OnTriggerChanged -= DisableStep;
			}
		}
	}
}
