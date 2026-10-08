public interface IRaccoonIntrosListener
{
	RaccoonIntrosListenerComponent raccoonIntrosListener { get; }

	bool hasRaccoonIntrosListener { get; }

	void AddRaccoonIntrosListener(IRaccoonIntroListener newListener);

	void ReplaceRaccoonIntrosListener(IRaccoonIntroListener newListener);

	void RemoveRaccoonIntrosListener();
}
