using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TextCounterEffect : CounterEffect
	{
		[SerializeField]
		private TextMeshProUGUI text;

		protected override void Changing()
		{
			if (text != null)
			{
				text.text = NumbersHelper.GetNumberSplitedBySpaces(base.CurrentCount);
			}
			base.Changing();
		}

		protected virtual void OnEnable()
		{
			if (text == null)
			{
				text = GetComponent<TextMeshProUGUI>();
			}
			if (text == null)
			{
				base.enabled = false;
				return;
			}
			Start();
			text.text = NumbersHelper.GetNumberSplitedBySpaces(base.CurrentCount);
		}
	}
}
