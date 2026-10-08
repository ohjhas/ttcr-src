using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HoldableButton : Button
{
	[Serializable]
	public class ButtonHoldEvent : UnityEvent
	{
	}

	[FormerlySerializedAs("onHold")]
	[SerializeField]
	private ButtonClickedEvent m_OnHold = new ButtonClickedEvent();

	private bool _isHolding;

	public ButtonClickedEvent onHold
	{
		get
		{
			return m_OnHold;
		}
		set
		{
			m_OnHold = value;
		}
	}

	public override void OnPointerDown(PointerEventData eventData)
	{
		base.OnPointerDown(eventData);
		_isHolding = true;
	}

	public override void OnPointerUp(PointerEventData eventData)
	{
		base.OnPointerUp(eventData);
		_isHolding = false;
	}

	private void Update()
	{
		if (_isHolding)
		{
			m_OnHold.Invoke();
		}
	}
}
