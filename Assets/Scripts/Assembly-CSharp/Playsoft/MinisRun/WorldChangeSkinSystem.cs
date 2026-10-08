using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class WorldChangeSkinSystem : ReactiveSystem<InputEntity>
	{
		private readonly GameContext gameContext;

		private readonly InputContext inputContext;

		private readonly StateContext stateContext;

		public WorldChangeSkinSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			gameContext = contexts.game;
			inputContext = contexts.input;
			stateContext = contexts.state;
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.hasCollision && entity.collision.to != null && entity.collision.from != null;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.Collision);
		}

		protected override void Execute(List<InputEntity> entities)
		{
			foreach (InputEntity entity in entities)
			{
				GameEntity gameEntity = entity.collision.from as GameEntity;
				GameEntity gameEntity2 = entity.collision.to as GameEntity;
				if (!gameEntity.isPlayer || !gameEntity2.hasChangeWorldSkin)
				{
					continue;
				}
				foreach (KeyValuePair<int, MissionsBalancingData> item in stateContext.balancingDataEntity.missionBalancingData.Map)
				{
					string worldskin = item.Value.Worldskin;
					if (worldskin.Equals(gameEntity2.changeWorldSkin.WorldSuffix))
					{
						LandSkinSettingsSO landSkinSettingsSO = Resources.Load<LandSkinSettingsSO>("SO/Lands/" + item.Value.Background);
						CreateRemoveOldSkinInput();
						gameContext.worldEntity.ReplaceNewWorldSkin(worldskin, landSkinSettingsSO.BackLayer);
					}
				}
			}
		}

		private void CreateRemoveOldSkinInput()
		{
			string newOldSuffix = ((!gameContext.worldEntity.hasNewWorldSkin) ? gameContext.worldEntity.missionData.MissionData.Worldskin : gameContext.worldEntity.newWorldSkin.WorldSuffix);
			InputEntity inputEntity = inputContext.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.AddDestroyOldSkin(newOldSuffix);
		}
	}
}
