using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class DisplayMissionDetailsSystem : ReactiveSystem<UIEntity>
	{
		private readonly UIContext uiContext;

		private readonly StateContext stateContext;

		public DisplayMissionDetailsSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			uiContext = contexts.uI;
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.UIAction);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.hasUIDisplayMissionDetails || entity.isUIHideMissionsDetails;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.hasUIDisplayMissionDetails)
				{
					SaveLastSelectedMission(entity.uIDisplayMissionDetails.LevelId);
					OpenMissionDetailsPopup(entity.uIDisplayMissionDetails.LevelId);
				}
				else if (entity.isUIHideMissionsDetails)
				{
					CloseMissionDetailsPopup();
				}
				entity.isDestroyed = true;
			}
		}

		private void SaveLastSelectedMission(int levelId)
		{
			SettingsService.SettingsSO.WorldIndex = levelId;
			StateEntity stateEntity = stateContext.GetEntitiesWithName("SelectedLevel").SingleEntity();
			stateEntity.integer.Amount = levelId;
		}

		private void OpenMissionDetailsPopup(int level)
		{
			BaseDialog baseDialog = uiContext.OpenDialog<MissionDetailsPopup>();
			if (baseDialog != null)
			{
				UIEntity uIEntity = uiContext.CreateEntity();
				uIEntity.isUIMissionListRequest = true;
				uIEntity.isUIChestListRequest = true;
				uIEntity.AddSetup(baseDialog);
				uIEntity.AddUIDisplayMissionDetails(level);
			}
		}

		private void CloseMissionDetailsPopup()
		{
			MissionDetailsPopup dialog = uiContext.GetDialog<MissionDetailsPopup>();
			if (dialog != null)
			{
				uiContext.CloseDialog(dialog);
			}
		}
	}
}
