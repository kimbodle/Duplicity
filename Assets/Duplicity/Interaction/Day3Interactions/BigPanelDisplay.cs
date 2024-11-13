using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        bigPosterImage.sprite = image;
        bigPosterPanel.SetActive(true);
    }

    public void ShowPhotoPanel(Sprite image, PhotoItem photoItem)
    {
        bigPosterImage.sprite = image;
        bigPosterPanel.SetActive(true);
        currentPhotoItem = photoItem;
    }

    //OnClick �̺�Ʈ ����
    public void HidePanel()
    {
        bigPosterPanel.SetActive(false);
    }
    //Onclick �̺�Ʈ ����
    public void HideAndDestroy()
    {
        bigPosterPanel.SetActive(false);
        if (currentPhotoItem != null)
        {
            Destroy(currentPhotoItem.gameObject); // ���� PhotoItem ������Ʈ �ı�
            currentPhotoItem = null; // ���� ����
        }
    }

}

