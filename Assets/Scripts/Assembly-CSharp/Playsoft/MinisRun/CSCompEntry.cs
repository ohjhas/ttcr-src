using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class CSCompEntry : MonoBehaviour
	{
		[SerializeField]
		private int id;

		[SerializeField]
		private Animator compAnimator;

		[SerializeField]
		private Image compRenderer;

		[SerializeField]
		private GameObject Lock;

		[SerializeField]
		private Transform newLabelHolder;

		[SerializeField]
		private NewLabel newLabelPrefab;

		[SerializeField]
		private Button button;

		private NewLabel newLabel;

		private RectTransform parentRectTransform;

		public int Id { get; private set; }

		private void Start()
		{
			Id = id;
			ScrollRect componentInParent = GetComponentInParent<ScrollRect>();
			parentRectTransform = ((!(componentInParent != null)) ? null : (componentInParent.transform as RectTransform));
			if (null == newLabel)
			{
				newLabel = Object.Instantiate(newLabelPrefab, newLabelHolder);
			}
			if (newLabel != null)
			{
				newLabel.Init(parentRectTransform);
			}
		}

		public void SetState(bool isEnabled, bool isNew)
		{
			compAnimator.enabled = isEnabled && !Lock.activeSelf;
			Color color = ((!isEnabled || Lock.activeSelf) ? Color.gray : Color.white);
			compRenderer.color = color;
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
		}

		public void ChangeButtonListener(UnityAction callback)
		{
			button.onClick.RemoveAllListeners();
			if (callback != null)
			{
				button.onClick.AddListener(callback);
			}
		}
	}
}
