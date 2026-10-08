using System.Collections.Generic;

public interface ITutorialLevelPassedAvailibilityListener
{
	void LevelUnlocked(List<TutorialOutgameLevel> unlockedLevels);
}
