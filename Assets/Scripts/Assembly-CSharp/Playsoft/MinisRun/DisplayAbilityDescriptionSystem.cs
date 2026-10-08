using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class DisplayAbilityDescriptionSystem : ReactiveSystem<UIEntity>
	{
		private readonly UIContext uiContext;

		public DisplayAbilityDescriptionSystem(Contexts contexts)
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
			return entity.hasUIDisplayAbility || entity.isUIHideAbility;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.hasUIDisplayAbility)
				{
					OpenAbilityPopup(entity.uIDisplayAbility.Id, entity.uIDisplayAbility.IsChar);
				}
				else if (entity.isUIHideAbility)
				{
					CloseAbilityPopup();
				}
				entity.isDestroyed = true;
			}
		}

		private void OpenAbilityPopup(int Id, bool isCharacter)
		{
			BaseDialog newDialog = uiContext.OpenDialog<AbilityPopup>();
			UIEntity uIEntity = uiContext.CreateEntity();
			if (isCharacter)
			{
				uIEntity.AddUICharSingleRequest(Id);
			}
			else
			{
				uIEntity.AddUICompSingleRequest(Id);
			}
			uIEntity.AddSetup(newDialog);
		}

		private void CloseAbilityPopup()
		{
			BaseDialog dialog = uiContext.GetDialog<AbilityPopup>();
			uiContext.CloseDialog(dialog);
		}
	}
}
