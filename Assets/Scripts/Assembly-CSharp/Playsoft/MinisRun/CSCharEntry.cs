using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class CSCharEntry : MonoBehaviour
	{
		[SerializeField]
		private int id;

		[SerializeField]
		private Animator charAnimator;

		[SerializeField]
		private Image charRenderer;

		[SerializeField]
		private GameObject Lock;

		[SerializeField]
		private Transform newLabelHolder;

		[SerializeField]
		private NewLabel newLabelPrefab;

		[SerializeField]
		private float characterSizeMultiplier;

		[SerializeField]
		private CharEntryPositioner charPositioner;

		[SerializeField]
		private Button button;

		private NewLabel newLabel;

		private CharacterAnimatorController animatorController;

		private Animator runningCharAnimator;

		private RectTransform parentRectTransform;

		public int Id { get; private set; }

		private void Start()
		{
			Id = id;
			ScrollRect componentInParent = GetComponentInParent<ScrollRect>();
			parentRectTransform = ((!(componentInParent != null)) ? null : (componentInParent.transform as RectTransform));
		}

		public void SetState(bool isEnabled, bool isNew)
		{
			charAnimator.enabled = isEnabled && !Lock.activeSelf;
			Color color = ((!isEnabled || Lock.activeSelf) ? Color.gray : Color.white);
			charRenderer.color = color;
			charPositioner.Init(charRenderer, characterSizeMultiplier);
			if (newLabel != null)
			{
				if (isNew)
				{
					newLabel.Show();
				}
				else
				{
					newLabel.Hide();
				}
			}
		}

		public void SetData(int ownedParts, int neededParts, bool isNextUpgradeAvailable, bool isUnlocked)
		{
			Lock.SetActive(!isUnlocked);
			Init();
		}

		public void ShowWinAnim()
		{
			animatorController = charAnimator.GetComponent<CharacterAnimatorController>();
			if (animatorController != null)
			{
				animatorController.ForceAnimation(Id, CharAnimationType.JumpAndWin);
				return;
			}
			runningCharAnimator = charAnimator.GetComponent<Animator>();
			runningCharAnimator.Play("UIJumpAndWinAnim");
		}

		public void ReturnToIdleAnim()
		{
			animatorController = charAnimator.GetComponent<CharacterAnimatorController>();
			if (animatorController != null)
			{
				animatorController.ClearForceAnimation(Id);
				return;
			}
			runningCharAnimator = charAnimator.GetComponent<Animator>();
			runningCharAnimator.Play("UIIdleAnim");
		}

		public void ChangeButtonListener(UnityAction callback)
		{
			button.onClick.RemoveAllListeners();
			if (callback != null)
			{
				button.onClick.AddListener(callback);
			}
		}

		private void Init()
		{
			if (animatorController == null)
			{
				animatorController = charAnimator.GetComponent<CharacterAnimatorController>();
				if (animatorController != null)
				{
					animatorController.Init(Id, ScreenType.CharSelect);
				}
			}
			if (null == newLabel)
			{
				newLabel = Object.Instantiate(newLabelPrefab, newLabelHolder);
			}
			if (newLabel != null)
			{
				newLabel.Init(parentRectTransform);
			}
		}
	}
}
