using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class UILoadingSpinnerSystem : ReactiveSystem<UIEntity>
	{
		public readonly StateContext stateContext;

		public readonly UIContext uiContext;

		public UILoadingSpinnerSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.LoadingSpinnerShow.Added(), UIMatcher.LoadingSpinnerHide.AddedOrRemoved());
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.isUIAction;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.hasLoadingSpinnerShow)
				{
					LoadingSpinnerPopup loadingSpinnerPopup = uiContext.OpenDialog<LoadingSpinnerPopup>() as LoadingSpinnerPopup;
					if (loadingSpinnerPopup != null)
					{
						loadingSpinnerPopup.BlockRaycasts(entity.loadingSpinnerShow.blockRaycasts);
					}
					entity.isDestroyed = true;
				}
				if (entity.isLoadingSpinnerHide)
				{
					uiContext.CloseDialog(ScreenId.LoadingSpinnerPopup);
					entity.isDestroyed = true;
				}
			}
		}
	}
}
