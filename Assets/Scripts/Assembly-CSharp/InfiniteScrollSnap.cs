using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
[RequireComponent(typeof(InfiniteScroll))]
public class InfiniteScrollSnap : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IEventSystemHandler
{
	private ScrollRect scroll;

	private int currentPage;

	private int LastPage;

	private float step;

	private float scrollSpeed = 0.1f;

	private int totalPages;

	private bool isDraging;

	private float minQuickSwipeAmount = 0.05f;

	private float movementTreshold = 0.0001f;

	private void OnEnable()
	{
		InitScroll(2);
	}

	public void InitScroll(int page = 0)
	{
		scroll = GetComponent<ScrollRect>();
		totalPages = scroll.content.childCount;
		step = 1f / (float)(totalPages - 1);
		step = 0.25f;
		if (page > 0)
		{
			currentPage = page;
			scroll.verticalNormalizedPosition = step * (float)page;
		}
		InfiniteScroll component = GetComponent<InfiniteScroll>();
		component.Init();
	}

	public void MoveRight()
	{
		currentPage++;
	}

	public void MoveLeft()
	{
		currentPage--;
	}

	private void Update()
	{
		if (!isDraging && !(Mathf.Abs(scroll.velocity.y) > 100f))
		{
			int num = Mathf.RoundToInt(scroll.verticalNormalizedPosition / step) + 1;
			if (num != LastPage)
			{
				currentPage = num;
			}
			LastPage = num;
			int num2 = currentPage;
			float b = 0f - step + (float)num2 * step;
			scroll.verticalNormalizedPosition = Mathf.Lerp(scroll.verticalNormalizedPosition, b, scrollSpeed);
		}
	}

	void IEndDragHandler.OnEndDrag(PointerEventData eventData)
	{
		isDraging = false;
	}

	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		isDraging = true;
	}
}
