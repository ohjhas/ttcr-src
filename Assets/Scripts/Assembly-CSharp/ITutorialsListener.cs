public interface ITutorialsListener
{
	TutorialsListenerComponent tutorialsListener { get; }

	bool hasTutorialsListener { get; }

	void AddTutorialsListener(ITutorialListener newListener);

	void ReplaceTutorialsListener(ITutorialListener newListener);

	void RemoveTutorialsListener();
}
