using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerRefreshViewSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		public PlayerRefreshViewSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
		}

		protected override bool Filter(GameEntity entity)
		{
			return true;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.PlayerRevive.Added(), GameMatcher.PlayerRelay.Added());
		}

		protected override void Execute(List<GameEntity> entities)
		{
			GameEntity playerEntity = gameContext.playerEntity;
			RemoveView(playerEntity);
			RestorePosition(playerEntity);
		}

		private void RemoveView(GameEntity player)
		{
			player.view.gameObject.GetComponent<Rigidbody2D>().simulated = false;
			player.view.gameObject.GetComponent<BoxCollider2D>().enabled = false;
			player.view.gameObject.Unlink();
			player.view.gameObject.transform.parent = null;
			player.RemoveView();
		}

		private void RestorePosition(GameEntity player)
		{
			Vector3 vector = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0f) - new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, 0f);
			Vector3 newValue = SettingsService.SettingsSO.PlayerCameraOffset + vector;
			player.ReplacePosition(newValue);
		}
	}
}
