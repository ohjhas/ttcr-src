using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class DisplayTransactionConfirmationSystem : ReactiveSystem<UIEntity>
	{
		private readonly UIContext uiContext;

		public DisplayTransactionConfirmationSystem(Contexts contexts)
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
			return entity.hasUIDisplayTransactionConfrimation || entity.isUIHideTransactionConfirmation;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.hasUIDisplayTransactionConfrimation)
				{
					OpenTransactionConfirmationPopup(entity.uIDisplayTransactionConfrimation.transaction);
				}
				else if (entity.isUIHideTransactionConfirmation)
				{
					CloseTransactionConfirmationPopup();
				}
				entity.isDestroyed = true;
			}
		}

		private void OpenTransactionConfirmationPopup(StateEntity transaction)
		{
			BaseDialog newDialog = uiContext.OpenDialog<ConfirmTransactionPopup>();
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.AddSetup(newDialog);
			uIEntity.AddUIDisplayTransactionConfrimation(transaction);
		}

		private void CloseTransactionConfirmationPopup()
		{
			ConfirmTransactionPopup dialog = uiContext.GetDialog<ConfirmTransactionPopup>();
			uiContext.CloseDialog(dialog);
		}
	}
}
