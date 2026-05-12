public class PinPostersQuestStep : QuestStep
{
    private int postersPinned = 0;
    private int postersToPin = 3;

    protected override void OnMinigameComplete()
    {
        postersPinned++;

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