using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage; // 화면을 덮는 검은 이미지
    [SerializeField] private float fadeSpeed = 2f; // 페이드 속도 조절 변수 (기본값 1)

    private void Start()
    {
        fadeImage.gameObject.SetActive(false);
    }

    public void StartFadeOut(System.Action onFadeOutComplete)
    {
        fadeImage.gameObject.SetActive(true); // 페이드 시작 시 활성화

        StartCoroutine(FadeOutCoroutine(onFadeOutComplete));
    }

    private IEnumerator FadeOutCoroutine(System.Action onFadeOutComplete)
    {
        for (float alpha = 0; alpha <= 1; alpha += Time.deltaTime * fadeSpeed)
        {
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        onFadeOutComplete?.Invoke();

        // 페이드 아웃이 끝난 후 자동으로 페이드 인 시작
        StartFadeIn();
    }

    public void StartFadeIn(System.Action onFadeInComplete = null)
    {
        fadeImage.gameObject.SetActive(true); // 페이드 인 시작 시 활성화
        StartCoroutine(FadeInCoroutine(onFadeInComplete));
    }

    private IEnumerator FadeInCoroutine(System.Action onFadeInComplete)
    {
        for (float alpha = 1; alpha >= 0; alpha -= Time.deltaTime * fadeSpeed)
        {
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        onFadeInComplete?.Invoke();
        fadeImage.gameObject.SetActive(false); // 페이드 인 완료 시 비활성화
    }
}
