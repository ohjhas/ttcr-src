using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
[RequireComponent(typeof(ScrollRect))]
public class ScrollSnapRect : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IEventSystemHandler
{
	private RectTransform _container;

	private int _currentPage;

	private bool _dragging;

	private int _fastSwipeThresholdMaxLimit;

	private bool _horizontal;

	private bool _lerp;

	private Vector2 _lerpTo;

	private int _pageCount;

	private readonly List<Vector2> _pagePositions = new List<Vector2>();

	private List<Image> _pageSelectionImages;

	private int _previousPageSelectionIndex;

	private ScrollRect _scrollRectComponent;

	private RectTransform _scrollRectRect;

	private bool _showPageSelection;

	private Vector2 _startPosition;

	private float _timeStamp;

	[Tooltip("How fast will page lerp to target position")]
	public float decelerationRate = 10f;

	[Tooltip("Threshold time for fast swipe in (unscaled) pixels")]
	public int fastSwipeThresholdDistance = 100;

	[Tooltip("Threshold time for fast swipe in seconds")]
	public float fastSwipeThresholdTime = 0.3f;

	[Tooltip("Button to go to the next page (optional)")]
	public Button nextButton;

	public Image nextButtonArrow;

	[Tooltip("Container with page images (optional)")]
	public Transform pageSelectionIcons;

	[Tooltip("Button to go to the previous page (optional)")]
	public Button prevButton;

	public Image prevButtonArrow;

	[Tooltip("Sprite for selected page (optional)")]
	public Sprite selectedPage;

	[Tooltip("Set starting page index - starting from 0")]
	public int startingPage;

	[SerializeField]
	private float widthScaler = 0.5f;

	private Sequence tweenSequencePrev;

	private Sequence tweenSequenceNext;

	[Tooltip("y anchored position for current page")]
	public float selectedPosition;

	[Tooltip("y anchored positno for not current page")]
	public float notSelectedPositon;

	[Tooltip("Sprite for unselected page (optional)")]
	public Sprite unselectedPage;

	public bool isInitialized { get; private set; }

	public void OnBeginDrag(PointerEventData aEventData)
	{
		_lerp = false;
		_dragging = false;
	}

	public void OnDrag(PointerEventData aEventData)
	{
		if (!_dragging)
		{
			_dragging = true;
			_timeStamp = Time.unscaledTime;
			_startPosition = _container.anchoredPosition;
		}
		else if (_showPageSelection)
		{
			SetPageSelection(GetNearestPage());
		}
	}

	public void OnEndDrag(PointerEventData aEventData)
	{
		float num = ((!_horizontal) ? (0f - (_startPosition.y - _container.anchoredPosition.y)) : (_startPosition.x - _container.anchoredPosition.x));
		if (Time.unscaledTime - _timeStamp < fastSwipeThresholdTime && Mathf.Abs(num) > (float)fastSwipeThresholdDistance && Mathf.Abs(num) < (float)_fastSwipeThresholdMaxLimit)
		{
			if (num > 0f)
			{
				NextScreen();
			}
			else
			{
				PreviousScreen();
			}
		}
		else
		{
			LerpToPage(GetNearestPage());
		}
		_dragging = false;
	}

	public void Init()
	{
		if (!isInitialized)
		{
			_scrollRectComponent = GetComponent<ScrollRect>();
			_scrollRectRect = GetComponent<RectTransform>();
			_container = _scrollRectComponent.content;
			_pageCount = _container.childCount;
			if (_scrollRectComponent.horizontal && !_scrollRectComponent.vertical)
			{
				_horizontal = true;
			}
			else if (!_scrollRectComponent.horizontal && _scrollRectComponent.vertical)
			{
				_horizontal = false;
			}
			else
			{
				Debug.LogWarning("Confusing setting of horizontal/vertical direction. Default set to horizontal.");
				_horizontal = true;
			}
			_lerp = false;
			SetPagePositions();
			SetPage(startingPage);
			InitPageSelection();
			SetPageSelection(startingPage);
			DoButtonScale();
		}
	}

	private void DoButtonScale()
	{
		if (_currentPage < _pageCount - 1)
		{
			nextButton.interactable = true;
			nextButtonArrow.color = nextButton.colors.normalColor;
			DoScaleNext();
		}
		else
		{
			tweenSequenceNext.Kill();
			nextButtonArrow.rectTransform.localScale = new Vector3(1f, 1f);
			nextButtonArrow.color = nextButton.colors.disabledColor;
			nextButton.interactable = false;
		}
		if (_currentPage > 0)
		{
			prevButton.interactable = true;
			prevButtonArrow.color = prevButton.colors.normalColor;
			DoScalePrev();
		}
		else
		{
			tweenSequencePrev.Kill();
			prevButtonArrow.rectTransform.localScale = new Vector3(1f, 1f);
			prevButtonArrow.color = prevButton.colors.disabledColor;
			prevButton.interactable = false;
		}
	}

	private void DoScalePrev()
	{
	}

	private void DoScaleNext()
	{
	}

	private void Update()
	{
		if (_lerp)
		{
			float t = Mathf.Min(decelerationRate * Time.deltaTime, 1f);
			_container.anchoredPosition = Vector2.Lerp(_container.anchoredPosition, _lerpTo, t);
			if (Vector2.SqrMagnitude(_container.anchoredPosition - _lerpTo) < 0.25f)
			{
				_container.anchoredPosition = _lerpTo;
				_lerp = false;
				_scrollRectComponent.velocity = Vector2.zero;
			}
			if (_showPageSelection)
			{
				SetPageSelection(GetNearestPage());
			}
		}
	}

