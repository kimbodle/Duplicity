using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage; // 화면을 덮는 검은 이미지
    [SerializeField] private float fadeSpeed = 2f; // 페이드 속도 조절 변수 (기본값 1)

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

        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        for (float alpha = 1; alpha >= 0; alpha -= Time.deltaTime * fadeSpeed)
        {
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // 페이드 인이 완료되면 fadeImage 게임 오브젝트 비활성화
        fadeImage.gameObject.SetActive(false);
    }
}
