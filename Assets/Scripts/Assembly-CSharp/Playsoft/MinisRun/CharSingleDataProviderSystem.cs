using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CharSingleDataProviderSystem : ReactiveSystem<UIEntity>
	{
		public CharSingleDataProviderSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.Setup);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.hasUICharSingleRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				CSCharacterData charData = DataProviderHelper.GetCharData(entity.uICharSingleRequest.CharId);
				entity.AddUICharSingleResponse(charData);
			}
		}
	}
}
