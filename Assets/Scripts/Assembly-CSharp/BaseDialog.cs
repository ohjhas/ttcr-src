using UnityEngine;

public abstract class BaseDialog : BaseUI
{
	public bool IsClickable { get; protected set; }

	public virtual ScreenId Id
	{
		get
		{
			return ScreenId.Undefined;
		}
	}

	protected UIContext uiContext
	{
		get
		{
			return Contexts.sharedInstance.uI;
		}
	}

	protected override void Setup()
	{
		base.Setup();
		IsClickable = true;
	}

	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	public virtual RectTransform GetLastFocusElement()
	{
		return base.transform as RectTransform;
	}

	public override void SetVisible(bool visible)
	{
		base.SetVisible(visible);
		IsClickable = visible;
		base.transform.SetAsLastSibling();
	}
}
