using System.Collections.Generic;
using Entitas;
using O7SDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ScreenChangedBannerSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public ScreenChangedBannerSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			uiContext = contexts.uI;
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.ActiveScreen.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return uiContext != null && uiContext.uIEntity != null && uiContext.uIEntity.isNewSceneLoaded;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			if (stateContext.activeScreen.ScreenId.Equals(ScreenId.FailScreen) || stateContext.activeScreen.ScreenId.Equals(ScreenId.WinScreen))
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
