namespace Playsoft.MinisRun
{
	public class PlayerStaminaSystems : Feature
	{
		public PlayerStaminaSystems(Contexts contexts)
			: base("Player Stamina")
		{
			Add(new StaminaDecreaseSystem(contexts));
			Add(new TutorialStaminaHitSystem(contexts));
			Add(new StaminaHitSystem(contexts));
			Add(new InvulnerabilityDecreaseSystem(contexts));
		}
	}
}
