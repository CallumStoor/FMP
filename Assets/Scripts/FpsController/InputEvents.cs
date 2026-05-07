using System;
using UnityEngine;

public class InputEvents : MonoBehaviour
{
    public event Action onSubmitPressed;
    public void SubmitPressed()
    {
        if (onSubmitPressed != null)
        {
            onSubmitPressed();
        }
    }
}
