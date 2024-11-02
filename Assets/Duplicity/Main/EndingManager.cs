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
        // 전달받은 엔딩 이름으로 구분
        switch (endingName)
        {
            case "GoodEnding":
                SceneManager.LoadScene("GoodEndingScene");
                break;
            case "BadEnding":
                SceneManager.LoadScene("BadEndingScene");
                break;
            // 추가 엔딩이 있을 경우 추가
            default:
                Debug.LogError("Unknown ending: " + endingName);
                break;
        }
        EndingCanvas.SetActive(true);
        // 메세지 표시
        StartCoroutine(ShowEndingMessage(message));
    }

    private IEnumerator ShowEndingMessage(string message)
    {
        // 씬이 로드될 때까지 대기 후 메세지 업데이트
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
