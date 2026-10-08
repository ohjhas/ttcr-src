using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public static class UIExtensions
{
	public static BaseDialog OpenDialog<T>(this UIContext context, bool trackOnStack = true) where T : BaseDialog
	{
		BaseDialog baseDialog = context.FindCurrentScreen();
		BaseDialog baseDialog2 = context.OpenBaseDialog<T>();
		if (null == baseDialog2)
		{
			Debug.LogError(string.Format("Dialog {0} was not possible to open", typeof(T)));
			return null;
		}
		if (trackOnStack && !context.uIEntity.openedScreens.Dialogs.Contains(baseDialog2))
		{
			context.uIEntity.openedScreens.Dialogs.Insert(0, baseDialog2);
		}
		if (trackOnStack)
		{
			if (baseDialog != null)
			{
				Contexts.sharedInstance.state.ReplacePreviousScreen(baseDialog.Id);
			}
			Contexts.sharedInstance.state.ReplaceActiveScreen(baseDialog2.Id);
		}
		Contexts.sharedInstance.state.sessionEntity.ReplaceTransitionInformation(0f);
		return baseDialog2;
	}

	public static T GetDialog<T>(this UIContext context) where T : BaseDialog
	{
		if (context != null && context.uIEntity != null && context.uIEntity.hasScreenMap && context.uIEntity.screenMap.ScreenMap != null)
		{
			Dictionary<Type, GameObject> screenMap = context.uIEntity.screenMap.ScreenMap;
			GameObject gameObject = ((!screenMap.ContainsKey(typeof(T))) ? null : screenMap[typeof(T)]);
			if (gameObject != null)
			{
				return gameObject.GetComponent<T>();
			}
		}
		return (T)null;
	}

	public static void CloseDialog(this UIContext context, BaseDialog dialog)
	{
		dialog.SetVisible(false);
		if (context.uIEntity != null && context.uIEntity.hasOpenedScreens && context.uIEntity.openedScreens.Dialogs != null && context.uIEntity.openedScreens.Dialogs.Remove(dialog))
		{
			Contexts.sharedInstance.state.ReplacePreviousScreen(dialog.Id);
		}
		BaseDialog baseDialog = context.FindCurrentScreen();
		if (baseDialog != null && !Contexts.sharedInstance.state.activeScreen.ScreenId.Equals(baseDialog.Id))
		{
			Contexts.sharedInstance.state.ReplaceActiveScreen(baseDialog.Id);
		}
	}

	public static void CloseDialog(this UIContext context, ScreenId screenId)
	{
		BaseDialog baseDialog = context.uIEntity.openedScreens.Dialogs.FirstOrDefault((BaseDialog x) => x.Id == screenId);
		if (baseDialog != null)
		{
			context.CloseDialog(baseDialog);
		}
	}

	public static BaseDialog FindCurrentScreen(this UIContext context)
	{
		BaseDialog result = ((context.uIEntity == null || !context.uIEntity.hasHUD || !(context.uIEntity.hUD.hud != null)) ? null : context.uIEntity.hUD.hud);
		if (context.uIEntity != null && context.uIEntity.hasOpenedScreens && context.uIEntity.openedScreens.Dialogs != null && context.uIEntity.openedScreens.Dialogs.Count > 0)
		{
			result = context.uIEntity.openedScreens.Dialogs[0];
		}
		return result;
	}

	public static void RefreshScreen(this UIContext context)
	{
		UIEntity uIEntity = context.CreateEntity();
		uIEntity.isRefreshScreen = true;
	}

	public static void CheckPlayerProgress(this UIContext context)
	{
		UIEntity uIEntity = context.CreateEntity();
		uIEntity.isUIAction = true;
		uIEntity.isUICheckPlayerProgress = true;
	}

	public static RectTransform GetRaccoonUIAsset(this UIContext context)
	{
		string path = "Characters/UI/UIRaccoon";
		return Resources.Load<RectTransform>(path);
	}

	public static void GetRacoonUIAssetAsync(this UIContext context, Action<RectTransform> callback)
	{
		string path = "Characters/UI/UIRaccoon";
		ResourceRequest loadRequest = Resources.LoadAsync<RectTransform>(path);
		UIEntity uIEntity = context.CreateEntity();
		uIEntity.AddAsyncRequest(loadRequest, delegate
		{
			callback((RectTransform)loadRequest.asset);
		});
	}

	public static RectTransform GetCharacterUIAsset(this UIContext context, int charId)
	{
		string path = "Characters/UI/" + SettingsService.SettingsSO.CharSettings[charId].UIAssetName;
		return Resources.Load<RectTransform>(path);
	}

	public static void GetCharacterUIAssetAsync(this UIContext context, int charId, Action<RectTransform> callback)
	{
		string path = "Characters/UI/" + SettingsService.SettingsSO.CharSettings[charId].UIAssetName;
		ResourceRequest loadRequest = Resources.LoadAsync<RectTransform>(path);
		UIEntity uIEntity = context.CreateEntity();
		uIEntity.AddAsyncRequest(loadRequest, delegate
		{
			callback((RectTransform)loadRequest.asset);
		});
	}

	public static RectTransform GetCharacterCardUIAsset(this UIContext context, int charId)
	{
		string path = "Characters/UI/Cards/" + SettingsService.SettingsSO.CharSettings[charId].UICardAssetName;
		return Resources.Load<RectTransform>(path);
	}

	public static RectTransform GetCompanionUIAsset(this UIContext context, int compId)
	{
		string path = "Companions/UI/" + SettingsService.SettingsSO.CompanionSettings[compId].UIAssetName;
		return Resources.Load<RectTransform>(path);
	}

	public static void GetCompanionUIAssetAsync(this UIContext context, int compId, Action<RectTransform> callback)
	{
		string path = "Companions/UI/" + SettingsService.SettingsSO.CompanionSettings[compId].UIAssetName;
		ResourceRequest loadRequest = Resources.LoadAsync<RectTransform>(path);
		UIEntity uIEntity = context.CreateEntity();
		uIEntity.AddAsyncRequest(loadRequest, delegate
		{
			callback((RectTransform)loadRequest.asset);
		});
	}

	public static void GetCompanionUIAssetAsync(this UIContext context, int compId, Action<RectTransform, UnityAction> callback, UnityAction action)
	{
		string path = "Companions/UI/" + SettingsService.SettingsSO.CompanionSettings[compId].UIAssetName;
		ResourceRequest assetRequest = Resources.LoadAsync<RectTransform>(path);
		UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
		uIEntity.AddAsyncRequest(assetRequest, delegate
		{
			callback((RectTransform)assetRequest.asset, action);
		});
	}

	public static RectTransform GetCompanionCardUIAsset(this UIContext context, int compId)
	{
		string path = "Companions/UI/Cards/" + SettingsService.SettingsSO.CompanionSettings[compId].UICardAssetName;
		return Resources.Load<RectTransform>(path);
	}

	private static BaseDialog OpenBaseDialog<T>(this UIContext context) where T : BaseDialog
	{
		BaseDialog baseDialog = context.GetDialog<T>();
		if (baseDialog == null)
		{
			baseDialog = context.CreateDialogFromResources<T>();
			if (baseDialog == null)
			{
				Debug.LogError("No Dialog found " + typeof(T));
				return null;
			}
		}
		baseDialog.SetVisible(true);
		baseDialog.transform.SetAsLastSibling();
		return baseDialog;
	}

	private static BaseDialog CreateDialogFromResources<T>(this UIContext context) where T : BaseDialog
	{
		T val = Resources.Load("UI/Screens/" + typeof(T).Name, typeof(T)) as T;
		if (val != null && context.uIEntity != null && context.uIEntity.hasScreenMap && context.uIEntity.screenMap.Root != null && context.uIEntity.screenMap.ScreenMap != null)
		{
			T val2 = UnityEngine.Object.Instantiate(val, context.uIEntity.screenMap.Root.transform);
			if (val2 != null)
			{
				RectTransform component = val2.GetComponent<RectTransform>();
				context.uIEntity.screenMap.ScreenMap.Add(typeof(T), val2.GetGameObject());
				component.localScale = Vector3.one;
				component.anchorMin = Vector2.zero;
				component.anchorMax = Vector2.one;
				component.anchoredPosition3D = Vector3.zero;
				component.sizeDelta = Vector2.zero;
				val2.GetGameObject().SetActive(false);
			}
			return val2;
		}
		return null;
	}
}
