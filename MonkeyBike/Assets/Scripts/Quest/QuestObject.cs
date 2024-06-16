using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QuestObject", order = 1)]
public class QuestObject : ScriptableObject
{
    public string questIntro;
    public string questBrief;
    public string questComplete;
    public QuestItems item;

    private Dictionary<QuestState, string> dict;

    private void Awake()
    {
        InitDictionary();
    }

    public bool TryToCompleteQuest(List<QuestItems> playerItems)
    {
        foreach (var questItem in playerItems)
        {
            if (questItem == item)
            {
                return true;
            }
        }

        return false;
    }

    public string GetNextQuest(QuestState state)
    {
        Debug.Log((int)state);
        return dict[state];
    }

    public void InitDictionary()
    {
        dict = new Dictionary<QuestState, string>
        {
            {QuestState.Start, questIntro },
            {QuestState.Ongoing, questBrief},
            {QuestState.Completed, questComplete }
        };
    }
}