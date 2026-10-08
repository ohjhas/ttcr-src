using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class DailyMissionEntry : MonoBehaviour
	{
		[Space(5f)]
		[Header("Childs")]
		[SerializeField]
		private TextMeshProUGUI _title;

		[SerializeField]
		private TextMeshProUGUI _completedText;

		[SerializeField]
		private Image _icon;

		[SerializeField]
		private TextMeshProUGUI _counter;

		[SerializeField]
		private Image _gaugeFillImage;

		[SerializeField]
		private GameObject _claimButton;

		[SerializeField]
		private GameObject _goToButton;

		[SerializeField]
		private GameObject _completeIcon;

		[SerializeField]
		private DailyMissionReward _reward;

		[SerializeField]
		private GameObject _fade;

		[SerializeField]
		private GameObject _rewardCollectedText;

		private DailyMissionData _data;

		public void SetData(DailyMissionData data)
		{
			_data = data;
			SetTitle();
			SetIcon();
			SetCounter();
			SetElementsVisibility();
			_reward.Init(data);
		}

		private void SetTitle()
		{
			if (_data != null)
			{
				string text = ScriptLocalization.Get(_data.MissionData.Title);
				_title.text = (string.IsNullOrEmpty(text) ? string.Empty : text);
			}
		}

		private void SetIcon()
		{
			if (_data != null)
			{
				AssetsSettingsSO.DailyMissionIconAsset dailyMissionIcon = SettingsService.AssetsSettingsSO.GetDailyMissionIcon(_data.MissionData.DAILYMISSIONTYPE);
				if (dailyMissionIcon != null)
				{
					_icon.sprite = dailyMissionIcon.Sprite;
					_icon.transform.localScale = dailyMissionIcon.Size * Vector3.one;
					_icon.rectTransform.anchoredPosition = dailyMissionIcon.Offset;
				}
			}
		}

		private void SetCounter()
		{
			if (_data != null)
			{
				int num = Mathf.Min(_data.CurrentProgress, _data.MissionData.Dailymissiongoal);
				int dailymissiongoal = _data.MissionData.Dailymissiongoal;
				_counter.text = string.Format("{0}/{1}", NumbersHelper.GetNumberSplitedBySpaces(num), NumbersHelper.GetNumberSplitedBySpaces(dailymissiongoal));
				_gaugeFillImage.fillAmount = (float)num / (float)dailymissiongoal;
			}
		}

		private void SetElementsVisibility()
		{
			if (_data != null)
			{
				_claimButton.SetActive(!_data.IsClaimed && _data.CurrentProgress >= _data.MissionData.Dailymissiongoal);
				_goToButton.SetActive(!_data.IsClaimed && _data.CurrentProgress < _data.MissionData.Dailymissiongoal);
				_completeIcon.SetActive(_data.IsClaimed);
				_title.gameObject.SetActive(!_data.IsClaimed);
				_completedText.gameObject.SetActive(_data.IsClaimed);
				_fade.SetActive(_data.IsClaimed);
				_rewardCollectedText.SetActive(_data.IsClaimed);
				_reward.gameObject.SetActive(!_data.IsClaimed);
			}
		}

		public void OnGoButtonPressed()
		{
			if (_data != null)
			{
				switch (_data.MissionData.DAILYMISSIONTYPE)
				{
				case DailyMissionType.Destroy_Obstacles:
				case DailyMissionType.Collect_Coins:
				case DailyMissionType.Collect_CoinsBig:
				case DailyMissionType.Collect_ScoreJelly:
				case DailyMissionType.Collect_ScoreIceCream:
				case DailyMissionType.Collect_ScoreCake:
				case DailyMissionType.Collect_ScoreFruit:
				case DailyMissionType.Collect_ScoreCandy:
				case DailyMissionType.Collect_ScoreTasty:
				case DailyMissionType.Collect_ScoreJellyMedium:
				case DailyMissionType.Collect_ScoreIceCreamMedium:
				case DailyMissionType.Collect_ScoreCakeMedium:
				case DailyMissionType.Collect_ScoreFruitMedium:
				case DailyMissionType.Collect_ScoreCandyMedium:
				case DailyMissionType.Collect_ScoreTastyMedium:
				case DailyMissionType.Collect_ScoreJellyBig:
				case DailyMissionType.Collect_ScoreIceCreamBig:
				case DailyMissionType.Collect_ScoreCakeBig:
				case DailyMissionType.Collect_ScoreFruitBig:
				case DailyMissionType.Collect_ScoreCandyBig:
				case DailyMissionType.Collect_ScoreTastyBig:
				case DailyMissionType.Collect_BoosterStamina:
				case DailyMissionType.Collect_BossterBigStamina:
				case DailyMissionType.Collect_BoosterGiant:
				case DailyMissionType.Collect_BoosterMagnet:
				case DailyMissionType.Collect_BoosterSpeedup:
				case DailyMissionType.Collect_BoosterScoreMultipier:
				case DailyMissionType.Collect_BoosterUpgradeScore:
				case DailyMissionType.Play_Missions:
					GoToWorldSelectScreen();
					break;
				case DailyMissionType.Upgrade_Character:
					GoToCharSelectScreen();
					break;
				case DailyMissionType.Upgrade_Companion:
					GoToCompSelectScreen();
					break;
				case DailyMissionType.Open_Chests:
					GoToShopScreen();
					break;
				}
			}
		}

		public void OnClaimButtonPressed()
		{
			if (_data != null)
			{
				_reward.GetReward(_data);
			}
		}

		private void GoToWorldSelectScreen()
		{
			BaseDialog hud = Contexts.sharedInstance.uI.uIEntity.hUD.hud;
			if (hud is WorldSelectScreen)
			{
				InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
				inputEntity.isInput = true;
				inputEntity.isGoBack = true;
			}
			else if (hud is HomeScreen)
			{
				UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
				uIEntity.isHomeScreen = true;
				uIEntity.isUIOpenPvE = true;
			}
		}

		private void GoToCharSelectScreen()
		{
			UIEntity properScreenAction = GetProperScreenAction();
			if (properScreenAction != null)
			{
				properScreenAction.isUIOpenCharSelection = true;
			}
		}

		private void GoToCompSelectScreen()
		{
			UIEntity properScreenAction = GetProperScreenAction();
			if (properScreenAction != null)
			{
				properScreenAction.isUIOpenCompSelection = true;
			}
		}

		private void GoToShopScreen()
		{
			UIEntity properScreenAction = GetProperScreenAction();
			if (properScreenAction != null)
			{
				properScreenAction.AddUIOpenShop(ShopGroupType.Chests);
			}
		}

		private UIEntity GetProperScreenAction()
		{
			if (Contexts.sharedInstance != null && Contexts.sharedInstance.uI != null && Contexts.sharedInstance.uI.uIEntity.hasHUD && Contexts.sharedInstance.uI.uIEntity.hUD.hud != null)
			{
				UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
				BaseDialog hud = Contexts.sharedInstance.uI.uIEntity.hUD.hud;
				if (hud is HomeScreen)
				{
					uIEntity.isHomeScreen = true;
					return uIEntity;
				}
				if (hud is WorldSelectScreen)
				{
					uIEntity.isWorldSelectScreen = true;
					return uIEntity;
				}
				uIEntity.isDestroyed = true;
			}
			return null;
		}
	}
}
