using System.Collections.Generic;
using Entitas;
using O7SDK;

namespace Playsoft.MinisRun
{
	public class O7SdkGenderGateSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private O7SDKGateManager gateManager;

		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public O7SdkGenderGateSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
			uiContext = context.uI;
		}

		public void Initialize()
		{
			gateManager = stateContext.o7SdkEntity.o7SdkAgeGateManager.Manager;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkGenderGateSelected);
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				gateManager.SetUserGender(entity.o7SdkGenderGateSelected.gender);
				StateEntity stateEntity = stateContext.CreateEntity();
				stateEntity.isO7SdkGdprRequest = true;
				stateEntity.isO7SdkGdprShowDecider = true;
				entity.isDestroyed = true;
			}
		}
	}
}
