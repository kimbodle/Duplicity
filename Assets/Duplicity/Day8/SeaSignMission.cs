using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeaSignMission : MonoBehaviour
{
    [Header("Sign Mission")]
    public GameObject roadBackground;
    public GameObject roadBackgroundReverse;
    public GameObject[] regularCards; // �Ϲ� ����� �� ����� �� ���� ǥ����
    public GameObject[] reverseCards; // ������ ����� �� ����� �� ���� ǥ����
    public Sprite[] seaSigns; // �ٴ� ���õ� ǥ���� �̹�����
    public Sprite[] nonSeaSigns; // �ٴٿ� ���� ���� ǥ���� �̹�����

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
        // 3����� 5���忡���� ������ ���, �� �ܿ��� �Ϲ� ��� ����
        bool isReverseRound = (round == 3 || round == 5);
        roadBackground.SetActive(!isReverseRound);
        roadBackgroundReverse.SetActive(isReverseRound);

        // ���� ���忡 �´� ī�� ������Ʈ Ȱ��ȭ
        ActivateCards(isReverseRound);

        // ǥ������ �������� ��ġ (�ϳ��� �ٴ� ���� ǥ����)
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

        // �ٴ� ���� ǥ���ǰ� ����� ǥ������ �ϳ��� ����
        int seaSignIndex = Random.Range(0, 2);
        currentCards[seaSignIndex].GetComponent<Image>().sprite = seaSigns[Random.Range(0, seaSigns.Length)];
        currentCards[1 - seaSignIndex].GetComponent<Image>().sprite = nonSeaSigns[Random.Range(0, nonSeaSigns.Length)];

        // Ŭ�� �̺�Ʈ ����
        currentCards[seaSignIndex].GetComponent<Button>().onClick.RemoveAllListeners();
        currentCards[seaSignIndex].GetComponent<Button>().onClick.AddListener(OnCorrectChoice);

        currentCards[1 - seaSignIndex].GetComponent<Button>().onClick.RemoveAllListeners();
        currentCards[1 - seaSignIndex].GetComponent<Button>().onClick.AddListener(OnGameOver);
    }

    private void OnCorrectChoice()
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

    private void OnGameOver()
    {
        Debug.Log("Ʋ�Ƚ��ϴ�. ���� ����!");
        if(EndingManager.Instance != null)
        {
            EndingManager.Instance.LoadEnding("GameOver", "���� �Ҵ�.", 7);
        }
    }

    private void MissionClear()
    {
        Debug.Log("�̼� Ŭ����!");
        if (EndingManager.Instance != null)
        {
            //���̵� ��, �ƿ�
            EndingManager.Instance.LoadEnding("Ending", "Try: ����ϴ�", 1);
        }
    }
}
