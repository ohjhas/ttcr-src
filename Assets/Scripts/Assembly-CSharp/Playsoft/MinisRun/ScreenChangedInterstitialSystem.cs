using System.Collections.Generic;
using Entitas;
using O7SDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ScreenChangedInterstitialSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public ScreenChangedInterstitialSystem(Contexts contexts)
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
			if (!stateContext.previousScreen.ScreenId.Equals(ScreenId.Undefined) && !stateContext.activeScreen.ScreenId.Equals(ScreenId.Undefined))
			{
				ShowIntersitial();
			}
			else if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Concat("undefined screen screen1=", stateContext.previousScreen.ScreenId, " screen2=", stateContext.activeScreen.ScreenId));
			}
		}

		private void ShowIntersitial()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			stateEntity.AddO7SdkInterstitialAd(stateContext.previousScreen.ScreenId.ToString(), stateContext.activeScreen.ScreenId.ToString());
		}
	}
}
