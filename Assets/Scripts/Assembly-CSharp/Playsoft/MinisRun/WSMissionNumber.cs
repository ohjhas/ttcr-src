using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WSMissionNumber
	{
		[SerializeField]
		private RectTransform ContainerRT;

		[SerializeField]
		private List<TextMeshProUGUI> LevelNumber;

		[SerializeField]
		private RectTransform SelectedPos;

		[SerializeField]
		private RectTransform UnselectedPos;

		public void SetData(int missionNumber)
		{
			LevelNumber.ForEach(delegate(TextMeshProUGUI x)
			{
				x.text = missionNumber.ToString();
			});
		}

		public void SetSelection(bool isSelected)
		{
			ContainerRT.position = ((!isSelected) ? UnselectedPos.position : SelectedPos.position);
		}
	}
}
