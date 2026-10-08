using System;
using System.Collections.Generic;
using Playsoft.MinisRun;
using UnityEngine;

public static class SettingsService
{
	private static string settingsPath = "SO/";

	private static Dictionary<Type, ScriptableObject> availableSettings = new Dictionary<Type, ScriptableObject>();

	private const string settingsSOPath = "SO/SettingsSO";

	private static SettingsSO settingsSO;

	private const string assetsSettings = "SO/AssetsSettings";

	private static AssetsSettingsSO assetsSettingsSO;

	private const string tutorialSteps = "SO/Tutorial/OutgameTutorialSteps";

	private static TutorialSteps tutorialOutgameSteps;

	public static SettingsSO SettingsSO
	{
		get
		{
			if (settingsSO == null)
			{
				settingsSO = Resources.Load("SO/SettingsSO") as SettingsSO;
			}
			return settingsSO;
		}
	}

	public static AssetsSettingsSO AssetsSettingsSO
	{
		get
		{
			if (assetsSettingsSO == null)
			{
				assetsSettingsSO = Resources.Load("SO/AssetsSettings") as AssetsSettingsSO;
			}
			return assetsSettingsSO;
		}
	}

	public static TutorialSteps TutorialOutgameSteps
	{
		get
		{
			if (tutorialOutgameSteps == null)
			{
				tutorialOutgameSteps = Resources.Load("SO/Tutorial/OutgameTutorialSteps") as TutorialSteps;
			}
			return tutorialOutgameSteps;
		}
	}

	public static T GetSetting<T>() where T : ScriptableObject
	{
		if (availableSettings.ContainsKey(typeof(T)))
		{
			return availableSettings[typeof(T)] as T;
		}
		T val = Resources.Load(settingsPath + typeof(T).Name, typeof(T)) as T;
		if (val != null)
		{
			availableSettings.Add(typeof(T), val);
			return val;
		}
		return (T)null;
	}
}
