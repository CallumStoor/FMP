using UnityEngine;
using UnityEngine.UI;

public class CollectStickQuestStep : QuestStep
{
    private int sticksCollected = 0;
    private int sticksToCollect = 2;


    protected override void OnMinigameComplete()
    {
        sticksCollected++;

        // update State when a poster is pinned
        ChangeState(
            sticksCollected.ToString(),
            $"Posters Pinned: {sticksCollected}/{sticksToCollect}"
        );

        if (sticksCollected >= sticksToCollect)
        {
            FinishQuestStep();
        }
    }

    protected override void SetQuestStepState(string state)
    {
        sticksCollected = int.Parse(state);
    }
}