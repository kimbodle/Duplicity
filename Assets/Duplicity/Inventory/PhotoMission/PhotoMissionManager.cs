using UnityEngine;

public class PhotoMissionManager : MonoBehaviour
{
    public Frame[] frames;
    public Item[] correctOrder;
    public InventoryManager inventoryManager;

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
            Debug.Log("Photos are in the correct order!");
            // 미션 성공 로직
        }
        else
        {
            Debug.Log("Photos are not in the correct order. Returning to inventory.");
            foreach (var frame in frames)
            {
                if (frame.HasPhoto())
                {
                    inventoryManager.AddItemToInventory(frame.GetCurrentItem());
                    frame.ResetFrame(); // 액자 초기화
                }
            }
        }
    }
}
