using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class WorldFinishLevelSystem : ReactiveSystem<InputEntity>
	{
		private readonly UIContext uiContext;

		public WorldFinishLevelSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			uiContext = contexts.uI;
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.isLevelEndAnimationFinished;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.LevelEndAnimationFinished);
		}

		protected override void Execute(List<InputEntity> entities)
		{
			Contexts.sharedInstance.state.CreateEntity().AddChangeTimeScale(0f);
			uiContext.OpenDialog<WinScreen>();
		}
	}
}
