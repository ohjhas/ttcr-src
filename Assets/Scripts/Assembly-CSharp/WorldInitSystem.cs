using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldInitSystem : IInitializeSystem, ISystem
{
	private const string LevelPath = "SO/Levels/";

	private const string testLevelKey = "TestLevel";

	private readonly GameContext gameContext;

	private readonly StateContext stateContext;

	public WorldInitSystem(Contexts contexts)
	{
		gameContext = contexts.game;
		stateContext = contexts.state;
	}

	public void Initialize()
	{
		int worldIndex = SettingsService.SettingsSO.WorldIndex;
		MissionsBalancingData missionsBalancingData = stateContext.balancingDataEntity.missionBalancingData.Map[worldIndex];
		LevelData levelData = Resources.Load<LevelData>("SO/Levels/" + missionsBalancingData.Missionid);
		LevelTutorialSettings newLevelTutorialSettings = loadlLevelTutorialSettings(levelData);
		List<LevelElementData> newLevelElements = LoadLevel(levelData);
		float baseSpeed = SettingsService.SettingsSO.BaseSpeed;
		float newScale = (float)missionsBalancingData.Worldspeed / SettingsService.SettingsSO.BaseSpeed;
		GameEntity gameEntity = gameContext.CreateEntity();
		gameEntity.AddInitialTimeScale(1f);
		gameEntity.AddWorldTimeScale(newScale);
		gameEntity.AddWorld(0f, baseSpeed, newLevelElements);
		gameEntity.AddLevelSettings(newLevelTutorialSettings);
		gameEntity.AddWaitToStart(0.5f);
		gameEntity.AddScore(0);
		gameEntity.AddCoins(0);
		gameEntity.AddMissionData(missionsBalancingData);
		GameEntity gameEntity2 = gameContext.CreateEntity();
		gameEntity2.isDestroyZoned = true;
		gameEntity2.AddAsset("DestroyZone");
		gameEntity2.AddPosition(new Vector3(SettingsService.SettingsSO.DestroyZoneOffset, 0f, 0f));
		gameEntity2.isCameraAttached = true;
		if (!GameIntroHelper.IsIntroTime())
		{
			gameContext.CreateEntity().isPlayGameMusic = true;
			gameEntity.isGameStarted = true;
		}
		else
		{
			SceneManager.LoadScene(ProjectConstants.INITIAL_START_SCENE_NAME, LoadSceneMode.Additive);
		}
		HashSet<StateEntity> entitiesWithName = stateContext.GetEntitiesWithName("FirstCharSelectData");
		int newCharId = 0;
		if (entitiesWithName.Count > 0)
		{
			StateEntity stateEntity = entitiesWithName.SingleEntity();
			newCharId = stateEntity.integer.Amount;
		}
		gameEntity.AddSelectedCharacter(newCharId);
		gameEntity.AddRunDuration(0f);
	}

	private List<LevelElementData> LoadLevel(LevelData levelData)
	{
		if (PlayerPrefs.HasKey("TestLevel"))
		{
			string text = PlayerPrefs.GetString("TestLevel");
			levelData = Resources.Load("SO/Levels/" + text) as LevelData;
			PlayerPrefs.DeleteKey("TestLevel");
		}
		return new List<LevelElementData>(levelData.LevelElements);
	}

	private LevelTutorialSettings loadlLevelTutorialSettings(LevelData levelData)
	{
		if (PlayerPrefs.HasKey("TestLevel"))
		{
			return new LevelTutorialSettings();
		}
		return levelData.LevelTutorialSettings;
	}
}
