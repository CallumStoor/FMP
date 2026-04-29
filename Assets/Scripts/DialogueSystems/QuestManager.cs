using FpsHorrorKit;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public enum QuestState
{
    incomplete,
    inProgress,
    complete
}

public class QuestManager : MonoBehaviour
{
    [Header("Quest Values")]
    [SerializeField] private Image questBoard;
    [SerializeField] private Text questText;

    public List<QuestData> QuestObjects;
    // public List<DialogueData> cards;
    public int questValue { get; private set; }

    private QuestState currentState;
    

    void Start()
    {
        // cards = Resources.LoadAll<DialogueData>("").ToList();

        questValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void QuestValueUpdate (int increment)
    {
        questValue += increment;

    }
    //scriptable objects with quest value, complete conditions e.g if the objects in a list are active, dialogs active for the quest. 
}
