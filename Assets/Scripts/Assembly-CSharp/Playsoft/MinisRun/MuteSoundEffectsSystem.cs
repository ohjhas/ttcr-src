using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Entitas;

namespace Playsoft.MinisRun
{
	public class MuteSoundEffectsSystem : ReactiveSystem<StateEntity>
	{
		public MuteSoundEffectsSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.MuteSound.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasMuteSound;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				PersistentAudioSettings.MixerMuted = entity.muteSound.Mute;
			}
		}
	}
}
