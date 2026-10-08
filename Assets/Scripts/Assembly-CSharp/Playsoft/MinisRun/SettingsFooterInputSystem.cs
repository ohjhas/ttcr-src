using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class SettingsFooterInputSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public SettingsFooterInputSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.UIAction);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.hasUISettingsFooterPress;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.uISettingsFooterPress.LinkType.Equals(SettingsFooter.SettingsFooterHyperlink.App_Name))
				{
					uiContext.CreateEntity().isDisplayUDID = true;
				}
				else if (entity.uISettingsFooterPress.LinkType.Equals(SettingsFooter.SettingsFooterHyperlink.Outfit_Copyright))
				{
					stateContext.CreateEntity().isOpenPrivacyUrl = true;
					stateContext.CreateEntity().isPrivacyAndLegalClickedEventToBeSent = true;
				}
				else if (entity.uISettingsFooterPress.LinkType.Equals(SettingsFooter.SettingsFooterHyperlink.Others_Copyright))
				{
					stateContext.CreateEntity().AddOpenUrl(stateContext.balancingDataEntity.privacyAndLegacyBalancingData.Data.Otherscopyright);
				}
				entity.isDestroyed = true;
			}
		}
	}
}
