using UnityEngine;

namespace Playsoft.MinisRun
{
	public class IntroRacoon : MonoBehaviour
	{
		[SerializeField]
		private IntroController introController;

		public void MoveBehindDesk()
		{
			introController.MoveRacoonBehindDesk();
		}

		public void MoveFrontDesk()
		{
			introController.MoveRacoonFrontDesk();
		}

		public void MoveBehindWindow()
		{
			introController.MoveRacoonBehindWindow();
		}

		public void StealFromBehind()
		{
			introController.StealBehindElement();
		}

		public void StealFromFrontBottom()
		{
			introController.StealFrontBottomElement();
		}

		public void StealFromFrontTop()
		{
			introController.StealFrontTopElement();
		}

		public void StealInitial()
		{
			introController.StealInitialElement();
		}
	}
}
