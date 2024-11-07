using UnityEngine;
using UnityEngine.UI;

public class SecretDocument : MonoBehaviour
{
    private Button button; // ��ư ������Ʈ
    private Image image; // ��ư�� �̹��� ������Ʈ
    public GameObject secretDocumentImage; // �������� ǥ���� �̹���
    public int clickThreshold = 7; // ��ư�� ������ �ϴ� Ƚ��
    private int clickCount = 0; // ���� Ŭ�� Ƚ��

    private Color initialColor = Color.white; // �ʱ� ����
    private Color targetColor = Color.red; // ���� ��ȭ�� ����

    void Start()
    {
        // ������Ʈ �ʱ�ȭ
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        // ��ư Ŭ�� �̺�Ʈ �߰�
        button.onClick.AddListener(OnButtonClick);

        // secretDocumentImage �ʱ� ��Ȱ��ȭ
        if (secretDocumentImage != null)
        {
            secretDocumentImage.SetActive(false);
        }
    }

    private void OnButtonClick()
    {
        clickCount++;

        // Ŭ�� Ƚ���� ���� ���� ��ȭ
        float progress = (float)clickCount / clickThreshold;
        image.color = Color.Lerp(initialColor, targetColor, progress);

        // Ŭ�� Ƚ���� �Ӱ谪�� ������ SecretDocumentImage ǥ��
        if (clickCount >= clickThreshold && secretDocumentImage != null)
        {
            secretDocumentImage.SetActive(true);
            Debug.Log("Secret Document Image displayed!");
        }
    }
}
