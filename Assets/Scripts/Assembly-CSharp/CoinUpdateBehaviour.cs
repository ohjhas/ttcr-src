using DG.Tweening;
using Playsoft.MinisRun;
using TMPro;
using UnityEngine;

public class CoinUpdateBehaviour : MonoBehaviour, ICoinListener
{
	private const string COINS_FORMAT = "{0}";

	private const int BIG_COINS_AMOUNT = 50;

	private Transform partToAnimate;

	private TextMeshProUGUI coins;

	private int lastCoins;

	private Tween jumpTweener;

	private void Start()
	{
		coins = GetComponent<TextMeshProUGUI>();
		partToAnimate = coins.transform.parent;
		Contexts.sharedInstance.game.CreateEntity().AddCoinsListener(this);
		coins.text = 0.ToString();
	}

	public void CoinsChanged(int newScore)
	{
		coins.text = NumbersHelper.GetNumberSplitedBySpaces(newScore);
		if (newScore - lastCoins > 50)
		{
			if (jumpTweener != null && jumpTweener.IsPlaying())
			{
				jumpTweener.Restart();
			}
			else
			{
				jumpTweener = partToAnimate.DOScale(1.3f, 0.1f).SetLoops(2, LoopType.Yoyo);
			}
		}
		lastCoins = newScore;
	}
}
