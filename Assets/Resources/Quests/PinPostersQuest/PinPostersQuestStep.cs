using UnityEngine;

public class PinPostersQuestStep : QuestStep
{
    private int postersPinned = 0;

    private int postersToPin = 3;

    private void OnEnable()
    {
        GameEventsManager.instance.minigameEvents.onMinigameComplete += PostersPinned;
    }

    private void PostersPinned()
    {
        if (postersPinned < postersToPin)
        {
            postersPinned++;
        }

        if(postersPinned >= postersToPin)
        {
            FinishQuestStep();
        }
    }
}
