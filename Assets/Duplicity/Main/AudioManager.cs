using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource bgmSource; // ������� AudioSource
    public AudioSource sfxSource; // ȿ���� AudioSource

    [Header("Scene BGM Settings")]
    public SceneBGM[] sceneBGMs; // ���� ������� ���� �迭
    public AudioClip defaultBGM; // �⺻ �������

    [Header("Audio Settings")]
    [Range(0f, 1f)] public float bgmVolume = 1.0f;
    [Range(0f, 1f)] public float sfxVolume = 1.0f;

    private Coroutine currentFadeCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ���� AudioManager ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // �� �ε� �̺�Ʈ ���
        UpdateBGMVolume();
        UpdateSFXVolume();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �̺�Ʈ ����
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGMForScene(scene.name); // �� �̸��� ���� ������� ���
    }

    private void PlayBGMForScene(string sceneName, float fadeDuration = 1.0f)
    {
        // �� �̸��� �´� ������� �˻�
        AudioClip newClip = GetBGMClipForScene(sceneName);

        // ���ο� ������� ����
        if (newClip == null)
        {
            Debug.Log($"�� '{sceneName}'�� ���� ��������� ����. �⺻ ��������� ���.");
            newClip = defaultBGM;
        }

        if (newClip != null)
        {
            // ���̵� ��ȯ ����
            if (currentFadeCoroutine != null) StopCoroutine(currentFadeCoroutine);
            currentFadeCoroutine = StartCoroutine(FadeToNewBGM(newClip, fadeDuration));
        }
        else
        {
            Debug.LogWarning("�⺻ ��������� �������� ����");
        }
    }

    private AudioClip GetBGMClipForScene(string sceneName)
    {
        foreach (var sceneBGM in sceneBGMs)
        {
            if (sceneBGM.sceneName == sceneName)
            {
                return sceneBGM.bgmClip;
            }
        }
        return null; // �� �̸��� ��Ī�Ǵ� ��������� ������ null ��ȯ
    }

    private IEnumerator FadeToNewBGM(AudioClip newClip, float fadeDuration)
    {
        // ���� ������� ���̵� �ƿ�
        float startVolume = bgmSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }
        bgmSource.volume = 0;
        bgmSource.Stop();

        // ���ο� ������� ���� �� ���̵� ��
        bgmSource.clip = newClip;
        bgmSource.Play();

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(0, bgmVolume, t / fadeDuration);
            yield return null;
        }
        bgmSource.volume = bgmVolume;
    }
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip); // ȿ���� ���
        }
        else
        {
            Debug.LogWarning("ȿ���� Ŭ���� null�Դϴ�.");
        }
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        UpdateBGMVolume();
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        UpdateSFXVolume();
    }

    private void UpdateBGMVolume()
    {
        bgmSource.volume = bgmVolume;
    }

    private void UpdateSFXVolume()
    {
        sfxSource.volume = sfxVolume;
    }
}