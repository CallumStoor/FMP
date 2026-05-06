using FpsHorrorKit;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MiniGameSystem : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Open MiniGame";
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform PrefabPosition;

    private FpsController fpsController;
    private GameObject minigame;

    private void Awake()
    {
        fpsController = FindAnyObjectByType<FpsController>();
    }

    public void Interact()
    {
        //Let the user interact with the UI elements
        Debug.Log("Interacted with Game");
        fpsController.isInteracting = true; // Stop movement; 
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //What happens once its been interacted with

        minigame = Instantiate(prefab, PrefabPosition);
    }

    public void CloseMinigame() // call from minigame to close the game and enable movement
    {
        Destroy(minigame);
        fpsController.isInteracting = false; // enable movement
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Highlight()
    {
        PlayerInteract.Instance.ChangeInteractText(interactText); // Updates the UI text when overing over the object
    }

    public void HoldInteract() { }
    public void UnHighlight() { }
}
