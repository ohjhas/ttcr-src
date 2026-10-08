using System;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RealTimeSystem : IInitializeSystem, IExecuteSystem, ISystem
	{
		private readonly StateContext stateContext;

		private int lastSecond;

		public RealTimeSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			if (stateContext.currentRealTimeEntity == null)
			{
				stateContext.CreateEntity().AddCurrentRealTime(DateTime.Now);
			}
		}

		public void Execute()
		{
			if (lastSecond != DateTime.Now.Second)
			{
				lastSecond = DateTime.Now.Second;
				DateTime now = DateTime.Now;
				now = now.AddMilliseconds(-now.Millisecond);
				stateContext.currentRealTimeEntity.ReplaceCurrentRealTime(now);
			}
		}
	}
}
