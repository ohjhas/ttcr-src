using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PauseIngameOnAppPause : MonoBehaviour
	{
		private void OnApplicationPause(bool pauseStatus)
		{
			if (pauseStatus)
			{
				GameEntity worldEntity = Contexts.sharedInstance.game.worldEntity;
				GameEntity playerEntity = Contexts.sharedInstance.game.playerEntity;
				BaseDialog baseDialog = Contexts.sharedInstance.uI.FindCurrentScreen();
				if (!(baseDialog == null) && !worldEntity.isPaused && baseDialog is IngameHUD && !playerEntity.isDead && worldEntity.isGameStarted)
				{
					worldEntity.isPaused = true;
				}
			}
		}
	}
}
