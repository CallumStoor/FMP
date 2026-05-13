using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private string defaultText = "Talk to the Old Lady";

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStepStateChange += UpdateObjective;
        GameEventsManager.instance.questEvents.onQuestStateChange += StartObjective;
    }

    private void OnDisable()
    {
        if (GameEventsManager.instance == null) return;

        GameEventsManager.instance.questEvents.onQuestStepStateChange -= UpdateObjective;
        GameEventsManager.instance.questEvents.onQuestStateChange -= StartObjective;
    }

    private void UpdateObjective(
        string questId,
        int stepIndex,
        QuestStepState questStepState
    )
    {
        objectiveText.text = questStepState.status;
    }

    public void StartObjective(Quest quest)
    {
        if (quest.state == QuestState.CAN_FINISH || quest.state == QuestState.CAN_START)
        {
            objectiveText.text = defaultText;
        }
        if (quest.state == QuestState.IN_PROGRESS)
        {
            objectiveText.text = quest.info.displayName;
        }
    }
}