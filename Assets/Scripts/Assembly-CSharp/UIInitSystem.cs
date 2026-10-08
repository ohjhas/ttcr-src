using System;
using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;
using UnityEngine;

public class UIInitSystem : IInitializeSystem, ISystem
{
	private const string uiRootPath = "UICanvas";

	private const string hudLayerPath = "HUDLayer";

	private const string screenLayerPath = "ScreenLayer";

	private readonly UIContext uiContext;

	public UIInitSystem(Contexts contexts)
	{
		uiContext = contexts.uI;
	}

	public void Initialize()
	{
		RefreshUIEntity();
		GameObject rootGO = GameObject.Find("UICanvas");
		GameObject layerRoot = GetLayerRoot(rootGO, "HUDLayer");
		BaseDialog[] hudDialogs = FindDialogsInLayer(layerRoot);
		SetupHUDLayer(hudDialogs);
		GameObject layerRoot2 = GetLayerRoot(rootGO, "ScreenLayer");
		BaseDialog[] screenDialogs = FindDialogsInLayer(layerRoot2);
		SetupScreenLayer(layerRoot2, screenDialogs);
		TutorialLayer tutorialLayer = UnityEngine.Object.FindObjectOfType<TutorialLayer>();
		SetTutorialLayer(tutorialLayer);
	}

	private void RefreshUIEntity()
	{
		if (uiContext.uIEntity != null)
		{
			uiContext.uIEntity.Destroy();
		}
		UIEntity uIEntity = uiContext.CreateEntity();
		uIEntity.isUI = true;
	}

	private BaseDialog[] FindDialogsInLayer(GameObject layer)
	{
		if (layer != null)
		{
			return layer.GetComponentsInChildren<BaseDialog>(true);
		}
		return null;
	}

	private GameObject GetLayerRoot(GameObject rootGO, string layerPath)
	{
		if (rootGO != null)
		{
			Transform transform = rootGO.transform.Find(layerPath);
			if (transform != null)
			{
				return transform.gameObject;
			}
		}
		return null;
	}

	private void SetupHUDLayer(BaseDialog[] hudDialogs)
	{
		if (hudDialogs != null && hudDialogs.Length > 0)
		{
			uiContext.uIEntity.AddHUD(hudDialogs[0]);
			if (Contexts.sharedInstance.state.hasActiveScreen)
			{
				Contexts.sharedInstance.state.ReplacePreviousScreen(Contexts.sharedInstance.state.activeScreen.ScreenId);
			}
			else
			{
				Contexts.sharedInstance.state.ReplacePreviousScreen(hudDialogs[0].Id);
			}
			Contexts.sharedInstance.state.ReplaceActiveScreen(hudDialogs[0].Id);
		}
	}

	private void SetupScreenLayer(GameObject root, BaseDialog[] screenDialogs)
	{
		uiContext.uIEntity.AddOpenedScreens(new List<BaseDialog>());
		if (screenDialogs != null)
		{
			uiContext.uIEntity.AddScreenMap(root, new Dictionary<Type, GameObject>());
			for (int i = 0; i < screenDialogs.Length; i++)
			{
				GameObject gameObject = screenDialogs[i].GetGameObject();
				uiContext.uIEntity.screenMap.ScreenMap.Add(screenDialogs[i].GetType(), gameObject);
				gameObject.SetActive(false);
			}
		}
	}

	private void SetTutorialLayer(TutorialLayer layer)
	{
		uiContext.uIEntity.AddTutorialLayer(layer);
	}
}
