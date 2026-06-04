using UnityEngine;

public class BillboardConstantSize : MonoBehaviour
{
    [SerializeField] private float scaleFactor = 0.05f;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        transform.forward = mainCamera.transform.forward;

        float distance = Vector3.Distance(
            mainCamera.transform.position,
            transform.position);

        transform.localScale = Vector3.one * distance * scaleFactor;
    }
}