using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class BigPanelDisplay : MonoBehaviour
{
    [Header("Day4")]
    public GameObject bigPosterPanel;
    public Image bigPosterImage;

    [Header("Day5")]
    [SerializeField] private PhotoItem currentPhotoItem;

    private void Start()
    {
        bigPosterPanel.SetActive(false);
    }
    public void ShowPanel(Sprite image)
    {
        InputBlocker.IsInteractionBlocked = true;
        bigPosterImage.sprite = image;
        bigPosterPanel.SetActive(true);
    }

    public void ShowPhotoPanel(Sprite image, PhotoItem photoItem)
    {
        InputBlocker.IsInteractionBlocked = true;
        bigPosterImage.sprite = image;
        bigPosterPanel.SetActive(true);
        currentPhotoItem = photoItem;
    }

    //OnClick 이벤트 연결
    public void HidePanel()
    {
        bigPosterPanel.SetActive(false);
        InputBlocker.IsInteractionBlocked = false;
    }
    //Onclick 이벤트 연결
    public void HideAndDestroy()
    {
        bigPosterPanel.SetActive(false);
        if (currentPhotoItem != null)
        {
            Destroy(currentPhotoItem.gameObject); // 현재 PhotoItem 오브젝트 파괴
            currentPhotoItem = null; // 참조 제거
        }
        InputBlocker.IsInteractionBlocked = false;
    }

}

