using UnityEngine;
using System.Collections;

public class DelayTriggerMove : MonoBehaviour
{

    [SerializeField] private Transform objectTransform;
    [SerializeField] private float distance;
    [SerializeField] private float duration;
    [SerializeField] private int delay;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Vector3 newPosition = objectTransform.position + (objectTransform.forward * distance);
            StartCoroutine(LerpPosition(newPosition, duration));
        }
    }

    private void Update()
    {
        // face player Script
    }

    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        yield return new WaitForSeconds(delay);

        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}

