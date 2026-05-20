using FpsHorrorKit;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class QuestPoint : MonoBehaviour, IInteractable
{
    [Header("Dialogue (Optional)")]
    [SerializeField] private DialogueData dialogueData;

    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint;

    [Header("UI")]
    [SerializeField] private string interactText = "Interact";

    [Header("Config")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;

    private string questId;
    private QuestState currentQuestState;

    private void Awake()
    {
        questId = questInfoForPoint.id;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    public void Interact()
    {

        if (dialogueData != null)
        {
            DialogueSystem.Instance.StartDialogue(
                0,
                new DialogueData[] { dialogueData }
            );
        }

       
        if (currentQuestState == QuestState.CAN_START && startPoint)
        {
            GameEventsManager.instance.questEvents.StartQuest(questId);
            Debug.Log($"Started quest: {questId}");
        }
        else if (currentQuestState == QuestState.CAN_FINISH && finishPoint)
        {
            GameEventsManager.instance.questEvents.FinishQuest(questId);
            Debug.Log($"Finished quest: {questId}");
        }
    }

    public void Highlight()
    {
        if (PlayerInteract.Instance != null)
        {
            PlayerInteract.Instance.ChangeInteractText(interactText);
        }
    }

    public void UnHighlight()
    {
    }

    public void HoldInteract()
    {
    }

    private void QuestStateChange(Quest quest)
    {
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
        }
    }
}