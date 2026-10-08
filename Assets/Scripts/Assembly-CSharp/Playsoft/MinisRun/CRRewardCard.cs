using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CRRewardCard : MonoBehaviour
	{
		[SerializeField]
		private GameObject rareBackground;

		[SerializeField]
		private GameObject commonBackground;

		public void SetData(bool isCardEpic)
		{
			rareBackground.SetActive(isCardEpic);
			commonBackground.SetActive(!isCardEpic);
		}
	}
}
