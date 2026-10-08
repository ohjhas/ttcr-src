namespace Playsoft.MinisRun
{
	public class PlayerMovementSystems : Feature
	{
		public PlayerMovementSystems(Contexts contexts)
			: base("Player Movement")
		{
			Add(new PlayerJumpSystem(contexts));
			Add(new PlayerSlideSystem(contexts));
			Add(new PlayerSlideStopSystem(contexts));
			Add(new PlayerSlideSafetyCheckSystem(contexts));
			Add(new PlayerGroundSystem(contexts));
			Add(new PlayerFallingSystem(contexts));
			Add(new PlayerJumpStopSystem(contexts));
			Add(new PlayerUnderMapSystem(contexts));
		}
	}
}
