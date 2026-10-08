using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class LogScreenChangedSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public LogScreenChangedSystem(Contexts contexts)
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
			return uiContext.uIEntity.isNewSceneLoaded;
		}

		protected override void Execute(List<StateEntity> entities)
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
