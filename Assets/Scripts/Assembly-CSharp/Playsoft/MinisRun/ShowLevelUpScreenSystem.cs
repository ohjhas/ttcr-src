using Entitas;

namespace Playsoft.MinisRun
{
	public class ShowLevelUpScreenSystem : IExecuteSystem, ISystem
	{
		private readonly UIContext uiContext;

		private readonly StateContext stateContext;

		private readonly IGroup<StateEntity> levelUpPopupsQueue;

		public ShowLevelUpScreenSystem(Contexts contexts)
		{
			uiContext = contexts.uI;
			stateContext = contexts.state;
			levelUpPopupsQueue = stateContext.GetGroup(StateMatcher.PlayerLevelUp);
		}

		public void Execute()
		{
			if (!CanExecute())
			{
				return;
			}
			StateEntity[] entities = levelUpPopupsQueue.GetEntities();
			int num = 0;
			for (int i = 1; i < entities.Length; i++)
			{
				if (entities[i].playerLevelUp.CurrentLevel < entities[num].playerLevelUp.CurrentLevel)
				{
					num = i;
				}
			}
			ShowLevelUpScreen(entities[num].playerLevelUp.CurrentLevel);
			entities[num].isDestroyed = true;
		}

		private bool CanExecute()
		{
			if (uiContext.uIEntity != null && uiContext.uIEntity.hasScreenMap && levelUpPopupsQueue.count > 0)
			{
				ActiveScreenComponent activeScreen = stateContext.activeScreen;
				if (uiContext.uIEntity.hUD.hud.Id == ScreenId.IngameHUD)
				{
					return false;
				}
				if (activeScreen == null || activeScreen.ScreenId == ScreenId.LevelUpPopup)
				{
					return false;
				}
				if (TutorialHelpers.IsTutorialActive())
				{
					return false;
				}
				CurrencyAndExperienceScreen dialog = uiContext.GetDialog<CurrencyAndExperienceScreen>();
				PlayerLevelUpScreen dialog2 = uiContext.GetDialog<PlayerLevelUpScreen>();
				ChestRevealScreen dialog3 = uiContext.GetDialog<ChestRevealScreen>();
				return (dialog2 == null || !dialog2.gameObject.activeInHierarchy) && (dialog == null || !dialog.IsPlayingExperienceAnimations) && (dialog3 == null || !dialog3.gameObject.activeInHierarchy);
			}
			return false;
		}

		private void ShowLevelUpScreen(int currentLevel)
		{
			BaseDialog baseDialog = uiContext.OpenDialog<PlayerLevelUpScreen>();
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.AddUIPlayerLevel(currentLevel);
			baseDialog.SetData(uIEntity);
		}
	}
}
