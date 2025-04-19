using UnityEngine;
using UnityEngine.UI;

public class Frame : MonoBehaviour
{
    public Image frameImage;
    public PhotoMissionManager photoMissionManager;
    private Item currentItem;

    private void Start()
    {
        frameImage.color = new Color(1, 1, 1, 0); // ����
    }
    public bool SetPhoto(Item item)
    {
        if (currentItem != null) return false; // �̹� ������ ������ ���� ��ȯ

        frameImage.color = Color.white;
        currentItem = item;
        frameImage.sprite = item.itemIcon;

        // PhotoMissionManager�� �˸�
        photoMissionManager?.OnPhotoPlaced();

        return true; // ���������� ������ �ɸ�
    }


    public void ResetFrame()
    {
        frameImage.color = new Color(1, 1, 1, 0); // ����
        currentItem = null;
        frameImage.sprite = null; // �⺻ �̹����� ����

    }

    public bool HasPhoto()
    {
        return currentItem != null;
    }

    public Item GetCurrentItem()
    {
        return currentItem;
    }
}
