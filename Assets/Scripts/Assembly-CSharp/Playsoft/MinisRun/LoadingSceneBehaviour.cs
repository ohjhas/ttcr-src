using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class LoadingSceneBehaviour : MonoBehaviour, ISceneChangeRequestListener
	{
		private static LoadingSceneBehaviour loadingInstance;

		[SerializeField]
		private GameObject loadinGameObject;

		[Header("Initial loading")]
		[SerializeField]
		private GameObject splashScreenGO;

		[SerializeField]
		private Image LoadingFill;

		[Header("Normal loading")]
		[SerializeField]
		private GameObject normalLayerGO;

		[SerializeField]
		private RectTransform maskObject;

		[SerializeField]
		private GameObject character;

		[Space]
		[SerializeField]
		private float fadeDuration;

		[SerializeField]
		private Vector2 maskInitSize;

		[SerializeField]
		private Vector2 maskEndSize;

		private bool initialLoading;

		private float changeStartTime;

		private void Start()
		{
			if (loadingInstance == null)
			{
				Object.DontDestroyOnLoad(base.gameObject);
				loadingInstance = this;
				loadinGameObject.SetActive(false);
			}
			else
			{
				Object.Destroy(base.gameObject);
			}
		}

		private void OnEnable()
		{
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		private void OnDisable()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
		}

		private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
		{
			if (!(arg0.name == ProjectConstants.INITIAL_START_SCENE_NAME) && !(arg0.name == ProjectConstants.TRANSITION_SCENE))
			{
				Contexts.sharedInstance.uI.CreateEntity().AddSceneChangeRequestListener(this);
			}
		}

		public void OnSceneChange(string sceneName, bool isInitialLoading)
		{
			initialLoading = isInitialLoading && splashScreenGO != null;
			SetupLoadingScreen();
			LoadLevel(sceneName);
		}

		private void SetupLoadingScreen()
		{
			loadinGameObject.SetActive(true);
			if (initialLoading)
			{
				splashScreenGO.SetActive(true);
				normalLayerGO.SetActive(false);
				if (LoadingFill != null)
				{
					LoadingFill.fillAmount = 0f;
				}
				return;
			}
			if (splashScreenGO != null)
			{
				splashScreenGO.SetActive(false);
			}
			normalLayerGO.SetActive(true);
			character.SetActive(false);
			maskObject.sizeDelta = maskInitSize;
		}

		private void LoadLevel(string sceneName)
		{
			if (initialLoading)
			{
				StartCoroutine(LoadAsyncUsingSplashScreen(sceneName));
			}
			else
			{
				StartCoroutine(LoadAsyncUsingLoadingScreen(sceneName));
			}
		}

		private IEnumerator LoadAsyncUsingLoadingScreen(string sceneToLoadName)
		{
			changeStartTime = Time.realtimeSinceStartup;
			string sceneToUnload = GetNameOfSceneToUnload();
			yield return StartCoroutine(ShowLoadingScreen());
			yield return StartCoroutine(LoadTransitionScene());
			yield return StartCoroutine(ReplaceScenes(sceneToUnload, sceneToLoadName));
			yield return new WaitUntil(IsNoPendingAsyncLoadings);
			yield return StartCoroutine(HideLoadingScreen());
			yield return StartCoroutine(SendNewSceneLoaded());
			SendTransitionLog();
		}

		private IEnumerator LoadAsyncUsingSplashScreen(string sceneToLoadName)
		{
			changeStartTime = Time.realtimeSinceStartup;
			yield return StartCoroutine(LoadNewSceneAsync(sceneToLoadName));
			yield return StartCoroutine(WaitForAsyncRequests());
			yield return StartCoroutine(FinishLoadingBarAnim());
			yield return StartCoroutine(HideEndDestroySplashScreen());
			yield return StartCoroutine(SendNewSceneLoaded());
			SendTransitionLog();
		}

		private IEnumerator LoadNewSceneAsync(string sceneToLoadName)
		{
			AsyncOperation loadingScreen = SceneManager.LoadSceneAsync(sceneToLoadName);
			while (!loadingScreen.isDone)
			{
				SetLoadingBarValue(loadingScreen.progress * 0.5f);
				yield return null;
			}
		}

		private IEnumerator WaitForAsyncRequests()
		{
			yield return null;
			yield return null;
			float baseProgress = 0.5f;
			while (!IsNoPendingAsyncLoadings())
			{
				baseProgress += Time.smoothDeltaTime * 0.01f;
				SetLoadingBarValue(baseProgress + GetLoadingProgress() * 0.5f);
				yield return null;
			}
		}

		private IEnumerator FinishLoadingBarAnim()
		{
			while (!Mathf.Approximately(LoadingFill.fillAmount, 1f))
			{
				SetLoadingBarValue(1f);
				yield return null;
			}
		}

		private IEnumerator HideEndDestroySplashScreen()
		{
			yield return null;
			yield return null;
			Time.timeScale = 1f;
			loadinGameObject.SetActive(false);
			if (splashScreenGO != null)
			{
				Object.Destroy(splashScreenGO);
			}
		}

		private void SetLoadingBarValue(float currentProgress)
		{
			if (LoadingFill != null)
			{
				LoadingFill.fillAmount = Mathf.Min(LoadingFill.fillAmount + Time.smoothDeltaTime, currentProgress);
			}
		}

		private bool IsNoPendingAsyncLoadings()
		{
			return Mathf.Approximately(GetLoadingProgress(), 1f);
		}

		private bool IsUIEntityExists()
		{
			return Contexts.sharedInstance != null && Contexts.sharedInstance.uI != null && Contexts.sharedInstance.uI.uIEntity != null;
		}

		private float GetLoadingProgress()
		{
			if (Contexts.sharedInstance.state != null && Contexts.sharedInstance.state.sessionEntity != null && Contexts.sharedInstance.state.sessionEntity.hasPendingAsyncRequests)
			{
				return Contexts.sharedInstance.state.sessionEntity.pendingAsyncRequests.Progress;
			}
			return 0f;
		}

		private string GetNameOfSceneToUnload()
		{
			return SceneManager.GetActiveScene().name;
		}

		private IEnumerator ShowLoadingScreen()
		{
			maskObject.DOSizeDelta(maskEndSize, fadeDuration).SetUpdate(true);
			yield return new WaitForSecondsRealtime(fadeDuration);
			character.SetActive(true);
			yield return null;
		}

		private IEnumerator LoadTransitionScene()
		{
			yield return SceneManager.LoadSceneAsync(ProjectConstants.TRANSITION_SCENE, LoadSceneMode.Additive);
			Scene transitionScene = SceneManager.GetSceneByName(ProjectConstants.TRANSITION_SCENE);
			SceneManager.SetActiveScene(transitionScene);
		}

		private IEnumerator ReplaceScenes(string sceneToUnload, string sceneToLoadName)
		{
			yield return SceneManager.UnloadSceneAsync(sceneToUnload);
			yield return Resources.UnloadUnusedAssets();
			yield return SceneManager.LoadSceneAsync(sceneToLoadName, LoadSceneMode.Single);
			yield return null;
			yield return null;
		}

		private IEnumerator HideLoadingScreen()
		{
			yield return null;
			yield return null;
			Time.timeScale = 1f;
			character.SetActive(false);
			maskObject.DOSizeDelta(maskInitSize, fadeDuration).OnComplete(delegate
			{
				loadinGameObject.SetActive(false);
			}).SetUpdate(true);
		}

		private void SendTransitionLog()
		{
			float newTransitionDuration = (Time.realtimeSinceStartup - changeStartTime + fadeDuration) * 1000f;
			if (Contexts.sharedInstance.state != null && Contexts.sharedInstance.state.sessionEntity != null)
			{
				Contexts.sharedInstance.state.sessionEntity.ReplaceTransitionInformation(newTransitionDuration);
			}
		}

		private IEnumerator SendNewSceneLoaded()
		{
			yield return new WaitUntil(IsUIEntityExists);
			Contexts.sharedInstance.uI.uIEntity.isNewSceneLoaded = true;
		}
	}
}
