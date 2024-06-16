using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [SerializeField] private List<QuestObject> quest;
    [SerializeField] private int currentQuest;
    [SerializeField] private Text text;
    private Player player;
    private bool questActive = false;
    private QuestState currentState;

    private void Update()
    {
        if (!questActive) { return; }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            player.StartGameplay();
            questActive = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (quest[currentQuest].TryToCompleteQuest(player.questItems))
            {
                currentState = QuestState.Completed;
                GetQuestText();
                
                currentQuest++;
                currentQuest = Mathf.Clamp(currentQuest, 0, quest.Count - 1);

                currentState = QuestState.Start;
                return;
            }
            GetQuestText();
        }
    }

    public void StartQuest(Player player)
    {
        this.player = player;
        questActive = true;
    }

    public void GetQuestText()
    {
        if (currentState == QuestState.Start)
        {
            text.text = quest[currentQuest].GetNextQuest(currentState);
            currentState++;
            return;
        }
        text.text = quest[currentQuest].GetNextQuest(currentState);
    }
}