using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class PassUnscaledTimeToShader : MonoBehaviour
	{
		public Image Image;

		private void Update()
		{
			Image.material.SetFloat("UnscaledTime", Time.unscaledTime);
		}
	}
}
