using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class FlyingElementEffectSystem : ReactiveSystem<UIEntity>
	{
		private readonly UIContext uiContext;

		public FlyingElementEffectSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.FlyingElementEffect.Added());
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.isEnabled)
				{
					FlyingElementEffect flyingElementEffect = CreateFlyingEffect();
					if (flyingElementEffect != null)
					{
						flyingElementEffect.ElementPrefab = entity.flyingElementEffect.ElementPrefab;
						flyingElementEffect.StartPoint = entity.flyingElementEffect.StartPoint;
						flyingElementEffect.EndPoint = entity.flyingElementEffect.EndPoint;
						flyingElementEffect.ElementsAmount = entity.flyingElementEffect.ElementsAmount;
						flyingElementEffect.EffectDuration = entity.flyingElementEffect.EffectDuration;
						flyingElementEffect.Callback = entity.flyingElementEffect.Callback;
						flyingElementEffect.StartEffect();
					}
					entity.isDestroyed = true;
				}
			}
		}

		private FlyingElementEffect CreateFlyingEffect()
		{
			BaseDialog baseDialog = uiContext.FindCurrentScreen();
			if (baseDialog != null)
			{
				GameObject gameObject = new GameObject();
				FlyingElementEffect flyingElementEffect = gameObject.AddComponent<FlyingElementEffect>();
				flyingElementEffect.transform.SetParent(baseDialog.transform.parent);
				flyingElementEffect.transform.SetAsLastSibling();
				return flyingElementEffect;
			}
			return null;
		}
	}
}
