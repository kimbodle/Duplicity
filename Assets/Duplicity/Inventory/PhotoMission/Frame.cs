using UnityEngine;
using UnityEngine.UI;

public class Frame : MonoBehaviour
{
    public Image frameImage;
    public PhotoMissionManager photoMissionManager;
    private Item currentItem;

    private void Start()
    {
        frameImage.color = new Color(1, 1, 1, 0); // 투명
    }
    public bool SetPhoto(Item item)
    {
        if (currentItem != null) return false; // 이미 사진이 있으면 실패 반환

        frameImage.color = Color.white;
        currentItem = item;
        frameImage.sprite = item.itemIcon;

        // PhotoMissionManager에 알림
        photoMissionManager?.OnPhotoPlaced();

        return true; // 성공적으로 사진이 걸림
    }


    public void ResetFrame()
    {
        frameImage.color = new Color(1, 1, 1, 0); // 투명
        currentItem = null;
        frameImage.sprite = null; // 기본 이미지로 설정

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
