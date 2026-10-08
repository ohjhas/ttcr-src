namespace Playsoft.MinisRun
{
	public class CharacterEnergySystems : Feature
	{
		public CharacterEnergySystems(Contexts contexts)
			: base("Char Energy Systems")
		{
			Add(new CharacterLifeRefillUpdateSystem(contexts));
			Add(new CharacterLifeLostUpdateSystem(contexts));
			Add(new CharactersLifesSaveDataSystem(contexts));
			Add(new CharactersLifeRefillSaveDataSystem(contexts));
		}
	}
}
