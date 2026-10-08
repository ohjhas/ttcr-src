using UnityEngine;

public class bgParallaxBehaviour : MonoBehaviour
{
	[SerializeField]
	private float MainLayerSpeed = 1f;

	[SerializeField]
	private float MiddleLayerSpeed = 3f;

	[SerializeField]
	private float FrontLayerSpeed = 5f;

	[Space]
	[SerializeField]
	private Material separateLayersParallaxMaterial;

	[SerializeField]
	private Material atlasParallaxMaterial;

	[HideInInspector]
	public Vector2 cameraPos = new Vector2(1f, 1f);

	private float mainLayerPosition = 0.15f;

	private float middleLayerPosition = 0.15f;

	private float frontLayerPosition = 0.15f;

	private float screenCoverageFactor = 1f;

	private SpriteRenderer spriteRenderer;

	private void Start()
	{
		FindRenderer();
		UpdatePosition();
		UpdateParallax();
	}

	private void Update()
	{
		if (CanExecute())
		{
			FindRenderer();
			UpdateParallax();
		}
	}

	private void LateUpdate()
	{
		UpdatePosition();
	}

	public void SetSprites(Sprite mainLayer, Sprite middleLayer, Sprite frontLayer)
	{
		FindRenderer();
		spriteRenderer.sprite = mainLayer;
		float num = Mathf.Min((!(middleLayer != null)) ? mainLayer.rect.height : middleLayer.rect.height, (!(frontLayer != null)) ? mainLayer.rect.height : frontLayer.rect.height);
		screenCoverageFactor = mainLayer.rect.height / num;
		if (middleLayer == null && frontLayer == null)
		{
			InitSingleSpriteVersion(mainLayer);
		}
		else if (middleLayer.texture == frontLayer.texture && middleLayer.texture == mainLayer.texture)
		{
			InitAtlasVersion(mainLayer, middleLayer, frontLayer);
		}
		else
		{
			InitSeparateLayersVersion(mainLayer, middleLayer, frontLayer);
		}
		spriteRenderer.material.SetVector("_mainTexOffset", new Vector4(mainLayerPosition, 0f));
		spriteRenderer.material.SetVector("_secondLayerTexOffset", new Vector4(middleLayerPosition, 0f));
		spriteRenderer.material.SetVector("_firstLayerTexOffset", new Vector4(frontLayerPosition, 0f));
	}

	private void InitSingleSpriteVersion(Sprite mainLayer)
	{
		spriteRenderer.material = new Material(separateLayersParallaxMaterial);
		spriteRenderer.sprite = mainLayer;
		MainLayerSpeed = FrontLayerSpeed;
		spriteRenderer.material.SetFloat("_secondLayerSize", 1f);
		spriteRenderer.material.SetFloat("_firstLayerSize", 1f);
	}

	private void InitAtlasVersion(Sprite mainLayer, Sprite middleLayer, Sprite frontLayer)
	{
		spriteRenderer.material = new Material(atlasParallaxMaterial);
		float value = (mainLayer.rect.height + mainLayer.textureRect.y) / (float)mainLayer.texture.height;
		spriteRenderer.material.SetFloat("mainTexEnd", value);
		spriteRenderer.material.SetFloat("_secondLayerSize", 1f - middleLayer.rect.height / mainLayer.rect.height);
		float value2 = (middleLayer.rect.height + middleLayer.textureRect.y) / (float)middleLayer.texture.height;
		spriteRenderer.material.SetFloat("secondTexEnd", value2);
		spriteRenderer.material.SetFloat("_firstLayerSize", 1f - frontLayer.rect.height / mainLayer.rect.height);
		float value3 = (frontLayer.rect.height + frontLayer.textureRect.y) / (float)frontLayer.texture.height;
		spriteRenderer.material.SetFloat("firstTexEnd", value3);
	}

	private void InitSeparateLayersVersion(Sprite mainLayer, Sprite middleLayer, Sprite frontLayer)
	{
		spriteRenderer.material = new Material(separateLayersParallaxMaterial);
		spriteRenderer.material.SetTexture("_SecondLayerTex", middleLayer.texture);
		spriteRenderer.material.SetFloat("_secondLayerSize", 1f - middleLayer.rect.height / mainLayer.rect.height);
		spriteRenderer.material.SetTexture("_FirstLayerTex", frontLayer.texture);
		spriteRenderer.material.SetFloat("_firstLayerSize", 1f - frontLayer.rect.height / mainLayer.rect.height);
	}

	private bool CanExecute()
	{
		GameEntity worldEntity = Contexts.sharedInstance.game.worldEntity;
		GameEntity playerEntity = Contexts.sharedInstance.game.playerEntity;
		if (worldEntity == null || worldEntity.world.Distance <= 0f || playerEntity == null || playerEntity.isDead || playerEntity.isCameraStop)
		{
			return false;
		}
		return true;
	}

	private void FindRenderer()
	{
		if (spriteRenderer == null)
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
		}
	}

	private void UpdatePosition()
	{
		if (spriteRenderer != null && spriteRenderer.sprite != null)
		{
			float num = (float)Screen.width / (float)Screen.height;
			float orthographicSize = Camera.main.orthographicSize;
			float num2 = Mathf.Ceil(spriteRenderer.sprite.rect.width) / spriteRenderer.sprite.pixelsPerUnit;
			float num3 = spriteRenderer.sprite.rect.height / spriteRenderer.sprite.pixelsPerUnit;
			float num4 = num2 * 0.5f - orthographicSize * num;
			float num5 = screenCoverageFactor * orthographicSize / (num3 * 0.5f);
			base.transform.localScale = num5 * Vector3.one;
			base.transform.localPosition = new Vector3(cameraPos.x + num4 * num5, cameraPos.y);
		}
	}

	private void UpdateParallax()
	{
		mainLayerPosition += MainLayerSpeed / 300f * Time.deltaTime;
		if (mainLayerPosition > 1f)
		{
			mainLayerPosition -= 1f;
		}
		middleLayerPosition += MiddleLayerSpeed / 300f * Time.deltaTime;
		if (middleLayerPosition > 1f)
		{
			middleLayerPosition -= 1f;
		}
		frontLayerPosition += FrontLayerSpeed / 300f * Time.deltaTime;
		if (frontLayerPosition > 1f)
		{
			frontLayerPosition -= 1f;
		}
		spriteRenderer.material.SetVector("_mainTexOffset", new Vector4(mainLayerPosition, 0f));
		spriteRenderer.material.SetVector("_secondLayerTexOffset", new Vector4(middleLayerPosition, 0f));
		spriteRenderer.material.SetVector("_firstLayerTexOffset", new Vector4(frontLayerPosition, 0f));
	}
}
