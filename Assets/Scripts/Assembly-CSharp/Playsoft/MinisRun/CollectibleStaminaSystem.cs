using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public sealed class CollectibleStaminaSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext _gameContext;

		private readonly IGroup<GameEntity> _listeners;

		public CollectibleStaminaSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			_gameContext = contexts.game;
			_listeners = contexts.game.GetGroup(GameMatcher.StaminasListener);
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.hasCollectibleStamina;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.Collected);
		}

		protected override void Execute(List<GameEntity> entities)
		{
			GameEntity playerEntity = _gameContext.playerEntity;
			if (playerEntity.isDead)
			{
				return;
			}
			float num = 0f;
			foreach (GameEntity entity in entities)
			{
				num += entity.collectibleStamina.Value;
				InformUI(entity);
			}
			RecoverStamina(playerEntity, num);
		}

		private void RecoverStamina(GameEntity from, float amount)
		{
			GameEntity playerEntity = _gameContext.playerEntity;
			if (playerEntity != null && !playerEntity.isDead)
			{
				float value = from.stamina.Value;
				float newValue = Mathf.Min(SettingsService.SettingsSO.StartingStamina, value + amount);
				from.ReplaceStamina(newValue);
			}
		}

		private void InformUI(GameEntity stamina)
		{
			GameEntity[] entities = _listeners.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				gameEntity.staminasListener.listener.OnStaminaPotionCollected(stamina.view.gameObject.transform.position);
			}
		}
	}
}
