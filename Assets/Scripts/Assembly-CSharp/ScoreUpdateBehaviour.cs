using TMPro;
using UnityEngine;

public class ScoreUpdateBehaviour : MonoBehaviour, IScoreListener
{
	private TextMeshProUGUI score;

	private void Start()
	{
		score = GetComponent<TextMeshProUGUI>();
		Contexts.sharedInstance.game.CreateEntity().AddScoreListener(this);
	}

	public void ScoreChanged(int newScore)
	{
		score.text = newScore.ToString("N0");
	}
}
