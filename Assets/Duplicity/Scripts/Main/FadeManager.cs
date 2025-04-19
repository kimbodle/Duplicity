using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static FadeManager Instance { get; private set; }

    public Image fadeImage; // ȭ���� ���� ���� �̹���
    [SerializeField] private float fadeSpeed = 2f;

    private void Awake()
    {
        // �̱��� �ν��Ͻ� �ʱ�ȭ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� �ı�
        }
    }

    private void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(false); // �ʱ� ���¿��� ��Ȱ��ȭ
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
        // ���̵� �ƿ�
        for (float alpha = 0; alpha <= 1; alpha += Time.deltaTime * fadeSpeed)
        {
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1f);

        // ���̵� �ƿ� �Ϸ� �� �ݹ� ȣ��
        onFadeOutComplete?.Invoke();

        // ������ ��� �ð� ���� ���� ȭ�� ����
        if (delayBeforeFadeIn > 0f)
        {
            yield return new WaitForSeconds(delayBeforeFadeIn);
        }

        // �ڵ� ���̵� ��
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
        // ���̵� ��
        for (float alpha = 1; alpha >= 0; alpha -= Time.deltaTime * fadeSpeed)
        {
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        // ���̵� �� �Ϸ� �� �̹��� ��Ȱ��ȭ
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.gameObject.SetActive(false);
        onFadeInComplete?.Invoke();
    }
}
