using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class CharEntryPositioner : MonoBehaviour
	{
		private const float Y_ANCHOR_OFFSET = -265f;

		[SerializeField]
		private RectTransform charContainer;

		private Image charRenderer;

		private float characterSizeMultiplier;

		public void Init(Image charImage, float sizeMultiplier)
		{
			charRenderer = charImage;
			characterSizeMultiplier = sizeMultiplier;
		}

		private void LateUpdate()
		{
			if (!(null == charRenderer))
			{
				float x = charRenderer.sprite.pivot.x / charRenderer.sprite.rect.size.x;
				charContainer.anchoredPosition = new Vector2(charContainer.anchoredPosition.x, -265f);
				charContainer.sizeDelta = charRenderer.sprite.rect.size * characterSizeMultiplier;
				charContainer.pivot = new Vector2(x, 0f);
			}
		}
	}
}
