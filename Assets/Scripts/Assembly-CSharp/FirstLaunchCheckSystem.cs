using Entitas;
using Playsoft.MinisRun;

public class FirstLaunchCheckSystem : IInitializeSystem, ISystem
{
	private readonly StateContext stateContext;

	public FirstLaunchCheckSystem(Contexts contexts)
	{
		stateContext = contexts.state;
	}

	public void Initialize()
	{
		if (stateContext.sessionEntity != null && !stateContext.sessionEntity.isGameFirstLaunch)
		{
			stateContext.sessionEntity.isGameFirstLaunch = GameIntroHelper.IsIntroTime();
		}
	}
}
