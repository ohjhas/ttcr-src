using Entitas;

namespace Playsoft.MinisRun
{
	public class LastSceneSaveDataSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public LastSceneSaveDataSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			stateContext.CreateStringData("LastScene", ProjectConstants.HOME_SCENE_NAME, false);
			stateContext.CreateStringData("SceneToLoad", string.Empty, false);
		}
	}
}
