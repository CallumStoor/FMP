using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance;

    public MinigameEvents minigameEvents;
    public QuestEvents questEvents;
    public InputEvents inputEvents;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Game Events Manager in the scene.");
        }
        instance = this;
        Debug.Log(instance);

        minigameEvents = gameObject.AddComponent<MinigameEvents>();
        questEvents = gameObject.AddComponent<QuestEvents>();
        inputEvents = gameObject.AddComponent<InputEvents>();
    }
}