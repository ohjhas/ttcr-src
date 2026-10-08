using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Entitas;

namespace Playsoft.MinisRun
{
	public class MuteMusicSystem : ReactiveSystem<StateEntity>
	{
		public MuteMusicSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.MuteMusic.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasMuteMusic;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				PersistentAudioSettings.MusicMuted = entity.muteMusic.Mute;
			}
		}
	}
}
