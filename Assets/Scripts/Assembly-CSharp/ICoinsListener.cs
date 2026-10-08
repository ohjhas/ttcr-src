public interface ICoinsListener
{
	CoinsListenerComponent coinsListener { get; }

	bool hasCoinsListener { get; }

	void AddCoinsListener(ICoinListener newListener);

	void ReplaceCoinsListener(ICoinListener newListener);

	void RemoveCoinsListener();
}
