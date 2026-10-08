using System;
using UnityEngine;

namespace Playsoft
{
	[ExecuteInEditMode]
	public class AspectRatioScaleSwitch : MonoBehaviour
	{
		[Serializable]
		public class ElementToScale
		{
			public Transform Target;

			public float NormalScale;

			public float LowerScale;
		}

		[SerializeField]
		private Vector2 aspectToSwitch;

		[SerializeField]
		private ElementToScale[] elementsToScale;

		private void Awake()
		{
			if (elementsToScale == null)
			{
				return;
			}
			bool flag = IsLowAspectRatio();
			ElementToScale[] array = elementsToScale;
			foreach (ElementToScale elementToScale in array)
			{
				if (!(null == elementToScale.Target))
				{
					float num = ((!flag) ? elementToScale.NormalScale : elementToScale.LowerScale);
					elementToScale.Target.localScale = new Vector3(num, num, num);
				}
			}
		}

		private bool IsLowAspectRatio()
		{
			return (float)Screen.width / (float)Screen.height < aspectToSwitch.x / aspectToSwitch.y + 0.1f;
		}

		public void AddNewElement(ElementToScale newElement)
		{
			ElementToScale[] array = new ElementToScale[elementsToScale.Length + 1];
			for (int i = 0; i < elementsToScale.Length; i++)
			{
				array[i] = elementsToScale[i];
			}
			array[elementsToScale.Length] = newElement;
			elementsToScale = array;
			Awake();
		}
	}
}
