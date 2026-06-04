using FpsHorrorKit;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private bool isGamePaused = false;
    [SerializeField] private GameObject pauseMenuUI;

    private FpsAssetsInputs _input;
    private FpsController _controller;


    private void Awake()
    {
        _input = FindAnyObjectByType<FpsAssetsInputs>();
        _controller = FindAnyObjectByType<FpsController>();
    }

    void Update()
    {
        HandleEscapeKey();
    }
    private void Start()
    {
        pauseMenuUI.SetActive(false);
    }

    private void HandleEscapeKey()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isGamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _input.SetCursorState(!isGamePaused);
        _controller.isInteracting = true;

        pauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isGamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _input.SetCursorState(!isGamePaused);
        _controller.isInteracting = false;
        pauseMenuUI.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
