using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public QuestInfoSO info;
    public QuestState state;

    private int currentQuestStepIndex;
    private Dictionary<int, QuestStepState> questStepStates;

    public Quest(QuestInfoSO questInfo)
    {
        info = questInfo;
        state = QuestState.REQUIREMENTS_NOT_MET;
        currentQuestStepIndex = 0;
        questStepStates = new Dictionary<int, QuestStepState>();
    }

    public void MoveToNextStep() => currentQuestStepIndex++;

    public bool CurrentStepExists()
    {
        return currentQuestStepIndex < info.questStepsPrefab.Length;
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject prefab = GetCurrentQuestStepPrefab();
        if (prefab == null) return;

        GameObject obj = Object.Instantiate(prefab, parentTransform);

        QuestStep step = obj.GetComponent<QuestStep>();
        if (step == null) return;

        string stateData = "";

        if (questStepStates.ContainsKey(currentQuestStepIndex))
            stateData = questStepStates[currentQuestStepIndex].state;

        step.InitializeQuestStep(info.id, currentQuestStepIndex, stateData);
    }

    public void StoreQuestStepState(QuestStepState state, int index)
    {
        questStepStates[index] = state;
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        if (!CurrentStepExists()) return null;
        return info.questStepsPrefab[currentQuestStepIndex];
    }
}