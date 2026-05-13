using UnityEngine;
using UnityEngine.UI;

public class PinPostersQuestStep : QuestStep
{
    private int postersPinned = 0;
    private int postersToPin = 3;


    protected override void OnMinigameComplete()
    {
        postersPinned++;

        // update State when a poster is pinned
        ChangeState(
            postersPinned.ToString(),
            $"Posters Pinned: {postersPinned}/{postersToPin}"
        );

        if (postersPinned >= postersToPin)
        {
            FinishQuestStep();
        }
    }

    protected override void SetQuestStepState(string state)
    {
        postersPinned = int.Parse(state);
    }
}