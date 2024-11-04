using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage; // 화면을 덮는 검은 이미지
    [SerializeField] private float fadeSpeed = 1f; // 페이드 속도 조절 변수 (기본값 1)

    public void StartFadeOut(System.Action onFadeOutComplete)
    {
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

        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        for (float alpha = 1; alpha >= 0; alpha -= Time.deltaTime * fadeSpeed)
        {
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}
