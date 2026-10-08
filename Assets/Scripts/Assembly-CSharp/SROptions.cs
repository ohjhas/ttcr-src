using System;
using System.Collections.Generic;
using System.ComponentModel;
using Entitas;
using O7SDK;
using Playsoft.MinisRun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SROptions
{
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class NumberRangeAttribute : Attribute
	{
		public readonly double Max;

		public readonly double Min;

		public NumberRangeAttribute(double min, double max)
		{
			Min = min;
			Max = max;
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public sealed class IncrementAttribute : Attribute
	{
		public readonly double Increment;

		public IncrementAttribute(double increment)
		{
			Increment = increment;
		}
	}

	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	public sealed class SortAttribute : Attribute
	{
		public readonly int SortPriority;

		public SortAttribute(int priority)
		{
			SortPriority = priority;
		}
	}

	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property)]
	public sealed class DisplayNameAttribute : Attribute
	{
		public readonly string Name;

		public DisplayNameAttribute(string name)
		{
			Name = name;
		}
	}

	private static readonly SROptions _current = new SROptions();

	[Category("Energy System")]
	[Sort(1)]
	public int TomLifes
	{
		get
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("CharLifesData" + 0).SingleEntity();
			if (stateEntity != null)
			{
				return stateEntity.integer.Amount;
			}
			return 0;
		}
		set
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("CharLifesDataSend");
			stateEntity.AddLabel("CharLifesData" + 0);
			stateEntity.AddInteger(value);
			Contexts.sharedInstance.uI.RefreshScreen();
		}
	}

	[Category("Inventory")]
	[Sort(1)]
	[Increment(100.0)]
	public int Coins
	{
		get
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("Coins").SingleEntity();
			if (stateEntity != null)
			{
				return stateEntity.integer.Amount;
			}
			return 0;
		}
		set
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("Coins").SingleEntity();
			if (stateEntity != null)
			{
				stateEntity.ReplaceInteger(value);
				UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
				uIEntity.isRefreshScreen = true;
			}
		}
	}

	[Category("Inventory")]
	[Sort(2)]
	[Increment(100.0)]
	public int HardCurrency
	{
		get
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("HardCurrency").SingleEntity();
			if (stateEntity != null)
			{
				return stateEntity.integer.Amount;
			}
			return 0;
		}
		set
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("HardCurrency").SingleEntity();
			if (stateEntity != null)
			{
				stateEntity.ReplaceInteger(value);
				UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
				uIEntity.isRefreshScreen = true;
			}
		}
	}

	[Category("Inventory")]
	[Sort(3)]
	[Increment(10.0)]
	public int TomParts
	{
		get
		{
			return GetCharPartsEntity(0).integer.Amount;
		}
		set
		{
			SetCharPartsValue(0, value);
		}
	}

	[Category("Inventory")]
	[Sort(4)]
	[Increment(10.0)]
	public int AngelaParts
	{
		get
		{
			return GetCharPartsEntity(1).integer.Amount;
		}
		set
		{
			SetCharPartsValue(1, value);
		}
	}

	[Category("Inventory")]
	[Sort(5)]
	[Increment(10.0)]
	public int HankParts
	{
		get
		{
			return GetCharPartsEntity(2).integer.Amount;
		}
		set
		{
			SetCharPartsValue(2, value);
		}
	}

	[Category("Inventory")]
	[Sort(6)]
	[Increment(10.0)]
	public int BenParts
	{
		get
		{
			return GetCharPartsEntity(3).integer.Amount;
		}
		set
		{
			SetCharPartsValue(3, value);
		}
	}

	[Category("Inventory")]
	[Sort(7)]
	[Increment(10.0)]
	public int GingerParts
	{
		get
		{
			return GetCharPartsEntity(4).integer.Amount;
		}
		set
		{
			SetCharPartsValue(4, value);
		}
	}

	[Category("Inventory")]
	[Sort(8)]
	[Increment(10.0)]
	public int TomPOParts
	{
		get
		{
			return GetCharPartsEntity(5).integer.Amount;
		}
		set
		{
			SetCharPartsValue(5, value);
		}
	}

	[Category("Inventory")]
	[Sort(9)]
	[Increment(10.0)]
	public int BenInvParts
	{
		get
		{
			return GetCharPartsEntity(6).integer.Amount;
		}
		set
		{
			SetCharPartsValue(6, value);
		}
	}

	[Category("Inventory")]
	[Sort(10)]
	[Increment(10.0)]
	public int ShinyHeartParts
	{
		get
		{
			return GetCompPartsEntity(0).integer.Amount;
		}
		set
		{
			SetCompPartsValue(0, value);
		}
	}

	[Category("Inventory")]
	[Sort(11)]
	[Increment(10.0)]
	public int UnicornParts
	{
		get
		{
			return GetCompPartsEntity(1).integer.Amount;
		}
		set
		{
			SetCompPartsValue(1, value);
		}
	}

	[Category("Inventory")]
	[Sort(12)]
	[Increment(10.0)]
	public int DragonParts
	{
		get
		{
			return GetCompPartsEntity(2).integer.Amount;
		}
		set
		{
			SetCompPartsValue(2, value);
		}
	}

	[Category("Inventory")]
	[Sort(13)]
	[Increment(10.0)]
	public int HappyFaceParts
	{
		get
		{
			return GetCompPartsEntity(3).integer.Amount;
		}
		set
		{
			SetCompPartsValue(3, value);
		}
	}

	[Category("Inventory")]
	[Sort(14)]
	[Increment(10.0)]
	public int HappyCloudParts
	{
		get
		{
			return GetCompPartsEntity(4).integer.Amount;
		}
		set
		{
			SetCompPartsValue(4, value);
		}
	}

	[Category("Player Progress")]
	[Sort(2)]
	[Increment(10.0)]
	public int PlayerXP
	{
		get
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("PlayerLevelExp").SingleEntity();
			return stateEntity.integer.Amount;
		}
		set
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("PlayerLevelExp").SingleEntity();
			Contexts.sharedInstance.state.CreateEntity().AddGrantPlayerExperience(value - stateEntity.integer.Amount);
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.isRefreshScreen = true;
		}
	}

	[Category("Player Progress")]
	[Sort(1)]
	[Increment(1.0)]
	public int UnlockedLevel
	{
		get
		{
			StateContext state = Contexts.sharedInstance.state;
			StateEntity stateEntity = state.GetEntitiesWithName("UnlockedLevel").SingleEntity();
			return 1 + ((stateEntity != null) ? stateEntity.integer.Amount : 0);
		}
		set
		{
			StateContext state = Contexts.sharedInstance.state;
			StateEntity stateEntity = state.GetEntitiesWithName("UnlockedLevel").SingleEntity();
			if (stateEntity != null)
			{
				stateEntity.ReplaceInteger(value - 1);
				UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
				uIEntity.isRefreshScreen = true;
			}
		}
	}

	public static SROptions Current
	{
		get
		{
			return _current;
		}
	}

	public event SROptionsPropertyChanged PropertyChanged;

	[Category("Daily Missions")]
	[Sort(3)]
	public void RefreshDailyMissions()
	{
		StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("DailyMissionCreationTime").SingleEntity();
		stateEntity.dateTime.Value -= TimeSpan.FromDays(1.0);
		IGroup<UIEntity> obj = Contexts.sharedInstance.uI.GetGroup(UIMatcher.SaveDataLoadedListener);
		UIEntity[] entities = obj.GetEntities();
		foreach (UIEntity uIEntity in entities)
		{
			if (uIEntity.saveDataLoadedListener.listener is DailyMissionsSaveDataSystem)
			{
				uIEntity.saveDataLoadedListener.listener.OnDataLoaded();
			}
		}
		UIEntity uIEntity2 = Contexts.sharedInstance.uI.CreateEntity();
		uIEntity2.isRefreshScreen = true;
	}

	[Category("Daily Missions")]
	[Sort(4)]
	public void FinishFirstMission()
	{
		DailyMissionsCollectionComponent dailyMissionsCollection = Contexts.sharedInstance.state.GetEntitiesWithName("DailyMissionData").SingleEntity().dailyMissionsCollection;
		if (dailyMissionsCollection != null)
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("DailyMissionDataSend");
			stateEntity.AddDailyMissionProgressUpdate(dailyMissionsCollection.Collection.DailyMissions[0].MissionData.DAILYMISSIONGROUP, dailyMissionsCollection.Collection.DailyMissions[0].MissionData.Dailymissiongoal);
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.isRefreshScreen = true;
		}
	}

	[Category("Daily Missions")]
	[Sort(5)]
	public void FinishSecondMission()
	{
		DailyMissionsCollectionComponent dailyMissionsCollection = Contexts.sharedInstance.state.GetEntitiesWithName("DailyMissionData").SingleEntity().dailyMissionsCollection;
		if (dailyMissionsCollection != null)
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("DailyMissionDataSend");
			stateEntity.AddDailyMissionProgressUpdate(dailyMissionsCollection.Collection.DailyMissions[1].MissionData.DAILYMISSIONGROUP, dailyMissionsCollection.Collection.DailyMissions[1].MissionData.Dailymissiongoal);
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.isRefreshScreen = true;
		}
	}

	[Category("Daily Missions")]
	[Sort(6)]
	public void FinishThirdMission()
	{
		DailyMissionsCollectionComponent dailyMissionsCollection = Contexts.sharedInstance.state.GetEntitiesWithName("DailyMissionData").SingleEntity().dailyMissionsCollection;
		if (dailyMissionsCollection != null)
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("DailyMissionDataSend");
			stateEntity.AddDailyMissionProgressUpdate(dailyMissionsCollection.Collection.DailyMissions[2].MissionData.DAILYMISSIONGROUP, dailyMissionsCollection.Collection.DailyMissions[2].MissionData.Dailymissiongoal);
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.isRefreshScreen = true;
		}
	}

	[Category("Daily Missions")]
	[Sort(7)]
	public void ResetChestReward()
	{
		DailyMissionsCollectionComponent dailyMissionsCollection = Contexts.sharedInstance.state.GetEntitiesWithName("DailyMissionData").SingleEntity().dailyMissionsCollection;
		if (dailyMissionsCollection != null)
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("DailyMissionDataSend");
			stateEntity.AddDailyMissionChestStateUpdate(false);
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.isRefreshScreen = true;
		}
	}

	[Category("Ingame")]
	[Sort(1)]
	public void GodMode()
	{
		GameEntity playerEntity = Contexts.sharedInstance.game.playerEntity;
		if (playerEntity != null)
		{
			playerEntity.isGodMode = true;
		}
	}

	[Category("Ingame")]
	[Sort(2)]
	public void AlmostDie()
	{
		GameEntity playerEntity = Contexts.sharedInstance.game.playerEntity;
		if (playerEntity != null)
		{
			playerEntity.ReplaceStamina(5f);
		}
	}

	[Category("Ingame")]
	[Sort(3)]
	public void WinLevelWithOneStar()
	{
		WinLevel(1);
	}

	[Category("Ingame")]
	[Sort(4)]
	public void WinLevelWithTwoStars()
	{
		WinLevel(2);
	}

	[Category("Ingame")]
	[Sort(5)]
	public void WinLevelWithThreeStars()
	{
		WinLevel(3);
	}

	[Category("Ingame")]
	[Sort(6)]
	public void TriggerCompanionAbility()
	{
		GameEntity companionEntity = Contexts.sharedInstance.game.companionEntity;
		if (companionEntity != null)
		{
			companionEntity.ReplaceAbilityTimer(0f);
		}
	}

	private void WinLevel(int numberOfStars)
	{
		GameEntity playerEntity = Contexts.sharedInstance.game.playerEntity;
		if (playerEntity != null)
		{
			playerEntity.ReplaceMissionResult(numberOfStars, 0);
			MissionsBalancingData missionsBalancingData = Contexts.sharedInstance.state.balancingDataEntity.missionBalancingData.Map[SettingsService.SettingsSO.WorldIndex];
			int num;
			switch (numberOfStars)
			{
			case 1:
				num = missionsBalancingData.Star1 + 1;
				break;
			case 2:
				num = missionsBalancingData.Star2 + 1;
				break;
			default:
				num = missionsBalancingData.Star3 + 1;
				break;
			}
			int num2 = num;
			Contexts.sharedInstance.game.worldEntity.ReplaceMissionCounter(num2);
			Contexts.sharedInstance.game.playerEntity.ReplaceMissionResult(num2, numberOfStars);
			Time.timeScale = 0f;
			Contexts.sharedInstance.uI.OpenDialog<WinScreen>();
		}
	}

	public StateEntity GetCharPartsEntity(int charId)
	{
		return Contexts.sharedInstance.state.GetEntitiesWithName("CharPartsData" + charId).SingleEntity();
	}

	public void SetCharPartsValue(int charId, int value)
	{
		StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("CharPartsData" + charId).SingleEntity();
		if (stateEntity != null)
		{
			StateEntity stateEntity2 = Contexts.sharedInstance.state.GetEntitiesWithName("CharUnlockData" + charId).SingleEntity();
			stateEntity2.ReplaceBoolean(value > 0);
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.AddUICharacterUnlocked(charId);
			stateEntity.ReplaceInteger(value);
			StateEntity stateEntity3 = Contexts.sharedInstance.state.CreateEntity();
			stateEntity3.isSavedData = true;
			stateEntity3.AddLabel("CharPartsData" + charId);
			stateEntity3.AddName("CharPartsDataSend");
			stateEntity3.AddInteger(value);
			UIEntity uIEntity2 = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity2.isRefreshScreen = true;
		}
	}

	public StateEntity GetCompPartsEntity(int compId)
	{
		return Contexts.sharedInstance.state.GetEntitiesWithName("CompPartsData" + compId).SingleEntity();
	}

	public void SetCompPartsValue(int compId, int value)
	{
		StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("CompPartsData" + compId).SingleEntity();
		if (stateEntity != null)
		{
			StateEntity stateEntity2 = Contexts.sharedInstance.state.GetEntitiesWithName("CompUnlockData" + compId).SingleEntity();
			stateEntity2.ReplaceBoolean(value > 0);
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.AddUICompanionUnlocked(compId);
			stateEntity.ReplaceInteger(value);
			StateEntity stateEntity3 = Contexts.sharedInstance.state.CreateEntity();
			stateEntity3.isSavedData = true;
			stateEntity3.AddLabel("CompPartsData" + compId);
			stateEntity3.AddName("CompPartsDataSend");
			stateEntity3.AddInteger(value);
			UIEntity uIEntity2 = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity2.isRefreshScreen = true;
		}
	}

	[Category("O7SDK")]
	[Sort(1)]
	public void SendLog()
	{
		StateContext state = Contexts.sharedInstance.state;
		StateEntity stateEntity = state.CreateEntity();
		stateEntity.isO7SdkCall = true;
		stateEntity.AddO7SdkSendLogs("ext-publishing@outfit7.com", "Talking Tom Candy Run Logs", string.Empty, null, 9);
	}

	[Category("O7SDK")]
	[Sort(2)]
	public void CrashBuild()
	{
		Debug.LogError("Crash forced");
		StateEntity stateEntity = null;
		string o7SdkVersion = stateEntity.o7SdkManager.Manager.O7SdkVersion;
		Crash(0);
	}

	private void Crash(int i)
	{
		Crash(i++);
	}

	[Category("O7SDK")]
	[Sort(2)]
	public void OpenDebugMenu()
	{
		try
		{
			O7SDKUtilityManager manager = Contexts.sharedInstance.state.o7SdkEntity.o7SdkUtilityManager.Manager;
			manager.ShowDebugConsole();
		}
		catch (Exception ex)
		{
			Debug.LogWarning("[O7SDK] Open Debug Menu Failure: " + ex.Message);
		}
	}

	[Category("O7SDK")]
	[Sort(4)]
	public void PaidUser()
	{
		StateContext state = Contexts.sharedInstance.state;
		StateEntity stateEntity = state.CreateEntity();
		stateEntity.isSavedData = true;
		stateEntity.AddName("PaidUserSend");
		stateEntity.AddBoolean(true);
	}

	[Category("O7SDK")]
	[Sort(5)]
	public void NotPaidUser()
	{
		StateContext state = Contexts.sharedInstance.state;
		StateEntity stateEntity = state.CreateEntity();
		stateEntity.isSavedData = true;
		stateEntity.AddName("PaidUserSend");
		stateEntity.AddBoolean(false);
	}

	[Category("Player Progress")]
	[Sort(3)]
	public void ResetPlayerLevel()
	{
		StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("PlayerLevelExp").SingleEntity();
		if (stateEntity != null)
		{
			stateEntity.ReplaceInteger(0);
			stateEntity.isToSave = true;
		}
		StateEntity stateEntity2 = Contexts.sharedInstance.state.GetEntitiesWithName("PlayerLevelNumber").SingleEntity();
		if (stateEntity2 != null)
		{
			stateEntity2.ReplaceInteger(1);
			stateEntity2.isToSave = true;
		}
		StateEntity stateEntity3 = Contexts.sharedInstance.state.GetEntitiesWithName("PlayerProgressionUpdate").SingleEntity();
		if (stateEntity3 != null)
		{
			string newValue = JsonUtility.ToJson(new PlayerProgressionUpdateData(1, 0));
			stateEntity3.ReplaceStringData(newValue);
		}
		UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
		uIEntity.isRefreshScreen = true;
	}

	[Category("Player Progress")]
	[Sort(1)]
	public void ClearUpgrades()
	{
		Dictionary<int, CharactersStaticBalancingData>.Enumerator enumerator = Contexts.sharedInstance.state.balancingDataEntity.characterStaticBalancingData.Map.GetEnumerator();
		while (enumerator.MoveNext())
		{
			string name = "CharUpgradeData" + enumerator.Current.Key;
			StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName(name).SingleEntity();
			stateEntity.ReplaceInteger(1);
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.isRefreshScreen = true;
		}
	}

	[Category("Player Progress")]
	[Sort(2)]
	public void ResetProfile()
	{
		PlayerPrefs.DeleteAll();
		Contexts.sharedInstance.Reset();
		Time.timeScale = 1f;
		SceneManager.LoadScene("splashScreen");
	}

	[Category("Tutorial")]
	[Sort(1)]
	public void ClearTutorials()
	{
		ClearHitObstacleTutorial();
	}

	[Category("Tutorial")]
	[Sort(1)]
	public void ClearHitObstacleTutorial()
	{
		StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("TutorialStaminaHitData").SingleEntity();
		if (stateEntity != null)
		{
			stateEntity.ReplaceInteger(0);
			StateEntity stateEntity2 = Contexts.sharedInstance.state.CreateEntity();
			stateEntity2.isSavedData = true;
			stateEntity2.AddName("TutorialStaminaHitDataSend");
			stateEntity2.AddInteger(0);
		}
		UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
		uIEntity.isRefreshScreen = true;
	}

	[Category("Tutorial")]
	[Sort(1)]
	public void DisableOutgameTutorial()
	{
		Dictionary<string, List<TutorialBaseStep>> stepsGroups = Contexts.sharedInstance.state.tutorialEntity.tutorialPossibleGroups.StepsGroups;
		foreach (KeyValuePair<string, List<TutorialBaseStep>> item in stepsGroups)
		{
			foreach (TutorialBaseStep item2 in item.Value)
			{
				Contexts.sharedInstance.state.CreateEntity().AddTutorialDisableStep(item2);
			}
		}
		StateEntity tutorialEntity = Contexts.sharedInstance.state.tutorialEntity;
		tutorialEntity.isTutorialDisabled = true;
		List<TutorialOutgameLevel> list = new List<TutorialOutgameLevel>();
		foreach (object value in Enum.GetValues(typeof(TutorialOutgameLevel)))
		{
			list.Add((TutorialOutgameLevel)value);
		}
		IGroup<UIEntity> obj = Contexts.sharedInstance.uI.GetGroup(UIMatcher.TutorialLevelPassedAvailability);
		UIEntity[] entities = obj.GetEntities();
		foreach (UIEntity uIEntity in entities)
		{
			uIEntity.tutorialLevelPassedAvailability.Listener.LevelUnlocked(list);
		}
	}

	[Category("UI Modification")]
	[Sort(1)]
	public void DisableIngameHUD()
	{
		UIContext uI = Contexts.sharedInstance.uI;
		if (uI == null || uI.uIEntity == null || !uI.uIEntity.hasHUD || !(uI.uIEntity.hUD.hud != null))
		{
			return;
		}
		IngameHUD ingameHUD = uI.uIEntity.hUD.hud as IngameHUD;
		if (ingameHUD != null)
		{
			ingameHUD.HideButKeepInteractive();
			HashSet<GameEntity> entitiesWithAsset = Contexts.sharedInstance.game.GetEntitiesWithAsset("AbilityChargingBar");
			GameEntity gameEntity = entitiesWithAsset.SingleEntity();
			if (gameEntity.hasView)
			{
				gameEntity.view.gameObject.SetActive(false);
			}
		}
	}

	[Category("UI Modification")]
	[Sort(2)]
	public void DisablePvEScreenHUD()
	{
		UIContext uI = Contexts.sharedInstance.uI;
		if (uI == null || uI.uIEntity == null || !uI.uIEntity.hasHUD || !(uI.uIEntity.hUD.hud != null))
		{
			return;
		}
		WorldSelectScreen worldSelectScreen = uI.uIEntity.hUD.hud as WorldSelectScreen;
		if (worldSelectScreen != null)
		{
			worldSelectScreen.HideButKeepInteractive();
			CurrencyAndExperienceScreen dialog = uI.GetDialog<CurrencyAndExperienceScreen>();
			if (dialog != null && dialog.gameObject.activeInHierarchy)
			{
				dialog.HideBarWithAnimation();
			}
		}
	}

	[Category("UI Modification")]
	[Sort(3)]
	public void DisableHomeScreenHUD()
	{
		UIContext uI = Contexts.sharedInstance.uI;
		if (uI == null || uI.uIEntity == null || !uI.uIEntity.hasHUD || !(uI.uIEntity.hUD.hud != null))
		{
			return;
		}
		HomeScreen homeScreen = uI.uIEntity.hUD.hud as HomeScreen;
		if (homeScreen != null)
		{
			homeScreen.HideButKeepInteractive();
			CurrencyAndExperienceScreen dialog = uI.GetDialog<CurrencyAndExperienceScreen>();
			if (dialog != null && dialog.gameObject.activeInHierarchy)
			{
				dialog.HideBarWithAnimation();
			}
		}
	}

	public void OnPropertyChanged(string propertyName)
	{
		if (this.PropertyChanged != null)
		{
			this.PropertyChanged(this, propertyName);
		}
	}
}
