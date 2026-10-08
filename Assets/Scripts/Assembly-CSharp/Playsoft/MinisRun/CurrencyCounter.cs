using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	[RequireComponent(typeof(TextCounterEffect))]
	public class CurrencyCounter : BaseUI
	{
		public RectTransform Icon;

		[SerializeField]
		private GameObject _plusIcon;

		private TextCounterEffect _counterEffect;

		private RectTransform _counterRT;

		private Button _button;

		public RectTransform CounterRT
		{
			get
			{
				if (_counterRT == null)
				{
					_counterRT = GetComponent<RectTransform>();
				}
				return _counterRT;
			}
		}

		public TextCounterEffect CounterEffect
		{
			get
			{
				if (_counterEffect == null)
				{
					_counterEffect = GetComponent<TextCounterEffect>();
				}
				return _counterEffect;
			}
		}

		protected override void Awake()
		{
			_button = GetComponent<Button>();
		}

		public void TurnOnButtonMode(UnityAction action)
		{
			if (_button != null)
			{
				_button.enabled = true;
				_button.onClick.AddListener(action);
			}
			_plusIcon.SetActive(true);
		}

		public void TurnOffButtonMode()
		{
			if (_button != null)
			{
				_button.enabled = false;
				_button.onClick.RemoveAllListeners();
			}
			_plusIcon.SetActive(false);
		}
	}
}
