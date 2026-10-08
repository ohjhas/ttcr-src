using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class O7SdkPrivacyExplanationShowSystem : ReactiveSystem<UIEntity>
	{
		public O7SdkPrivacyExplanationShowSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.UIExplanationPopupShown);
		}

		protected override bool Filter(UIEntity entity)
		{
			return GameIntroHelper.IsIntroTime();
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				ExplanationPopup explanationPopup = Object.FindObjectOfType<ExplanationPopup>();
				if (explanationPopup == null)
				{
					break;
				}
				InitialStartScene initialStartScene = Object.FindObjectOfType<InitialStartScene>();
				if (initialStartScene == null)
				{
					break;
				}
				explanationPopup.transform.SetParent(initialStartScene.transform, false);
				explanationPopup.transform.SetAsLastSibling();
				entity.isDestroyed = true;
			}
		}
	}
}
