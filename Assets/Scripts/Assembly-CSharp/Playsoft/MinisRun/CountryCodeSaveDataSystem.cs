using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CountryCodeSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string currentCountryCodeKey = "countryCodeKey";

		public const string currentCountryCodeSendKey = "countryCodeSendKey";

		public CountryCodeSaveDataSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
		}

		public void Initialize()
		{
			stateContext.CreateStringData("countryCodeKey", string.Empty, false);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "countryCodeSendKey";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("countryCodeKey").SingleEntity();
			foreach (StateEntity entity in entities)
			{
				stateEntity.ReplaceStringData(entity.stringData.Value);
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
