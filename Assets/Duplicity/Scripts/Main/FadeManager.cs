using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static FadeManager Instance { get; private set; }

    public Image fadeImage; // 화면을 덮는 검은 이미지
    [SerializeField] private float fadeSpeed = 2f;

    private void Awake()
    {
        // 싱글톤 인스턴스 초기화
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 파괴
        }
    }

    private void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(false); // 초기 상태에서 비활성화
        }
    }

    public void StartFadeOut(System.Action onFadeOutComplete, bool autoFadeIn = false, float delayBeforeFadeIn = 0f)
    {
        if (fadeImage == null) return;

        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeOutCoroutine(onFadeOutComplete, autoFadeIn, delayBeforeFadeIn));
    }

    private IEnumerator FadeOutCoroutine(System.Action onFadeOutComplete, bool autoFadeIn, float delayBeforeFadeIn)
    {
        // 페이드 아웃
        for (float alpha = 0; alpha <= 1; alpha += Time.deltaTime * fadeSpeed)
        {
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1f);

        // 페이드 아웃 완료 후 콜백 호출
        onFadeOutComplete?.Invoke();

        // 지정된 대기 시간 동안 검은 화면 유지
        if (delayBeforeFadeIn > 0f)
        {
            yield return new WaitForSeconds(delayBeforeFadeIn);
        }

        // 자동 페이드 인
        if (autoFadeIn)
        {
            StartFadeIn(null);
        }
    }

    public void StartFadeIn(System.Action onFadeInComplete)
    {
        if (fadeImage == null) return;

        fadeImage.gameObject.SetActive(true);
        StartCoroutine(FadeInCoroutine(onFadeInComplete));
    }

    private IEnumerator FadeInCoroutine(System.Action onFadeInComplete)
    {
        // 페이드 인
        for (float alpha = 1; alpha >= 0; alpha -= Time.deltaTime * fadeSpeed)
        {
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // 페이드 인 완료 후 이미지 비활성화
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.gameObject.SetActive(false);
        onFadeInComplete?.Invoke();
    }
}
