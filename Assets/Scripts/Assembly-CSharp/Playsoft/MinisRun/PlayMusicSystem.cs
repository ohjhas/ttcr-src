using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlayMusicSystem : ReactiveSystem<StateEntity>
	{
		public PlayMusicSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.PlayMusic.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				MasterAudio.ChangePlaylistByName("PlaylistController", entity.playMusic.MusicId);
				entity.isDestroyed = true;
			}
		}
	}
}
