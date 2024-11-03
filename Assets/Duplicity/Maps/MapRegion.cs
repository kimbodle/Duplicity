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
    public Dialog[] dialogIfIncomplete;

    private void Start()
    {
        regionButton.onClick.AddListener(OnRegionClicked);
        //Debug.Log("�ʸ��� ��ŸƮ???????????");
        //� ������������, Start�ӿ��� �ұ��ϰ� ����ȯ�ϰ� mapIcon�� Ŭ�������� �̰� ��� ����
        //UpdateRegionStatus(); // �ʱ� ���� ������Ʈ
    }

    // ���� Ȱ��ȭ ���� ������Ʈ
    public void UpdateRegionStatus()
    {
        regionButton.interactable = true; // ��ư Ȱ��ȭ
        regionButton.GetComponent<Image>().color = Color.gray; // ���� ����
    }

    // ���� Ŭ�� ��
    // ���� �̸����� �ؾ��ұ� �� �̸����� �ؾ��ұ�...
    private void OnRegionClicked()
    {
        //�� UI �ݱ�
        UIManager.Instance.ToggleMapUI();
        FindObjectOfType<DayController>().MapIconClick(regionName);
    }

    // ���� Ȱ��ȭ �޼���
    public void Unlock()
    {
        Debug.Log(regionName + "���");
        regionButton.interactable = true;
        regionButton.GetComponent<Image>().color = Color.white; // ���� ����
    }

    // ���� ��Ȱ��ȭ �޼���
    public void Lock()
    {
        Debug.Log(regionName + "Lock");
        regionButton.interactable = false;
        regionButton.GetComponent<Image>().color = Color.gray; // ���� ����
    }
}