	private void SetPagePositions()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		if (_horizontal)
		{
			num = (int)(_scrollRectRect.rect.width * widthScaler);
			num3 = (int)((float)num / widthScaler);
			num5 = num3 * _pageCount;
			_fastSwipeThresholdMaxLimit = num;
		}
		else
		{
			num2 = (int)_scrollRectRect.rect.height;
			num4 = num2 / 2;
			num6 = num2 * _pageCount;
			_fastSwipeThresholdMaxLimit = num2;
		}
		Vector2 sizeDelta = new Vector2(num5, num6);
		_container.sizeDelta = sizeDelta;
		Vector2 anchoredPosition = new Vector2(num5 / 2, num6 / 2);
		_container.anchoredPosition = anchoredPosition;
		_pagePositions.Clear();
		for (int i = 0; i < _pageCount; i++)
		{
			RectTransform component = _container.GetChild(i).GetComponent<RectTransform>();
			Vector2 vector = (component.anchoredPosition = ((!_horizontal) ? new Vector2(0f, -(i * num2 - num6 / 2 + num4)) : new Vector2(i * num - num5 / 2 + num3, 0f)));
			_pagePositions.Add(-vector);
		}
	}

	public void SetPage(int aPageIndex)
	{
		aPageIndex = Mathf.Clamp(aPageIndex, 0, _pageCount - 1);
		_container.anchoredPosition = _pagePositions[aPageIndex];
		_currentPage = aPageIndex;
		DoButtonScale();
	}

	public void LerpToPage(int aPageIndex)
	{
		aPageIndex = Mathf.Clamp(aPageIndex, 0, _pageCount - 1);
		_lerpTo = _pagePositions[aPageIndex];
		_lerp = true;
		_currentPage = aPageIndex;
		DoButtonScale();
	}

	private void InitPageSelection()
	{
		_showPageSelection = unselectedPage != null && selectedPage != null;
		if (!_showPageSelection)
		{
			return;
		}
		if (pageSelectionIcons == null || pageSelectionIcons.childCount != _pageCount)
		{
			Debug.LogWarning("Different count of pages and selection icons - will not show page selection");
			_showPageSelection = false;
			return;
		}
		_previousPageSelectionIndex = -1;
		_pageSelectionImages = new List<Image>();
		for (int i = 0; i < pageSelectionIcons.childCount; i++)
		{
			Image component = pageSelectionIcons.GetChild(i).GetComponent<Image>();
			if (component == null)
			{
				Debug.LogWarning("Page selection icon at position " + i + " is missing Image component");
			}
			_pageSelectionImages.Add(component);
		}
	}

	private void SetPageSelection(int aPageIndex)
	{
		if (_previousPageSelectionIndex != aPageIndex)
		{
			if (_previousPageSelectionIndex >= 0)
			{
				_pageSelectionImages[_previousPageSelectionIndex].sprite = unselectedPage;
				_pageSelectionImages[_previousPageSelectionIndex].SetNativeSize();
			}
			_pageSelectionImages[aPageIndex].sprite = selectedPage;
			_pageSelectionImages[aPageIndex].SetNativeSize();
			_previousPageSelectionIndex = aPageIndex;
		}
	}

	public void NextScreen()
	{
		LerpToPage(_currentPage + 1);
	}

	public void PreviousScreen()
	{
		LerpToPage(_currentPage - 1);
	}

	public int GetNearestPage()
	{
		Vector2 anchoredPosition = _container.anchoredPosition;
		float num = float.MaxValue;
		int result = _currentPage;
		for (int i = 0; i < _pagePositions.Count; i++)
		{
			float num2 = Vector2.SqrMagnitude(anchoredPosition - _pagePositions[i]);
			if (num2 < num)
			{
				num = num2;
				result = i;
			}
		}
		return result;
	}

	private float GetDistanceScale(int page)
	{
		Vector2 anchoredPosition = _container.anchoredPosition;
		float num = Vector2.SqrMagnitude(_pagePositions[0] - _pagePositions[1]);
		float num2 = Vector2.SqrMagnitude(anchoredPosition - _pagePositions[page]);
		return num2 / num;
	}

	public void ScaleOthers(float scaleFactor, float minScale)
	{
		for (int i = 0; i < _pageCount; i++)
		{
			RectTransform component = _container.GetChild(i).GetComponent<RectTransform>();
			float a = 1f - GetDistanceScale(i);
			a = Mathf.Max(a, minScale);
			float distancePosition = GetDistancePosition(i);
			component.anchoredPosition = new Vector2(component.anchoredPosition.x, distancePosition);
			component.localScale = new Vector3(a, a, a) * scaleFactor;
		}
	}

	private float GetDistancePosition(int page)
	{
		Vector2 anchoredPosition = _container.anchoredPosition;
		float num = Vector2.SqrMagnitude(_pagePositions[0] - _pagePositions[1]);
		float num2 = Vector2.SqrMagnitude(anchoredPosition - _pagePositions[page]);
		return Mathf.Lerp(selectedPosition, notSelectedPositon, num2 / num);
	}
}
