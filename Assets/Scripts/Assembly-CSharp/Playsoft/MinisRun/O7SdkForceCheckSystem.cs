using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class O7SdkForceCheckSystem : ReactiveSystem<UIEntity>
	{
		public readonly StateContext stateContext;

		public readonly UIContext uiContext;

		public O7SdkForceCheckSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.NewSceneLoaded);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			if (stateContext.sessionEntity.isO7SdkForceCheckProcessed)
			{
				return;
			}
			if (uiContext.uIEntity.hUD.hud.Id == ScreenId.HomeScreen || uiContext.uIEntity.hUD.hud.Id == ScreenId.SagaMap)
			{
				stateContext.CreateEntity().isO7SdkAgeGateDecider = true;
				StateEntity o7SdkEntity = stateContext.o7SdkEntity;
				int num = o7SdkEntity.o7SdkGdprManager.manager.MustShowGdprConsent();
				if (num > 0)
				{
					stateContext.sessionEntity.isO7SdkForceGdpr = true;
				}
				bool flag = o7SdkEntity.o7SdkAgeGateManager.Manager.MustShowAgeGate();
				stateContext.sessionEntity.isO7SdkForceAgeGate = true;
			}
			stateContext.sessionEntity.isO7SdkForceCheckProcessed = true;
		}
	}
}
