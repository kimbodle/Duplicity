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

    [Header("Excluded Scenes")]
    public string[] excludedScenes;

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
        if (IsExcludedScene(scene.name))
        {
            StopBGM();
        }
        else
        {
            PlayBGMForSceneAndDay(scene.name, GameManager.Instance.GetCurrentDay()); // ���� Day�� ���� BGM ���
        }
    }

    private bool IsExcludedScene(string sceneName)
    {
        foreach (string excludedScene in excludedScenes)
        {
            if (excludedScene == sceneName)
            {
                return true;
            }
        }
        return false;
    }

    private void PlayBGMForSceneAndDay(string sceneName, int day, float fadeDuration = 1.0f)
    {
        // ���� Day�� �´� ������� �˻�
        AudioClip newClip = GetBGMClipForSceneAndDay(sceneName, day);

        // ���ο� ������� ����
        if (newClip == null)
        {
            Debug.Log($"�� '{sceneName}' Day {day}�� ���� ��������� ����. �⺻ ��������� ���.");
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

    private AudioClip GetBGMClipForSceneAndDay(string sceneName, int day)
    {
        foreach (var sceneBGM in sceneBGMs)
        {
            if (sceneBGM.sceneName == sceneName && sceneBGM.day == day)
            {
                return sceneBGM.bgmClip;
            }
            else if(sceneName == "GameOverScene" || sceneName == "MainScene")
            {
                if (sceneBGM.sceneName == sceneName)
                {
                    return sceneBGM.bgmClip;
                }
            }  
        }
        return null; // ���� Day �̸��� ��Ī�Ǵ� ��������� ������ null ��ȯ
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
    private void StopBGM()
    {
        if (currentFadeCoroutine != null) StopCoroutine(currentFadeCoroutine);
        bgmSource.Stop();
        bgmSource.clip = null;
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