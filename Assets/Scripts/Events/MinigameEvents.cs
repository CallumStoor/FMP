using System;
using UnityEngine;

public class MinigameEvents : MonoBehaviour
{
    public event Action<string> onMinigameComplete;
    public void minigameComplete(string id)
    {
        if (onMinigameComplete != null)
        {
            onMinigameComplete(id);
        }
    }
}
