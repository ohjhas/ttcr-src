using UnityEngine;

public class DebugFPS : MonoBehaviour
{
	private static DebugFPS counter;

	private float fpsCounterDeltaTime;

	private float cheatCounter;

	private int tapsInLastSecond;

	private bool displayCounter;

	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	private void Update()
	{
		fpsCounterDeltaTime += (Time.unscaledDeltaTime - fpsCounterDeltaTime) * 0.1f;
		UpdateCheatCounter();
		DetectCheatTrigger();
	}

	public void OnGUI()
	{
		if (displayCounter)
		{
			float num = (float)Screen.height / 400f;
			string text = string.Format("{0:0.} FPS", 1f / fpsCounterDeltaTime);
			GUIContent content = new GUIContent(text);
			GUIStyle gUIStyle = new GUIStyle();
			gUIStyle.fontSize = 30;
			GUI.Label(new Rect(10f * num, 0f, 67f * num, 28f * num), content, gUIStyle);
		}
	}

	private void UpdateCheatCounter()
	{
		if (tapsInLastSecond >= 1)
		{
			cheatCounter += Time.deltaTime;
			if (cheatCounter > 1f)
			{
				cheatCounter = 0f;
				tapsInLastSecond = 0;
			}
		}
	}

	private void DetectCheatTrigger()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0) && IsMousePointerInCorner())
		{
			tapsInLastSecond++;
			if (tapsInLastSecond >= 3)
			{
				displayCounter = !displayCounter;
				tapsInLastSecond = 0;
				cheatCounter = 0f;
			}
		}
	}

	private bool IsMousePointerInCorner()
	{
		return Input.mousePosition.x < (float)Screen.width * 0.2f && Input.mousePosition.y > (float)Screen.height * 0.8f;
	}
}
