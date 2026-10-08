using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class O7SdkAutoNewsShowSystem : ReactiveSystem<UIEntity>
	{
		public readonly StateContext stateContext;

		public readonly UIContext uiContext;

		public O7SdkAutoNewsShowSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
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
			foreach (UIEntity entity in entities)
			{
				if (stateContext.activeScreen.ScreenId.Equals(ScreenId.HomeScreen) && stateContext.o7SdkEntity.isO7SdkAutoNewsRequest && !stateContext.sessionEntity.isGameFirstLaunch && TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.EndTutorial))
				{
					ShowAutoNews();
				}
			}
		}

		private void ShowAutoNews()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			stateEntity.AddO7SdkCrossPromo(true, false);
		}
	}
}
