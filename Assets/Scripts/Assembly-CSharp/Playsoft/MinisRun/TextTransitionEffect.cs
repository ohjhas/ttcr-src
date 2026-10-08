using System.Collections;
using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TextTransitionEffect : MonoBehaviour
	{
		private TMP_Text _text;

		private void Awake()
		{
			_text = GetComponent<TMP_Text>();
		}

		private void Start()
		{
			_text.color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0);
		}

		private void OnEnable()
		{
			StartCoroutine(ShowTextTransition());
			StartCoroutine(ShowTextTransition(false, 2f));
		}

		private IEnumerator ShowTextTransition(bool shouldFadeIn = true, float delay = 0.05f, float offset = 0.05f)
		{
			yield return new WaitForSecondsRealtime(delay);
			TMP_TextInfo textInfo = _text.textInfo;
			byte alpha = (byte)(shouldFadeIn ? 255u : 0u);
			Color32 newColor = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, alpha);
			for (int i = 0; i < textInfo.characterCount; i++)
			{
				ShowLetterTransition(i, newColor);
				yield return new WaitForSecondsRealtime(offset);
			}
		}

		private void ShowLetterTransition(int letterIndex, Color32 newColor)
		{
			TMP_TextInfo textInfo = _text.textInfo;
			int materialReferenceIndex = textInfo.characterInfo[letterIndex].materialReferenceIndex;
			Color32[] colors = textInfo.meshInfo[materialReferenceIndex].colors32;
			int vertexIndex = textInfo.characterInfo[letterIndex].vertexIndex;
			if (textInfo.characterInfo[letterIndex].isVisible)
			{
				colors[vertexIndex] = newColor;
				colors[vertexIndex + 1] = newColor;
				colors[vertexIndex + 2] = newColor;
				colors[vertexIndex + 3] = newColor;
				_text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
			}
		}
	}
}
