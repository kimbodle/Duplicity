using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EndingImage : MonoBehaviour
{
    [Header("GameOver / Ending")]
    public Image endingBackgroundImage; // ��� �̹��� ������Ʈ
    public Sprite[] EndingBackgrounds; // ���� ��� �̹��� �迭
    public TMP_Text endingMessageText; // �޽��� �ؽ�Ʈ
    public Button retryButton; // ����� ��ư

    private void Start()
    {
 
        // EndingManager�� ���� �ε����� �޽����� UI ����
        var endingManager = EndingManager.Instance;
        int endingIndex = endingManager.endingIndex;
        string endingMessage = endingManager.endingMessage;

        // ���� �ٹ��� ���� (�ߺ� ����)
        GameManager.Instance.SaveEnding("Ending", endingIndex);

        // �ε����� �´� ��� �̹��� ����
        if (endingIndex >= 0 && endingIndex < EndingBackgrounds.Length)
        {
            endingBackgroundImage.sprite = EndingBackgrounds[endingIndex];
        }
        else
        {
            Debug.LogWarning("��ȿ���� ���� ���� �ε���");
        }
        /*
        // ������ ���� �޽��� ����
        if (endingMessageText != null)
        {
            endingMessageText.text = endingManager.endingMessage;
            endingMessageText.gameObject.SetActive(true);
        }
        */
        retryButton.onClick.AddListener(OnClickRetryButton);

        retryButton.gameObject.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ
        StartCoroutine(ShowRetryButtonWithDelay());
    }

    private IEnumerator ShowRetryButtonWithDelay()
    {
        yield return new WaitForSeconds(2f); // 2�� ���
        retryButton.gameObject.SetActive(true); // 2�� �� ��ư Ȱ��ȭ
    }

    public void OnClickRetryButton()
    {
        GameManager.Instance.GameOver();
    }

    public void CloseRetryUI()
    {
        retryButton.gameObject.SetActive(false);
        endingMessageText.gameObject.SetActive(false);
    }
}
