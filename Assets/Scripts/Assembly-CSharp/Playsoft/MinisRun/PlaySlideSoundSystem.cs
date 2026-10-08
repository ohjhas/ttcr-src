using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlaySlideSoundSystem : ReactiveSystem<InputEntity>
	{
		private const string SLIDE_SOUND_ID = "character_slide_loop";

		private readonly StateContext stateContext;

		private PlaySoundResult sound;

		public PlaySlideSoundSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.InputSlideStart.Added(), InputMatcher.InputSlide.Added(), InputMatcher.InputSlideStop.Added());
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.isInputSlide || entity.isInputSlideStart || entity.isInputSlideStop;
		}

		protected override void Execute(List<InputEntity> entities)
		{
			foreach (InputEntity entity in entities)
			{
				if (entity.isInputSlideStart)
				{
					stateContext.CreateEntity().AddPlayLoopSoundEffect("character_slide_loop");
				}
				else if (entity.isInputSlideStop)
				{
					stateContext.CreateEntity().AddStopLoopSoundEffect("character_slide_loop");
				}
			}
		}
	}
}
