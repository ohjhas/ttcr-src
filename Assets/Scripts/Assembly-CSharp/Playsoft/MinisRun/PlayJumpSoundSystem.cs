using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlayJumpSoundSystem : ReactiveSystem<InputEntity>
	{
		private readonly GameContext gameContext;

		private readonly StateContext stateContext;

		public PlayJumpSoundSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			gameContext = contexts.game;
			stateContext = contexts.state;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.InputJumpPerformed.Added());
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.isInputJumpPerformed && gameContext.playerEntity.hasJumping;
		}

		protected override void Execute(List<InputEntity> entities)
		{
			foreach (InputEntity entity in entities)
			{
				string newSoundId = ((gameContext.playerEntity.jumping.Quantity != 1) ? "character_double_jump" : "character_jump");
				stateContext.CreateEntity().AddPlaySoundEffect(newSoundId);
			}
		}
	}
}
