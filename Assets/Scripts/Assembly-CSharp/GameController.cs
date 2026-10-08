using Entitas;
using Playsoft.MinisRun;
using UnityEngine;

public class GameController : MonoBehaviour
{
	private Systems _systems;

	private void Start()
	{
		Random.InitState(42);
		Application.targetFrameRate = 120;
		QualitySettings.vSyncCount = 0;
		Contexts sharedInstance = Contexts.sharedInstance;
		_systems = createSystems(sharedInstance);
		_systems.Initialize();
	}

	private void Update()
	{
		_systems.Execute();
		_systems.Cleanup();
	}

	private void OnDestroy()
	{
		_systems.ClearReactiveSystems();
		_systems.DeactivateReactiveSystems();
		GameEntity[] entities = Contexts.sharedInstance.game.GetEntities();
		foreach (GameEntity gameEntity in entities)
		{
			gameEntity.Destroy();
		}
		InputEntity[] entities2 = Contexts.sharedInstance.input.GetEntities();
		foreach (InputEntity inputEntity in entities2)
		{
			inputEntity.Destroy();
		}
		_systems.TearDown();
	}

	private Systems createSystems(Contexts contexts)
	{
		return new Feature("Systems").Add(new InputSystems(contexts)).Add(new BalancingDataSystems(contexts)).Add(new O7SDKSystem(contexts))
			.Add(new GameSystems(contexts))
			.Add(new StateSystems(contexts))
			.Add(new UISystems(contexts))
			.Add(new ViewSystems(contexts))
			.Add(new DestroySystem(contexts))
			.Add(new StateDestroySystem(contexts));
	}
}
