using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlayLoopSoundEffectSystem : ReactiveSystem<StateEntity>
	{
		private readonly List<PlaySoundResult> playSoundResults = new List<PlaySoundResult>();

		public PlayLoopSoundEffectSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.PlayLoopSoundEffect.Added(), StateMatcher.StopLoopSoundEffect.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasPlayLoopSoundEffect)
				{
					playSoundResults.Add(MasterAudio.PlaySound(entity.playLoopSoundEffect.SoundId));
				}
				if (entity.hasStopLoopSoundEffect)
				{
					foreach (PlaySoundResult playSoundResult in playSoundResults)
					{
						if (playSoundResult != null && playSoundResult.ActingVariation.SoundGroupName == entity.stopLoopSoundEffect.SoundId)
						{
							playSoundResult.ActingVariation.Stop();
							playSoundResults.Remove(playSoundResult);
							break;
						}
					}
				}
				entity.isDestroyed = true;
			}
		}
	}
}
