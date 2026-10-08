using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlayCollectSoundSystem : ReactiveSystem<InputEntity>
	{
		private readonly StateContext stateContext;

		public PlayCollectSoundSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.Collision.Added());
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.hasCollision;
		}

		protected override void Execute(List<InputEntity> entities)
		{
			foreach (InputEntity entity in entities)
			{
				if (IsEntityCollectible(entity))
				{
					stateContext.CreateEntity().AddPlaySoundEffect(((GameEntity)entity.collision.to).collectSound.SoundId);
				}
			}
		}

		private bool IsEntityCollectible(InputEntity entity)
		{
			return entity.hasCollision && entity.collision.to != null && entity.collision.from != null && ((GameEntity)entity.collision.to).isCollectible && ((GameEntity)entity.collision.from).isPlayer;
		}
	}
}
