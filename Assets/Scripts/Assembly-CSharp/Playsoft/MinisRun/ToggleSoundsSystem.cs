using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ToggleSoundsSystem : ReactiveSystem<InputEntity>
	{
		private readonly StateContext stateContext;

		public ToggleSoundsSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.SoundsToggled);
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.isSoundsToggled;
		}

		protected override void Execute(List<InputEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("Sounds").SingleEntity();
				stateEntity.ReplaceBoolean(!stateEntity.boolean.Value);
				stateEntity.isToSave = true;
				stateContext.CreateEntity().AddMuteSound(!stateEntity.boolean.Value);
			}
		}
	}
}
