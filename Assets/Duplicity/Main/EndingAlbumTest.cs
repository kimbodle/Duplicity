using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAlbumTest : MonoBehaviour
{
    public void ClearBadEnding1()
    {
        string endingType = "BadEnding";
        int endingIndex = 1;

        CompleteEnding(endingType, endingIndex);
    }

    public void ClearHappyEnding1()
    {
        string endingType = "HappyEnding";
        int endingIndex = 1;

        CompleteEnding(endingType, endingIndex);
    }

    public void GetNotobook()
    {
        string endingType = "Notebook";
        int endingIndex = 1;

        CompleteEnding(endingType, endingIndex);
    }

    public void CompleteEnding(string endingType, int endingIndex)
    {
        // Firestore에 엔딩 저장
        GameManager.Instance.SaveEnding(endingType, endingIndex);

        // UI에 엔딩 이미지 표시
        //UIManager.Instance.DisplayEndingImage(endingType, endingIndex);
    }

    public void CheckHidden()
    {
        GameManager.Instance.HasSeenEnding("Notebook", 1);
    }

}
