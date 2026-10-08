using System.Collections.Generic;
using Entitas;
using O7SDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class NewSceneBannerSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public NewSceneBannerSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.NewSceneLoaded.Added(), UIMatcher.ChangeScene.Added());
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			if (entities.Count == 1 && !entities.SingleEntity().hasChangeScene && (stateContext.activeScreen.ScreenId.Equals(ScreenId.FailScreen) || stateContext.activeScreen.ScreenId.Equals(ScreenId.WinScreen)))
			{
				ShowBanner();
			}
			else
			{
				HideBanner();
			}
		}

		private void ShowBanner()
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.CreateEntity();
			stateEntity.isO7SdkCall = true;
			stateEntity.isO7SdkBannerAdShow = true;
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("showBanner screen=" + stateContext.activeScreen.ScreenId);
			}
		}

		private void HideBanner()
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.CreateEntity();
			stateEntity.isO7SdkCall = true;
			stateEntity.isO7SdkBannerAdHide = true;
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("hideBanner screen=" + stateContext.activeScreen.ScreenId);
			}
		}
	}
}
