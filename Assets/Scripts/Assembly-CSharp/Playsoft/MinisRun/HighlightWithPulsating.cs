using System;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class HighlightWithPulsating : MonoBehaviour
	{
		[SerializeField]
		private Image Image;

		[SerializeField]
		private RectTransform RT;

		[SerializeField]
		private Animator Animator;

		[SerializeField]
		private Color Color = Color.clear;

		private ButtonFeedback buttonFeedback;

		private float startTime;

		private float period;

		private float animSpeed;

		private float minScale;

		private float maxScale;

		private Vector3 scaleOrigin;

		private Vector3 initialScale;

		private Material originalMaterial;

		private bool shouldHighlight;

		private bool shouldPulsate;

		private bool isInitialized;

		public void Init(float minScale, float maxScale, float animSpeed, Material material, float period = 1f, bool shouldHighlight = true, bool shouldPulsate = true)
		{
			if (Image == null)
			{
				Image = GetComponent<Image>();
			}
			if (RT == null)
			{
				RT = GetComponent<RectTransform>();
			}
			if (Animator == null)
			{
				Animator = GetComponent<Animator>();
			}
			buttonFeedback = GetComponent<ButtonFeedback>();
			this.minScale = minScale;
			this.maxScale = maxScale;
			this.animSpeed = animSpeed;
			this.period = period;
			this.shouldHighlight = shouldHighlight;
			this.shouldPulsate = shouldPulsate;
			initialScale = RT.localScale;
			startTime = Time.unscaledTime;
			if (shouldHighlight && null != Image && null != material)
			{
				originalMaterial = Image.material;
				Image.material = new Material(material);
				Image.material.SetFloat("_Period", period);
				if (Color == Color.clear)
				{
					Color = Image.material.GetColor("_BlinkingColor");
				}
				else
				{
					Image.material.SetColor("_BlinkingColor", Color);
				}
				base.enabled = true;
				isInitialized = true;
			}
			if (shouldPulsate && null != RT)
			{
				scaleOrigin = RT.localScale;
				base.enabled = true;
				isInitialized = true;
			}
		}

		public void Disable()
		{
			if (base.enabled)
			{
				isInitialized = false;
				base.enabled = false;
				if (shouldHighlight && null != Image)
				{
					Image.material = originalMaterial;
				}
			}
		}

		private void LateUpdate()
		{
			if (!isInitialized)
			{
				return;
			}
			if (buttonFeedback != null && buttonFeedback.IsScaleDown)
			{
				if (RT.localScale.x < scaleOrigin.x)
				{
					scaleOrigin = RT.localScale;
				}
				else
				{
					scaleOrigin = new Vector2(buttonFeedback.MinimumScale, buttonFeedback.MinimumScale);
				}
			}
			else
			{
				scaleOrigin = initialScale;
			}
			if (Animator != null)
			{
				scaleOrigin = RT.localScale;
			}
			float num = (Time.unscaledTime - startTime) * animSpeed;
			float num2 = num % period / period;
			if (shouldHighlight && null != Image)
			{
				Image.material.SetFloat("UnscaledTime", num2);
			}
			if (shouldPulsate && null != RT)
			{
				float t = Mathf.Sin(num2 * (float)Math.PI);
				RT.localScale = Vector3.Lerp(scaleOrigin * minScale, scaleOrigin * maxScale, t);
			}
		}
	}
}
