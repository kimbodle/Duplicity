using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage; // ȭ���� ���� ���� �̹���
    [SerializeField] private float fadeSpeed = 2f; // ���̵� �ӵ� ���� ���� (�⺻�� 1)

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

        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        for (float alpha = 1; alpha >= 0; alpha -= Time.deltaTime * fadeSpeed)
        {
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // ���̵� ���� �Ϸ�Ǹ� fadeImage ���� ������Ʈ ��Ȱ��ȭ
        fadeImage.gameObject.SetActive(false);
    }
}
