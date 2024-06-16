using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QuestObject", order = 1)]
public class QuestObject : ScriptableObject
{
    public string questIntro;
    private string[] QI;
    public string questBrief;
    private string[] QB;
    public string questComplete;
    private string[] QC;
    public QuestItems item;

    private Dictionary<QuestState, string[]> dict;

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

    public string[] GetQuestText(QuestState state)
    {
        return dict[state];
    }

    public void InitDictionary()
    {
        QI = questIntro.Split('.', System.StringSplitOptions.RemoveEmptyEntries);
        QB = questBrief.Split('.', System.StringSplitOptions.RemoveEmptyEntries);
        QC = questComplete.Split('.', System.StringSplitOptions.RemoveEmptyEntries);

        dict = new Dictionary<QuestState, string[]>
        {
            {QuestState.Start, QI },
            {QuestState.Ongoing, QB},
            {QuestState.Completed, QC}
        };
    }
}