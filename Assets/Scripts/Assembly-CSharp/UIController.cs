using Entitas;
using Playsoft.MinisRun;
using UnityEngine;

public class UIController : MonoBehaviour
{
	private Systems _systems;

	private void Start()
	{
		Application.targetFrameRate = 120;
		Contexts sharedInstance = Contexts.sharedInstance;
		_systems = createSystems(sharedInstance);
		_systems.Initialize();
	}

	private void Update()
	{
		if (_systems != null)
		{
			_systems.Execute();
			_systems.Cleanup();
		}
		else
		{
			Debug.LogError("UI systems destroyed");
			Start();
		}
	}

	private void OnDestroy()
	{
		if (_systems != null)
		{
			_systems.TearDown();
		}
	}

	private Systems createSystems(Contexts contexts)
	{
		return new Feature("Systems").Add(new LogRouterSystems(contexts)).Add(new BalancingDataSystems(contexts)).Add(new O7SDKSystem(contexts))
			.Add(new StateSystems(contexts))
			.Add(new UISystems(contexts))
			.Add(new DestroySystem(contexts))
			.Add(new StateDestroySystem(contexts));
	}
}
