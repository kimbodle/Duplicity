using UnityEngine;
using UnityEngine.UI;

public class SecretDocument : MonoBehaviour
{
    private Button button; // 버튼 컴포넌트
    private Image image; // 버튼의 이미지 컴포넌트
    public GameObject secretDocumentImage; // 마지막에 표시할 이미지
    public int clickThreshold = 7; // 버튼을 눌러야 하는 횟수
    private int clickCount = 0; // 현재 클릭 횟수

    private Color initialColor = Color.white; // 초기 색상
    private Color targetColor = Color.red; // 점점 변화할 색상

    void Start()
    {
        // 컴포넌트 초기화
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        // 버튼 클릭 이벤트 추가
        button.onClick.AddListener(OnButtonClick);

        // secretDocumentImage 초기 비활성화
        if (secretDocumentImage != null)
        {
            secretDocumentImage.SetActive(false);
        }
    }

    private void OnButtonClick()
    {
        clickCount++;

        // 클릭 횟수에 따라 색상 변화
        float progress = (float)clickCount / clickThreshold;
        image.color = Color.Lerp(initialColor, targetColor, progress);

        // 클릭 횟수가 임계값을 넘으면 SecretDocumentImage 표시
        if (clickCount >= clickThreshold && secretDocumentImage != null)
        {
            secretDocumentImage.SetActive(true);
            Debug.Log("Secret Document Image displayed!");
        }
    }
}
