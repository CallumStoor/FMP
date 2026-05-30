namespace FpsHorrorKit
{
    using UnityEngine;

    public class TakeItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private string interactText = "Take Item [E]";

        public void Interact()
        {
            Destroy(gameObject);
        }
        public void Highlight()
        {
            PlayerInteract.Instance.ChangeInteractText(interactText);
        }
        public void HoldInteract() { }
        public void UnHighlight() { }
    }
}