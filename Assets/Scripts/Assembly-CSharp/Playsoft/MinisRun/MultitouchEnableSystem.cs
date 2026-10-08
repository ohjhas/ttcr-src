using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class MultitouchEnableSystem : ReactiveSystem<StateEntity>
	{
		public MultitouchEnableSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.ActiveScreen);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasActiveScreen;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				bool multiTouchEnabled = entity.activeScreen.ScreenId == ScreenId.IngameHUD;
				Input.multiTouchEnabled = multiTouchEnabled;
			}
		}
	}
}
