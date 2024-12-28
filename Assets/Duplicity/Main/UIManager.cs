using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Setting")]
    public Button settingIcon;
    public GameObject settingUI;

    [Header("Map")]
    public GameObject mapUI; // ���� UI ������Ʈ
    public Button mapIconButton; // ���� ������ ��ư

    [Header("Inventory")]
    public GameObject inventoryManager;
    public Button inventoryIconButton;
    public GameObject inventoryUI;

    [Header("DialogHistory")]
    public GameObject dialogHistoryIconButton;

    [Header("Days")]
    public GameObject dayIntroCanvas;
    public Image dayIntroImage;
    public List<Sprite> daySprites;
    public AudioClipData dayIntroSound;
    private Dictionary<int, Sprite> daySpriteDictionary;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SingletonManager.Instance.RegisterSingleton(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        mapUI.SetActive(false);

        DeactivateInventory();
        inventoryUI.SetActive(false);

        mapIconButton.gameObject.SetActive(false);
        dialogHistoryIconButton.gameObject.SetActive(false);

        //mapIconButton.onClick.AddListener(ToggleMapUI);
        InitializeDaySprites();
    }


    //Setting
    public void ToggleSettingUI()
    {
        settingUI.SetActive(!settingUI.activeSelf);
    }

    public void LogoutButton()
    {
        FirebaseAuthController.Instance.Logout();
        ToggleSettingUI();
        DialogManager.Instance.ClearHistory();
        SingletonManager.Instance.LogoutAndDestroySingletons();
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

    //Map
    public void ActiveMapIcon()
    {
        mapIconButton.gameObject.SetActive(true);
    }
    public void DeactivateMapIcon()
    {
        mapIconButton.gameObject.SetActive(false);
    }

    public void ToggleMapUI()
    {
        mapUI.SetActive(!mapUI.activeSelf);
    }

    //Inventory
    public void ActiveInventory()
    {
        Debug.Log("�κ��丮 Ȱ��ȭ");
        inventoryManager.SetActive(true);
        inventoryIconButton.gameObject.SetActive(true);
    }

    public void DeactivateInventory()
    {
        inventoryManager.SetActive(false);
        inventoryIconButton.gameObject.SetActive(false);
    }

    public void TogglInventoryUI()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }
    
    //DialogHistorySystem
    public void ActiveDialogHistoryIcon()
    {
        dialogHistoryIconButton.SetActive(true);
    }
    public void DeactivateDialogHistoryIcon()
    {
        dialogHistoryIconButton.SetActive(false);
    }

    public void TogglDialogHistoryUI()
    {
        DialogManager.Instance.ToggleHistoryUI();
    }

    //Ending
    public void EndingUI()
    {
        DeactivateInventory();
        DeactivateMapIcon();
        DeactivateDialogHistoryIcon();
        DialogManager.Instance.ClearHistory();
        mapUI.SetActive(false);
    }

    //DayIntro
    private void InitializeDaySprites()
    {
        daySpriteDictionary = new Dictionary<int, Sprite>();

        for (int i = 0; i < daySprites.Count; i++)
        {
            daySpriteDictionary.Add(i + 1, daySprites[i]);
        }
    }
    public void DisplayDayIntro(int currentDay)
    {
        if (currentDay == 0) { return; }
        if (daySpriteDictionary.TryGetValue(currentDay, out Sprite daySprite))
        {
            dayIntroCanvas.SetActive(true);
            dayIntroImage.sprite = daySprite;
            AudioManager.Instance.PlaySFX(dayIntroSound.clip);
            StartCoroutine(DisplayDayIntroCoroutine());
        }
        else
        {
            Debug.LogWarning($"�̹����� ���� Day: {currentDay}");
        }
    }

    private IEnumerator DisplayDayIntroCoroutine()
    {
        yield return new WaitForSeconds(3f);
        dayIntroCanvas.SetActive(false);
    }
}
