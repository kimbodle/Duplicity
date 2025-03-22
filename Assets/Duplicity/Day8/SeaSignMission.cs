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