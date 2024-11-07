using UnityEngine;
using UnityEngine.UI;

public class Frame : MonoBehaviour
{
    public Image frameImage;
    public PhotoMissionManager photoMissionManager;
    private Item currentItem;

    public void SetPhoto(Item item)
    {
        currentItem = item;
        frameImage.sprite = item.itemIcon;
    }

    public void ResetFrame()
    {
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
