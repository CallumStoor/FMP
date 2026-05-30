using FpsHorrorKit;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MiniGameSystem : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Open MiniGame";

    [Header("Prefabs")]
    [SerializeField] private GameObject panelPrefab;
    [SerializeField] private Transform panelPrefabPosition;
    [SerializeField] private GameObject completePrefab;

    private FpsController fpsController;
    private GameObject minigame;
    

    private void Awake()
    {
        fpsController = FindAnyObjectByType<FpsController>();
    }

    public void Interact()
    { 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        minigame = Instantiate(panelPrefab, panelPrefabPosition);
        MinigameHammer hammer = minigame.GetComponent<MinigameHammer>();
        hammer.SetOwner(this);
        Debug.Log($"Interacted with: {gameObject.name}");
    }
    public void CloseMinigame()
    {
        GameEventsManager.instance.minigameEvents.minigameComplete("PostersMinigame");
        Destroy(minigame);
        minigame = null;
        if (completePrefab != null)
        {
            Instantiate(completePrefab, transform.position, transform.rotation, GameObject.Find("Environment").transform);
            Debug.Log($"place gameObject: {gameObject.name} at {transform.position}");
            gameObject.SetActive(false);
        }
        fpsController.isInteracting = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Highlight()
    {
        PlayerInteract.Instance.ChangeInteractText(interactText); // Updates the UI text when Highlighting the object
    }

    public void HoldInteract() { }
    public void UnHighlight() { }
}
