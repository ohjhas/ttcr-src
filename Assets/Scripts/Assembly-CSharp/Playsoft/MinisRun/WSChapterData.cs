namespace Playsoft.MinisRun
{
	public class WSChapterData
	{
		public int Id { get; private set; }

		public string Name { get; private set; }

		public int RequiredStars { get; private set; }

		public WSChapterData(int _id, string _name, int _requiredStars)
		{
			Id = _id;
			Name = _name;
			RequiredStars = _requiredStars;
		}
	}
}
