using UnityEngine;
using UnityEngine.UI;

public class TestParticleEmiter : MonoBehaviour
{
	public int m_currentIndex;

	public GameObject[] ParticleDemos;

	public Transform m_container;

	public Button m_NextButton;

	public Button m_PreviousButton;

	private GameObject m_currentParticle;

	public Button m_StartEmitterButton;

	public Button m_PauseEmitterButton;

	public Button m_RestartEmitterButton;

	public Button m_StopEmitterButton;

	public Button m_FreezeEmitterButton;

	public Button m_UnfreezeEmitterButton;

	private void Start()
	{
		m_NextButton.onClick.AddListener(NextDemoParticle);
		m_PreviousButton.onClick.AddListener(PreviousDemoParticle);
		CreateDemoParticle();
	}

	private void NextDemoParticle()
	{
		m_currentIndex++;
		if (m_currentIndex > ParticleDemos.Length - 1)
		{
			m_currentIndex = 0;
		}
		CreateDemoParticle();
	}

	private void PreviousDemoParticle()
	{
		m_currentIndex--;
		if (m_currentIndex < 0)
		{
			m_currentIndex = ParticleDemos.Length - 1;
		}
		CreateDemoParticle();
	}

	private void CreateDemoParticle()
	{
		m_StartEmitterButton.onClick.RemoveAllListeners();
		m_PauseEmitterButton.onClick.RemoveAllListeners();
		m_StopEmitterButton.onClick.RemoveAllListeners();
		m_RestartEmitterButton.onClick.RemoveAllListeners();
		m_FreezeEmitterButton.onClick.RemoveAllListeners();
		m_UnfreezeEmitterButton.onClick.RemoveAllListeners();
		Object.Destroy(m_currentParticle);
		m_currentParticle = Object.Instantiate(ParticleDemos[m_currentIndex]);
		m_currentParticle.transform.SetParent(m_container, false);
		CanvasParticleEmitter[] componentsInChildren = m_currentParticle.GetComponentsInChildren<CanvasParticleEmitter>();
		CanvasParticleEmitter[] array = componentsInChildren;
		foreach (CanvasParticleEmitter canvasParticleEmitter in array)
		{
			m_StartEmitterButton.onClick.AddListener(canvasParticleEmitter.StartEmitter);
			m_PauseEmitterButton.onClick.AddListener(canvasParticleEmitter.PauseEmitter);
			m_StopEmitterButton.onClick.AddListener(canvasParticleEmitter.StopEmitter);
			m_RestartEmitterButton.onClick.AddListener(canvasParticleEmitter.RestartEmitter);
			m_FreezeEmitterButton.onClick.AddListener(canvasParticleEmitter.FreezeEmitter);
			m_UnfreezeEmitterButton.onClick.AddListener(canvasParticleEmitter.UnFreezeEmitter);
		}
	}
}
