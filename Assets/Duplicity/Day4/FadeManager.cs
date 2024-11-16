using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage; // ȭ���� ���� ���� �̹���
    [SerializeField] private float fadeSpeed = 2f; // ���̵� �ӵ� ���� ���� (�⺻�� 1)
    [SerializeField] private float fadeSpeed22 = 5f; // ���̵� �ӵ� ���� ���� (�⺻�� 1)

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

    public void OnlyStartFadeIn()
    {
        fadeImage.gameObject.SetActive(true); // ���̵� �� ���� �� Ȱ��ȭ
        StartCoroutine(OnlyFadeInCoroutine()); // �ڷ�ƾ���� ȣ��
    }

    private IEnumerator OnlyFadeInCoroutine()
    {
        for (float alpha = 1; alpha >= 0; alpha -= Time.deltaTime * fadeSpeed22)
        {
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
    public void OnlyStartFadeOut()
    {
        fadeImage.gameObject.SetActive(true); // ���̵� �� ���� �� Ȱ��ȭ
        fadeImage.color = new Color(0, 0, 0, 0);
        StartCoroutine(OnlyFadeOutCoroutine()); // �ڷ�ƾ���� ȣ��
    }

    private IEnumerator OnlyFadeOutCoroutine() // ���̵� �ƿ�
    {
        for (float alpha = 0; alpha <= 1; alpha += Time.deltaTime * fadeSpeed22)
        {
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}
