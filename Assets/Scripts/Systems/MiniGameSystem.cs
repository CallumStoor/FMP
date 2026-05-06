using FpsHorrorKit;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MiniGameSystem : MonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Open MiniGame";
    [SerializeField] private Image minigamePanel;

    private FpsController fpsController;

    private void Awake()
    {
        fpsController = FindAnyObjectByType<FpsController>();
    }

    public void Interact()
    {
        Debug.Log("Interacted with Game");
        fpsController.isInteracting = !fpsController.isInteracting; // Stop movement; 
        Cursor.visible = true;

        //Interact Effects

        minigamePanel.enabled = true;
    }

    public void CloseMinigame()
    {
        fpsController.isInteracting = false;
        Cursor.visible = false;
    }

    public void Highlight()
    {
        PlayerInteract.Instance.ChangeInteractText(interactText);
    }

    public void HoldInteract() { }
    public void UnHighlight() { }
}
