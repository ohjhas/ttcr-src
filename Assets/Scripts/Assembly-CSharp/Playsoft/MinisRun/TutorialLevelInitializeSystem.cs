using Entitas;

namespace Playsoft.MinisRun
{
	public class TutorialLevelInitializeSystem : IInitializeSystem, ISystem
	{
		private readonly GameContext gameContext;

		public TutorialLevelInitializeSystem(Contexts contexts)
		{
			gameContext = contexts.game;
		}

		public void Initialize()
		{
			LevelTutorialSettings levelTutorialSettings = gameContext.worldEntity.levelSettings.levelTutorialSettings;
			if (levelTutorialSettings.GodMode)
			{
				gameContext.playerEntity.isGodMode = true;
			}
		}
	}
}
