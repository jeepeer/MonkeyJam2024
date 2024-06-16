using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Quest : MonoBehaviour
{
    [SerializeField] private List<QuestObject> quest;
    [SerializeField] private int currentQuest;
    [SerializeField] private Text text;
    [SerializeField] private UnityEvent lmao;
    [SerializeField] private float rewardValue;

    private string[] questToRead;
    private int currentPage = 0;

    private Player player;
    private bool questActive = false;
    private QuestState currentState = QuestState.Start;

    private void Start()
    {
        foreach (var item in quest)
        {
            item.InitDictionary();
        }
    }

    private void Update()
    {
        if (!questActive) { return; }

        // jump out of quest
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            player.StartGameplay();
            questActive = false;
        }

        // read text
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            ReadText();
        }
    }

    private void ReadText()
    {
        GetQuestText();

        text.text = questToRead[currentPage];
        currentPage++;
        if (currentPage == questToRead.Length -1)
        {
            TryToChangeState();

            player.StartGameplay();
            questActive = false;

            currentPage = 0;
        }
    }

    private void TryToChangeState()
    {
        switch (currentState)
        {
            case QuestState.Start:
                currentState = QuestState.Ongoing;
                break;
            case QuestState.Ongoing:
                if (quest[currentQuest].TryToCompleteQuest(player.questItems)) { currentState = QuestState.Completed; }
                break;
            case QuestState.Completed:
                Reward();
                currentState = QuestState.Start;
                currentQuest++;
                currentQuest = Mathf.Clamp(currentQuest, 0, quest.Count - 1);
                if (currentQuest == quest.Count - 1) { currentState = QuestState.Completed; }
                break;
            default:
                Debug.LogError($"Invalid state : {currentState}");
                break;
        }
    }

    private void Reward()
    {
        lmao?.Invoke();
    }

    public void StartQuest(Player player)
    {
        GetQuestText();
        text.text = questToRead[0];
        currentPage++;
        currentPage = Mathf.Clamp(currentPage, 0, questToRead.Length);

        this.player = player;
        questActive = true;
    }

    public void GetQuestText()
    {
        questToRead = quest[currentQuest].GetQuestText(currentState);
    }
}