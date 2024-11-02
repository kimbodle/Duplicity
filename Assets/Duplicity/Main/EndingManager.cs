using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    public static EndingManager Instance { get; private set; }
    public GameObject EndingCanvas;
    public TMP_Text endingMessageText;
    public GameObject retryButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadEnding(string endingName, string message)
    {
        GameManager.Instance.isInitializingGameState = true;
        // ���޹��� ���� �̸����� ����
        switch (endingName)
        {
            case "GoodEnding":
                SceneManager.LoadScene("GoodEndingScene");
                break;
            case "BadEnding":
                SceneManager.LoadScene("BadEndingScene");
                break;
            // �߰� ������ ���� ��� �߰�
            default:
                Debug.LogError("Unknown ending: " + endingName);
                break;
        }
        EndingCanvas.SetActive(true);
        // �޼��� ǥ��
        StartCoroutine(ShowEndingMessage(message));
    }

    private IEnumerator ShowEndingMessage(string message)
    {
        // ���� �ε�� ������ ��� �� �޼��� ������Ʈ
        yield return new WaitForSeconds(1f);

        if (endingMessageText != null)
        {
            endingMessageText.text = message;
            endingMessageText.gameObject.SetActive(true);
            retryButton.gameObject.SetActive(true);
        }
    }

    public void CloseRetryUI()
    {
        retryButton.SetActive(false);
        endingMessageText.gameObject.SetActive(false);
        EndingCanvas.SetActive(false);
    }
}
