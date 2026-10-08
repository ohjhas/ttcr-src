using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class LogGameFinishedSystem : ReactiveSystem<InputEntity>
	{
		[Serializable]
		private class MissionObjectiveSerializable
		{
			public string Type;

			public float MissionResult;

			public float Star1Threshold;
		}

		private readonly GameContext gameContext;

		private readonly StateContext stateContext;

		public LogGameFinishedSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			gameContext = contexts.game;
			stateContext = contexts.state;
		}

		protected override bool Filter(InputEntity entity)
		{
			return true;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.LevelEndAnimationFinished.Added(), InputMatcher.PlayerLost.Added());
		}

		protected override void Execute(List<InputEntity> entities)
		{
			InputEntity inputEntity = entities.SingleEntity();
			SendGameFinishLog(inputEntity.isLevelEndAnimationFinished);
		}

		private void SendGameFinishLog(bool isPlayerWon)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			string newP = (isPlayerWon ? "win" : ((!gameContext.playerEntity.isDead) ? "cancel" : "lose"));
			int charId = gameContext.worldEntity.selectedCharacter.charId;
			string newP2 = stateContext.balancingDataEntity.characterStaticBalancingData.Map[charId].Name.ToLower();
			float duration_MS = gameContext.worldEntity.runDuration.Duration_MS;
			MissionObjectiveSerializable missionObjectiveSerializable = new MissionObjectiveSerializable();
			missionObjectiveSerializable.Type = gameContext.worldEntity.missionType.Type.ToString();
			missionObjectiveSerializable.MissionResult = gameContext.worldEntity.missionCounter.Value;
			missionObjectiveSerializable.Star1Threshold = gameContext.worldEntity.missionThresholds.FirstStar;
			MissionObjectiveSerializable obj = missionObjectiveSerializable;
			string newJsonData = JsonUtility.ToJson(obj);
			stateEntity.AddEventCustom("finish", "run", newP, newP2, null, gameContext.playerEntity.stamina.Value, (SettingsService.SettingsSO.WorldIndex + 1).ToString(), duration_MS, newJsonData, false);
		}
	}
}
