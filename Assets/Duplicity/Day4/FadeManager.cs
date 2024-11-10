using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage; // ȭ���� ���� ���� �̹���
    [SerializeField] private float fadeSpeed = 2f; // ���̵� �ӵ� ���� ���� (�⺻�� 1)

    private void Start()
    {
        fadeImage.gameObject.SetActive(false);
    }

    public void StartFadeOut(System.Action onFadeOutComplete)
    {
        fadeImage.gameObject.SetActive(true); // ���̵� ���� �� Ȱ��ȭ

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

        // ���̵� �ƿ��� ���� �� �ڵ����� ���̵� �� ����
        StartFadeIn();
    }

    public void StartFadeIn(System.Action onFadeInComplete = null)
    {
        fadeImage.gameObject.SetActive(true); // ���̵� �� ���� �� Ȱ��ȭ
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
        fadeImage.gameObject.SetActive(false); // ���̵� �� �Ϸ� �� ��Ȱ��ȭ
    }
}
