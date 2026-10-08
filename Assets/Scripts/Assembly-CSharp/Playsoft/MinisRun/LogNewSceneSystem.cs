using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class LogNewSceneSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public LogNewSceneSystem(Contexts contexts)
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
			if (!stateContext.activeScreen.ScreenId.Equals(ScreenId.Undefined) && !stateContext.activeScreen.ScreenId.Equals(stateContext.activeScreenEntity.lastLoggedScreen.ScreenId))
			{
				LogSceneTransition();
				if (stateContext.activeScreen.ScreenId.Equals(ScreenId.ShopScreen))
				{
					LogTransitionToIAPScreen();
				}
				stateContext.activeScreenEntity.ReplaceLastLoggedScreen(stateContext.activeScreen.ScreenId);
				stateContext.activeScreenEntity.ReplaceLastSceneTransitionTime(DateTime.Now);
				stateContext.activeScreenEntity.ReplaceLastSceneFramesCount(Time.frameCount);
			}
		}

		private void LogSceneTransition()
		{
			ScreenId screenId = stateContext.activeScreenEntity.lastLoggedScreen.ScreenId;
			DateTime transitionTime = stateContext.activeScreenEntity.lastSceneTransitionTime.TransitionTime;
			int num = (stateContext.activeScreenEntity.hasLastSceneFramesCount ? stateContext.activeScreenEntity.lastSceneFramesCount.FramesCount : 0);
			double totalMilliseconds = (DateTime.Now - transitionTime).TotalMilliseconds;
			int num2 = Time.frameCount - num;
			float num3 = ((!stateContext.sessionEntity.hasTransitionInformation) ? 0f : stateContext.sessionEntity.transitionInformation.TransitionDuration);
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			stateEntity.AddEventCustom("screen-enter", "screen", screenId.ToString(), stateContext.activeScreen.ScreenId.ToString(), num2, num3, "undefined", totalMilliseconds, null, true);
		}

		private void LogTransitionToIAPScreen()
		{
			ScreenId screenId = stateContext.activeScreenEntity.lastLoggedScreen.ScreenId;
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			stateEntity.AddEventCustom("screen-enter", "screen-iap", null, screenId.ToString(), null, null, null, null, null, false);
		}
	}
}
