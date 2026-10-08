using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class OpenUrlSystem : ReactiveSystem<StateEntity>
	{
		private readonly UIContext uiContext;

		public OpenUrlSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			uiContext = contexts.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.OpenUrl);
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (string.IsNullOrEmpty(entity.openUrl.Url))
				{
					Debug.LogWarning("Provided url was empty");
				}
				else if (Application.internetReachability == NetworkReachability.NotReachable)
				{
					UIEntity uIEntity = uiContext.CreateEntity();
					uIEntity.isUIAction = true;
					uIEntity.AddUIDisplayExplanation(ExplanationType.NoInternet);
				}
				else
				{
					Application.OpenURL(entity.openUrl.Url);
				}
				entity.isDestroyed = true;
			}
		}
	}
}
