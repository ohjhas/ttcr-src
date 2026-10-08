using System.Collections;
using DG.Tweening;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class CRNewReward : MonoBehaviour
	{
		[SerializeField]
		private CanvasGroup canvasGroup;

		[SerializeField]
		private TextMeshProUGUI titleText;

		[SerializeField]
		private TextMeshProUGUI nameText;

		[SerializeField]
		private RectTransform speachBubbleTransform;

		[SerializeField]
		private TextMeshProUGUI speachBubbleText;

		[SerializeField]
		private CanvasParticleEmitter unlockParticle;

		[SerializeField]
		private CanvasParticleEmitter confetiParticle;

		private Coroutine showCoroutine;

		private void OnEnable()
		{
			canvasGroup.alpha = 0f;
		}

		public void SetData(GeneratedReward reward, CSCharacterData? characterData = null, CSCompanionData companionData = null)
		{
			if (reward.IsNew && (characterData.HasValue || companionData != null))
			{
				Show(characterData, companionData);
			}
			else
			{
				Hide();
			}
		}

		private IEnumerator ShowAnimation(CSCharacterData? characterData, CSCompanionData companionData)
		{
			titleText.transform.localScale = Vector3.zero;
			speachBubbleTransform.localScale = Vector3.zero;
			yield return null;
			yield return null;
			unlockParticle.RestartEmitter();
			confetiParticle.RestartEmitter();
			canvasGroup.DOFade(1f, 0.3f).SetUpdate(UpdateType.Normal, true);
			if (characterData.HasValue)
			{
				SetCharData(characterData);
			}
			else if (companionData != null)
			{
				SetCompData(companionData);
			}
			titleText.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetDelay(0.5f)
				.SetUpdate(UpdateType.Normal, true);
			if (characterData.HasValue)
			{
				speachBubbleTransform.DOScale(1f, 0.3f).SetEase(Ease.OutBack).SetDelay(0.8f)
					.SetUpdate(UpdateType.Normal, true);
			}
		}

		private void Show(CSCharacterData? characterData, CSCompanionData companionData)
		{
			if (showCoroutine != null)
			{
				StopCoroutine(showCoroutine);
				showCoroutine = null;
			}
			showCoroutine = StartCoroutine(ShowAnimation(characterData, companionData));
		}

		private void Hide()
		{
			if (showCoroutine != null)
			{
				StopCoroutine(showCoroutine);
				showCoroutine = null;
				Debug.LogWarning("[CR New Reward] Show coroutine killed");
			}
			canvasGroup.DOFade(0f, 0.3f).SetUpdate(UpdateType.Normal, true);
		}

		private void SetCharData(CSCharacterData? characterData)
		{
			if (characterData.HasValue)
			{
				titleText.text = ScriptLocalization.chests_new_character;
				nameText.text = characterData.Value.Name;
				speachBubbleText.text = characterData.Value.WelcomeMessage;
			}
		}

		private void SetCompData(CSCompanionData companionData)
		{
			if (companionData != null)
			{
				titleText.text = ScriptLocalization.chests_new_companion;
				nameText.text = companionData.Name;
			}
		}
	}
}
