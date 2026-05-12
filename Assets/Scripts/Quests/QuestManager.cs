using FpsHorrorKit;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;

    [Header("Quest Chain")]
    [SerializeField] private QuestChainSO questChain;

    private void Awake()
    {
        questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
        GameEventsManager.instance.questEvents.onQuestStepStateChange += QuestStepStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
        GameEventsManager.instance.questEvents.onQuestStepStateChange -= QuestStepStateChange;
    }

    private void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            if (CheckRequirementsMet(quest))
            {
                quest.state = QuestState.CAN_START;
                GameEventsManager.instance.questEvents.QuestStateChange(quest);
            }

            if (quest.state == QuestState.IN_PROGRESS)
            {
                quest.InstantiateCurrentQuestStep(transform);
            }

            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuestById(id);
        if (quest == null) return;

        // optional dialogue
        if (quest.info.introDialogue != null)
        {
            DialogueSystem.Instance.StartDialogue(
                0,
                new DialogueData[] { quest.info.introDialogue }
            );
        }

        quest.state = QuestState.IN_PROGRESS;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);

        quest.InstantiateCurrentQuestStep(transform);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuestById(id);
        if (quest == null) return;

        quest.MoveToNextStep();

        if (quest.CurrentStepExists())
        {
            quest.InstantiateCurrentQuestStep(transform);
        }
        else
        {
            quest.state = QuestState.CAN_FINISH;
            GameEventsManager.instance.questEvents.QuestStateChange(quest);
        }
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuestById(id);
        if (quest == null) return;

        quest.state = QuestState.FINISHED;
        GameEventsManager.instance.questEvents.QuestStateChange(quest);

        // optional completion dialogue
        if (quest.info.completionDialogue != null)
        {
            DialogueSystem.Instance.StartDialogue(
                0,
                new DialogueData[] { quest.info.completionDialogue }
            );
        }

        TryAdvanceQuestChain(quest.info);
    }

    private void TryAdvanceQuestChain(QuestInfoSO completedQuest)
    {
        if (questChain == null || questChain.quests == null) return;

        for (int i = 0; i < questChain.quests.Length - 1; i++)
        {
            if (questChain.quests[i] == completedQuest)
            {
                StartQuest(questChain.quests[i + 1].id);
                break;
            }
        }
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        if (quest.info.questPrerequisites == null ||
            quest.info.questPrerequisites.Length == 0)
        {
            return true;
        }

        foreach (QuestInfoSO prerequisite in quest.info.questPrerequisites)
        {
            if (GetQuestById(prerequisite.id).state != QuestState.FINISHED)
            {
                return false;
            }
        }

        return true;
    }

    private void QuestStepStateChange(
        string id,
        int stepIndex,
        QuestStepState questStepState)
    {
        if (string.IsNullOrEmpty(id))
        {
            Debug.LogError("QuestStepStateChange: NULL quest ID");
            return;
        }

        if (!questMap.TryGetValue(id, out Quest quest) || quest == null)
        {
            Debug.LogError("QuestStepStateChange: Quest not found -> " + id);
            return;
        }

        if (questStepState == null)
        {
            Debug.LogError("QuestStepStateChange: NULL state for quest -> " + id);
            return;
        }

        quest.StoreQuestStepState(questStepState, stepIndex);

    }


    private Quest GetQuestById(string id)
    {
        if (questMap.TryGetValue(id, out Quest quest))
            return quest;

        Debug.LogError("Quest not found: " + id);
        return null;
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfoSO[] all = Resources.LoadAll<QuestInfoSO>("Quests");

        Dictionary<string, Quest> map = new();

        foreach (QuestInfoSO info in all)
        {
            map.Add(info.id, new Quest(info));
        }

        return map;
    }
}