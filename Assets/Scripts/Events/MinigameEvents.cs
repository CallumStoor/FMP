using System;
using UnityEngine;

public class MinigameEvents : MonoBehaviour
{
    public event Action onMinigameComplete;
    public void minigameComplete()
    {
        if (onMinigameComplete != null)
        {
            onMinigameComplete();
        }
    }
}
