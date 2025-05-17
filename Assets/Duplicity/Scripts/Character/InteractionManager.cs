using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static GameManager;

public class InteractionManager : MonoBehaviour
{
    private Canvas canvasObj;

    public GameObject interactButton;
    public TMP_Text interactionText;

    private IInteractable currentInteractable; // 현재 상호작용 가능한 오브젝트

    public bool isInteraction = true;

    private void Start()
    {
        canvasObj = FindAnyObjectByType<Canvas>();
        interactionText.gameObject.SetActive(false);
        interactButton.SetActive(false);

        // 버튼에 클릭 리스너 추가
        Button buttonComponent = interactButton.GetComponent<Button>();
        buttonComponent.onClick.AddListener(OnInteractButtonClicked);
    }

    public void ShowInteractionUI(string message)
    {
        interactButton.SetActive(true);
        interactionText.gameObject.SetActive(true);
        interactionText.text = message; // 메시지 설정
    }

    public void HideInteractionUI()
    {
        interactButton.SetActive(false);
        interactionText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (InputBlocker.IsInteractionBlocked) return;
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            OnInteractButtonClicked();
            AudioManager.Instance.PlayInteractionButton();
        }
    }

    public void OnInteractButtonClicked()
    {
        if (currentInteractable != null)
        {
            currentInteractable.OnInteract();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInteraction)
        {
            IInteractable interactable = collision.GetComponent<IInteractable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                ShowInteractionUI(interactable.GetInteractionMessage());
            }
        }
        else
        {
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isInteraction)
        {
            IInteractable interactable = collision.GetComponent<IInteractable>();
            if (interactable != null && interactable == currentInteractable)
            {
                HideInteractionUI();
                currentInteractable = null;
            }
        }
    }
}
