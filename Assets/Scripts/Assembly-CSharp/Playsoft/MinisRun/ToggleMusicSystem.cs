using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ToggleMusicSystem : ReactiveSystem<InputEntity>
	{
		private readonly StateContext stateContext;

		public ToggleMusicSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.MusicToggled);
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.isMusicToggled;
		}

		protected override void Execute(List<InputEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("Music").SingleEntity();
				stateEntity.ReplaceBoolean(!stateEntity.boolean.Value);
				stateEntity.isToSave = true;
				stateContext.CreateEntity().AddMuteMusic(!stateEntity.boolean.Value);
			}
		}
	}
}
