namespace Playsoft.MinisRun
{
	public abstract class AbstractProperty
	{
		public bool IsEnabled;

		public abstract void AddToEntity(GameEntity entity);
	}
}
