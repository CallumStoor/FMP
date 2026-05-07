namespace FpsHorrorKit
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class FpsAssetsInputs : MonoBehaviour
    {
        public static FpsAssetsInputs Instance { get; private set; }

        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        [Header("Interaction Values")]
        public bool interact;
        public bool stopInteract;

        public bool fire;
        public bool fKey;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        //int currentItemIndex = -1;
        GameObject flashlight;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            flashlight = GameObject.Find("Flashlight");
        }

        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }
        public void OnLook(InputValue value)
        {
            LookInput(value.Get<Vector2>());
        }
        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }
        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }
        
        public void OnFire(InputValue value)
        {
            FireInput(value.isPressed);
        }

        public void OnInteract(InputValue value)
        {
            InteractInput(value.isPressed);
        }
        public void OnStopInteract(InputValue value)
        {
            StopInteractInput(value.isPressed);
        }

        public void OnKeyF(InputValue value)
        {
            fKey = !fKey;
            flashlight.SetActive(fKey);
        }

        private void MoveInput(Vector2 moveInput) => move = moveInput;
        private void LookInput(Vector2 lookInput) => look = lookInput;
        private void JumpInput(bool jumpInput) => jump = jumpInput;
        private void SprintInput(bool sprintInput) => sprint = sprintInput;
        private void FireInput(bool fireInput) => fire = fireInput;
        private void InteractInput(bool interactInput) => interact = interactInput;
        private void StopInteractInput(bool stopInteractInput) => stopInteract = stopInteractInput;


        private void OnApplicationFocus(bool hasFocus)
        {
            if(!GameObject.FindAnyObjectByType<FpsController>().isInteracting)
                SetCursorState(cursorLocked);
        }

        public void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}