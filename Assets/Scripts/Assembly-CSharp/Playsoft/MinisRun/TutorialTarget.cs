using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TutorialTarget : MonoBehaviour
	{
		[SerializeField]
		private string targetId;

		public string TargetId
		{
			get
			{
				return targetId;
			}
			set
			{
				targetId = value;
			}
		}
	}
}
