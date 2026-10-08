using System;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WinScreenCharacter
	{
		[SerializeField]
		private RectTransform characterSlot;

		public void Init()
		{
			PutCharactersIntoSlots();
		}

		private void PutCharactersIntoSlots()
		{
			int amount = Contexts.sharedInstance.state.GetEntitiesWithName("FirstCharSelectData").SingleEntity().integer.Amount;
			characterSlot.gameObject.SetActive(true);
			RectTransform characterUIAsset = Contexts.sharedInstance.uI.GetCharacterUIAsset(amount);
			RectTransform rectTransform = UnityEngine.Object.Instantiate(characterUIAsset);
			if (!(null == rectTransform))
			{
				rectTransform.SetParent(characterSlot.transform);
				rectTransform.localScale = Vector3.one;
				rectTransform.anchoredPosition3D = Vector3.zero;
				rectTransform.sizeDelta = Vector2.zero;
				PlayCharacterAnim(amount, rectTransform.GetComponent<Animator>(), characterSlot);
			}
		}

		private void PlayCharacterAnim(int charIndex, Animator character, RectTransform slot)
		{
			CharacterAnimatorController component = character.GetComponent<CharacterAnimatorController>();
			if (component != null)
			{
				component.ForceAnimation(charIndex, CharAnimationType.JumpAndWin);
			}
			else
			{
				character.Play("UIJumpAndWinAnim");
			}
		}
	}
}
