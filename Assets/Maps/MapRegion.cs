using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapRegion : MonoBehaviour
{
    [Header("�� �������� ����")]
    public string regionName; // ���� �̸�
    public string sceneName; // �ش� ������ �� �̸�
    public Button regionButton; // UI ��ư
    public int unlockDay; // Ȱ��ȭ�Ǵ� ��. 

    private void Start()
    {
        regionButton.onClick.AddListener(OnRegionClicked);
        //UpdateRegionStatus(); // �ʱ� ���� ������Ʈ
    }

    // ���� Ȱ��ȭ ���� ������Ʈ
    public void UpdateRegionStatus()
    {
        if (GameManager.Instance.GetCurrentDay() >= unlockDay) // ���� ��¥�� ���� Ȱ��ȭ
        {
            regionButton.interactable = true; // ��ư Ȱ��ȭ
            regionButton.GetComponent<Image>().color = Color.white; // ���� ����
        }
        else
        {
            regionButton.interactable = false; // ��ư ��Ȱ��ȭ
            regionButton.GetComponent<Image>().color = Color.gray; // ���� ����
        }
    }

    // ���� Ŭ�� ��
    // ���� �̸����� �ؾ��ұ� �� �̸����� �ؾ��ұ�...
    private void OnRegionClicked()
    {
        if (regionButton.interactable)
        {
            UIManager.Instance.ToggleMapUI();
            //SceneManager.LoadScene(sceneName);
            FindObjectOfType<DayController>().MapIconClick(regionName);
        }
    }

    // ���� Ȱ��ȭ �޼���
    public void Unlock()
    {
        regionButton.interactable = true;
        regionButton.GetComponent<Image>().color = Color.white; // ���� ����
    }

    // ���� ��Ȱ��ȭ �޼���
    public void Lock()
    {
        regionButton.interactable = false;
        regionButton.GetComponent<Image>().color = Color.gray; // ���� ����
    }
}
