using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage; // ȭ���� ���� ���� �̹���
    [SerializeField] private float fadeSpeed = 1f; // ���̵� �ӵ� ���� ���� (�⺻�� 1)

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
