using UnityEngine;

namespace Playsoft.MinisRun
{
	public class WinScreen : BaseDialog
	{
		private const string ANIMATION_SHOW_RESULT_TRIGGER = "PlayResultScreenAnimation";

		[SerializeField]
		private WinScreenCharacter character;

		[SerializeField]
		private WinScreenStarsAndBar starsAndBar;

		[SerializeField]
		private WinScreenCoins coins;

		[SerializeField]
		private WinScreenChest chest;

		[SerializeField]
		private WinScreenButtons buttons;

		private Animator animator;

		private MissionsBalancingData missionData;

		private GameEntity player;

		private GameEntity world;

		private bool hasPlayerThreeStars;

		private int chestIndex;

		private LevelTutorialSettings levelSettings;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.WinScreen;
			}
		}

		protected override void Setup()
		{
			base.Setup();
			animator = GetComponent<Animator>();
		}

		public override void SetVisible(bool visible)
		{
			base.SetVisible(visible);
			if (visible)
			{
				player = Contexts.sharedInstance.game.playerEntity;
				world = Contexts.sharedInstance.game.worldEntity;
				levelSettings = world.levelSettings.levelTutorialSettings;
				hasPlayerThreeStars = player.hasMissionResult && player.missionResult.Stars >= 3;
				chestIndex = ChestHelper.GetChestIdIfAvailable(hasPlayerThreeStars, levelSettings.ForceChestReward);
				AnimateEverything();
			}
		}

		private void AnimateEverything()
		{
			animator.SetTrigger("PlayResultScreenAnimation");
			bool goalBar = levelSettings.ResultSettings.GoalBar;
			bool flag = chestIndex >= 0;
			character.Init();
			coins.Init(this, goalBar);
			starsAndBar.Init(this, goalBar, flag, hasPlayerThreeStars);
			buttons.Init(this, flag, player.missionResult.Stars);
			chest.Init(this, animator, chestIndex, flag);
		}
	}
}
