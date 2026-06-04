namespace FpsHorrorKit
{
    using UnityEngine;

    public class TakeItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private string interactText = "Take Item [E]";

        public void Interact()
        {
            GameEventsManager.instance.minigameEvents.minigameComplete("CollectMinigame");

            gameObject.SetActive(false);
        }
        public void Highlight()
        {
            PlayerInteract.Instance.ChangeInteractText(interactText);
        }
        public void HoldInteract() { }
        public void UnHighlight() { }
    }
}