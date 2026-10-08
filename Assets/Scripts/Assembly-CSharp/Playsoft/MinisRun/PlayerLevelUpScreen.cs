using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class PlayerLevelUpScreen : BaseScreen
	{
		[SerializeField]
		private TextMeshProUGUI levelNumberText;

		[SerializeField]
		private RectTransform titlePartRect;

		[SerializeField]
		private Button confirmButton;

		[SerializeField]
		private CanvasParticleEmitter particleEmitter;

		[SerializeField]
		private RewardEntry[] rewardEntries;

		private List<RewardData> rewards = new List<RewardData>();

		private bool isSequenceBeingPlayed;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.LevelUpPopup;
			}
		}

		public override void SetData(UIEntity parameters)
		{
			if (isSequenceBeingPlayed)
			{
				return;
			}
			isSequenceBeingPlayed = true;
			base.SetData(parameters);
			RewardEntry[] array = rewardEntries;
			foreach (RewardEntry rewardEntry in array)
			{
				rewardEntry.gameObject.SetActive(false);
			}
			confirmButton.onClick.RemoveAllListeners();
			confirmButton.onClick.AddListener(ConfirmClicked);
			AnimateTitle();
			Debug.Log("parameters.hasUIPlayerLevel: " + parameters.hasUIPlayerLevel);
			if (parameters.hasUIPlayerLevel)
			{
				rewards = Contexts.sharedInstance.state.balancingDataEntity.playerLevelBalancingData.Map[parameters.uIPlayerLevel.CurrentLevel].Rewards;
				for (int j = 0; j < rewards.Count; j++)
				{
					rewardEntries[j].gameObject.SetActive(true);
					rewardEntries[j].transform.localScale = Vector3.zero;
					rewardEntries[j].transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack).SetDelay(1.8f + 0.3f * (float)j)
						.SetUpdate(true);
					rewardEntries[j].SetUp(rewards[j]);
				}
				confirmButton.transform.parent.localScale = Vector3.zero;
				confirmButton.transform.parent.DOScale(1f, 0.5f).SetEase(Ease.OutBack).SetDelay(2f + 0.3f * (float)rewards.Count)
					.SetUpdate(true)
					.OnComplete(delegate
					{
						isSequenceBeingPlayed = false;
					});
				levelNumberText.text = parameters.uIPlayerLevel.CurrentLevel.ToString();
			}
		}

		private void AnimateTitle()
		{
			RectTransform rectTransform = levelNumberText.transform as RectTransform;
			float y = rectTransform.anchoredPosition.y;
			float y2 = titlePartRect.anchoredPosition.y;
			rectTransform.anchoredPosition = new Vector2(0f, 820f);
			titlePartRect.anchoredPosition = new Vector2(0f, 820f);
			titlePartRect.DOAnchorPosY(y2, 0.5f).SetEase(Ease.OutBack).SetDelay(0.8f)
				.SetUpdate(true);
			rectTransform.DOAnchorPosY(y, 0.5f).SetEase(Ease.OutBack).SetDelay(0.9f)
				.SetUpdate(true);
			DOVirtual.DelayedCall(1f, delegate
			{
				particleEmitter.StartEmitter();
			}).SetUpdate(true);
		}

		private void ConfirmClicked()
		{
			if (base.IsClickable)
			{
				base.IsClickable = false;
				GiveSoftCurrency();
				GiveHardCurrency();
				DOVirtual.DelayedCall(1f, delegate
				{
					base.uiContext.CloseDialog(this);
					GiveChest();
				});
			}
		}

		private void GiveSoftCurrency()
		{
			RewardData rewardData = rewards.FirstOrDefault((RewardData x) => x.Type == ItemType.Currency && x.Index == ProjectConstants.SOFT_CURRENCY_ID);
			if (rewardData.Amount > 0)
			{
				int num = rewards.IndexOf(rewardData);
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isUIAction = true;
				uIEntity.AddUIClaimReward(rewardData, rewardEntries[num].GetComponent<RectTransform>(), false);
			}
		}

		private void GiveHardCurrency()
		{
			RewardData rewardData = rewards.FirstOrDefault((RewardData x) => x.Type == ItemType.Currency && x.Index == ProjectConstants.HARD_CURRENCY_ID);
			if (rewardData.Amount > 0)
			{
				int num = rewards.IndexOf(rewardData);
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isUIAction = true;
				uIEntity.AddUIClaimReward(rewardData, rewardEntries[num].GetComponent<RectTransform>(), false);
			}
		}

		private void GiveChest()
		{
			RewardData newData = rewards.FirstOrDefault((RewardData x) => x.Type == ItemType.Chest);
			if (newData.Amount > 0)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isUIAction = true;
				uIEntity.AddUIClaimReward(newData, null, false);
			}
		}
	}
}
