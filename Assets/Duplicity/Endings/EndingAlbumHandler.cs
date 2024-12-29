using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingAlbumHandler : MonoBehaviour
{
    public static EndingAlbumHandler Instance { get; private set; }

    [SerializeField] private GameObject endingAlbumUI;
    [SerializeField] private Transform endingAlbumGrid; // ���� �̹����� ��ġ�� �θ� ������Ʈ
    [SerializeField] private GameObject endingImagePrefab; // ���� �̹����� ǥ���� ������
    [SerializeField] private Sprite defaultEndingSprite; // ���� ���� ���� ������ ǥ���� �⺻ �̹���
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

    // ���� �ٹ� UI �ʱ�ȭ
    public void InitializeEndingAlbum(Dictionary<string, bool> endingAlbum)
    {
        foreach (var ending in endingAlbum)
        {
            string endingKey = ending.Key;
            if (ending.Value) // ������ ���� ���� �߰�
            {
                AddEndingToAlbum(endingKey);
            }
        }
    }

    // ���ο� ���� ������ �߰�
    public void AddEndingToAlbum(string endingKey)
    {
        if (endingImages.ContainsKey(endingKey))
        {
            Debug.LogWarning($"�̹� �ٹ��� �߰��� ����: {endingKey}");
            return;
        }
        if (endingKey.StartsWith("EndingItem_"))
        {
            return;
        }

        // ������ ����
        GameObject endingImageObj = Instantiate(endingImagePrefab, endingAlbumGrid);
        Image endingImage = endingImageObj.GetComponent<Image>();

        // ���� ��������Ʈ ����
        Sprite endingSprite = GetEndingSprite(endingKey);
        if (endingSprite != null)
        {
            endingImage.sprite = endingSprite;
        }
        else
        {
            Debug.LogWarning($"��������Ʈ�� ã�� �� �����ϴ�: {endingKey}");
        }

        // Ŭ�� �̺�Ʈ �߰�
        Button button = endingImageObj.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => ShowEndingPopup(endingSprite));
        }

        // ��ųʸ��� �߰�
        endingImages[endingKey] = endingImageObj;
    }

    // ���� Ű�� ���� ��������Ʈ�� �������� �Լ�
    private Sprite GetEndingSprite(string endingKey)
    {
        // ������ ��������Ʈ�� �����ϰų� ���ҽ����� �ε��ϴ� ����
        //"Happy_1" -> "Sprites/Endings/Happy1"
        Sprite sprite = Resources.Load<Sprite>($"Sprites/Endings/{endingKey}");
        if (sprite == null)
        {
            Debug.LogWarning($"��������Ʈ�� �������� ����: {endingKey}. �⺻ ��������Ʈ�� ��ȯ.");
            return defaultEndingSprite; // �⺻ ��������Ʈ ��ȯ
        }

        return sprite;
    }

    // �˾� ����
    public void ShowEndingPopup(Sprite endingSprite)
    {
        popupImage.sprite = endingSprite; // �˾��� ������ �̹��� ǥ��
        endingPanel.SetActive(true); // �˾� Ȱ��ȭ
    }
    // �˾� �ݱ�
    public void HideEndingPopup()
    {
        endingPanel.SetActive(false); // �˾� ��Ȱ��ȭ
    }

    public void TogglEndingAlbumUI()
    {
        if (!FirebaseAuthController.Instance.IsLoggedIn()) // �α��� ���� Ȯ��
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
            Debug.LogWarning("�α��� ��û �޽��� UI�� �������� �ʾҽ��ϴ�.");
        }
    }

    private IEnumerator HideLoginRequiredMessageAfterDelay()
    {
        yield return new WaitForSeconds(2f); // 2�� �� �޽��� ��Ȱ��ȭ
        loginRequiredMessageUI.SetActive(false);
    }
}
