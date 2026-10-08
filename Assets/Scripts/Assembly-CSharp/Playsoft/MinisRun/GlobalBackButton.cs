using UnityEngine;

namespace Playsoft.MinisRun
{
	public class GlobalBackButton : MonoBehaviour
	{
		private static GlobalBackButton backButtonBehaviour;

		private void Awake()
		{
			if (backButtonBehaviour == null)
			{
				backButtonBehaviour = this;
				Object.DontDestroyOnLoad(base.gameObject);
			}
			else
			{
				Object.Destroy(base.gameObject);
			}
		}

		private void Update()
		{
			if (backButtonBehaviour == this && Input.GetKeyDown(KeyCode.Escape))
			{
				InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
				inputEntity.isInput = true;
				inputEntity.isGoBack = true;
			}
		}
	}
}
