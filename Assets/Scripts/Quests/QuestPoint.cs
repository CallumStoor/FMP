using FpsHorrorKit;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class QuestPoint : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] private string interactText = "Start Quest";
    [SerializeField] private QuestInfoSO questInfoForPoint;

    private bool playerIsNear = false;
    private string questId;
    private QuestState currentQuestState;

    private void Awake()
    {
        questId = questInfoForPoint.id;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
        GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed;
        
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
        GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed; // 35:32
    }


    public void SubmitPressed()
    {
        if(!playerIsNear)
        {
            return;
        }

        GameEventsManager.instance.questEvents.StartQuest(questId);
        GameEventsManager.instance.questEvents.AdvanceQuest(questId);
        GameEventsManager.instance.questEvents.FinishQuest(questId);
        Debug.Log("interacted with quest");
    }

    private void QuestStateChange(Quest quest)
    {
        if(quest.info.id.Equals(quest))
        {
            currentQuestState = quest.state;
            Debug.Log("Change quest state");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }
}
