using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class UpdateMissionChestAvailabilitySystem : ReactiveSystem<GameEntity>
	{
		private readonly StateContext stateContext;

		public UpdateMissionChestAvailabilitySystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.MissionChestGranted);
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isPlayer && entity.isMissionChestGranted;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			string newLabel = "ChestData" + SettingsService.SettingsSO.WorldIndex;
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("ChestDataSend");
			stateEntity.AddLabel(newLabel);
			stateEntity.AddBoolean(true);
		}
	}
}
