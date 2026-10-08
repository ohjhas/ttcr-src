using Entitas;
using Playsoft.MinisRun;
using UnityEngine;

public sealed class MagnetPowerupSystem : IExecuteSystem, ISystem
{
	private readonly GameContext gameContext;

	private readonly IGroup<GameEntity> collectibles;

	public MagnetPowerupSystem(Contexts contexts)
	{
		gameContext = contexts.game;
		collectibles = gameContext.GetGroup(GameMatcher.Collectible);
	}

	public void Execute()
	{
		bool flag = CanExecute();
		if (flag)
		{
			MagnetizeCollectibles();
			CalculateRemainingTime();
			flag = CanExecute();
		}
		SetParticlesState(flag);
	}

	private bool CanExecute()
	{
		GameEntity playerEntity = gameContext.playerEntity;
		return playerEntity.hasView && playerEntity.hasMagnetized && !playerEntity.isDead;
	}

	private void MagnetizeCollectibles()
	{
		if (!gameContext.playerEntity.hasView)
		{
			return;
		}
		Vector3 vector = gameContext.playerEntity.view.gameObject.transform.position + new Vector3(0f, 1f, 0f);
		SettingsPowerupsSO setting = SettingsService.GetSetting<SettingsPowerupsSO>();
		float magnetDistance = setting.MagnetDistance;
		float magnetForce = setting.MagnetForce;
		float magnetMinRadius = setting.MagnetMinRadius;
		AnimationCurve magnetCurve = setting.MagnetCurve;
		GameEntity[] entities = collectibles.GetEntities();
		foreach (GameEntity gameEntity in entities)
		{
			float sqrMagnitude = (vector - gameEntity.position.Value).sqrMagnitude;
			if (sqrMagnitude < magnetMinRadius)
			{
				gameEntity.ReplacePosition(vector);
			}
			else if (sqrMagnitude < magnetDistance)
			{
				Vector3 newValue = Vector3.Lerp(gameEntity.position.Value, vector, magnetCurve.Evaluate(sqrMagnitude / magnetDistance) * Time.deltaTime * magnetForce);
				gameEntity.ReplacePosition(newValue);
			}
		}
	}

	private void CalculateRemainingTime()
	{
		GameEntity playerEntity = gameContext.playerEntity;
		playerEntity.magnetized.Duration -= Time.deltaTime;
		if (playerEntity.magnetized.Duration <= 0f)
		{
			playerEntity.RemoveMagnetized();
		}
	}

	private void SetParticlesState(bool shouldPlay)
	{
		if (gameContext.playerEntity.hasView)
		{
			ParticleSystem particleModule = GetParticleModule();
			if (particleModule != null)
			{
				Transform transform = gameContext.playerEntity.view.gameObject.transform;
				ParticleSystem.EmissionModule emission = particleModule.emission;
				emission.enabled = shouldPlay;
				emission.rateOverTime = 15f * transform.localScale.y;
				ParticleSystem.ShapeModule shape = particleModule.shape;
				float x = ((!gameContext.playerEntity.isSliding) ? 0f : (-0.75f * transform.localScale.y));
				Vector3 scale = ((!gameContext.playerEntity.isSliding) ? new Vector3(1f, 0.5f, 1f) : new Vector3(0.5f, 1.5f, 1f));
				shape.position = new Vector3(x, shape.position.y, shape.position.z);
				shape.scale = scale;
				shape.radius = Mathf.Lerp(1f, 3f, transform.localScale.y - 1f);
			}
		}
	}

	private ParticleSystem GetParticleModule()
	{
		GameObject gameObject = gameContext.playerEntity.view.gameObject;
		ParticleSystem[] componentsInChildren = gameObject.transform.GetComponentsInChildren<ParticleSystem>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].name.Equals("magnet"))
			{
				if (!componentsInChildren[i].isPlaying)
				{
					componentsInChildren[i].Play();
				}
				return componentsInChildren[i];
			}
		}
		return null;
	}
}
