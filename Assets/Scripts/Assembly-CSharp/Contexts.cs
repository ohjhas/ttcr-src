using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Entitas;
using Entitas.CodeGeneration.Attributes;

public class Contexts : IContexts
{
	private static Contexts _sharedInstance;

	public const string Asset = "Asset";

	public const string Name = "Name";

	public static Contexts sharedInstance
	{
		get
		{
			if (_sharedInstance == null)
			{
				_sharedInstance = new Contexts();
			}
			return _sharedInstance;
		}
		set
		{
			_sharedInstance = value;
		}
	}

	public GameContext game { get; set; }

	public InputContext input { get; set; }

	public StateContext state { get; set; }

	public UIContext uI { get; set; }

	public IContext[] allContexts
	{
		get
		{
			return new IContext[4] { game, input, state, uI };
		}
	}

	public Contexts()
	{
		game = new GameContext();
		input = new InputContext();
		state = new StateContext();
		uI = new UIContext();
		IEnumerable<MethodInfo> enumerable = from method in GetType().GetMethods()
			where Attribute.IsDefined(method, typeof(PostConstructorAttribute))
			select method;
		foreach (MethodInfo item in enumerable)
		{
			item.Invoke(this, null);
		}
	}

	public void Reset()
	{
		IContext[] array = allContexts;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Reset();
		}
	}

	[PostConstructor]
	public void InitializeEntityIndices()
	{
		game.AddEntityIndex(new EntityIndex<GameEntity, string>("Asset", game.GetGroup(GameMatcher.Asset), (GameEntity e, IComponent c) => ((AssetComponent)c).name));
		state.AddEntityIndex(new EntityIndex<StateEntity, string>("Name", state.GetGroup(StateMatcher.Name), (StateEntity e, IComponent c) => ((NameComponent)c).Name));
	}
}
