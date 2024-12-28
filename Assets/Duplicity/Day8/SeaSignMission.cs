using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeaSignMission : MonoBehaviour
{
    [Header("Sign Mission")]
    public GameObject roadBackground;
    public GameObject roadBackgroundReverse;
    public GameObject[] regularCards; // 일반 배경일 때 사용할 표지판들
    public GameObject[] reverseCards; // 리버스 배경일 때 사용할 표지판들

    [Header("Round Data")]
    public Sprite[] correctSigns; // 정답 표지판 이미지 배열 (라운드별로 설정)
    public Sprite[] incorrectSigns; // 오답 표지판 이미지 배열 (라운드별로 설정)

    private int round = 1;
    [SerializeField] private int maxRounds = 5;

    [Space(10)]
    public SignMisisonTimer timer;

    private int[] correctPositions = { 1, 1, 0, 0, 1 }; // 정답 위치 (0: 왼쪽, 1: 오른쪽)

    private void Start()
    {
        timer.isMissionActive = true;
        StartRound();
    }

    private void StartRound()
    {
        // 4라운드와 5라운드에서는 리버스 배경, 그 외에는 일반 배경 설정
        bool isReverseRound = (round == 4 || round == 5);
        roadBackground.SetActive(!isReverseRound);
        roadBackgroundReverse.SetActive(isReverseRound);

        // 현재 라운드에 맞는 카드 오브젝트 활성화
        ActivateCards(isReverseRound);

        // 정답/오답 표지판 설정
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

        // 현재 라운드 데이터 가져오기
        if (round - 1 < correctSigns.Length && round - 1 < incorrectSigns.Length && round - 1 < correctPositions.Length)
        {
            int correctIndex = correctPositions[round - 1]; // 현재 라운드 정답 위치 (0: 왼쪽, 1: 오른쪽)

            // 정답 카드 설정
            Image correctImage = currentCards[correctIndex].GetComponent<Image>();
            Button correctButton = currentCards[correctIndex].GetComponent<Button>();
            correctImage.sprite = correctSigns[round - 1];
            correctButton.onClick.RemoveAllListeners();
            correctButton.onClick.AddListener(() => OnCardClick(true)); // 정답 처리

            // 오답 카드 설정
            int incorrectIndex = 1 - correctIndex; // 정답의 반대 위치
            Image incorrectImage = currentCards[incorrectIndex].GetComponent<Image>();
            Button incorrectButton = currentCards[incorrectIndex].GetComponent<Button>();
            incorrectImage.sprite = incorrectSigns[round - 1];
            incorrectButton.onClick.RemoveAllListeners();
            incorrectButton.onClick.AddListener(() => OnCardClick(false)); // 오답 처리
        }
        else
        {
            Debug.LogWarning($"라운드 {round}에 대한 데이터가 설정되지 않았습니다.");
        }
    }

    private void OnCardClick(bool isCorrect)
    {
        if (isCorrect)
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
        else
        {
            Debug.Log("틀렸습니다. 게임 오버!");
            if (EndingManager.Instance != null)
            {
                FadeManager.Instance.StartFadeOut(() => { EndingManager.Instance.LoadEnding("GameOver", "길을 잃다.", 7); }, true, 3f);
            }
        }
    }

    private void MissionClear()
    {
        Debug.Log("미션 클리어!");
        if(FadeManager.Instance != null)
        {
            FadeManager.Instance.StartFadeOut(() =>
            {
                if (EndingManager.Instance != null)
                {
                    EndingManager.Instance.LoadEnding("Ending", "평범한 토끼의 삶, 난 만족해.", 1);
                }
            }, true, 3f);
        }
    }
}
/*
 //표지판 랜덤 코드
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

 */