using System.Collections.Generic;
using Entitas;
using O7SDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class NewSceneInterstitialSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public NewSceneInterstitialSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.NewSceneLoaded);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
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
