using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class CharacterSizeController : MonoBehaviour
	{
		[SerializeField]
		private RectTransform OtherRT;

		private Image image;

		private RectTransform RT;

		private Rect originSpriteSize;

		private Sprite lastSprite;

		private void Awake()
		{
			image = GetComponent<Image>();
			if (OtherRT == null)
			{
				RT = GetComponent<RectTransform>();
			}
			originSpriteSize = image.sprite.rect;
			lastSprite = image.sprite;
		}

		private void LateUpdate()
		{
			if (lastSprite != image.sprite)
			{
				lastSprite = image.sprite;
				float y = lastSprite.rect.height / originSpriteSize.height;
				float num = lastSprite.rect.width / originSpriteSize.width - 1f;
				if (OtherRT != null)
				{
					OtherRT.anchorMin = new Vector2((0f - num) * 0.5f, 0f);
					OtherRT.anchorMax = new Vector2(1f + num * 0.5f, y);
				}
				else
				{
					RT.anchorMin = new Vector2((0f - num) * 0.5f, 0f);
					RT.anchorMax = new Vector2(1f + num * 0.5f, y);
				}
			}
		}
	}
}
