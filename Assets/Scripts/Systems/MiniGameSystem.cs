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
        //Unlocks the mouse and freezes player to allow UI interaction
        fpsController.isInteracting = true; // Stop movement
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //What happens once its been interacted with

        minigame = Instantiate(panelPrefab, panelPrefabPosition);

        MinigameHammer hammer = minigame.GetComponent<MinigameHammer>();

        hammer.SetOwner(this);

        Debug.Log($"Interacted with: {gameObject.name}");
    }

    public void CloseMinigame()
    {
        // event called for completeing minigame with the ID so that only activates for the single minigame
        GameEventsManager.instance.minigameEvents.minigameComplete("PostersMinigame");

        Destroy(minigame);
        minigame = null;

        if (completePrefab != null) // Replace Poster With complete Prefab
        {
            Instantiate(completePrefab, transform.position, transform.rotation, GameObject.Find("Environment").transform);
            Debug.Log($"place gameObject: {gameObject.name} at {transform.position}");
            gameObject.SetActive(false);
        }

        // enable movement
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
