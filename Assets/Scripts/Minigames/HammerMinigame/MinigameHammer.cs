using UnityEngine;

public class MinigameHammer : MonoBehaviour
{
    [SerializeField] private GameObject hammerButton;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Hit()
    {
        float randWidth = Random.Range(rectTransform.rect.width * -1, rectTransform.rect.width);
        float randHeight = Random.Range(rectTransform.rect.height * -1, rectTransform.rect.height);
        Debug.Log($"Moved Button to x: {randWidth} y: {randHeight}\nUsing the perameters x: {rectTransform.rect.width} y: {rectTransform.rect.height}");
        hammerButton.transform.position = new Vector3(randWidth, randHeight, 0);
    }
}
