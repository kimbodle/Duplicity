using System.Collections;
using UnityEngine;

public class PhotoMissionManager : MonoBehaviour
{
    public Frame[] frames;
    public Item[] correctOrder;
    [Space(10)]
    public GameObject PhotoMissionPanel;
    public GameObject WallCrackPanel;
    public GameObject SecretNotePanel;
    [Space(10)]
    public AudioClip destorySound;
    private PhotoMissionTimer timer;

    private void Start()
    {
        timer = GetComponent<PhotoMissionTimer>();
        WallCrackPanel.SetActive(false);
        SecretNotePanel.SetActive(false);
    }

    // ��� �����ӿ� ������ ��ġ�Ǿ����� Ȯ��
    public void OnPhotoPlaced()
    {
        if (AreAllFramesFilled())
        {
            StartCoroutine(DelayedCheckFrames());
        }
    }

    // ��� �����ӿ� ������ ��ġ�Ǿ����� ���θ� ��ȯ
    private bool AreAllFramesFilled()
    {
        foreach (var frame in frames)
        {
            if (!frame.HasPhoto())
            {
                return false;
            }
        }
        return true;
    }

    // ���� �ð� ���� �� CheckFrames ȣ��
    private IEnumerator DelayedCheckFrames()
    {
        yield return new WaitForSeconds(0.1f); // �ణ�� ���� �߰�
        CheckFrames();
    }

    public void CheckFrames()
    {
        bool isCorrect = true;
        for (int i = 0; i < frames.Length; i++)
        {
            if (frames[i].GetCurrentItem() != correctOrder[i])
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log("���� �̼� ����!");
            timer.CompleteMission();
            WallCrackPanel.SetActive(true);
            AudioManager.Instance.PlaySFX(destorySound);
            UIManager.Instance.DeactInventoryUI();
        }
        else
        {
            Debug.Log("���� �̼� ����");


            // �κ��丮�� ���� ����
            InventoryManager.Instance.ClearAllItemSlot();

            // ��� �������� �������� �κ��丮�� �߰�
            foreach (var frame in frames)
            {
                if (frame.HasPhoto())
                {
                    InventoryManager.Instance.AddItemToInventory(frame.GetCurrentItem());
                }
            }

            // ��� ������ �ʱ�ȭ
            foreach (var frame in frames)
            {
                frame.ResetFrame();
            }
        }
    }

    //OnClick �̺�Ʈ ����
    public void OnSecretNoteClick()
    {
        SecretNotePanel.SetActive(true);
    }
    //OnClick �̺�Ʈ ����
    public void OnCloseButton()
    {
        PhotoMissionPanel.SetActive(false);
    }
}
