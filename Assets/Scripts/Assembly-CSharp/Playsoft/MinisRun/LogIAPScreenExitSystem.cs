using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LogIAPScreenExitSystem : ReactiveSystem<UIEntity>
	{
		public readonly StateContext stateContext;

		public readonly UIContext uiContext;

		public LogIAPScreenExitSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.UIShopScreenExit);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				LogScreenExit();
				entity.isDestroyed = true;
			}
		}

		private void LogScreenExit()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			stateEntity.AddEventCustom("screen-exit", "screen-iap", null, null, null, null, null, null, null, false);
		}
	}
}
