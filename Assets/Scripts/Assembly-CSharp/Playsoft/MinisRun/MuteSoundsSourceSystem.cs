using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Entitas;

namespace Playsoft.MinisRun
{
	public class MuteSoundsSourceSystem : ReactiveSystem<StateEntity>
	{
		public MuteSoundsSourceSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.MuteSoundSource.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasMuteSoundSource;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				PersistentAudioSettings.SetBusVolume(entity.muteSoundSource.SourceId, (!entity.muteSoundSource.Mute) ? 1f : 0f);
				entity.isDestroyed = true;
			}
		}
	}
}
