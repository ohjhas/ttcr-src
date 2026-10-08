using DG.Tweening;
using UnityEngine;

public class NewLabel : MonoBehaviour
{
	[SerializeField]
	private RectTransform label;

	[SerializeField]
	private GameObject normalLabel;

	[SerializeField]
	private GameObject leftLabel;

	[SerializeField]
	private GameObject rightLabel;

	private bool isShowed;

	private bool isInArea;

	private bool initialyDisplayed;

	private Camera canvasCamera;

	private RectTransform labelArea;

	public void Init(RectTransform area)
	{
		labelArea = area;
		label.transform.SetParent(area);
		label.transform.SetAsLastSibling();
		label.localScale = Vector3.one;
	}

	public void Show()
	{
		if (!(null == label))
		{
			isShowed = true;
			label.gameObject.SetActive(true);
		}
	}

	public void Hide()
	{
		if (!(null == label))
		{
			isShowed = false;
			label.gameObject.SetActive(false);
		}
	}

	private void Start()
	{
		canvasCamera = Camera.main;
	}

	private void LateUpdate()
	{
		if (!isShowed || null == label)
		{
			return;
		}
		if (!RectTransformUtility.RectangleContainsScreenPoint(labelArea, base.transform.position))
		{
			if (isInArea || !initialyDisplayed)
			{
				initialyDisplayed = true;
				label.localScale = Vector3.zero;
				label.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
				isInArea = false;
				bool flag = base.transform.position.x < labelArea.transform.position.x;
				float num = ((!flag) ? (-1.5f) : 1.5f);
				label.transform.position = new Vector3(canvasCamera.orthographicSize * canvasCamera.aspect * (float)((!flag) ? 1 : (-1)) + num, label.transform.position.y, 0f);
				label.transform.localPosition = new Vector3(label.transform.localPosition.x, label.transform.localPosition.y, 0f);
				RefreshLabels(flag);
			}
		}
		else
		{
			if (!isInArea)
			{
				label.localScale = Vector3.zero;
				label.DOScale(1f, 0.3f).SetEase(Ease.OutBack);
			}
			isInArea = true;
			label.transform.position = base.transform.position;
			RefreshLabels(false);
		}
	}

	private void OnDisable()
	{
		isShowed = false;
		initialyDisplayed = false;
	}

	private void RefreshLabels(bool isLeft)
	{
		normalLabel.SetActive(isInArea);
		leftLabel.SetActive(!isInArea && isLeft);
		rightLabel.SetActive(!isInArea && !isLeft);
	}
}
