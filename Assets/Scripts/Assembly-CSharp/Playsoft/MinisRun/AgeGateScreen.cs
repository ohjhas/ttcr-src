using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class AgeGateScreen : BaseDialog
	{
		private const int MIDDLE_YEAR_SIBLING = 3;

		private const int NO_YEAR_NUMBER = -1;

		private const int MINIMUM_YEAR = 1900;

		private const int MIDDLE_YEAR = 2000;

		private int MAXIMUM_YEAR = DateTime.Today.Year;

		[SerializeField]
		private InfiniteScroll infiniteScroll;

		[SerializeField]
		private ScrollRect scrollRect;

		[SerializeField]
		private AgeGateYear[] years;

		[SerializeField]
		private Button confirmButton;

		[SerializeField]
		private GameObject confitmNotActive;

		[SerializeField]
		private HoldableButton upArrowButton;

		[SerializeField]
		private HoldableButton downArrowButton;

		private int currentSelectedYear = -1;

		private float canvasPosition;

		private StateContext stateContext
		{
			get
			{
				return Contexts.sharedInstance.state;
			}
		}

		public override ScreenId Id
		{
			get
			{
				return ScreenId.AgeGateScreen;
			}
		}

		public void OnUpArrow()
		{
			scrollRect.velocity = Vector2.up * 500f;
		}

		public void OnDownArrow()
		{
			scrollRect.velocity = -Vector2.up * 500f;
		}

		public void OnOkClicked()
		{
			if (currentSelectedYear != -1)
			{
				StateEntity stateEntity = stateContext.CreateEntity();
				stateEntity.AddO7SdkAgeGateSelected(currentSelectedYear);
			}
		}

		public void PrivacyAndPolicyClicked()
		{
			stateContext.CreateEntity().isOpenPrivacyUrl = true;
			stateContext.CreateEntity().isPrivacyAndLegalClickedEventToBeSent = true;
		}

		private void OnEnable()
		{
			Canvas componentInParent = GetComponentInParent<Canvas>();
			canvasPosition = ((!(componentInParent == null)) ? componentInParent.transform.position.y : 0f);
			infiniteScroll.OnElementMovedInList += SetYearForElement;
			upArrowButton.onHold.AddListener(OnUpArrow);
			downArrowButton.onHold.AddListener(OnDownArrow);
		}

		private void Start()
		{
			List<AgeGateYear> list = years.OrderByDescending((AgeGateYear x) => x.transform.position.y).ToList();
			for (int num = 0; num < list.Count(); num++)
			{
				list[num].Offset = 0.3653564f;
				list[num].Year = YearToSet(currentSelectedYear, 3 - num);
			}
		}

		private void OnDisable()
		{
			infiniteScroll.OnElementMovedInList -= SetYearForElement;
			upArrowButton.onHold.RemoveAllListeners();
			downArrowButton.onHold.RemoveAllListeners();
		}

		private void Update()
		{
			currentSelectedYear = years.OrderBy((AgeGateYear x) => Mathf.Abs(x.transform.position.y - canvasPosition)).First().Year;
			SetYearForElement(base.transform);
			confirmButton.gameObject.SetActive(currentSelectedYear != -1);
			confitmNotActive.SetActive(currentSelectedYear == -1);
		}

		private void SetYearForElement(Transform yearElement)
		{
			List<AgeGateYear> list = years.OrderByDescending((AgeGateYear x) => x.transform.position.y).ToList();
			int year = list[3].Year;
			for (int num = 0; num < list.Count; num++)
			{
				list[num].Year = YearToSet(year, 3 - num);
			}
		}

		private int YearToSet(int currentYear, int offset)
		{
			int num = -1;
			if (currentYear >= 2000)
			{
				currentYear++;
			}
			int num2 = ((currentYear != -1) ? currentYear : 2000);
			num2 += offset;
			if (num2 == 2000)
			{
				num = -1;
			}
			else if (num2 > 2000)
			{
				num = num2 - 1;
				if (num > MAXIMUM_YEAR)
				{
					num = 1900 + (num2 - MAXIMUM_YEAR - 1) - 1;
				}
			}
			else if (num2 < 2000)
			{
				num = num2;
				if (num < 1900)
				{
					num = MAXIMUM_YEAR + 1 - Mathf.Abs(num2 - 1900);
				}
			}
			return num;
		}
	}
}
