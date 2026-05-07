using UnityEngine;

public class MinigameHammer : MonoBehaviour
{
    [SerializeField] private GameObject hammerButton;

    private RectTransform rectTransform;
    private MiniGameSystem minigameManager;


    int currentHits = 0;
    int maxHits = 4;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        minigameManager = GameObject.FindAnyObjectByType<MiniGameSystem>();
    }

    public void Hit()
    {
        currentHits++;

        if (currentHits >= maxHits)
        {
            minigameManager.CloseMinigame();
            return;
        }
        else
        {
            float randWidth = Random.Range(rectTransform.rect.width * -1 / 2, rectTransform.rect.width / 2);
            float randHeight = Random.Range(rectTransform.rect.height * -1 / 2, rectTransform.rect.height / 2);
            hammerButton.transform.position = new Vector3(transform.position.x + randWidth, transform.position.y + randHeight, 0);
        }
    }

    //subscribe to complete event to change gameobject to completed prefab 
}
