using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
	protected RectTransform rectTransform;

	protected virtual void Awake()
	{
		rectTransform = base.transform as RectTransform;
		Setup();
	}

	protected virtual void Setup()
	{
	}

	public virtual void SetData(UIEntity parameters)
	{
		if (parameters != null)
		{
			parameters.isDestroyed = true;
		}
	}

	protected GameObject GetGameObject(string pathToChild)
	{
		Transform transform = base.transform.Find(pathToChild);
		if (transform != null)
		{
			return transform.gameObject;
		}
		return null;
	}

	protected T GetUIComponent<T>()
	{
		return GetUIComponent<T>(string.Empty);
	}

	protected T GetUIComponent<T>(string pathToChild)
	{
		if (string.IsNullOrEmpty(pathToChild))
		{
			return GetComponent<T>();
		}
		Transform transform = base.transform.Find(pathToChild);
		if (transform != null)
		{
			return transform.GetComponent<T>();
		}
		return default(T);
	}

	protected List<T> GetUIComponents<T>()
	{
		return GetUIComponents<T>(1);
	}

	protected List<T> GetUIComponents<T>(int maxDepth)
	{
		List<T> list = new List<T>();
		GetUIComponents(list, base.transform, maxDepth);
		return list;
	}

	private static void GetUIComponents<T>(List<T> results, Transform currentTransform, int maxDepth)
	{
		T[] components = currentTransform.GetComponents<T>();
		results.AddRange(components);
		if (maxDepth > 0)
		{
			int childCount = currentTransform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				GetUIComponents(results, currentTransform.GetChild(i), maxDepth - 1);
			}
		}
	}

	protected T GetUIParentComponent<T>()
	{
		Transform parent = base.transform.parent;
		if (parent != null)
		{
			return parent.GetComponent<T>();
		}
		return default(T);
	}

	public virtual void SetVisible(bool visible)
	{
		base.gameObject.SetActive(visible);
	}
}
