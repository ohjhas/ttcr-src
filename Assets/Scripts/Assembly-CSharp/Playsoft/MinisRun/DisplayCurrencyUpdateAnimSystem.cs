using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class DisplayCurrencyUpdateAnimSystem : ReactiveSystem<UIEntity>
	{
		public DisplayCurrencyUpdateAnimSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.UIAction);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.hasUIShowCurrencyUpdate;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				CurrencyAndExperienceHelper.OpenScreenForCurrencyUpdate(entity.uIShowCurrencyUpdate.ItemIndex, entity.uIShowCurrencyUpdate.ItemAmount, entity.uIShowCurrencyUpdate.origin, entity.uIShowCurrencyUpdate.UpdateTargetManually);
				entity.isDestroyed = true;
			}
		}
	}
}
