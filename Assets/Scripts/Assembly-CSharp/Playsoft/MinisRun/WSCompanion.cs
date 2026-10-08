using System;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WSCompanion
	{
		[SerializeField]
		private float companionSize;

		[SerializeField]
		private Vector2 companionOffset = new Vector2(-68.5f, 200f);

		private RectTransform Companion;

		private Transform parent;

		private bool isCompanionRequested;

		public void CreateCompanion(Transform parent, int selectedCompanion)
		{
			this.parent = parent;
			if (!(Companion != null) && !isCompanionRequested)
			{
				isCompanionRequested = true;
				Contexts.sharedInstance.uI.GetCompanionUIAssetAsync(selectedCompanion, SetCompanion);
			}
		}

		public void SetPosition(Vector2 targetPosition, float rotation)
		{
			if (Companion != null)
			{
				Companion.localScale = new Vector3(rotation, 1f, 1f);
				Companion.anchoredPosition3D = new Vector3(targetPosition.x + rotation * companionOffset.x, targetPosition.y + companionOffset.y, 0f);
			}
		}

		private void SetCompanion(RectTransform companion)
		{
			Companion = UnityEngine.Object.Instantiate(companion);
			Companion.SetParent(parent);
			Companion.localScale = Vector3.one;
			Companion.pivot = new Vector2(0.5f, 0f);
			RectTransform companion2 = Companion;
			Vector2 vector = new Vector2(0.5f, 0.5f);
			Companion.anchorMax = vector;
			companion2.anchorMin = vector;
			Companion.sizeDelta = new Vector2(companionSize, companionSize);
			Image[] componentsInChildren = Companion.GetComponentsInChildren<Image>();
			Image[] array = componentsInChildren;
			foreach (Image image in array)
			{
				image.raycastTarget = false;
			}
		}
	}
}
