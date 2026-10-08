using I2.Loc;

public static class MissionHelper
{
	public static string GetMissionNameByType(MissionType missionType)
	{
		switch (missionType)
		{
		case MissionType.Collect_ScoreJelly:
			return ScriptLocalization.mission_collect_score_jelly;
		case MissionType.Collect_ScoreJellyMedium:
			return ScriptLocalization.mission_collect_score_jelly_medium;
		case MissionType.Collect_ScoreJellyBig:
			return ScriptLocalization.mission_collect_score_jelly_big;
		case MissionType.Collect_ScoreIceCream:
			return ScriptLocalization.mission_collect_score_icecream;
		case MissionType.Collect_ScoreIceCreamMedium:
			return ScriptLocalization.mission_collect_score_icecream_medium;
		case MissionType.Collect_ScoreIceCreamBig:
			return ScriptLocalization.mission_collect_score_icecream_big;
		case MissionType.Collect_ScoreCake:
			return ScriptLocalization.mission_collect_score_cake;
		case MissionType.Collect_ScoreCakeMedium:
			return ScriptLocalization.mission_collect_score_cake_medium;
		case MissionType.Collect_ScoreCakeBig:
			return ScriptLocalization.mission_collect_score_cake_big;
		case MissionType.Collect_ScoreFruit:
			return ScriptLocalization.mission_collect_score_fruit;
		case MissionType.Collect_ScoreFruitMedium:
			return ScriptLocalization.mission_collect_score_fruit_medium;
		case MissionType.Collect_ScoreFruitBig:
			return ScriptLocalization.mission_collect_score_fruit_big;
		case MissionType.Collect_ScoreCandy:
			return ScriptLocalization.mission_collect_score_candy;
		case MissionType.Collect_ScoreCandyMedium:
			return ScriptLocalization.mission_collect_score_candy_medium;
		case MissionType.Collect_ScoreCandyBig:
			return ScriptLocalization.mission_collect_score_candy_big;
		case MissionType.Collect_ScoreTasty:
			return ScriptLocalization.mission_collect_score_tasty;
		case MissionType.Collect_ScoreTastyMedium:
			return ScriptLocalization.mission_collect_score_tasty_medium;
		case MissionType.Collect_ScoreTastyBig:
			return ScriptLocalization.mission_collect_score_tasty_big;
		case MissionType.Have_Stamina:
			return ScriptLocalization.mission_have_stamina;
		case MissionType.Collect_Coins:
			return ScriptLocalization.mission_collect_coins;
		case MissionType.Collect_CoinsBig:
			return ScriptLocalization.mission_collect_coins_big;
		default:
			return string.Empty;
		}
	}
}
