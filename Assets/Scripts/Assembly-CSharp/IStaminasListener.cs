public interface IStaminasListener
{
	StaminasListenerComponent staminasListener { get; }

	bool hasStaminasListener { get; }

	void AddStaminasListener(IStaminaListener newListener);

	void ReplaceStaminasListener(IStaminaListener newListener);

	void RemoveStaminasListener();
}
