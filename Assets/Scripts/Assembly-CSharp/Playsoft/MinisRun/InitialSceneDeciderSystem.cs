using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class InitialSceneDeciderSystem : ReactiveSystem<UIEntity>
	{
		private readonly UIContext uiContext;

		public InitialSceneDeciderSystem(Contexts context)
			: base((IContext<UIEntity>)context.uI)
		{
			uiContext = context.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.IntialSceneDecider);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				string newName;
				if (GameIntroHelper.IsIntroTime())
				{
					SettingsService.SettingsSO.WorldIndex = 0;
					newName = ProjectConstants.INGAME_SCENE_NAME;
				}
				else
				{
					newName = ((!TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.PlayAgain) || TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.SelectCharacter)) ? ProjectConstants.HOME_SCENE_NAME : ProjectConstants.WORLD_SELECT_SCENE_NAME);
				}
				uiContext.CreateEntity().AddChangeScene(newName, true);
				entity.isDestroyed = true;
			}
		}
	}
}
