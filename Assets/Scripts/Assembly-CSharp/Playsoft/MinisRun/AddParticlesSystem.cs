using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class AddParticlesSystem : ReactiveSystem<InputEntity>
	{
		private GameContext gameContext;

		public AddParticlesSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			gameContext = contexts.game;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.CreateParticles);
		}

		protected override bool Filter(InputEntity entity)
		{
			return true;
		}

		protected override void Execute(List<InputEntity> entities)
		{
			foreach (InputEntity entity in entities)
			{
				GameEntity gameEntity = gameContext.CreateEntity();
				gameEntity.AddAsset("ImprovedTokenParticles");
				gameEntity.AddPosition(entity.createParticles.Position);
				gameEntity.isMovable = true;
				gameEntity.isPoolable = true;
			}
		}
	}
}
