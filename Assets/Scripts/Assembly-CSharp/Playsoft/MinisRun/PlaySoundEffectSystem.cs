using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlaySoundEffectSystem : ReactiveSystem<StateEntity>
	{
		public PlaySoundEffectSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.PlaySoundEffect.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				MasterAudio.PlaySoundAndForget(entity.playSoundEffect.SoundId);
				entity.isDestroyed = true;
			}
		}
	}
}
