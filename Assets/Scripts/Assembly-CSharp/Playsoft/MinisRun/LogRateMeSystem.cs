using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LogRateMeSystem : ReactiveSystem<StateEntity>
	{
		private enum RateMeExitType
		{
			Rate_it = 0,
			Not_Now = 1,
			Dont_Like = 2,
			X_Button = 3
		}

		public readonly StateContext stateContext;

		public readonly UIContext uiContext;

		public LogRateMeSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkRateMeNow.Added(), StateMatcher.O7SdkRateMeLater.Added(), StateMatcher.O7SdkRateMeDontLike.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isO7SdkRateMeNow || entity.isO7SdkRateMeLater || entity.isO7SdkRateMeDontLike || entity.isO7SdkRateMeClosed;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.isO7SdkRateMe)
				{
					SendRatemeEvent(RateMeExitType.Rate_it);
				}
				if (entity.isO7SdkRateMeDontLike)
				{
					SendRatemeEvent(RateMeExitType.Dont_Like);
				}
				if (entity.isO7SdkRateMeClosed)
				{
					SendRatemeEvent(RateMeExitType.X_Button);
				}
				if (entity.isO7SdkRateMeLater)
				{
					SendRatemeEvent(RateMeExitType.Not_Now);
				}
			}
		}

		private void SendRatemeEvent(RateMeExitType type)
		{
			string newP = type.ToString().ToLower().Replace('_', '-');
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			stateEntity.AddEventCustom("screen-exit", "screen-rate-this-app", null, newP, null, null, null, null, null, true);
		}
	}
}
