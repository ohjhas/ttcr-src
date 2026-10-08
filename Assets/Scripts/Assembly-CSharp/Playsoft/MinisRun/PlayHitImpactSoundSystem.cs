using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlayHitImpactSoundSystem : ReactiveSystem<InputEntity>
	{
		private readonly StateContext stateContext;

		public PlayHitImpactSoundSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.CollisionFeedback.Added());
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.hasCollisionFeedback;
		}

		protected override void Execute(List<InputEntity> entities)
		{
			foreach (InputEntity entity in entities)
			{
				string newSoundId = ((!entity.collisionFeedback.TouchPointIsLow) ? "character_hit_head" : "character_hit_knee");
				stateContext.CreateEntity().AddPlaySoundEffect(newSoundId);
			}
		}
	}
}
