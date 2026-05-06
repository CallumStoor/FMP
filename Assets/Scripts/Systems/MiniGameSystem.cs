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
        Debug.Log("Interacted with Game");
        fpsController.isInteracting = !fpsController.isInteracting; // Stop movement; 
        Cursor.visible = !Cursor.visible;

        //Interact Effects

        minigame = Instantiate(prefab, PrefabPosition);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseMinigame()
    {
        fpsController.isInteracting = false;
        Cursor.visible = false;
        Destroy(minigame);
    }

    public void Highlight()
    {
        PlayerInteract.Instance.ChangeInteractText(interactText);
    }

    public void HoldInteract() { }
    public void UnHighlight() { }
}
