using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class LoadingSpinner : MonoBehaviour
	{
		[SerializeField]
		private float SpinDuration = 0.8f;

		[SerializeField]
		private int FrameCount = 12;

		private Image spinnerImage;

		private float dt;

		private Transform spinnerTransform;

		private void Awake()
		{
			spinnerImage = GetComponent<Image>();
			spinnerTransform = spinnerImage.GetComponent<Transform>();
		}

		private void Update()
		{
			dt += Time.unscaledDeltaTime;
			Vector3 eulerAngles = spinnerTransform.localRotation.eulerAngles;
			float num = eulerAngles.z;
			float num2 = SpinDuration / (float)FrameCount;
			bool flag = false;
			while (dt > num2)
			{
				num -= 360f / (float)FrameCount;
				dt -= num2;
				flag = true;
			}
			if (flag)
			{
				spinnerTransform.localRotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, num);
			}
		}
	}
}
