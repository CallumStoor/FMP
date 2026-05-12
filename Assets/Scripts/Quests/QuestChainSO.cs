using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Quest Chain")]
public class QuestChainSO : ScriptableObject
{
    public QuestInfoSO[] quests;
}