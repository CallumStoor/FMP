using FpsHorrorKit;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/Quest")]
public class QuestData : ScriptableObject
{

    public int QuestValue;
    public List<DialogueData> questDialogue;

}
