using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeaSignMission : MonoBehaviour
{
    [Header("Sign Mission")]
    public GameObject roadBackground;
    public GameObject roadBackgroundReverse;
    public GameObject[] regularCards; // �Ϲ� ����� �� ����� ǥ���ǵ�
    public GameObject[] reverseCards; // ������ ����� �� ����� ǥ���ǵ�

    [Header("Round Data")]
    public Sprite[] correctSigns; // ���� ǥ���� �̹��� �迭 (���庰�� ����)
    public Sprite[] incorrectSigns; // ���� ǥ���� �̹��� �迭 (���庰�� ����)

    private int round = 1;
    [SerializeField] private int maxRounds = 5;

    [Space(10)]
    public SignMisisonTimer timer;

    private int[] correctPositions = { 1, 1, 0, 0, 1 }; // ���� ��ġ (0: ����, 1: ������)

    private void Start()
    {
        timer.isMissionActive = true;
        StartRound();
    }

    private void StartRound()
    {
        // 4����� 5���忡���� ������ ���, �� �ܿ��� �Ϲ� ��� ����
        bool isReverseRound = (round == 4 || round == 5);
        roadBackground.SetActive(!isReverseRound);
        roadBackgroundReverse.SetActive(isReverseRound);

        // ���� ���忡 �´� ī�� ������Ʈ Ȱ��ȭ
        ActivateCards(isReverseRound);

        // ����/���� ǥ���� ����
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

        // ���� ���� ������ ��������
        if (round - 1 < correctSigns.Length && round - 1 < incorrectSigns.Length && round - 1 < correctPositions.Length)
        {
            int correctIndex = correctPositions[round - 1]; // ���� ���� ���� ��ġ (0: ����, 1: ������)

            // ���� ī�� ����
            Image correctImage = currentCards[correctIndex].GetComponent<Image>();
            Button correctButton = currentCards[correctIndex].GetComponent<Button>();
            correctImage.sprite = correctSigns[round - 1];
            correctButton.onClick.RemoveAllListeners();
            correctButton.onClick.AddListener(() => OnCardClick(true)); // ���� ó��

            // ���� ī�� ����
            int incorrectIndex = 1 - correctIndex; // ������ �ݴ� ��ġ
            Image incorrectImage = currentCards[incorrectIndex].GetComponent<Image>();
            Button incorrectButton = currentCards[incorrectIndex].GetComponent<Button>();
            incorrectImage.sprite = incorrectSigns[round - 1];
            incorrectButton.onClick.RemoveAllListeners();
            incorrectButton.onClick.AddListener(() => OnCardClick(false)); // ���� ó��
        }
        else
        {
            Debug.LogWarning($"���� {round}�� ���� �����Ͱ� �������� �ʾҽ��ϴ�.");
        }
    }

    private void OnCardClick(bool isCorrect)
    {
        if (isCorrect)
        {
            Debug.Log("�����Դϴ�!");

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
            Debug.Log("Ʋ�Ƚ��ϴ�. ���� ����!");
            if (EndingManager.Instance != null)
            {
                FadeManager.Instance.StartFadeOut(() => { EndingManager.Instance.LoadEnding("GameOver", "���� �Ҵ�.", 7); }, true, 3f);
            }
        }
    }

    private void MissionClear()
    {
        Debug.Log("�̼� Ŭ����!");
        if(FadeManager.Instance != null)
        {
            FadeManager.Instance.StartFadeOut(() =>
            {
                if (EndingManager.Instance != null)
                {
                    EndingManager.Instance.LoadEnding("Ending", "����� �䳢�� ��, �� ������.", 1);
                }
            }, true, 3f);
        }
    }
}