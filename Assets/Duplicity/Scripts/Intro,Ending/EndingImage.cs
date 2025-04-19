using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndingImage : MonoBehaviour
{
    [Header("GameOver / Ending")]
    public Image endingBackgroundImage; // 배경 이미지 오브젝트
    public Sprite[] EndingBackgrounds; // 엔딩 배경 이미지 배열
    public GameObject endingMessagePanel;
    public Button retryButton; // 재시작 버튼

    private void Start()
    {
        if (GameManager.Instance.GetCurrentDay() == 8 && EndingManager.Instance.endingIndex == 1)
        {
            endingMessagePanel.gameObject.SetActive(false);
        }
        // EndingManager의 엔딩 인덱스와 메시지로 UI 설정
        var endingManager = EndingManager.Instance;
        int endingIndex = endingManager.endingIndex;
        string endingMessage = endingManager.endingMessage;

        string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "GameOverScene")
        {
            // 엔딩 앨범에 저장 (중복 방지)
            GameManager.Instance.SaveEnding("GameOver", endingIndex);
        }
        else
        {
            GameManager.Instance.SaveEnding("Ending", endingIndex);
        }

        // 인덱스에 맞는 배경 이미지 설정
        if (endingIndex >= 0 && endingIndex < EndingBackgrounds.Length)
        {
            endingBackgroundImage.sprite = EndingBackgrounds[endingIndex];
        }
        else
        {
            Debug.LogWarning("유효하지 않은 엔딩 인덱스");
        }
        retryButton.onClick.AddListener(OnClickRetryButton);

        retryButton.gameObject.SetActive(false); // 초기에는 비활성화
        StartCoroutine(ShowRetryButtonWithDelay());
    }

    private IEnumerator ShowRetryButtonWithDelay()
    {
        yield return new WaitForSeconds(2f); // 2초 대기
        retryButton.gameObject.SetActive(true); // 2초 후 버튼 활성화
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
