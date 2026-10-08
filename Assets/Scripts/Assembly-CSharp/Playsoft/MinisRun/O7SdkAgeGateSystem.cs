using System.Collections.Generic;
using Entitas;
using O7SDK;

namespace Playsoft.MinisRun
{
	public class O7SdkAgeGateSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ITearDownSystem, ISystem
	{
		private readonly StateContext stateContext;

		private O7SDKGateManager manager;

		public O7SdkAgeGateSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
		}

		public void Initialize()
		{
			manager = stateContext.o7SdkEntity.o7SdkAgeGateManager.Manager;
			manager.OnAgeGateStatusChanged += AgeGateStatusChanged;
		}

		public void TearDown()
		{
			manager = stateContext.o7SdkEntity.o7SdkAgeGateManager.Manager;
			manager.OnAgeGateStatusChanged -= AgeGateStatusChanged;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkAgeGateSelected);
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				manager.SetUserBirthYear(entity.o7SdkAgeGateSelected.year);
				ShowGenderGate();
				entity.isDestroyed = true;
			}
		}

		private void AgeGateStatusChanged(bool status)
		{
		}

		private void ShowGenderGate()
		{
			if (manager.MustShowGenderGate())
			{
				StateEntity stateEntity = stateContext.CreateEntity();
				stateEntity.isO7SdkGenderGateShow = true;
			}
			else
			{
				StateEntity stateEntity2 = stateContext.CreateEntity();
				stateEntity2.isO7SdkGdprRequest = true;
				stateEntity2.isO7SdkGdprShowDecider = true;
			}
		}
	}
}
