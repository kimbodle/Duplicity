using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeaSignMission : MonoBehaviour
{
    [Header("Sign Mission")]
    public GameObject roadBackground;
    public GameObject roadBackgroundReverse;
    public GameObject[] regularCards; // 일반 배경일 때 사용할 두 개의 표지판
    public GameObject[] reverseCards; // 리버스 배경일 때 사용할 두 개의 표지판
    public Sprite[] seaSigns; // 바다 관련된 표지판 이미지들
    public Sprite[] nonSeaSigns; // 바다와 관련 없는 표지판 이미지들

    private int round = 1;
    [SerializeField] private int maxRounds = 5;

    [Space(10)]
    public SignMisisonTimer timer; 

    private void Start()
    {
        timer.isMissionActive = true;
        StartRound();
    }

    private void StartRound()
    {
        // 3라운드와 5라운드에서는 리버스 배경, 그 외에는 일반 배경 설정
        bool isReverseRound = (round == 3 || round == 5);
        roadBackground.SetActive(!isReverseRound);
        roadBackgroundReverse.SetActive(isReverseRound);

        // 현재 라운드에 맞는 카드 오브젝트 활성화
        ActivateCards(isReverseRound);

        // 표지판을 무작위로 배치 (하나만 바다 관련 표지판)
        SetSigns(isReverseRound);
    }

    private void ActivateCards(bool isReverse)
    {
        foreach (var card in regularCards) card.SetActive(!isReverse);
        foreach (var card in reverseCards) card.SetActive(isReverse);
    }

    private void SetSigns(bool isReverse)
    {
        GameObject[] currentCards = isReverse ? reverseCards : regularCards;

        // 바다 관련 표지판과 비관련 표지판을 하나씩 설정
        int seaSignIndex = Random.Range(0, 2);
        currentCards[seaSignIndex].GetComponent<Image>().sprite = seaSigns[Random.Range(0, seaSigns.Length)];
        currentCards[1 - seaSignIndex].GetComponent<Image>().sprite = nonSeaSigns[Random.Range(0, nonSeaSigns.Length)];

        // 클릭 이벤트 설정
        currentCards[seaSignIndex].GetComponent<Button>().onClick.RemoveAllListeners();
        currentCards[seaSignIndex].GetComponent<Button>().onClick.AddListener(OnCorrectChoice);

        currentCards[1 - seaSignIndex].GetComponent<Button>().onClick.RemoveAllListeners();
        currentCards[1 - seaSignIndex].GetComponent<Button>().onClick.AddListener(OnGameOver);
    }

    private void OnCorrectChoice()
    {
        Debug.Log("정답입니다!");

        round++;
        if (round > maxRounds)
        {
            MissionClear();
        }
        else
        {
            StartRound();
        }
    }

    private void OnGameOver()
    {
        Debug.Log("틀렸습니다. 게임 오버!");
        if(EndingManager.Instance != null)
        {
            EndingManager.Instance.LoadEnding("GameOver", "길을 잃다.", 7);
        }
    }

    private void MissionClear()
    {
        Debug.Log("미션 클리어!");
        if (EndingManager.Instance != null)
        {
            //페이드 인, 아웃
            EndingManager.Instance.LoadEnding("Ending", "Try: 노력하다", 1);
        }
    }
}
