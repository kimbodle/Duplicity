using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndingImage : MonoBehaviour
{
    [Header("GameOver / Ending")]
    public Image endingBackgroundImage; // ��� �̹��� ������Ʈ
    public Sprite[] EndingBackgrounds; // ���� ��� �̹��� �迭
    public GameObject endingMessagePanel;
    public Button retryButton; // ����� ��ư

    private void Start()
    {
        if (GameManager.Instance.GetCurrentDay() == 8 && EndingManager.Instance.endingIndex == 1)
        {
            endingMessagePanel.gameObject.SetActive(false);
        }
        // EndingManager�� ���� �ε����� �޽����� UI ����
        var endingManager = EndingManager.Instance;
        int endingIndex = endingManager.endingIndex;
        string endingMessage = endingManager.endingMessage;

        string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "GameOverScene")
        {
            // ���� �ٹ��� ���� (�ߺ� ����)
            GameManager.Instance.SaveEnding("GameOver", endingIndex);
        }
        else
        {
            GameManager.Instance.SaveEnding("Ending", endingIndex);
        }

        // �ε����� �´� ��� �̹��� ����
        if (endingIndex >= 0 && endingIndex < EndingBackgrounds.Length)
        {
            endingBackgroundImage.sprite = EndingBackgrounds[endingIndex];
        }
        else
        {
            Debug.LogWarning("��ȿ���� ���� ���� �ε���");
        }
        retryButton.onClick.AddListener(OnClickRetryButton);

        retryButton.gameObject.SetActive(false); // �ʱ⿡�� ��Ȱ��ȭ
        StartCoroutine(ShowRetryButtonWithDelay());
    }

    private IEnumerator ShowRetryButtonWithDelay()
    {
        yield return new WaitForSeconds(2f); // 2�� ���
        retryButton.gameObject.SetActive(true); // 2�� �� ��ư Ȱ��ȭ
        if (GameManager.Instance.GetCurrentDay() == 8 && EndingManager.Instance.endingIndex == 1)
        {
            endingMessagePanel.gameObject.SetActive(true);
        }
    }

    public void OnClickRetryButton()
    {
        if(GameManager.Instance.GetCurrentDay() == 9 && GameManager.Instance.currentTask =="TheEnd" ||
            GameManager.Instance.GetCurrentDay() == 8 && EndingManager.Instance.endingIndex == 1)
        {
            GameManager.Instance.ResetGameState();
            FirebaseAuthController.Instance.Logout();
            DialogManager.Instance.ClearHistory();
            SingletonManager.Instance.LogoutAndDestroySingletons();
        }
        else
        {
            GameManager.Instance.GameOver();
        }
    }

    public void CloseRetryUI()
    {
        endingMessagePanel.gameObject.SetActive(false);
    }
}
