using O7SDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class GenderGateScreen : BaseScreen
	{
		public override ScreenId Id
		{
			get
			{
				return ScreenId.GenderGateScreen;
			}
		}

		public void GenderSelected(int gender)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log((O7SDKGateManager.Gender)gender);
			}
			StateEntity stateEntity = Contexts.sharedInstance.state.CreateEntity();
			stateEntity.AddO7SdkGenderGateSelected((O7SDKGateManager.Gender)gender);
		}
	}
}
