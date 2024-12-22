using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource bgmSource; // 배경음악 AudioSource
    public AudioSource sfxSource; // 효과음 AudioSource

    [Header("Scene BGM Settings")]
    public SceneBGM[] sceneBGMs; // 씬별 배경음악 설정 배열
    public AudioClip defaultBGM; // 기본 배경음악

    [Header("Audio Settings")]
    [Range(0f, 1f)] public float bgmVolume = 1.0f;
    [Range(0f, 1f)] public float sfxVolume = 1.0f;

    private Coroutine currentFadeCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환에도 AudioManager 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // 씬 로드 이벤트 등록
        UpdateBGMVolume();
        UpdateSFXVolume();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 해제
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGMForScene(scene.name); // 씬 이름에 따라 배경음악 재생
    }

    private void PlayBGMForScene(string sceneName, float fadeDuration = 1.0f)
    {
        // 씬 이름에 맞는 배경음악 검색
        AudioClip newClip = GetBGMClipForScene(sceneName);

        // 새로운 배경음악 설정
        if (newClip == null)
        {
            Debug.Log($"씬 '{sceneName}'에 대한 배경음악이 없음. 기본 배경음악을 재생.");
            newClip = defaultBGM;
        }

        if (newClip != null)
        {
            // 페이드 전환 시작
            if (currentFadeCoroutine != null) StopCoroutine(currentFadeCoroutine);
            currentFadeCoroutine = StartCoroutine(FadeToNewBGM(newClip, fadeDuration));
        }
        else
        {
            Debug.LogWarning("기본 배경음악이 설정되지 않음");
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
        return null; // 씬 이름과 매칭되는 배경음악이 없으면 null 반환
    }

    private IEnumerator FadeToNewBGM(AudioClip newClip, float fadeDuration)
    {
        // 현재 배경음악 페이드 아웃
        float startVolume = bgmSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            bgmSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null;
        }
        bgmSource.volume = 0;
        bgmSource.Stop();

        // 새로운 배경음악 설정 및 페이드 인
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
            sfxSource.PlayOneShot(clip); // 효과음 재생
        }
        else
        {
            Debug.LogWarning("효과음 클립이 null입니다.");
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