using System.Collections.Generic;
using Entitas;
using O7SDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class O7SdkAgeGateDeciderSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		private O7SDKGateManager manager;

		public O7SdkAgeGateDeciderSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
		}

		public void Initialize()
		{
			manager = stateContext.o7SdkEntity.o7SdkAgeGateManager.Manager;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkAgeGateDecider);
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				entity.isDestroyed = true;
			}
			if (manager.MustShowAgeGate())
			{
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log("Age Gate Must be shown");
				}
				stateContext.CreateEntity().isO7SdkAgeGateShow = true;
				return;
			}
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("Age Gate Must Not be shown");
			}
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkGdprRequest = true;
			stateEntity.isO7SdkGdprShowDecider = true;
		}
	}
}
