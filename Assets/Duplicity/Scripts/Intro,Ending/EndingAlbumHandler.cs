using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingAlbumHandler : MonoBehaviour
{
    public static EndingAlbumHandler Instance { get; private set; }

    [SerializeField] private GameObject endingAlbumUI;
    [SerializeField] private Transform endingAlbumGrid; // 엔딩 이미지를 배치할 부모 오브젝트
    [SerializeField] private GameObject endingImagePrefab; // 엔딩 이미지를 표시할 프리팹
    [SerializeField] private Sprite defaultEndingSprite; // 아직 보지 않은 엔딩을 표시할 기본 이미지
    [Space(10)]
    [SerializeField] private GameObject endingPanel;
    [SerializeField] private Image popupImage;
    [Space(10)]
    [SerializeField] private GameObject loginRequiredMessageUI;

    private Dictionary<string, GameObject> endingImages = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 엔딩 앨범 UI 초기화
    public void InitializeEndingAlbum(Dictionary<string, bool> endingAlbum)
    {
        foreach (var ending in endingAlbum)
        {
            string endingKey = ending.Key;
            if (ending.Value) // 엔딩을 봤을 때만 추가
            {
                AddEndingToAlbum(endingKey);
            }
        }
    }

    // 새로운 엔딩 프리팹 추가
    public void AddEndingToAlbum(string endingKey)
    {
        if (endingImages.ContainsKey(endingKey))
        {
            Debug.LogWarning($"이미 앨범에 추가된 엔딩: {endingKey}");
            return;
        }
        if (endingKey.StartsWith("EndingItem_"))
        {
            return;
        }

        // 프리팹 생성
        GameObject endingImageObj = Instantiate(endingImagePrefab, endingAlbumGrid);
        Image endingImage = endingImageObj.GetComponent<Image>();

        // 엔딩 스프라이트 설정
        Sprite endingSprite = GetEndingSprite(endingKey);
        if (endingSprite != null)
        {
            endingImage.sprite = endingSprite;
        }
        else
        {
            Debug.LogWarning($"스프라이트를 찾을 수 없습니다: {endingKey}");
        }

        // 클릭 이벤트 추가
        Button button = endingImageObj.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => ShowEndingPopup(endingSprite));
        }

        // 딕셔너리에 추가
        endingImages[endingKey] = endingImageObj;
    }

    // 엔딩 키에 따른 스프라이트를 가져오는 함수
    private Sprite GetEndingSprite(string endingKey)
    {
        // 엔딩별 스프라이트를 매핑하거나 리소스에서 로드하는 로직
        //"Happy_1" -> "Sprites/Endings/Happy1"
        Sprite sprite = Resources.Load<Sprite>($"Sprites/Endings/{endingKey}");
        if (sprite == null)
        {
            Debug.LogWarning($"스프라이트가 존재하지 않음: {endingKey}. 기본 스프라이트를 반환.");
            return defaultEndingSprite; // 기본 스프라이트 반환
        }

        return sprite;
    }

    // 팝업 열기
    public void ShowEndingPopup(Sprite endingSprite)
    {
        popupImage.sprite = endingSprite; // 팝업에 선택한 이미지 표시
        endingPanel.SetActive(true); // 팝업 활성화
    }
    // 팝업 닫기
    public void HideEndingPopup()
    {
        endingPanel.SetActive(false); // 팝업 비활성화
    }

    public void TogglEndingAlbumUI()
    {
        if (!FirebaseAuthController.Instance.IsLoggedIn()) // 로그인 상태 확인
        {
            ShowLoginRequiredMessage();
            return;
        }

        endingAlbumUI.SetActive(!endingAlbumUI.activeSelf);
    }
    private void ShowLoginRequiredMessage()
    {
        if (loginRequiredMessageUI != null)
        {
            loginRequiredMessageUI.SetActive(true);
            StartCoroutine(HideLoginRequiredMessageAfterDelay());
        }
        else
        {
            Debug.LogWarning("로그인 요청 메시지 UI가 설정되지 않았습니다.");
        }
    }

    private IEnumerator HideLoginRequiredMessageAfterDelay()
    {
        yield return new WaitForSeconds(2f); // 2초 후 메시지 비활성화
        loginRequiredMessageUI.SetActive(false);
    }
}
