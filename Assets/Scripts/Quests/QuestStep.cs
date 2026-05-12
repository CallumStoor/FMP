using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    [SerializeField] private string requiredMinigameId;

    protected bool isFinished;
    protected bool isInitialized;

    protected string questId;
    protected int stepIndex;

    protected virtual void OnEnable()
    {
        if (GameEventsManager.instance == null) return;

        GameEventsManager.instance.minigameEvents.onMinigameComplete += HandleMinigameComplete;
    }

    protected virtual void OnDisable()
    {
        if (GameEventsManager.instance == null) return;

        GameEventsManager.instance.minigameEvents.onMinigameComplete -= HandleMinigameComplete;
    }

    public void InitializeQuestStep(string questId, int stepIndex, string state)
    {
        this.questId = questId;
        this.stepIndex = stepIndex;
        isInitialized = true;

        if (!string.IsNullOrEmpty(state))
            SetQuestStepState(state);

        Debug.Log($"QuestStep Init | ID: {questId} | Step: {stepIndex}");
    }

    protected void FinishQuestStep()
    {
        if (isFinished || !isInitialized || string.IsNullOrEmpty(questId))
        {
            Debug.LogError("QuestStep invalid state");
            return;
        }

        isFinished = true;

        GameEventsManager.instance.questEvents.AdvanceQuest(questId);
        Destroy(gameObject);
    }

    protected void ChangeState(string state, string status)
    {
        if (!isInitialized || string.IsNullOrEmpty(questId))
        {
            Debug.LogError("QuestStep has NULL questId — step is invalid");
            return;
        }

        GameEventsManager.instance.questEvents.QuestStepStateChange(
            questId,
            stepIndex,
            new QuestStepState(state, status)
        );
    }

    protected virtual void HandleMinigameComplete(string minigameId)
    {
        if (minigameId != requiredMinigameId)
            return;

        OnMinigameComplete();
    }

    protected virtual void OnMinigameComplete() { }
    protected abstract void SetQuestStepState(string state);
}