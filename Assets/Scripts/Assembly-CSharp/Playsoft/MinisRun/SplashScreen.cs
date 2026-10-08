using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class SplashScreen : BaseDialog, ISaveDataLoadedListener
	{
		private bool isDataLoaded;

		private bool isSDKLoaded;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.SplashScreen;
			}
		}

		private void Start()
		{
			base.uiContext.CreateEntity().AddSaveDataLoadedListener(this);
			base.IsClickable = true;
			Contexts.sharedInstance.state.CreateEntity().AddMuteMusic(true);
		}

		private void Update()
		{
			if (!base.IsClickable)
			{
				return;
			}
			CheckSDKState();
			if (!isDataLoaded || !isSDKLoaded)
			{
				return;
			}
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.isIntialSceneDecider = true;
			base.IsClickable = false;
			if (!GameIntroHelper.IsIntroTime())
			{
				HashSet<StateEntity> entitiesWithName = Contexts.sharedInstance.state.GetEntitiesWithName("Music");
				if (entitiesWithName.Count <= 0 || entitiesWithName.SingleEntity().boolean.Value)
				{
					Contexts.sharedInstance.state.CreateEntity().AddMuteMusic(false);
				}
			}
		}

		public void OnDataLoaded()
		{
			isDataLoaded = true;
		}

		private void CheckSDKState()
		{
			isSDKLoaded = Contexts.sharedInstance.state.o7SdkEntity != null && Contexts.sharedInstance.state.o7SdkEntity.isO7SdkReadyToLoadGame;
		}
	}
}
