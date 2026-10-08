using Entitas;

namespace Playsoft.MinisRun
{
	public class SessionInitializeSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public SessionInitializeSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			if (stateContext.sessionEntity == null)
			{
				StateEntity stateEntity = stateContext.CreateEntity();
				stateEntity.isSession = true;
			}
		}
	}
}
