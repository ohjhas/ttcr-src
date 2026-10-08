using UnityEngine;

namespace Playsoft.MinisRun
{
	public sealed class PauseScreen : BaseScreen
	{
		public override ScreenId Id
		{
			get
			{
				return ScreenId.PausePopup;
			}
		}

		public void ResumePressed()
		{
			if (base.IsClickable)
			{
				base.uiContext.CloseDialog(this);
				IngameHUD ingameHUD = base.uiContext.uIEntity.hUD.hud as IngameHUD;
				if (ingameHUD != null)
				{
					ingameHUD.ResumeGame();
				}
				else
				{
					Debug.LogError("HUD not found");
				}
			}
		}

		public void QuitButtonClicked()
		{
			if (base.IsClickable)
			{
				base.uiContext.CloseDialog(this);
				if (Contexts.sharedInstance.game.worldEntity.hasLevelSettings && Contexts.sharedInstance.game.worldEntity.levelSettings.levelTutorialSettings.AfterLoseAction.Equals(AfterLoseAction.ReturnToHomeScreen))
				{
					Contexts.sharedInstance.game.worldEntity.isDead = true;
					Contexts.sharedInstance.game.worldEntity.isReturnToHomeLevel = true;
				}
				else
				{
					base.uiContext.OpenDialog<FailScreen>();
				}
				SendPlayerLostEvent();
			}
		}

		private void SendPlayerLostEvent()
		{
			InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.isPlayerLost = true;
			Contexts.sharedInstance.state.CreateEntity().AddPlayMusic("chapter1_fail");
		}
	}
}
