using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigPanelDisplay : MonoBehaviour
{
    public GameObject bigPosterPanel;
    public Image bigPosterImage;

    private void Start()
    {
        bigPosterPanel.SetActive(false);
    }
    public void ShowPanel(Sprite image)
    {
        bigPosterImage.sprite = image;
        bigPosterPanel.SetActive(true);
    }

    //OnClick �̺�Ʈ ����
    public void HidePanel()
    {
        bigPosterPanel.SetActive(false);
    }
}

