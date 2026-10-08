using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class DisplayExplanationSystem : ReactiveSystem<UIEntity>
	{
		private readonly UIContext uiContext;

		public DisplayExplanationSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.UIAction);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.hasUIDisplayExplanation || entity.isUIHideExplanation;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.hasUIDisplayExplanation)
				{
					OpenExplanationPopup(entity.uIDisplayExplanation.ExplanationType);
				}
				else if (entity.isUIHideExplanation)
				{
					CloseExplanationPopup();
				}
				entity.isDestroyed = true;
			}
		}

		private void OpenExplanationPopup(ExplanationType type)
		{
			BaseDialog newDialog = uiContext.OpenDialog<ExplanationPopup>();
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.AddSetup(newDialog);
			uIEntity.AddUIDisplayExplanation(type);
		}

		private void CloseExplanationPopup()
		{
			ExplanationPopup dialog = uiContext.GetDialog<ExplanationPopup>();
			uiContext.CloseDialog(dialog);
		}
	}
}
