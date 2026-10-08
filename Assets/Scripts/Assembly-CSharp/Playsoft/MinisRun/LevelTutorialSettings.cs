using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class LevelTutorialSettings
	{
		public bool GodMode;

		public bool CompanionEnabled = true;

		public bool CharacterAbilityEnabeld = true;

		public bool ForceChestReward;

		public bool LongerMissionGoalPopup;

		public bool ShowStaminaHitTutorialIfHappens = true;

		public AfterLoseAction AfterLoseAction;

		public HudSettings HudSettings = new HudSettings();

		public ResultScreenSettings ResultSettings = new ResultScreenSettings();
	}
}
