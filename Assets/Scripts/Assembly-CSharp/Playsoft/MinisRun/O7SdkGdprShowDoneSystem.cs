using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class O7SdkGdprShowDoneSystem : ReactiveSystem<StateEntity>
	{
		public readonly StateContext stateContext;

		public readonly UIContext uiContext;

		public O7SdkGdprShowDoneSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkGdprRequest);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isO7SdkGdprDone;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.isO7SdkGdprDone)
				{
					if (GameIntroHelper.IsIntroTime())
					{
						ShowUserState();
					}
					else
					{
						ScreenId screenId = stateContext.activeScreen.ScreenId;
						uiContext.CloseDialog(screenId);
						if (!stateContext.sessionEntity.isO7SdkForceGdpr)
						{
							uiContext.OpenDialog<SettingsScreen>();
						}
					}
				}
				entity.isDestroyed = true;
			}
		}

		private void ShowUserState()
		{
			stateContext.CreateEntity().isUserStateShow = true;
		}
	}
}
