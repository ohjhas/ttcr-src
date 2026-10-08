using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class IntroController : MonoBehaviour
	{
		[Serializable]
		public class ToSteal
		{
			public GameObject Element;

			public float Scale = 1f;
		}

		private const int BEHIND_DESK_SIBLING_INDEX = 4;

		private const int FRONT_DESK_SIBLING_INDEX = 5;

		private const int BEHIND_WINDOW_SIBLING_INDEX = 1;

		[SerializeField]
		private PlayableDirector playableDirector;

		[SerializeField]
		private TimelineAsset loopTimeline;

		[SerializeField]
		private TimelineAsset loopSecondTimeline;

		[SerializeField]
		private TimelineAsset endTimeline;

		[SerializeField]
		private TimelineAsset endFrameTimeLine;

		[SerializeField]
		private IntroRacoon racoon;

		[SerializeField]
		private GameObject tom;

		[SerializeField]
		private GameObject dustEffectPrefab;

		[SerializeField]
		private List<GameObject> initialElements;

		[SerializeField]
		private List<GameObject> behindElements;

		[SerializeField]
		private List<GameObject> frontTopElements;

		[SerializeField]
		private List<GameObject> frontBottomElements;

		[SerializeField]
		private Transform goButtonHolder;

		[SerializeField]
		private HighlightWithPulsating goButtonEffect;

		[SerializeField]
		private Material goButtonMaterial;

		[SerializeField]
		private GameObject tutorialHand;

		[SerializeField]
		private AudioClip endPartMusic;

		[SerializeField]
		private Sprite specialSprite;

		private bool startPartStarted;

		private bool loopPartStarted;

		private bool endPartFinished;

		private bool goClicked;

		private bool specialClicked;

		private bool allCandiesStolen;

		public event Action OnIntroDone;

		private void Start()
		{
			tutorialHand.SetActive(false);
			goButtonHolder.transform.localScale = Vector3.zero;
			goButtonEffect.GetComponent<Button>().onClick.AddListener(GoClicked);
		}

		private void Update()
		{
			if (playableDirector.state == PlayState.Playing && !startPartStarted)
			{
				startPartStarted = true;
			}
			if (Application.isPlaying && startPartStarted && playableDirector.state != PlayState.Playing && !loopPartStarted)
			{
				playableDirector.playableAsset = loopTimeline;
				playableDirector.extrapolationMode = DirectorWrapMode.Loop;
				playableDirector.Play();
				loopPartStarted = true;
				ShowButton();
			}
			if (Application.isPlaying && loopPartStarted && !endPartFinished && playableDirector.state != PlayState.Playing)
			{
				endPartFinished = true;
				playableDirector.playableAsset = endFrameTimeLine;
				playableDirector.extrapolationMode = DirectorWrapMode.Hold;
				playableDirector.Play();
				DOVirtual.DelayedCall(1.5f, delegate
				{
					Contexts.sharedInstance.state.CreateEntity().AddMuteMusic(false);
					Contexts.sharedInstance.game.CreateEntity().isPlayGameMusic = true;
				}).SetUpdate(true);
				Contexts.sharedInstance.game.worldEntity.isGameStarted = true;
				base.transform.parent.gameObject.SetActive(false);
				if (this.OnIntroDone != null)
				{
					this.OnIntroDone();
				}
				Contexts.sharedInstance.uI.CreateEntity().isIntroDone = true;
				tom.SetActive(false);
			}
		}

		private void LateUpdate()
		{
			if (specialClicked && loopPartStarted && !goClicked)
			{
				tom.GetComponentInChildren<Image>().sprite = specialSprite;
			}
		}

		private IEnumerator StartIntro()
		{
			yield return new WaitForSeconds(0.5f);
			playableDirector.Play();
		}

		private void GoClicked()
		{
			if (!goClicked)
			{
				goClicked = true;
				playableDirector.playableAsset = endTimeline;
				playableDirector.extrapolationMode = DirectorWrapMode.None;
				playableDirector.Play();
				goButtonHolder.DOScale(0f, 0.3f);
				tutorialHand.SetActive(false);
				GameObject gameObject = new GameObject();
				AudioSource audioSource = gameObject.AddComponent<AudioSource>();
				audioSource.clip = endPartMusic;
				audioSource.Play();
			}
		}

		private void ShowButton()
		{
			goButtonHolder.transform.DOScale(1f, 0.3f).SetDelay(0.5f);
			goButtonEffect.Init(1f, 1.03f, 1f, goButtonMaterial);
			DOVirtual.DelayedCall(6f, delegate
			{
				if (!goClicked && tutorialHand != null)
				{
					tutorialHand.SetActive(true);
				}
			});
		}

		private void ShowDustEffect(GameObject toSteal)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(dustEffectPrefab, toSteal.transform.parent);
			gameObject.transform.SetSiblingIndex(toSteal.transform.GetSiblingIndex());
			gameObject.transform.position = toSteal.transform.position;
			gameObject.transform.localScale = Vector3.one;
			toSteal.transform.DOScale(0f, 0.3f);
		}

		public void PlayIntro()
		{
			StartCoroutine(StartIntro());
		}

		public void MoveRacoonBehindDesk()
		{
			racoon.transform.SetSiblingIndex(4);
		}

		public void MoveRacoonFrontDesk()
		{
			racoon.transform.SetSiblingIndex(5);
		}

		public void MoveRacoonBehindWindow()
		{
			racoon.transform.SetSiblingIndex(1);
		}

		public void StealBehindElement()
		{
			if (behindElements.Count > 0 && !(playableDirector.time < 0.10000000149011612))
			{
				ShowDustEffect(behindElements[0]);
				behindElements.RemoveAt(0);
			}
		}

		public void StealFrontTopElement()
		{
			if (frontTopElements.Count > 0 && !(playableDirector.time < 0.10000000149011612))
			{
				ShowDustEffect(frontTopElements[0]);
				frontTopElements.RemoveAt(0);
				if (frontBottomElements.Count <= 0)
				{
					playableDirector.playableAsset = loopSecondTimeline;
					playableDirector.extrapolationMode = DirectorWrapMode.Loop;
					playableDirector.Play();
				}
			}
		}

		public void StealFrontBottomElement()
		{
			if (frontBottomElements.Count > 0 && !(playableDirector.time < 0.10000000149011612))
			{
				ShowDustEffect(frontBottomElements[0]);
				frontBottomElements.RemoveAt(0);
			}
		}

		public void StealInitialElement()
		{
			if (initialElements.Count > 0 && !(playableDirector.time < 0.10000000149011612))
			{
				ShowDustEffect(initialElements[0]);
				initialElements.RemoveAt(0);
			}
		}

		public void SpecialClicked()
		{
			specialClicked = true;
		}
	}
}
